using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGBAFrameWork.Pokemon.Tipo;
using Gabriel.Cat.S.Binaris;

namespace PokemonGBAFrameWork.Pokemon
{
	
	public  class TipoCompleto:PokemonFrameWorkItem
	{

        public const byte ID = 0x2A;
        public static readonly Zona ZonaImagenTipo;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<TipoCompleto>();
        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

        public Nombre Nombre { get; set; }

        static TipoCompleto()
		{
			
			ZonaImagenTipo = new Zona("Imagen Tipo");
			


			//añado las imagenes son algo pero no se que son...
			ZonaImagenTipo.Add(EdicionPokemon.VerdeHojaUsa10, 0x107d88, 0x107e00);
			ZonaImagenTipo.Add(EdicionPokemon.RojoFuegoUsa10, 0x107db0, 0x107e28);
			ZonaImagenTipo.Add(EdicionPokemon.EsmeraldaUsa10, 0x19a340);

			ZonaImagenTipo.Add(EdicionPokemon.VerdeHojaEsp10, 0x107f30);
			ZonaImagenTipo.Add(EdicionPokemon.RojoFuegoEsp10, 0x107f58);
			ZonaImagenTipo.Add(EdicionPokemon.EsmeraldaEsp10, 0x199F44);
			

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
            tipo.IdElemento = (ushort)posicion;
            if (((EdicionPokemon)rom.Edicion).Idioma == Idioma.Ingles)
                tipo.IdFuente = EdicionPokemon.IDMINRESERVADO - (int)Idioma.Español;
            else tipo.IdFuente = EdicionPokemon.IDMINRESERVADO - (int)Idioma.Ingles;

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

	
	}
}
