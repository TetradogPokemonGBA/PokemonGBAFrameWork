/*
 * Creado por SharpDevelop.
 * Usuario: tetradog
 * Fecha: 29/08/2016
 * Hora: 23:26
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using Gabriel.Cat;
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Objeto.
	/// </summary>
	public class Objeto
	{
		public enum Variables
		{
			Objeto
		}
		public enum LongitudCampos
		{
			Total=44,
			Nombre=13,
		}
		BloqueString nombre;
		/*por saber como va*/
		//imagen
		//descripcion
		//efecto
		//uso
		//etc
		static Objeto()
		{
			Zona zonaObjeto = new Zona(Variables.Objeto.ToString());

			
			//nombre
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x1C8);
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x1C8);
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x1C8);
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x1C8);
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x1C8);
			
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0xA98F0, 0xA9910, 0xA9910);
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0xA98F0, 0xA9910, 0xA9910);
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x1C8);
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x1C8);
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x1C8);
		}
		public Objeto(BloqueString nombre)
		{
			this.nombre = nombre;
			nombre.MaxCaracteres=(int)LongitudCampos.Nombre;

		}

		public BloqueString Nombre {
			get {
				return nombre;
			}
			private set {
				nombre = value;
			}
		}

		public static int TotalObjetos(RomPokemon rom)
		{
			return TotalObjetos(rom, Edicion.GetEdicion(rom));
		}
		public static int TotalObjetos(RomPokemon rom, Edicion edicion)
		{
			//en  un futuro sacarlos de la rom!!
			int totalItems = 0;
			switch (edicion.Abreviacion) {
				case Edicion.ABREVIACIONESMERALDA:
					totalItems = 0x179;
					break;
				case Edicion.ABREVIACIONRUBI:
				case Edicion.ABREVIACIONZAFIRO:
					totalItems = 0x15D;
					break;
				case Edicion.ABREVIACIONROJOFUEGO:
				case Edicion.ABREVIACIONVERDEHOJA:
					totalItems = 0x177;
					break;
			} 
			return totalItems;
		}
		//de momento solo se cargar el nombre
		public static Objeto GetObjeto(RomPokemon rom,Edicion edicion,CompilacionRom.Compilacion compilacion,int index)
		{
			Hex offsetObjeto=Zona.GetOffset(rom,Variables.Objeto.ToString(),edicion,compilacion)+index*(int)LongitudCampos.Total;
			return new Objeto(BloqueString.GetString(rom,offsetObjeto,(int)LongitudCampos.Nombre));
		}
		//de momento solo guarda el nombre
		public static void SetObjeto(RomPokemon rom,Edicion edicion,CompilacionRom.Compilacion compilacion,Objeto objeto,int index)
		{
			objeto.Nombre.Texto=objeto.Nombre.Texto.PadRight((int)LongitudCampos.Nombre);
			objeto.Nombre.OffsetInicio=Zona.GetOffset(rom,Variables.Objeto.ToString(),edicion,compilacion)+index*(int)LongitudCampos.Total;
			BloqueString.SetString(rom,objeto.Nombre);
		}
	}
}
