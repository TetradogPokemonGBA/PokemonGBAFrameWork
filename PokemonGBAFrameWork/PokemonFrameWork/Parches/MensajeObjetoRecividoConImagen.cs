/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 18/11/2017
 * Hora: 20:45
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of MensajeObjetoRecividoConImagen.
	/// </summary>
	public static class MensajeObjetoRecividoConImagen
	{
		public static readonly Creditos Creditos;
		public static readonly ASM RutinaEsmeraldaUsa;
		public static readonly Variable OffsetOverride;
		public static readonly Variable OffsetEsmeralda;
		const char ESMERALDA = '1';
		const char ROJOFUEGO = '0';
		const char MARCA = '€';
		static readonly byte[] Activado;
		static readonly byte[] Desactivado;
		static readonly byte[] Activado2EsmeraldaOnly;
		static readonly byte[] Desactivado2EsmeraldaOnly;
		static readonly int LengthRutina;
		static readonly KeyValuePair<int,Variable>[] VariablesASustituirEnLaRutina;
		public const string DESCRIPCION = "Cuando se da un objeto y sale el mensaje de objeto recivido a la derecha sale la imagen del objeto.";
		
		
		static MensajeObjetoRecividoConImagen()
		{
			const int NUMVARIABLESRUTINA = 4;
			Creditos = new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.POKEMONCOMMUNITY], "~Andrea", "Hacer la rutina y el post https://www.pokecommunity.com/showthread.php?t=393573");
			RutinaEsmeraldaUsa = ASM.Compilar(Recursos.RecursosStrings.ASMMensjaeObjetoRecividoConImagenEsmeraldaUSAYRojoFuego10USA.Replace(MARCA, ESMERALDA));
			Activado = new byte[] { 0x00, 0x48, 0x00, 0x47 };
			Desactivado = new byte[] { 0x03, 0xD1, 0x28, 0x1C, 0x03, 0x21, 0x0D, 0xF7 };
			OffsetOverride = new Variable("Es la posicion donde se pone los bytes de activado y luego el puntero+1 a la rutina");

			OffsetOverride.Add(EdicionPokemon.RojoFuegoUsa, 0x0F6F08, 0xF6F80);
			OffsetOverride.Add(EdicionPokemon.VerdeHojaUsa, 0x0F6EE0, 0xF6F58);
			OffsetOverride.Add(EdicionPokemon.RojoFuegoEsp, 0xF7284);
			OffsetOverride.Add(EdicionPokemon.VerdeHojaEsp, 0xF725C);
			OffsetOverride.Add(EdicionPokemon.EsmeraldaUsa, 0x1973E8);
			OffsetOverride.Add(EdicionPokemon.EsmeraldaEsp, 0x196FEC);

			Activado2EsmeraldaOnly = new byte[] { 0x0, 0x0 };
			Desactivado2EsmeraldaOnly = new byte[] { 0x20, 0x80 };
			OffsetEsmeralda = new Variable("Un cambio mas para la edicion esmeralda");
			OffsetEsmeralda.Add(EdicionPokemon.EsmeraldaUsa, 0x099738);
			OffsetEsmeralda.Add(EdicionPokemon.EsmeraldaEsp, 0x09974C);
			LengthRutina = RutinaEsmeraldaUsa.AsmBinary.Length;
			VariablesASustituirEnLaRutina = new KeyValuePair<int, Variable>[NUMVARIABLESRUTINA];
			VariablesASustituirEnLaRutina[0] = new KeyValuePair<int, Variable>(0xAC, new Variable("Pointer Rutina 1"));
			VariablesASustituirEnLaRutina[1] = new KeyValuePair<int, Variable>(0xBC, new Variable("Pointer Rutina 2"));
			VariablesASustituirEnLaRutina[2] = new KeyValuePair<int, Variable>(0xC0, new Variable("Pointer Rutina 3"));
			VariablesASustituirEnLaRutina[3] = new KeyValuePair<int, Variable>(0xC8, new Variable("Pointer Rutina 4"));
			
			//Esmeralda USA
			VariablesASustituirEnLaRutina[0].Value.Add(EdicionPokemon.EsmeraldaUsa, 0x003659);
			VariablesASustituirEnLaRutina[1].Value.Add(EdicionPokemon.EsmeraldaUsa, 0x271C62);
			VariablesASustituirEnLaRutina[2].Value.Add(EdicionPokemon.EsmeraldaUsa, 0x271C85);
			VariablesASustituirEnLaRutina[3].Value.Add(EdicionPokemon.EsmeraldaUsa, 0x614410);
			//Esmeralda ESP
			VariablesASustituirEnLaRutina[0].Value.Add(EdicionPokemon.EsmeraldaEsp, 0x003659);
			VariablesASustituirEnLaRutina[1].Value.Add(EdicionPokemon.EsmeraldaEsp, 0x275A66);
			VariablesASustituirEnLaRutina[2].Value.Add(EdicionPokemon.EsmeraldaEsp, 0x275A89);
			VariablesASustituirEnLaRutina[3].Value.Add(EdicionPokemon.EsmeraldaEsp, 0x617250);
			
			//Rojo Fuego USA 10
			VariablesASustituirEnLaRutina[0].Value.Add(EdicionPokemon.RojoFuegoUsa, 0x003F21, 0x3F35);
			VariablesASustituirEnLaRutina[1].Value.Add(EdicionPokemon.RojoFuegoUsa, 0x1A6816, 0x1A688E);
			VariablesASustituirEnLaRutina[2].Value.Add(EdicionPokemon.RojoFuegoUsa, 0x1A6820, 0x1A6898);
			VariablesASustituirEnLaRutina[3].Value.Add(EdicionPokemon.RojoFuegoUsa, 0x3D4294, 0x3D4304);
			
			//Verde Hoja USA 10
			VariablesASustituirEnLaRutina[0].Value.Add(EdicionPokemon.VerdeHojaUsa, 0x3F21, 0x3F35);
			VariablesASustituirEnLaRutina[1].Value.Add(EdicionPokemon.VerdeHojaUsa, 0x1A67F2, 0x1A686A);
			VariablesASustituirEnLaRutina[2].Value.Add(EdicionPokemon.VerdeHojaUsa, 0x1A67FC, 0x1A6874);
			VariablesASustituirEnLaRutina[3].Value.Add(EdicionPokemon.VerdeHojaUsa, 0x3D40D0, 0x3D4140);

			//Rojo Fuego ESP
			VariablesASustituirEnLaRutina[0].Value.Add(EdicionPokemon.RojoFuegoEsp, 0x3EBD);
			VariablesASustituirEnLaRutina[1].Value.Add(EdicionPokemon.RojoFuegoEsp, 0x1A63CF);
			VariablesASustituirEnLaRutina[2].Value.Add(EdicionPokemon.RojoFuegoEsp, 0x1A63D9);
			VariablesASustituirEnLaRutina[3].Value.Add(EdicionPokemon.RojoFuegoEsp, 0x3CF48C);
			
			//Verde Hoja ESP
			VariablesASustituirEnLaRutina[0].Value.Add(EdicionPokemon.VerdeHojaEsp, 0x3E8D);
			VariablesASustituirEnLaRutina[1].Value.Add(EdicionPokemon.VerdeHojaEsp, 0x1A63AB);
			VariablesASustituirEnLaRutina[2].Value.Add(EdicionPokemon.VerdeHojaEsp, 0x1A63B5);
			VariablesASustituirEnLaRutina[3].Value.Add(EdicionPokemon.VerdeHojaEsp, 0x3CF2C8);

			
			
		}
		public static bool Compatible(EdicionPokemon edicion, Compilacion compilacion)
		{
			bool compatible = OffsetOverride.Diccionario.ContainsKey(compilacion);
			if (compatible)
				compatible = OffsetOverride.Diccionario[compilacion].ContainsKey(edicion);
			return compatible;
		}
		public static bool EstaActivado(RomData rom)
		{
			return EstaActivado(rom.Rom, rom.Edicion, rom.Compilacion);
		}

		public static bool EstaActivado(RomGba romGBA, EdicionPokemon edicion, Compilacion compilacion)
		{
			int offsetInicio = Variable.GetVariable(OffsetOverride, edicion, compilacion);
			return romGBA.Data.SearchArray(offsetInicio, offsetInicio + Desactivado.Length, Desactivado) < 0;
		}
		public static void Activar(RomData rom)
		{
			Activar(rom.Rom, rom.Edicion, rom.Compilacion);
		}

		public static void Activar(RomGba romGBA, EdicionPokemon edicion, Compilacion compilacion)
		{
			int offsetOverride = Variable.GetVariable(OffsetOverride, edicion, compilacion);
			int offsetEsmeralda;
			int rutinaCompilada;
			byte[] rutina = null;
			if (!EstaActivado(romGBA, edicion, compilacion)) {
				romGBA.Data.SetArray(offsetOverride, Activado);
				offsetOverride += Activado.Length;
				rutina = RutinaEsmeraldaUsa.AsmBinary;
				
				if (edicion.AbreviacionRom == AbreviacionCanon.BPE) {
					
					offsetEsmeralda = Variable.GetVariable(OffsetEsmeralda, edicion, compilacion);
					romGBA.Data.SetArray(offsetEsmeralda, Activado2EsmeraldaOnly);
					
				}
				
				//pongo todos los pointers de la edicion,compilacion e idioma en su posición
				for (int i = 0; i < VariablesASustituirEnLaRutina.Length; i++)
					rutina.SetArray(VariablesASustituirEnLaRutina[i].Key, new OffsetRom(Variable.GetVariable(VariablesASustituirEnLaRutina[i].Value, edicion, compilacion)).BytesPointer);
				

				rutinaCompilada = romGBA.Data.SearchEmptySpaceAndSetArray(rutina);
				romGBA.Data.SetArray(offsetOverride, new OffsetRom(rutinaCompilada + 1).BytesPointer);
			}
		}
		public static void Desactivar(RomData rom)
		{
			Desactivar(rom.Rom, rom.Edicion, rom.Compilacion);
		}

		public static void Desactivar(RomGba romGBA, EdicionPokemon edicion, Compilacion compilacion)
		{
			int offsetOverride;
			OffsetRom offsetRutina;
			if (EstaActivado(romGBA, edicion, compilacion)) {
				offsetOverride = Variable.GetVariable(OffsetOverride, edicion, compilacion);
				offsetRutina = new OffsetRom(romGBA, offsetOverride + Activado.Length);
				romGBA.Data.Remove(offsetRutina.Offset - 1, LengthRutina);
				romGBA.Data.SetArray(offsetOverride, Desactivado);
				if (edicion.AbreviacionRom == AbreviacionCanon.BPE)
					romGBA.Data.SetArray( Variable.GetVariable(OffsetEsmeralda, edicion, compilacion), Desactivado2EsmeraldaOnly);
			}
		}
	}
}
