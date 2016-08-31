/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 19/08/2016
 * Time: 16:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Gabriel.Cat;
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of DescripcionPokedex.
	/// </summary>
	public class DescripcionPokedex
	{

		public enum LongitudCampos
		{
			TotalGeneral = 36,
			TotalEsmeralda = 32,
			NombreEspecie = 12,
			DescripcionRubiZafiro,
			DescipcionEsmeralda,
			DescripcionRojoFuegoVerdeHoja,
			PaginasRubiZafiro = 2,
			PaginasGeneral = 1,
			NumeroDeLineasPorPaginaGeneral = 3,
			NumeroDeLineasPorPaginaEsmeralda = 4,
		}
		public enum Variables
		{
			Descripcion
		}
		static readonly byte MarcaFinMensaje = 255;
		//en construccion
		Hex offsetInicio;
		BloqueString nombreEspecie;
		//tiene un tamaño maximo en todas las versiones de 0xC
		BloqueString descripcion;
		//falta proporcion con entrenador y peso aun no lo acabo de entender como va...
		static DescripcionPokedex()
		{
			Zona zonaDescripcion = new Zona(Variables.Descripcion);
			
			//aqui van las zonas ya descubiertas

			zonaDescripcion.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0xBFA48);
			zonaDescripcion.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0xBFA20);
			zonaDescripcion.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x88FEC);
			zonaDescripcion.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x88E34, 0x88E48);
			zonaDescripcion.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x88FC0);
			zonaDescripcion.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x88E08, 0x88E1C);
			zonaDescripcion.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x8F998);
			zonaDescripcion.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x8F998);
			zonaDescripcion.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x8F508, 0x8F528);
			zonaDescripcion.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x8F508, 0x8F528);
			//añado la zona al diccionario
			Zona.DiccionarioOffsetsZonas.Añadir(zonaDescripcion);
		}
		public DescripcionPokedex()
		{
			nombreEspecie = new BloqueString((int)LongitudCampos.NombreEspecie);
			descripcion = new BloqueString("");
		}
		//calcular OffsetInicio con el offset de la descripcion

		public Hex OffsetInicio {
			get {
				return offsetInicio;
			}
			set {
				offsetInicio = value;
			}
		}

		public BloqueString NombreEspecie {
			get {
				return nombreEspecie;
			}
		}

		public BloqueString Descripcion {
			get {
				return descripcion;
			}
		}
		public Hex OffsetFin(bool esEsmeralda = false)
		{
			return OffsetInicio + (esEsmeralda ? (int)LongitudCampos.TotalEsmeralda : (int)LongitudCampos.TotalGeneral);
		}

		internal static bool ValidarZona(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
		{
			bool tieneMasCompilaciones = edicion.Abreviacion != Edicion.ABREVIACIONESMERALDA && edicion.IdiomaRom == Edicion.Idioma.English;//esta pensado para las ediciones USA y ESP las demas no...de momento
			bool valido;
			if (tieneMasCompilaciones) {
                valido = ValidarOffset(rom,edicion,Zona.GetOffset(rom, Variables.Descripcion, edicion, compilacion));
				
			} else
				valido = compilacion == CompilacionRom.Compilacion.Primera;
			return valido;
		}
        internal static bool ValidarOffset(RomPokemon rom, Edicion edicion, Hex offsetInicioDescripcion)
        {
            Hex offsetByteValidador = offsetInicioDescripcion + (int)LongitudCampos.NombreEspecie + 4/*poner lo que es...*/ + (int)Longitud.Offset - 1;
            byte byteValidador = rom.Datos[offsetByteValidador];
            bool valido= (byteValidador == 0x8 || byteValidador == 0x9);
            if (valido&&(edicion.Abreviacion == Edicion.ABREVIACIONZAFIRO || edicion.Abreviacion == Edicion.ABREVIACIONRUBI))
            {
                offsetByteValidador += (int)Longitud.Offset;
                byteValidador = rom.Datos[offsetByteValidador];
                valido = (byteValidador == 0x8 || byteValidador == 0x9);
            }
            return valido;

        }
        internal static bool ValidarIndicePokemon(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex ordenGameFreak)
        {
            return ValidarOffset(rom,edicion,Zona.GetOffset(rom, Variables.Descripcion, edicion, compilacion) + ordenGameFreak * DameTotal(edicion));
        }

        private static int DameTotal(Edicion edicion)
        {
            int total;
            if (edicion.Abreviacion != Edicion.ABREVIACIONESMERALDA)
                total = (int)LongitudCampos.TotalGeneral;
            else total = (int)LongitudCampos.TotalEsmeralda;
            return total;
        }

        public static void SetDescripcionPokedex(RomPokemon rom,Edicion edicion,CompilacionRom.Compilacion compilacion,Hex ordenGameFreak, DescripcionPokedex descripcion)
		{
			
		}

        public static DescripcionPokedex GetDescripcionPokedex(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex ordenGameFreak)
        {
            throw new NotImplementedException();
        }
    }
}
