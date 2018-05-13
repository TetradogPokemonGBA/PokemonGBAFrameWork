/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 15/03/2017
 * Time: 17:52
 * 
 * Código bajo licencia GNU
 * créditos Wahackforo (los usuarios que han contribuido para hacer el mapa de la rom pokemon fire red 1.0)
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Habilidad.
	/// </summary>
	public class Habilidad
	{
		public const int LENGTHNOMBRE=13;
		public static readonly Zona ZonaNombreHabilidad;
		public static readonly Zona ZonaDescripcionHabilidad;
		
		
		
		BloqueString blNombre;
		BloqueString blDescripcion;
		
		static Habilidad()
		{
			ZonaNombreHabilidad=new Zona("Zona nombre habilidad");
			ZonaDescripcionHabilidad=new Zona("Zona descripcion habilidad");
			
			
			ZonaNombreHabilidad.Add(0x1C0,EdicionPokemon.RojoFuegoUsa,EdicionPokemon.VerdeHojaUsa,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
			ZonaNombreHabilidad.Add(EdicionPokemon.RubiUsa, 0x9FE64,0x9FE84);
			ZonaNombreHabilidad.Add(EdicionPokemon.ZafiroUsa, 0x9FE64, 0x9FE84);
			ZonaNombreHabilidad.Add(0xA0098,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp);
			
			ZonaDescripcionHabilidad.Add(0x1C4,EdicionPokemon.RojoFuegoUsa,EdicionPokemon.VerdeHojaUsa,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
			ZonaDescripcionHabilidad.Add(EdicionPokemon.RubiUsa,0x9FE68, 0x9FE88);
			ZonaDescripcionHabilidad.Add(EdicionPokemon.ZafiroUsa, 0x9FE68, 0x9FE88);
			ZonaDescripcionHabilidad.Add(0xA009C,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp);


			
		}
		
		public Habilidad()
		{
			blNombre=new BloqueString(LENGTHNOMBRE);
			blDescripcion=new BloqueString();
		}

		public BloqueString Nombre {
			get {
				return blNombre;
			}
		}

		public BloqueString Descripcion {
			get {
				return blDescripcion;
			}
		}
		public override string ToString()
		{
			return Nombre;
		}
		public static int GetTotal(RomGba rom)
		{
			return 78;
		}
		

		
		public static Habilidad GetHabilidad(RomGba rom,int index)
		{
			int offsetNombre=Zona.GetOffsetRom(ZonaNombreHabilidad,rom).Offset+index*LENGTHNOMBRE;
			int offsetDescripcion=new OffsetRom(rom,Zona.GetOffsetRom(ZonaDescripcionHabilidad,rom).Offset+index*OffsetRom.LENGTH).Offset;
			Habilidad habilidad=new Habilidad();
			habilidad.Nombre.Texto=BloqueString.GetString(rom,offsetNombre,LENGTHNOMBRE).Texto;
			habilidad.Descripcion.Texto=BloqueString.GetString(rom,offsetDescripcion).Texto;
			return habilidad;
		}

		public static Habilidad[] GetHabilidades(RomGba rom)
		{
			Habilidad[] habilidades=new Habilidad[GetTotal(rom)];
			for(int i=0;i<habilidades.Length;i++)
				habilidades[i]=GetHabilidad(rom,i);
			return habilidades;
		}

		public static void SetHabilidades(RomGba rom,IList<Habilidad> habilidades)
		{
			OffsetRom offsetNombre;
			OffsetRom offsetDescripcion;
			int totalActual=GetTotal(rom);
			
			if(totalActual!=habilidades.Count)
			{
				offsetNombre=Zona.GetOffsetRom(ZonaNombreHabilidad,rom);
				offsetDescripcion=Zona.GetOffsetRom(ZonaDescripcionHabilidad,rom);
				//borro
				rom.Data.Remove(offsetNombre.Offset,totalActual*LENGTHNOMBRE);
				rom.Data.Remove(offsetDescripcion.Offset,totalActual*OffsetRom.LENGTH);
				if(totalActual<habilidades.Count)
				{
					//reubico
					OffsetRom.SetOffset(rom,offsetNombre,rom.Data.SearchEmptyBytes(habilidades.Count*LENGTHNOMBRE));
					OffsetRom.SetOffset(rom,offsetDescripcion,rom.Data.SearchEmptyBytes(habilidades.Count*OffsetRom.LENGTH));
				}
			}
			for(int i=0;i<habilidades.Count;i++)
				SetHabilidad(rom,i,habilidades[i]);

		}
		

		
		public static void SetHabilidad(RomGba rom,int index,Habilidad habilidad)
		{
			int offsetNombre;
			int offsetDescripcion;
			
			offsetNombre=Zona.GetOffsetRom(ZonaNombreHabilidad,rom).Offset+index*LENGTHNOMBRE;
			BloqueString.Remove(rom,offsetNombre);
			BloqueString.SetString(rom,offsetNombre,habilidad.Nombre);
			
			try{
				offsetDescripcion=new OffsetRom(rom,Zona.GetOffsetRom(ZonaDescripcionHabilidad,rom).Offset+index*OffsetRom.LENGTH).Offset;
				BloqueString.Remove(rom,offsetDescripcion);
				BloqueString.SetString(rom,offsetDescripcion,habilidad.Descripcion);
			}catch{
				//quiere decir que no hay pointer y se tiene que poner todo
				rom.Data.SetArray(Zona.GetOffsetRom(ZonaDescripcionHabilidad,rom).Offset+index*OffsetRom.LENGTH,new OffsetRom(BloqueString.SetString(rom,habilidad.Descripcion)).BytesPointer);
			}
			
			
			

		}
		
	}
}
