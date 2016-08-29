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
		//por revisar...de momento esta mal que Rojo/Verde tengan una pagina si tienen 24 bytes...
		public enum LongitudCampos
		{
			TotalGeneral = 24,
			TotalEsmeralda = 20,
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
			Zona zonaDescripcion = new Zona(Variables.Descripcion.ToString());
			
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

		internal static bool Validar(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
		{
			byte byteDescripcion;
			bool tieneMasCompilaciones = edicion.Abreviacion != Edicion.ABREVIACIONESMERALDA && edicion.IdiomaRom == Edicion.Idioma.Ingles;//esta pensado para las ediciones USA y ESP las demas no...de momento
			bool valido;
			if (tieneMasCompilaciones) {
				byteDescripcion = rom.Datos[Convert.ToInt32(Zona.GetOffset(rom, Variables.Descripcion.ToString(), edicion, compilacion) + LongitudCampos.NombreEspecie + 4/*poner lo que es...*/ + (int)Longitud.Offset - 1)];
				//si lo que se ha leido no es el fin de un pointer es que no es la compilación que toca i/o tambien la edicion...
				valido = (byteDescripcion != 0x8 && byteDescripcion != 0x9);
				
			} else
				valido = compilacion == CompilacionRom.Compilacion.Primera;
			return valido;
		}
		public static void SetDescripcionPokedex(RomPokemon rom,Edicion edicion,CompilacionRom.Compilacion compilacion,int index,DescripcionPokedex descripcion)
		{
			
		}
	}
}
