/*
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
using Gabriel.Cat.S.Utilitats;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Binaris;
using System.Linq;
using PokemonGBAFramework;

namespace PokemonGBAFrameWork.Pokemon
{
	/// <summary>
	/// Description of AtaquesAprendidos.
	/// </summary>
	public class AtaquesAprendidos:IComparable,IComparable<AtaquesAprendidos>
	{
		public class AtaqueAprendido:IComparable<AtaqueAprendido>,IComparable,IElementoBinarioComplejo
		{
            public const byte ID = 0x19;
            public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<AtaqueAprendido>();

            Word ataque;// :S no acabo de ver que sea asi...porque no se lee ni se escribe como seria un word...o eso me parece...por mirar...
			byte nivel;
            public AtaqueAprendido() { }
			public AtaqueAprendido(Word ataque=null, byte nivel=1)
			{

				if(ataque==null)
					ataque=new Word(0);
				
				this.ataque = ataque;
				this.nivel = nivel;
			}

			public Word Ataque
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

            ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

            public int CompareTo(object other)
			{
				return CompareTo(other as AtaqueAprendido);
			}
			public int CompareTo(AtaqueAprendido other)
			{
				int compareTo =other!=null ? Nivel.CompareTo(other.Nivel) : (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
				if (compareTo == (int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals)
					compareTo = Ataque.CompareTo(other.Ataque);
				return compareTo;
			}
			
			public override string ToString()
			{
				return Ataque+":"+Nivel;
			}

			public static PokemonGBAFramework.Pokemon.Ataque.AtaqueAprendido GetAtaqueAprendido(AtaqueAprendido ataque)
			{
				return new PokemonGBAFramework.Pokemon.Ataque.AtaqueAprendido() { Ataque = ataque.Ataque, Nivel = ataque.Nivel };
			}
		}
        public const byte ID = 0x1A;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<AtaquesAprendidos>();

        public static readonly Zona ZonaAtaquesAprendidos;
		public static readonly byte[] MarcaFin;

		int offsetBytesAtaqueAprendido;
		Llista<AtaqueAprendido> ataques;

        static AtaquesAprendidos()
		{
			ZonaAtaquesAprendidos = new Zona("Ataques Aprendidos");
			MarcaFin = new byte[] { 0xFF, 0xFF };
			//añado las zonas
			ZonaAtaquesAprendidos.Add(0x6930C,EdicionPokemon.EsmeraldaUsa10,EdicionPokemon.EsmeraldaEsp10);
			ZonaAtaquesAprendidos.Add(0x3E968,EdicionPokemon.RojoFuegoEsp10,EdicionPokemon.VerdeHojaEsp10);
			ZonaAtaquesAprendidos.Add(EdicionPokemon.RojoFuegoUsa10, 0x3EA7C,0x3EA90);
			ZonaAtaquesAprendidos.Add(EdicionPokemon.VerdeHojaUsa10, 0x3EA7C, 0x3EA90);
			ZonaAtaquesAprendidos.Add(0x3B988,EdicionPokemon.ZafiroEsp10,EdicionPokemon.RubiEsp10);
			ZonaAtaquesAprendidos.Add(0x3B7BC,EdicionPokemon.ZafiroUsa10,EdicionPokemon.RubiUsa10);
		}
		public AtaquesAprendidos()
		{
			Ataques = new Llista<AtaqueAprendido>();
            Id = new IdUnico();
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


        public IdUnico Id { get; set; }

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

			Ataques.SortByQuickSort();

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

			Ataques.SortByQuickSort();

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
				ataques.Add(new AtaqueAprendido());

			return (AtaqueAprendido[])ataques.Values;

		}
		public AtaqueCompleto[] GetAtaques(int nivel,IList<AtaqueCompleto> lstAtaquesSource)
		{
			if (lstAtaquesSource == null)
				throw new ArgumentNullException();
			const int MAXATACKSFIGHT = 4;
			AtaqueCompleto[] ataques = new AtaqueCompleto[MAXATACKSFIGHT];
			AtaqueAprendido[] ataquesAprendidos = GetAtaquesAprendidos(nivel);
			for (int i = 0; i < MAXATACKSFIGHT; i++)
				if (ataquesAprendidos[i].Ataque > 0)
					ataques[i] = lstAtaquesSource[ataquesAprendidos[i].Ataque];
				else
			{
				ataques[i] = new AtaqueCompleto();
				
			}
			return ataques;

		}

        int IComparable.CompareTo(object obj)
        {
            return CompareTo(obj as AtaquesAprendidos);
        }
        int IComparable<AtaquesAprendidos>.CompareTo(AtaquesAprendidos other)
        {
            return CompareTo(other);
        }
        int CompareTo(AtaquesAprendidos other)
        {
            int compareTo;
            if (other != null)
                compareTo = Id.CompareTo(other.Id);
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
            return compareTo;
        }
        public static Paquete GetAtaquesAprendidos(RomGba rom)
        {
            return Poke.Extension.GetPaquete(rom,"Ataques aprendidos",(r,i)=>GetAtaquesAprendidos(r,i),Huella.GetTotal(rom));
        }
        public static PokemonGBAFramework.Pokemon.Ataque.AtaquesAprendidos GetAtaquesAprendidos(RomGba rom,int ordenGameFreakPokemon)
		{
			//missigno de por si usa el mismo puntero que bulbasaur por eso tienen los mismos ataques,supongo que sera para que no de error y no ocupar espacio...
			int offset =new OffsetRom(rom,GetOffsetPointer(rom, ordenGameFreakPokemon)).Offset;
			BloqueBytes bloque = BloqueBytes.GetBytes(rom.Data,offset,MarcaFin);
			AtaquesAprendidos ataquesAprendidos = new AtaquesAprendidos();

			//pongo los ataques
			for(int i=0;i<bloque.Bytes.Length;i+=2)
			{
				ataquesAprendidos.Ataques.Add(new AtaqueAprendido(new Word((ushort)(bloque.Bytes[i]+(bloque.Bytes[i+1]%2==0? byte.MinValue : byte.MaxValue+1))),(byte)(bloque.Bytes[i+1]>>1)));
			}
			ataquesAprendidos.Ataques.SortByQuickSort();//por si lo hacen de forma externa que lo lea bien :)
			ataquesAprendidos.OffsetBytesAtaqueAprendido = offset;


			return new PokemonGBAFramework.Pokemon.Ataque.AtaquesAprendidos() { Ataques = ataquesAprendidos.Ataques.Select((ataque) =>AtaqueAprendido.GetAtaqueAprendido(ataque)).ToList() };
		}
		private static int GetOffsetPointer(RomGba rom,int ordenGameFreakPokemon)
		{
			return  Zona.GetOffsetRom(ZonaAtaquesAprendidos, rom).Offset + (ordenGameFreakPokemon * OffsetRom.LENGTH);
		}



	


      
    }
}
