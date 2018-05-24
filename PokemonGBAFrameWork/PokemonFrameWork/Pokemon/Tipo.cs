﻿using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGBAFrameWork.Pokemon.Tipo;
using Gabriel.Cat.S.Binaris;

namespace PokemonGBAFrameWork.Pokemon
{
	
	public  class TipoCompleto:IElementoBinarioComplejo
	{
	
	
		public static readonly Zona ZonaImagenTipo;
        public static readonly ElementoBinario Serializador = ElementoBinarioNullable.GetElementoBinario(typeof(TipoCompleto));

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

        public Nombre Nombre { get; set; }

        static TipoCompleto()
		{
			
			ZonaImagenTipo = new Zona("Imagen Tipo");
			


			//añado las imagenes son algo pero no se que son...
			ZonaImagenTipo.Add(EdicionPokemon.VerdeHojaUsa, 0x107d88, 0x107e00);
			ZonaImagenTipo.Add(EdicionPokemon.RojoFuegoUsa, 0x107db0, 0x107e28);
			ZonaImagenTipo.Add(EdicionPokemon.EsmeraldaUsa, 0x19a340);

			ZonaImagenTipo.Add(EdicionPokemon.VerdeHojaEsp, 0x107f30);
			ZonaImagenTipo.Add(EdicionPokemon.RojoFuegoEsp, 0x107f58);
			ZonaImagenTipo.Add(EdicionPokemon.EsmeraldaEsp, 0x199F44);
			

		}
		



		public override string ToString()
		{
			return Nombre.Texto;
		}
        public static TipoCompleto GetTipo(RomGba rom,int posicion)
        {

			if (rom == null ||  posicion < 0) throw new ArgumentException();

            TipoCompleto tipo = new TipoCompleto();
            tipo.Nombre = Nombre.GetNombre(rom, posicion);
            return tipo;
		}
		public static int GetTotal(RomGba rom)
		{
			//de momento no se...mas adelante
			return 18;
		}

		public static TipoCompleto[] GetTipos(RomGba rom)
		{
            TipoCompleto[] tipos = new TipoCompleto[GetTotal(rom)];
			for (int i = 0; i < tipos.Length;i++)
				tipos[i] = GetTipo(rom, i);
			return tipos;
		}

		public static void SetTipo(RomGba rom, TipoCompleto tipo, int posicion)
		{
			if (rom == null ||tipo == null || posicion < 0) throw new ArgumentException();
            Nombre.SetNombre(rom, posicion, tipo.Nombre);

		}

		public static void SetTipos(RomGba rom,  IList<TipoCompleto> tipos)
		{

			if (rom == null || tipos == null) throw new ArgumentNullException();

            List<Nombre> nombres = new List<Nombre>();
            for (int i = 0; i < tipos.Count; i++)
            {
                nombres.Add(tipos[i].Nombre);
            }
            Nombre.SetNombre(rom, nombres);
		}
	}
}
