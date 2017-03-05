using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gabriel.Cat.Extension;
namespace PokemonGBAFrameWork
{
   public class AtaquesAprendidos:ObjectAutoId
    {
        public struct AtaqueAprendido:IComparable<AtaqueAprendido>,IComparable<ObjectAutoId>
        {
            ObjectAutoId obj;
            short ataque;
            byte nivel;

            public AtaqueAprendido(short ataque=0, byte nivel=1)
            {
                obj = new ObjectAutoId();
                this.ataque = ataque;
                this.nivel = nivel;
            }

            public short Ataque
            {
                get
                {
                    return ataque;
                }

                set
                {
                    ataque = value;
                }
            }

            public byte Nivel
            {
                get
                {
                    return nivel;
                }

                set
                {
                    if (value==0||value > 127)//ya se que el maximo es 100 pero por formato podria guardarse asi...   que lo aprenda es otra cosa
                        throw new ArgumentOutOfRangeException();
                    nivel = value;
                }
            }

            public int CompareTo(AtaqueAprendido other)
            {
                int compareTo = Nivel.CompareTo(other.Nivel);
                if (compareTo == (int)Gabriel.Cat.CompareTo.Iguales)
                    compareTo = Ataque.CompareTo(other.Ataque);
                return compareTo;
            }

            public int CompareTo(ObjectAutoId other)
            {
                int compareTo = other == null ? (int)Gabriel.Cat.CompareTo.Inferior : obj.CompareTo(other);
                return compareTo;
            }
        }
        enum Variable
        {
            AtaquesAprendidos
        }
      
        public static readonly byte[] MarcaFin;

        Hex offsetBytesAtaqueAprendido;
        Llista<AtaqueAprendido> ataques;

