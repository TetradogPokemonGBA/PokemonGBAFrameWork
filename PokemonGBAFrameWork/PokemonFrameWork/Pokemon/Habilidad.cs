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
		public static int GetTotal(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			return 78;
		}
		
		public static Habilidad GetHabilidad(RomData rom,int index)
		{
			return GetHabilidad(rom.Rom,rom.Edicion,rom.Compilacion,index);
		}
		
		public static Habilidad GetHabilidad(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int index)
		{
			int offsetNombre=Zona.GetOffsetRom(rom,ZonaNombreHabilidad,edicion,compilacion).Offset+index*LENGTHNOMBRE;
			int offsetDescripcion=new OffsetRom(rom,Zona.GetOffsetRom(rom,ZonaDescripcionHabilidad,edicion,compilacion).Offset+index*OffsetRom.LENGTH).Offset;
			Habilidad habilidad=new Habilidad();
			habilidad.Nombre.Texto=BloqueString.GetString(rom,offsetNombre,LENGTHNOMBRE).Texto;
			habilidad.Descripcion.Texto=BloqueString.GetString(rom,offsetDescripcion).Texto;
			return habilidad;
		}
		public static Habilidad[] GetHabilidades(RomData rom)
		{
			return GetHabilidades(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static Habilidad[] GetHabilidades(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			Habilidad[] habilidades=new Habilidad[GetTotal(rom,edicion,compilacion)];
			for(int i=0;i<habilidades.Length;i++)
				habilidades[i]=GetHabilidad(rom,edicion,compilacion,i);
			return habilidades;
		}
		
		public static void SetHabilidades(RomData rom)
		{
			SetHabilidades(rom.Rom,rom.Edicion,rom.Compilacion,rom.Habilidades);
		}
		public static void SetHabilidades(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,IList<Habilidad> habilidades)
		{
			OffsetRom offsetNombre;
			OffsetRom offsetDescripcion;
			int totalActual=GetTotal(rom,edicion,compilacion);
			
			if(totalActual!=habilidades.Count)
			{
				offsetNombre=Zona.GetOffsetRom(rom,ZonaNombreHabilidad,edicion,compilacion);
				offsetDescripcion=Zona.GetOffsetRom(rom,ZonaDescripcionHabilidad,edicion,compilacion);
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
				SetHabilidad(rom,edicion,compilacion,i,habilidades[i]);

		}
		
		public static void SetHabilidad(RomData rom,int index,Habilidad habilidad)
		{
			SetHabilidad(rom.Rom,rom.Edicion,rom.Compilacion,index,habilidad);
		}
		
		public static void SetHabilidad(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int index,Habilidad habilidad)
		{
			int offsetNombre;
			int offsetDescripcion;
			
			offsetNombre=Zona.GetOffsetRom(rom,ZonaNombreHabilidad,edicion,compilacion).Offset+index*LENGTHNOMBRE;
			BloqueString.Remove(rom,offsetNombre);
			BloqueString.SetString(rom,offsetNombre,habilidad.Nombre);
			
			try{
				offsetDescripcion=new OffsetRom(rom,Zona.GetOffsetRom(rom,ZonaDescripcionHabilidad,edicion,compilacion).Offset+index*OffsetRom.LENGTH).Offset;
				BloqueString.Remove(rom,offsetDescripcion);
				BloqueString.SetString(rom,offsetDescripcion,habilidad.Descripcion);
			}catch{
				//quiere decir que no hay pointer y se tiene que poner todo
				rom.Data.SetArray(Zona.GetOffsetRom(rom,ZonaDescripcionHabilidad,edicion,compilacion).Offset+index*OffsetRom.LENGTH,new OffsetRom(BloqueString.SetString(rom,habilidad.Descripcion)).BytesPointer);
			}
			
			
			

		}
		
	}
}
