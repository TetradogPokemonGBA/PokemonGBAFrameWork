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
        public class AtaqueAprendido:ObjectAutoId,IComparable<AtaqueAprendido>
        {
   
            short ataque;
            byte nivel;

            public AtaqueAprendido(short ataque=0, byte nivel=1)
            {

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
                int compareTo =other!=null ? Nivel.CompareTo(other.Nivel) : (int)Gabriel.Cat.CompareTo.Inferior;
                if (compareTo == (int)Gabriel.Cat.CompareTo.Iguales)
                    compareTo = Ataque.CompareTo(other.Ataque);
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

            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x3EA7C,0x3EA90);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x3EA7C, 0x3EA90);

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
            int totalPuestos = 0;
            byte nivelByte =(byte) nivel;
            AtaqueAprendido[] ataques = new AtaqueAprendido[MAXATACKSFIGHT];

            Ataques.Sort();

            while (posNivel < Ataques.Count-1 && Ataques[posNivel].Nivel <= nivel)
                posNivel++;
            //si el pokemon aprende dos veces el mismo ataque no lo tiene que tener duplicado...
            if(posNivel<MAXATACKSFIGHT)
            {
                for (int i = 0; i <= posNivel; i++)
                    if (ataques.Filtra((ataque)=>ataque!=null?ataque.Ataque==Ataques[i].Ataque:false).Count==0)
                        ataques[totalPuestos++] = Ataques[i];
         

            }else
            {
                for (int i = posNivel; totalPuestos<MAXATACKSFIGHT && i>=0; i--)
                {
                    if (ataques.Filtra((ataque) => ataque != null ? ataque.Ataque == Ataques[i].Ataque : false).Count == 0)
                    {
                        ataques[totalPuestos++] = Ataques[i];
                  
                    }
                }

            }

            for (int i = totalPuestos; i < MAXATACKSFIGHT; i++)
                    ataques[i] = new AtaqueAprendido();

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
        public static AtaquesAprendidos GetAtaquesAprendidos(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion,Hex ordenGameGrakPokemon)
        {
            //missigno de por si usa el mismo puntero que bulbasaur por eso tienen los mismos ataques,supongo que sera para que no de error y no ocupar espacio...
            Hex offset = Offset.GetOffset(rom,GetOffsetPointer(rom,edicion,compilacion, ordenGameGrakPokemon));
            BloqueBytes bloque = BloqueBytes.GetBytes(rom,offset,MarcaFin);
            AtaquesAprendidos ataquesAprendidos = new AtaquesAprendidos();
            //pongo los ataques
            for(int i=0;i<bloque.Bytes.Length;i+=2)
            {
                ataquesAprendidos.Ataques.Add(new AtaqueAprendido((short)(bloque.Bytes[i]+(bloque.Bytes[i+1]%2==0? byte.MinValue : byte.MaxValue)),(byte)(bloque.Bytes[i+1]>>1)));
            }
            ataquesAprendidos.Ataques.Sort();//por si lo hacen de forma externa que lo lea bien :)
            ataquesAprendidos.OffsetBytesAtaqueAprendido = offset;
            return ataquesAprendidos;
        }
        private static Hex GetOffsetPointer(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion,Hex ordenGameGrakPokemon)
        {
            return  Zona.GetOffset(rom, Variable.AtaquesAprendidos, edicion, compilacion) + ordenGameGrakPokemon * (int)Longitud.Offset;
        }
        public static void SetAtaquesAprendidos(RomData rom, Hex indexPokemon, AtaquesAprendidos ataquesAprendidos, LlistaOrdenadaPerGrups<Hex, AtaquesAprendidos> dicAtaquesPokemon)
        {  SetAtaquesAprendidos(rom.RomGBA, rom.Edicion, rom.Compilacion, indexPokemon,ataquesAprendidos,dicAtaquesPokemon); }
        public static void SetAtaquesAprendidos(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex ordenGameGrakPokemon,AtaquesAprendidos ataquesAprendidos,LlistaOrdenadaPerGrups<Hex,AtaquesAprendidos> dicAtaquesPokemon)
        {
            Hex offsetPointer = GetOffsetPointer(rom, edicion, compilacion, ordenGameGrakPokemon);
            Hex offset = Offset.GetOffset(rom,offsetPointer );
            BloqueBytes bloqueOri = BloqueBytes.GetBytes(rom,offset, MarcaFin);
            dicAtaquesPokemon.Remove(offset, ataquesAprendidos);
            if(!dicAtaquesPokemon.ContainsKey(offset))//si ya no hay ningun pokemon que use esos datos los borro
               BloqueBytes.RemoveBytes(rom, bloqueOri.OffsetInicio, bloqueOri.Bytes.Length);
            offset = BloqueBytes.SetBytes(rom, ataquesAprendidos.ToBytesGBA());//actualizo el offset con el nuevo
            Offset.SetOffset(rom, offsetPointer, offset);

            dicAtaquesPokemon.Add(offset, ataquesAprendidos);//añado al diccionario el nuevo offset con los ataques :)
        }
        public static LlistaOrdenadaPerGrups<Hex, AtaquesAprendidos> GetAtaquesAprendidosDic(RomData romGBA)
        {
            return GetAtaquesAprendidosDic(romGBA.RomGBA, romGBA.Edicion, romGBA.Compilacion);
        }
        public static LlistaOrdenadaPerGrups<Hex,AtaquesAprendidos> GetAtaquesAprendidosDic(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            LlistaOrdenadaPerGrups<Hex, AtaquesAprendidos> dic = new LlistaOrdenadaPerGrups<Hex, AtaquesAprendidos>();
            for (int i = 0, f = GetTotalPokemon(rom, edicion, compilacion); i < f; i++)
                dic.Add(Offset.GetOffset(rom, GetOffsetPointer(rom, edicion, compilacion, i)), GetAtaquesAprendidos(rom, edicion, compilacion, i));
            return dic;
        }



        internal static int GetTotalPokemon(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            int total = 0;
            while (Offset.IsAPointer(rom, GetOffsetPointer(rom, edicion, compilacion,total))) total++;
            return total;
        }

        
    }
}