        static AtaquesAprendidos()
        {
            Zona zonaAtaquesAprendidos = new Zona(Variable.AtaquesAprendidos);
            MarcaFin = new byte[] { 0xFF, 0xFF }; 
            //añado las zonas
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x6930C);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x6930C);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x3E968);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x3E968);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x3EA7C);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x3EA7C);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x3B988);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x3B988);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x3B7BC);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x3B7BC);

            Zona.DiccionarioOffsetsZonas.Add(zonaAtaquesAprendidos);
        }
        public AtaquesAprendidos()
        {
            Ataques = new Llista<AtaqueAprendido>();
        }
        public Hex OffsetBytesAtaqueAprendido
        {
            get
            {
                return offsetBytesAtaqueAprendido;
            }

            set
            {
                offsetBytesAtaqueAprendido = value;
            }
        }

        public Llista<AtaqueAprendido> Ataques
        {
            get
            {
                return ataques;
            }

           private set
            {
                ataques = value;
            }
        }
        public byte[] ToBytesGBA()
        {
            byte[] bytesGBA = new byte[ataques.Count * 2 + MarcaFin.Length];

            Ataques.Sort();

            unsafe
            {
                byte* ptrBytesGBA;
                fixed (byte* ptBytesGBA = bytesGBA)
                {
                    ptrBytesGBA = ptBytesGBA;
                    for (int i = 0, f = bytesGBA.Length - 2; i < f; i += 2)
                    {
                        *ptrBytesGBA = (byte)(ataques[i].Ataque % byte.MaxValue);
                        ptrBytesGBA++;
                        if (ataques[i].Ataque > byte.MaxValue)
                            *ptrBytesGBA = 0x1;
                        *ptrBytesGBA += (byte)(ataques[i].Nivel << 1);
                        ptrBytesGBA++;
                    }
                    *ptrBytesGBA = MarcaFin[0];
                    ptrBytesGBA++;
                    *ptrBytesGBA = MarcaFin[1];
                }
            }
            return bytesGBA;
        }
        public AtaqueAprendido[] GetAtaquesAprendidos(Hex nivel)
        {
            const int MAXATACKSFIGHT = 4;
            int posNivel=0;
            byte nivelByte =(byte) nivel;
            AtaqueAprendido[] ataques = new AtaqueAprendido[MAXATACKSFIGHT];
            Ataques.Sort();
            while (ataques[posNivel].Nivel <= nivel) posNivel++;
            if(posNivel<MAXATACKSFIGHT)
            {
                for (int i = 0; i <= posNivel; i++)
                    ataques[i] = Ataques[i];

            }else
            {
                for (int i = posNivel, j = MAXATACKSFIGHT - 1; i >= 0; i--, j--)
                    ataques[j] = Ataques[i];
            }
            return ataques;

        }
        public Ataque[] GetAtaques(Hex nivel,IList<Ataque> lstAtaquesSource)
        {
            if (lstAtaquesSource == null)
                throw new ArgumentNullException();
            const int MAXATACKSFIGHT = 4;
            Ataque[] ataques = new Ataque[MAXATACKSFIGHT];
            AtaqueAprendido[] ataquesAprendidos = GetAtaquesAprendidos(nivel);
            for (int i = 0; i < MAXATACKSFIGHT; i++)
                ataques[i] = lstAtaquesSource[ataquesAprendidos[i].Ataque];
            return ataques;

        }
        public static AtaquesAprendidos GetAtaquesAprendidos(RomData rom, Hex indexPokemon)
        { return GetAtaquesAprendidos(rom.RomGBA, rom.Edicion, rom.Compilacion, indexPokemon); }
        public static AtaquesAprendidos GetAtaquesAprendidos(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion,Hex indexPokemon)
        {
            //missigno de por si usa el mismo puntero que bulbasaur por eso tienen los mismos ataques,supongo que sera para que no de error y no ocupar espacio...
            Hex offset = Offset.GetOffset(rom,GetOffsetPrimerPointer(rom,edicion,compilacion) + indexPokemon * (int)Longitud.Offset);
            BloqueBytes bloque = BloqueBytes.GetBytes(rom,offset,MarcaFin);
            AtaquesAprendidos ataquesAprendidos = new AtaquesAprendidos();
            //pongo los ataques
            for(int i=0;i<bloque.Bytes.Length;i+=2)
            {
                ataquesAprendidos.Ataques.Add(new AtaqueAprendido((short)(bloque.Bytes[i]+(bloque.Bytes[i+1]%2==0? byte.MinValue : byte.MaxValue)),(byte)(bloque.Bytes[i+1]>>1)));
            }
            ataquesAprendidos.Ataques.Sort();//por si lo hacen de forma externa que lo lea bien :)

            return ataquesAprendidos;
        }
        private static Hex GetOffsetPrimerPointer(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion)
        {
            return  Zona.GetOffset(rom, Variable.AtaquesAprendidos, edicion, compilacion);
        }
        public static void SetAtaquesAprendidos(RomData rom, Hex indexPokemon, AtaquesAprendidos ataquesAprendidos)
        {  SetAtaquesAprendidos(rom.RomGBA, rom.Edicion, rom.Compilacion, indexPokemon,ataquesAprendidos); }
        public static void SetAtaquesAprendidos(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex indexPokemon,AtaquesAprendidos ataquesAprendidos)
        {
            Hex offset = Offset.GetOffset(rom,Zona.GetOffset(rom, Variable.AtaquesAprendidos, edicion, compilacion) + indexPokemon * (int)Longitud.Offset);
            BloqueBytes bloqueOri = BloqueBytes.GetBytes(rom,offset, MarcaFin);
            BloqueBytes.RemoveBytes(rom, bloqueOri.OffsetInicio, bloqueOri.Bytes.Length);
            Offset.SetOffset(rom, offset, BloqueBytes.SetBytes(rom, ataquesAprendidos.ToBytesGBA()));
        }



        internal static int GetTotalPokemon(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            int total = 0;
            while (Offset.IsAPointer(rom, GetOffsetPrimerPointer(rom, edicion, compilacion) + total * (int)Longitud.Offset)) total++;
            return total;
        }
    }
}
