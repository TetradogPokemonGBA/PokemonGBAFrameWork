using System;
using System.Collections.Generic;
using Gabriel.Cat.S.Utilitats;
using Gabriel.Cat.S.Extension;


namespace PokemonGBAFramework.Core
{
	/// <summary>
	/// Description of AtaquesAprendidos.
	/// </summary>
	public class AtaquesAprendidos :  IComparable, IComparable<AtaquesAprendidos>
	{
		public class AtaqueAprendido : IComparable<AtaqueAprendido>, IComparable
		{
			byte nivel;
			public AtaqueAprendido() { }
			public AtaqueAprendido(Word ataque = null, byte nivel = 1)
			{

				if (ataque == null)
					ataque = new Word(0);

				this.Ataque = ataque;
				this.nivel = nivel;
			}

            public Word Ataque { get; set; }

            public byte Nivel
			{
				get
				{
					return nivel;
				}

				set
				{
					if (value == 0 || value > 127)//ya se que el maximo es 100 pero por formato podria guardarse asi...   que lo aprenda es otra cosa
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
				int compareTo = other != null ? Nivel.CompareTo(other.Nivel) : (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
				if (compareTo == (int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals)
					compareTo = Ataque.CompareTo(other.Ataque);
				return compareTo;
			}

			public override string ToString()
			{
				return Ataque + ":" + Nivel;
			}
		}

		public static readonly byte[] MarcaFin=new byte[] { 0xFF, 0xFF };
		public static readonly byte[] MuestraAlgoritmo = {	0x18, 0x4B, 0x49, 0x46, 0x8C, 0x00, 0xE1};
		public static readonly int InicioRelativo = -MuestraAlgoritmo.Length - 16;

		public AtaquesAprendidos()
		{
			Ataques = new Llista<AtaqueAprendido>();
			Id = new IdUnico();
		}
        public int OffsetBytesAtaqueAprendido { get; set; }

        public Llista<AtaqueAprendido> Ataques { get; private set; }

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
			byte[] bytesGBA = new byte[Ataques.Count * 2 + MarcaFin.Length];

			Ataques.SortByQuickSort();

			unsafe
			{
				byte* ptrBytesGBA;
				fixed (byte* ptBytesGBA = bytesGBA)
				{
					ptrBytesGBA = ptBytesGBA;
					for (int i = 0; i < Ataques.Count; i++)
					{
						*ptrBytesGBA = (byte)(Ataques[i].Ataque % byte.MaxValue);
						ptrBytesGBA++;
						if (Ataques[i].Ataque > byte.MaxValue)
							*ptrBytesGBA = 0x1;
						*ptrBytesGBA += (byte)(Ataques[i].Nivel << 1);
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
			int posNivel = 0;
			byte nivelByte = (byte)nivel;
			LlistaOrdenada<AtaqueAprendido> ataques = new LlistaOrdenada<AtaqueAprendido>();

			Ataques.SortByQuickSort();

			while (posNivel < Ataques.Count - 1 && Ataques[posNivel].Nivel <= nivel)
				posNivel++;
			//si el pokemon aprende dos veces el mismo ataque no lo tiene que tener duplicado...
			if (posNivel < MAXATACKSFIGHT)
			{
				for (int i = 0; i <= posNivel; i++)
					if (!ataques.ContainsKey(((IClauUnicaPerObjecte)Ataques[i]).Clau))
						ataques.Add(Ataques[i]);


			}
			else
			{
				for (int i = posNivel; ataques.Count < MAXATACKSFIGHT && i >= 0; i--)
				{
					if (!ataques.ContainsKey(((IClauUnicaPerObjecte)Ataques[i]).Clau))
						ataques.Add(Ataques[i]);
				}

			}

			for (int i = ataques.Count; i < MAXATACKSFIGHT; i++)
				ataques.Add(new AtaqueAprendido());

			return (AtaqueAprendido[])ataques.Values;

		}
		public Ataque[] GetAtaques(int nivel, IList<Ataque> lstAtaquesSource)
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

		public static AtaquesAprendidos Get(RomGba rom, int ordenGameFreakPokemon,OffsetRom offsetInicioAtaquesAprendidos=default)
		{
			if (Equals(offsetInicioAtaquesAprendidos, default))
				offsetInicioAtaquesAprendidos = GetOffset(rom);
			//missigno de por si usa el mismo puntero que bulbasaur por eso tienen los mismos ataques,supongo que sera para que no de error y no ocupar espacio...
			int offset = new OffsetRom(rom, GetOffsetPointer(ordenGameFreakPokemon, offsetInicioAtaquesAprendidos)).Offset;
			BloqueBytes bloque = BloqueBytes.GetBytes(rom.Data, offset, MarcaFin);
			AtaquesAprendidos ataquesAprendidos = new AtaquesAprendidos();
			//pongo los ataques
			for (int i = 0; i < bloque.Bytes.Length; i += 2)
			{
				ataquesAprendidos.Ataques.Add(new AtaqueAprendido(new Word((ushort)(bloque.Bytes[i] + (bloque.Bytes[i + 1] % 2 == 0 ? byte.MinValue : byte.MaxValue + 1))), (byte)(bloque.Bytes[i + 1] >> 1)));
			}
			ataquesAprendidos.Ataques.SortByQuickSort();//por si lo hacen de forma externa que lo lea bien :)
			ataquesAprendidos.OffsetBytesAtaqueAprendido = offset;

			return ataquesAprendidos;
		}

		public static OffsetRom GetOffset(RomGba rom)
		{
			return new OffsetRom(rom, GetZona(rom));
		}

		public static int GetZona(RomGba rom)
		{
			return Zona.Search(rom, MuestraAlgoritmo, InicioRelativo);
		}

		private static int GetOffsetPointer(int ordenGameFreakPokemon, OffsetRom offsetInicioAtaquesAprendidos)
        {
			return offsetInicioAtaquesAprendidos + (ordenGameFreakPokemon * OffsetRom.LENGTH);
		}

		public static LlistaOrdenadaPerGrups<int, AtaquesAprendidos> GetDic(RomGba rom)
		{
			LlistaOrdenadaPerGrups<int, AtaquesAprendidos> dic = new LlistaOrdenadaPerGrups<int, AtaquesAprendidos>();
			OffsetRom offset = GetOffset(rom);
			for (int i = 0, f = Huella.GetTotal(rom); i < f; i++)
				dic.Add(new OffsetRom(rom, GetOffsetPointer(i, offset)).Offset, Get(rom, i));
			return dic;
		}

		public static AtaquesAprendidos[] GetRange(RomGba rom) => Huella.GetAll<AtaquesAprendidos>(rom, Get, GetOffset(rom));
		public static AtaquesAprendidos[] GetRangeOrdenLocal(RomGba rom) => OrdenLocal.GetOrdenados<AtaquesAprendidos>(rom, (r, o) => GetRange(r), GetOffset(rom));
		public static AtaquesAprendidos[] GetRangeOrdenNacional(RomGba rom) => OrdenNacional.GetOrdenados<AtaquesAprendidos>(rom, (r, o) => GetRange(r), GetOffset(rom));



	}
}