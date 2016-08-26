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
		enum LongitudCampos{
		TotalGeneral=24,TotalEsmeralda=20,
		NombreEspecie= 12,DescripcionRubiZafiro,DescipcionEsmeralda,DescripcionRojoFuegoVerdeHoja,
		PaginasRubiZafiro=2,PaginasGeneral=1,NumeroDeLineasPorPaginaGeneral=3,NumeroDeLineasPorPaginaEsmeralda=4,
		}
		static readonly byte MarcaFinMensaje = 255;
		//en construccion
		Hex offsetInicio;
		BloqueString nombreEspecie;//tiene un tamaño maximo en todas las versiones de 0xC
		BloqueString descripcion;
		//falta proporcion con entrenador y peso aun no lo acabo de entender como va...
		public DescripcionPokedex()
		{
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
		public Hex OffsetFin(bool esEsmeralda=false)
		{
			return OffsetInicio+(esEsmeralda?(int)LongitudCampos.TotalEsmeralda:(int)LongitudCampos.TotalGeneral);
		}
	}
}
