using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGBAFrameWork
{
	
	public  class Tipo:ObjectAutoId
	{
		public enum LongitudCampo
		{ Nombre=7 }
		public static readonly Zona ZonaNombreTipo;
		public static readonly Zona ZonaImagenTipo;
		static Tipo()
		{
			ZonaNombreTipo = new Zona("Nombre Tipo");
			ZonaImagenTipo = new Zona("Imagen Tipo");
			
			ZonaNombreTipo.Add(EdicionPokemon.ZafiroEsp, 0x2E574);
			ZonaNombreTipo.Add(EdicionPokemon.RubiEsp, 0x2E574);

			ZonaNombreTipo.Add(EdicionPokemon.ZafiroUsa, 0x2E3A8);
			ZonaNombreTipo.Add(EdicionPokemon.RubiUsa, 0x2E3A8);

			ZonaNombreTipo.Add(EdicionPokemon.EsmeraldaEsp, 0x166F4);
			ZonaNombreTipo.Add(EdicionPokemon.EsmeraldaUsa, 0x166F4);

			ZonaNombreTipo.Add(EdicionPokemon.RojoFuegoEsp, 0x308B4);
			ZonaNombreTipo.Add(EdicionPokemon.VerdeHojaEsp, 0x308B4);

			ZonaNombreTipo.Add(EdicionPokemon.RojoFuegoUsa, 0x309C8, 0x309DC);
			ZonaNombreTipo.Add(EdicionPokemon.VerdeHojaUsa, 0x309C8, 0x309DC);

			//añado las imagenes son algo pero no se que son...
			ZonaImagenTipo.Add(EdicionPokemon.VerdeHojaUsa, 0x107d88, 0x107e00);
			ZonaImagenTipo.Add(EdicionPokemon.RojoFuegoUsa, 0x107db0, 0x107e28);
			ZonaImagenTipo.Add(EdicionPokemon.EsmeraldaUsa, 0x19a340);

			ZonaImagenTipo.Add(EdicionPokemon.VerdeHojaEsp, 0x107f30);
			ZonaImagenTipo.Add(EdicionPokemon.RojoFuegoEsp, 0x107f58);
			ZonaImagenTipo.Add(EdicionPokemon.EsmeraldaEsp, 0x199F44);
			

		}
		BloqueString nombre;


		public Tipo(BloqueString nombre)
		{
			if (nombre == null) throw new ArgumentNullException();
			this.nombre = nombre;
			Nombre.MaxCaracteres = (int)LongitudCampo.Nombre;
		}

		public BloqueString Nombre
		{
			get
			{
				return nombre;
			}

			private set
			{
				nombre = value;
			}
		}
		public override string ToString()
		{
			return Nombre;
		}
		public static Tipo GetTipo(RomData rom, int posicion)
		{
			return GetTipo(rom.Rom,rom.Edicion,rom.Compilacion,posicion);
		}
		public static Tipo GetTipo(RomGba rom, EdicionPokemon edicion,Compilacion compilacion, int posicion)
		{
			if (rom == null || edicion == null ||  posicion < 0) throw new ArgumentException();
			BloqueString blNombre = BloqueString.GetString(rom, Zona.GetOffsetRom(rom, ZonaNombreTipo, edicion, compilacion).Offset + posicion * (int)LongitudCampo.Nombre, (int)LongitudCampo.Nombre,true);
			return new Tipo(blNombre);
		}
		public static int GetTotal(RomData rom)
		{
			return GetTotal(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static int GetTotal(RomGba rom, Edicion edicion, Compilacion compilacion)
		{
			//de momento no se...mas adelante
			return 18;
		}
		public static Tipo[] GetTipos(RomData rom)
		{
			return GetTipos(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static Tipo[] GetTipos(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			Tipo[] tipos = new Tipo[GetTotal(rom, edicion, compilacion)];
			for (int i = 0; i < tipos.Length;i++)
				tipos[i] = GetTipo(rom, edicion, compilacion, i);
			return tipos;
		}
		public static void SetTipo(RomData rom, Tipo tipo, int posicion)
		{
			SetTipo(rom.Rom,rom.Edicion,rom.Compilacion,tipo,posicion);
		}
		public static void SetTipo(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, Tipo tipo, int posicion)
		{
			if (rom == null || edicion == null || tipo == null || tipo.Nombre.Texto.Length > (int)LongitudCampo.Nombre || posicion < 0) throw new ArgumentException();
			int offset = Zona.GetOffsetRom(rom, ZonaNombreTipo, edicion, compilacion).Offset + posicion * (int)LongitudCampo.Nombre;
			
			BloqueString.Remove(rom,offset);
			BloqueString.SetString(rom, offset, tipo.Nombre);

		}
		public static void SetTipos(RomData rom)
		{
			
			SetTipos(rom.Rom,rom.Edicion,rom.Compilacion,rom.Tipos);
		}
		public static void SetTipos(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, IList<Tipo> tipos)
		{
			OffsetRom offsetNombres;
			if (rom == null || tipos == null) throw new ArgumentNullException();
			if (tipos.Count != GetTotal(rom, edicion, compilacion))
			{
				offsetNombres=Zona.GetOffsetRom(rom, ZonaNombreTipo, edicion, compilacion);
				rom.Data.Remove(offsetNombres.Offset, GetTotal(rom, edicion, compilacion) * (int)LongitudCampo.Nombre);
				OffsetRom.SetOffset(rom, offsetNombres, rom.Data.SearchEmptyBytes(tipos.Count * (int)LongitudCampo.Nombre));//actualizo el offset
			}
			for (int i = 0; i < tipos.Count; i++)
				SetTipo(rom, edicion, compilacion, tipos[i], i);
		}
	}
}
