/*
 * Created by SharpDevelop.
 * User: pikachu240
 * Date: 06/05/2017
 * Time: 5:32
 * Licencia GNU GPL V3
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Drawing;

//SE tiene que rehacer entero!!
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Mugshots.
	/// </summary>
	public class Mugshots : IList<BloqueImagen>
	{
		const string REEMPLAZAR = "POINTERTABLE";
		public static readonly LlistaOrdenada<EdicionPokemon, ASM> AsmMugshots;
		static readonly LlistaOrdenada<EdicionPokemon, int> TablaMugshotsPosicion;
		static readonly Size SizeMugshot = new Size(80, 80);
		public const string DESCRIPCION="Permite usar Mugshots(Imagenes encima de la caja de texto)";
		Llista<BloqueImagen> mugshots;

		static Mugshots()
		{
			ASM asmMugshots;
			AsmMugshots = new LlistaOrdenada<EdicionPokemon, ASM>();
			TablaMugshotsPosicion = new LlistaOrdenada<EdicionPokemon, int>();

			asmMugshots = ASM.Compilar(System.Text.ASCIIEncoding.ASCII.GetString(Resources.ASMMugshotsFR).Replace(REEMPLAZAR, "800000"));//mirar si es compatible con LF

			AsmMugshots.Add(EdicionPokemon.RojoFuegoUsa, asmMugshots);
			//falta añadir la posicion donde esta el pointer a la tabla
		}
		public Mugshots()
		{
			mugshots = new Llista<BloqueImagen>();
		}

		public int Count
		{
			get { return mugshots.Count; }
		}
		public BloqueImagen this[int index]
		{
			get { return mugshots[index]; }
			set { mugshots[index] = value; }
		}
		public void Add(BloqueImagen img)
		{
			mugshots.Insert(mugshots.Count, img);
		}
		public bool Remove(BloqueImagen img)
		{
			return mugshots.Remove(img);
		}

		void ICollection<BloqueImagen>.CopyTo(BloqueImagen[] array, int arrayIndex)
		{
			mugshots.CopyTo(array, arrayIndex);
		}
		public int IndexOf(BloqueImagen item)
		{
			return mugshots.IndexOf(item);
		}

		public void Insert(int index, BloqueImagen item)
		{
			if (item == null || !item.GetImg().Size.Equals(SizeMugshot))
				throw new ArgumentException();

			mugshots.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			mugshots.RemoveAt(index);
		}

		public void Clear()
		{
			mugshots.Clear();
		}

		public bool Contains(BloqueImagen item)
		{
			return mugshots.Contains(item);
		}



		public bool IsReadOnly
		{
			get
			{
				return mugshots.IsReadOnly;
			}
		}

		public IEnumerator<BloqueImagen> GetEnumerator()
		{
			return mugshots.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public static Mugshots GetMugshots(RomGba rom, EdicionPokemon edicion, Compilacion compilacio)
		{
			Mugshots mugshots = new Mugshots();
			int offsetTablaMugshots = GetOffsetTablaMugshots(rom, edicion, compilacio);

			try
			{
				while (true)
				{

					mugshots.Add(BloqueImagen.GetBloqueImagenSinHeader(rom, offsetTablaMugshots));
					mugshots[mugshots.Count - 1].Paletas.Add(Paleta.GetPaletaSinHeader(rom, offsetTablaMugshots + OffsetRom.LENGTH));
					offsetTablaMugshots += OffsetRom.LENGTH * 2;

				}//si el pointer no es una imagen puede dar error asi que es el fin de la carga
			}
			catch { }
			return mugshots;

		}
		public static bool Compatible(EdicionPokemon edicion,Compilacion compilacion)
		{
			bool compatible=AsmMugshots.ContainsKey(edicion);
			return compatible;
		}

		public static int GetOffsetTablaMugshots(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			int offsetTablaMugshots;

			if (!EstaActivado(rom, edicion, compilacion))
			{
				offsetTablaMugshots = -1;
			}
			else offsetTablaMugshots = new OffsetRom(rom, GetOffsetRutina(rom, edicion, compilacion) + TablaMugshotsPosicion[edicion]).Offset;

			return offsetTablaMugshots;
		}
		public static void SetMugshots(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, Mugshots mugshots)
		{

			Mugshots oldMugshots;
			int offsetTablaMugshots;
			int auxOffsetImg;
			int auxOffsetPaleta;
			byte[] datosImgComprimidos;
			byte[] datosPaletaComprimida;

			if (!EstaActivado(rom, edicion, compilacion))
			{
				//pongo el codigo asm
				rom.Data.SetArray(AsmMugshots[edicion].AsmBinary);

			}
			oldMugshots = GetMugshots(rom, edicion, compilacion);
			offsetTablaMugshots = GetOffsetTablaMugshots(rom, edicion, compilacion);
			offsetTablaMugshots += OffsetRom.LENGTH * 2 * oldMugshots.Count;
			if (oldMugshots.Count < mugshots.Count)
			{
				//hay mas miro si caben y si no caben pues busco un nuevo sitio
				if (rom.Data.SearchEmptyBytes(offsetTablaMugshots, OffsetRom.LENGTH * 2 * (mugshots.Count - oldMugshots.Count)) != offsetTablaMugshots)
				{
					//borro la actual
					rom.Data.Remove(GetOffsetTablaMugshots(rom, edicion, compilacion), oldMugshots.Count * OffsetRom.LENGTH * 2);
					offsetTablaMugshots = ChangeOffsetTablaMugshots(rom, edicion, compilacion, mugshots);

				}




			}
			else
			{
				//hay menos borro los que sobran
				rom.Data.Remove(offsetTablaMugshots, OffsetRom.LENGTH * 2 * (oldMugshots.Count - mugshots.Count), 0x0);

			}

			//pongo los datos desde el principio
			for (int i = 0; i < mugshots.Count; i++)
			{

				//miro si las imagenes estan y si estan tienen el pointer bien

				//pongo los datos de la imagen si no estan ya
				datosImgComprimidos = mugshots[i].DatosComprimidos();
				auxOffsetImg = rom.Data.SearchArray(mugshots[i].Offset, mugshots[i].Offset + datosImgComprimidos.Length, datosImgComprimidos);
				if (auxOffsetImg < 0)
				{
					auxOffsetImg = rom.Data.SearchArray(mugshots[i].Offset, datosImgComprimidos);
					if (auxOffsetImg < 0)
					{
						auxOffsetImg = rom.Data.SetArray(datosImgComprimidos);
					}
				}

				rom.Data.SetArray(offsetTablaMugshots, new OffsetRom(auxOffsetImg).BytesPointer);
				offsetTablaMugshots += OffsetRom.LENGTH;
				//pongo la paleta si no esta ya
				datosPaletaComprimida = Paleta.GetBytesGBA(mugshots[i].Paletas[0]);
				auxOffsetPaleta = rom.Data.SearchArray(mugshots[i].Paletas[0].Offset, mugshots[i].Paletas[0].Offset + datosPaletaComprimida.Length, datosPaletaComprimida);
				if (auxOffsetPaleta < 0)
				{
					auxOffsetPaleta = rom.Data.SearchArray(mugshots[i].Paletas[0].Offset, datosPaletaComprimida);
					if (auxOffsetPaleta < 0)
					{
						auxOffsetPaleta = rom.Data.SetArray(datosPaletaComprimida);
					}
				}

				rom.Data.SetArray(offsetTablaMugshots, new OffsetRom(auxOffsetPaleta).BytesPointer);
				offsetTablaMugshots += OffsetRom.LENGTH;
			}

		}

		static int ChangeOffsetTablaMugshots(RomGba rom, EdicionPokemon edicion, Compilacion compilacio, Mugshots mugshots)
		{
			int offset = rom.Data.SearchEmptyBytes(OffsetRom.LENGTH * 2 * mugshots.Count);
			//busco el inicio de la rutina
			//cambio el offset de la tabla de mugshots en la rutina
			rom.Data.SetArray(new OffsetRom(offset).BytesPointer, GetOffsetRutina(rom, edicion, compilacio) + TablaMugshotsPosicion[edicion]);

			return offset;

		}

		public static int GetOffsetRutina(RomGba rom, EdicionPokemon edicion, Compilacion compilacio)
		{
			if (!AsmMugshots.ContainsKey(edicion))
				throw new RomFaltaInvestigacionException();

			return rom.Data.SearchArray(AsmMugshots[edicion].AsmBinary);
		}
		public static bool EstaActivado(RomGba rom, EdicionPokemon edicion, Compilacion compilacio)
		{
			return GetOffsetRutina(rom, edicion, compilacio) > 0;
		}

		public static void Activar(RomGba rom, EdicionPokemon edicion, Compilacion compilacio)
		{
			SetMugshots(rom, edicion, compilacio, new Mugshots());
		}
		public static void Desactivar(RomGba rom, EdicionPokemon edicion, Compilacion compilacio)
		{

			int offsetTablaMugshots;
			int offsetRutina;

			if (EstaActivado(rom, edicion, compilacio))
			{
				offsetTablaMugshots = GetOffsetTablaMugshots(rom, edicion, compilacio);
				offsetRutina = GetOffsetRutina(rom, edicion, compilacio);
				//borro los datos de la tabla
				rom.Data.Remove(offsetTablaMugshots, OffsetRom.LENGTH * 2 * GetMugshots(rom, edicion, compilacio).Count, 0x0);
				//borro la rutina
				rom.Data.Remove(offsetRutina, AsmMugshots[edicion].AsmBinary.Length, 0x0);//suponiendo que siempre ocupa igual sino pues metodo privado que me de esa info
			}
		}
	}
}
