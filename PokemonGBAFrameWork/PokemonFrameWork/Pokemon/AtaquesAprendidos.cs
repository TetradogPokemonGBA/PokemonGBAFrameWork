﻿/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 14/03/2017
 * Time: 18:13
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Gabriel.Cat;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of AtaquesAprendidos.
	/// </summary>
	   public class AtaquesAprendidos:ObjectAutoId
    {
        public class AtaqueAprendido:ObjectAutoId,IComparable<AtaqueAprendido>,IComparable
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

            public int CompareTo(object other)
            {
                return CompareTo(other as AtaqueAprendido);
            }
            public int CompareTo(AtaqueAprendido other)
            {
                int compareTo =other!=null ? Nivel.CompareTo(other.Nivel) : (int)Gabriel.Cat.CompareTo.Inferior;
                if (compareTo == (int)Gabriel.Cat.CompareTo.Iguales)
                    compareTo = Ataque.CompareTo(other.Ataque);
                return compareTo;
            }
            
            public override string ToString()
            {
                return Ataque+":"+Nivel;
            }
        }
        
        public static readonly Zona ZonaAtaquesAprendidos;
        public static readonly byte[] MarcaFin;

        int offsetBytesAtaqueAprendido;
        Llista<AtaqueAprendido> ataques;

        static AtaquesAprendidos()
        {
            ZonaAtaquesAprendidos = new Zona("Ataques Aprendidos");
            MarcaFin = new byte[] { 0xFF, 0xFF }; 
            //añado las zonas
            ZonaAtaquesAprendidos.Add(0x6930C,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
            ZonaAtaquesAprendidos.Add(0x3E968,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);
            ZonaAtaquesAprendidos.Add(EdicionPokemon.RojoFuegoUsa, 0x3EA7C,0x3EA90);
            ZonaAtaquesAprendidos.Add(EdicionPokemon.VerdeHojaUsa, 0x3EA7C, 0x3EA90);
            ZonaAtaquesAprendidos.Add(0x3B988,EdicionPokemon.ZafiroEsp,EdicionPokemon.RubiEsp);
            ZonaAtaquesAprendidos.Add(0x3B7BC,EdicionPokemon.ZafiroUsa,EdicionPokemon.RubiUsa);
        }
        public AtaquesAprendidos()
        {
            Ataques = new Llista<AtaqueAprendido>();
        }
        public int OffsetBytesAtaqueAprendido
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
        public bool EstaElAtaque(int ataque)
        {
            bool esta = false;
            for (int i = 0; i < Ataques.Count && !esta; i++)
                esta = Ataques[i].Ataque == ataque;
            return esta;
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
                    for (int i = 0;i<ataques.Count;i++)
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
        public AtaqueAprendido[] GetAtaquesAprendidos(int nivel)
        {
            const int MAXATACKSFIGHT = 4;
            int posNivel=0;
            byte nivelByte =(byte) nivel;
            LlistaOrdenada<AtaqueAprendido> ataques=new LlistaOrdenada<AtaqueAprendido>();

            Ataques.Sort();

            while (posNivel < Ataques.Count-1 && Ataques[posNivel].Nivel <= nivel)
                posNivel++;
            //si el pokemon aprende dos veces el mismo ataque no lo tiene que tener duplicado...
            if(posNivel<MAXATACKSFIGHT)
            {
                for (int i = 0; i <= posNivel; i++)
                	if (!ataques.ContainsKey(((IClauUnicaPerObjecte)Ataques[i]).Clau))
                        ataques.Add(Ataques[i]);
         

            }else
            {
                for (int i = posNivel; ataques.Count<MAXATACKSFIGHT && i>=0; i--)
                {
                	if (!ataques.ContainsKey(((IClauUnicaPerObjecte)Ataques[i]).Clau))
                        ataques.Add(Ataques[i]);
                }

            }

            for (int i = ataques.Count; i < MAXATACKSFIGHT; i++)
                    ataques.Add(new AtaqueAprendido(-1));

            return (AtaqueAprendido[])ataques.Values;

        }
        public Ataque[] GetAtaques(int nivel,IList<Ataque> lstAtaquesSource)
        {
            if (lstAtaquesSource == null)
                throw new ArgumentNullException();
            const int MAXATACKSFIGHT = 4;
            Ataque[] ataques = new Ataque[MAXATACKSFIGHT];
            AtaqueAprendido[] ataquesAprendidos = GetAtaquesAprendidos(nivel);
            for (int i = 0; i < MAXATACKSFIGHT; i++)
                if (ataquesAprendidos[i].Ataque > 0)
                    ataques[i] = lstAtaquesSource[ataquesAprendidos[i].Ataque];
                else
                {
                    ataques[i] = new Ataque();
                  
                }
            return ataques;

        }

        public static AtaquesAprendidos GetAtaquesAprendidos(RomData rom, int indexPokemon)
        { return GetAtaquesAprendidos(rom.Rom, rom.Edicion, rom.Compilacion, indexPokemon); }
        public static AtaquesAprendidos GetAtaquesAprendidos(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int ordenGameFreakPokemon)
        {
            //missigno de por si usa el mismo puntero que bulbasaur por eso tienen los mismos ataques,supongo que sera para que no de error y no ocupar espacio...
            int offset =new OffsetRom(rom,GetOffsetPointer(rom,edicion,compilacion, ordenGameFreakPokemon)).Offset;
            BloqueBytes bloque = BloqueBytes.GetBytes(rom.Data,offset,MarcaFin);   
            AtaquesAprendidos ataquesAprendidos = new AtaquesAprendidos();
            //pongo los ataques
            for(int i=0;i<bloque.Bytes.Length;i+=2)
            {
            	
                ataquesAprendidos.Ataques.Add(new AtaqueAprendido((short)(bloque.Bytes[i]+(bloque.Bytes[i+1]%2==0? byte.MinValue : byte.MaxValue+1)),(byte)(bloque.Bytes[i+1]>>1)));
            }
            ataquesAprendidos.Ataques.Sort();//por si lo hacen de forma externa que lo lea bien :)
            ataquesAprendidos.OffsetBytesAtaqueAprendido = offset;
            return ataquesAprendidos;
        }
        private static int GetOffsetPointer(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int ordenGameFreakPokemon)
        {
        	return  Zona.GetOffsetRom(rom, ZonaAtaquesAprendidos, edicion, compilacion).Offset + (ordenGameFreakPokemon * OffsetRom.LENGTH);
        }
        public static void SetAtaquesAprendidos(RomData rom, int indexPokemon, AtaquesAprendidos ataquesAprendidos, LlistaOrdenadaPerGrups<int, AtaquesAprendidos> dicAtaquesPokemon)
        {  SetAtaquesAprendidos(rom.Rom, rom.Edicion, rom.Compilacion, indexPokemon,ataquesAprendidos,dicAtaquesPokemon); }
        public static void SetAtaquesAprendidos(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, int ordenGameFreakPokemon,AtaquesAprendidos ataquesAprendidos,LlistaOrdenadaPerGrups<int,AtaquesAprendidos> dicAtaquesPokemon)
        {
            int offsetPointer = GetOffsetPointer(rom, edicion, compilacion, ordenGameFreakPokemon);
            int offset = new OffsetRom(rom,offsetPointer).Offset;
            int offsetData=rom.Data.SetArray(ataquesAprendidos.ToBytesGBA());
            BloqueBytes bloqueOri = BloqueBytes.GetBytes(rom.Data,offset, MarcaFin);
            dicAtaquesPokemon.Remove(offset, ataquesAprendidos);
            if(!dicAtaquesPokemon.ContainsKey(offset))//si ya no hay ningun pokemon que use esos datos los borro
            {
              rom.Data.Remove( offset, bloqueOri.Bytes.Length);
              OffsetRom.SetOffset(rom,new OffsetRom(offset),offsetData);
            }else{
            
            	rom.Data.SetArray(offsetPointer,new OffsetRom(offsetData).BytesPointer);
            
            }

            dicAtaquesPokemon.Add(offset, ataquesAprendidos);//añado al diccionario el nuevo offset con los ataques :)
        }
        public static LlistaOrdenadaPerGrups<int, AtaquesAprendidos> GetAtaquesAprendidosDic(RomData romGBA)
        {
            return GetAtaquesAprendidosDic(romGBA.Rom, romGBA.Edicion, romGBA.Compilacion);
        }
        public static LlistaOrdenadaPerGrups<int,AtaquesAprendidos> GetAtaquesAprendidosDic(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
        {
            LlistaOrdenadaPerGrups<int, AtaquesAprendidos> dic = new LlistaOrdenadaPerGrups<int, AtaquesAprendidos>();
            for (int i = 0, f = Pokemon.GetTotalPokemon(rom, edicion, compilacion); i < f; i++)
                dic.Add(new OffsetRom(rom, GetOffsetPointer(rom, edicion, compilacion, i)).Offset, GetAtaquesAprendidos(rom, edicion, compilacion, i));
            return dic;
        }

		public static void Remove(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, int ordenGameFreak)
		{
			int offsetData=GetOffsetPointer(rom,edicion,compilacion,ordenGameFreak);
			rom.Data.Remove(offsetData,rom.Data.SearchArray(MarcaFin)-offsetData);
		}
    }
}