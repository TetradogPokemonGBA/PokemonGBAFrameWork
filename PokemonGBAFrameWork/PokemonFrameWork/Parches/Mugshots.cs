/*
 * Created by SharpDevelop.
 * User: pikachu240
 * Date: 06/05/2017
 * Time: 5:32
 * Licencia GNU GPL V3
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using Gabriel.Cat;
using Gabriel.Cat.Extension;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Mugshots.
	/// </summary>
	public class Mugshots:IList<BloqueImagen>
	{
		const string REEMPLAZAR="POINTERTABLE";
		static readonly byte[] AsmMugshotsFRMake=ASM.Compilar(System.Text.ASCIIEncoding.ASCII.GetString(Resources.ASMMugshotsFR).Replace(REEMPLAZAR,"800000")).AsmBinary;
		static readonly Size SizeMugshot=new Size(80,80);
		
		Llista<BloqueImagen> mugshots;
		public Mugshots()
		{
			mugshots=new Llista<BloqueImagen>();
		}
		
		public int Count{
			get{return mugshots.Count;}
		}
		public BloqueImagen this[int index]
		{
			get{return mugshots[index];}
			set{mugshots[index]=value;}
		}
		public void Add(BloqueImagen img)
		{
			mugshots.Insert(mugshots.Count,img);
		}
		public bool Remove(BloqueImagen img)
		{
			return mugshots.Remove(img);
		}

		void ICollection<BloqueImagen>.CopyTo(BloqueImagen[] array, int arrayIndex)
		{
			mugshots.CopyTo(array,arrayIndex);
		}
		public int IndexOf(BloqueImagen item)
		{
			return mugshots.IndexOf(item);
		}

		public void Insert(int index, BloqueImagen item)
		{
			if(item==null||!item.GetImg().Size.Equals(SizeMugshot))
				throw new ArgumentException();
			
			mugshots.Insert(index,item);
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

		

		public bool IsReadOnly {
			get {
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
		
		public static Mugshots GetMugshots(RomData rom)
		{
			Mugshots mugshots=new Mugshots();
			int offsetTablaMugshots=GetOffsetTablaMugshots(rom);
			
			try{
				while(true)
				{
					
					mugshots.Add(BloqueImagen.GetBloqueImagenSinHeader(rom.Rom,offsetTablaMugshots));
					mugshots[mugshots.Count-1].Paletas.Add(Paleta.GetPaletaSinHeader(rom.Rom,offsetTablaMugshots+OffsetRom.LENGTH));
					offsetTablaMugshots+=OffsetRom.LENGTH*2;
					
				}//si el pointer no es una imagen puede dar error asi que es el fin de la carga
			}catch{}
			return mugshots;
			
		}

		public static int GetOffsetTablaMugshots(RomData rom)
		{
			int offsetTablaMugshots;
			if(!EstaActivado(rom))
			{
				Activar(rom);
			}
			offsetTablaMugshots=0;//de momento
			return offsetTablaMugshots;
		}
		public static void SetMugshots(RomData rom)
		{
			Mugshots oldMugshots=GetMugshots(rom);
			int offsetTablaMugshots=GetOffsetTablaMugshots(rom);
			offsetTablaMugshots+=OffsetRom.LENGTH*2*oldMugshots.Count;
			if(oldMugshots.Count<rom.Mugshots.Count)
			{
				//hay mas miro si caben y si no caben pues busco un nuevo sitio
				if(rom.Rom.Data.SearchEmptyBytes(offsetTablaMugshots,OffsetRom.LENGTH*2*(rom.Mugshots.Count-oldMugshots.Count))!=offsetTablaMugshots)
				{
					//borro la actual
					rom.Rom.Data.Remove(GetOffsetTablaMugshots(rom),oldMugshots.Count*OffsetRom.LENGTH*2);
					offsetTablaMugshots=ChangeOffsetTablaMugshots(rom);
				
				}
				//pongo los datos desde el principio
				for(int i=0;i<rom.Mugshots.Count;i++)
				{
					rom.Rom.Data.SetArray(offsetTablaMugshots,new OffsetRom(rom.Mugshots[i].Offset).BytesPointer);
					offsetTablaMugshots+=OffsetRom.LENGTH;
					rom.Rom.Data.SetArray(offsetTablaMugshots,new OffsetRom(rom.Mugshots[i].Paletas[0].Offset).BytesPointer);
					offsetTablaMugshots+=OffsetRom.LENGTH;
				}

				
			}else{
				//hay menos borro los que sobran
				rom.Rom.Data.Remove(offsetTablaMugshots,OffsetRom.LENGTH*2*(oldMugshots.Count-rom.Mugshots.Count),0x0);
				
			}
		}

		static int ChangeOffsetTablaMugshots(RomData rom)
		{
			int offset=rom.Rom.Data.SearchEmptyBytes(OffsetRom.LENGTH*2*rom.Mugshots.Count);
			//busco el inicio de la rutina
			int offsetInicioRutina=GetOffsetRutina(rom);
			//cambio el offset de la tabla de mugshots en la rutina
			
			return offset;
			
		}

		public static int GetOffsetRutina(RomData rom)
		{
			byte[] bytesAsmToFind=null;
	
			switch(((EdicionPokemon)rom.Rom.Edicion).AbreviacionRom)
			{
				case AbreviacionCanon.BPR:
					bytesAsmToFind=AsmMugshotsFRMake;
					break;
			}
			bytesAsmToFind=bytesAsmToFind.SubArray(100);
			
			return rom.Rom.Data.SearchArray(bytesAsmToFind);
		}
		public static bool EstaActivado(RomData rom)
		{
			return GetOffsetRutina(rom)>0;
		}

		public static void Activar(RomData rom)
		{
			SetMugshots(rom);
		}
		public static void Desactivar(RomData rom)
		{
	       
	        int offsetTablaMugshots=GetOffsetTablaMugshots(rom);
	        int offsetRutina=GetOffsetRutina(rom);
	         //borro los datos de la tabla
	         rom.Rom.Data.Remove(offsetTablaMugshots,OffsetRom.LENGTH*2*GetMugshots(rom).Count,0x0);
	         //borro la rutina
	         rom.Rom.Data.Remove(offsetRutina,AsmMugshotsFRMake.Length,0x0);//suponiendo que siempre ocupa igual sino pues metodo privado que me de esa info
		}
	}
}
