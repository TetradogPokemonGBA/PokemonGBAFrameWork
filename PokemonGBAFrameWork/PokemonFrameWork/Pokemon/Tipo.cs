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
	
	public  class TipoCompleto
	{

        public const byte ID = 0x2A;
        public static readonly Zona ZonaImagenTipo;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<TipoCompleto>();


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
		

		public static int GetTotal(RomGba rom)
		{
			//de momento no se...mas adelante
			return 18;
		}

	
	
	}
}
