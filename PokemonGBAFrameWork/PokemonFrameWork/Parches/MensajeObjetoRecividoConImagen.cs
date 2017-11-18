/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 18/11/2017
 * Hora: 20:45
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of MensajeObjetoRecividoConImagen.
	/// </summary>
	public static class MensajeObjetoRecividoConImagen
	{
        public static readonly Creditos Creditos;
        public static readonly ASM RutinaEsmeraldaUsa;
        public static readonly ASM RutinaRojoFuegoUsa10;
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
        static MensajeObjetoRecividoConImagen()
		{
            Creditos = new Creditos();
            Creditos.Add(Creditos.Comunidades[Creditos.POKEMONCOMMUNITY], "~Andrea", "Hacer la rutina y el post https://www.pokecommunity.com/showthread.php?t=393573");
            RutinaEsmeraldaUsa = ASM.Compilar(Resources.MensajeObjetoRecivido_EsmeraldaUSA_RojoFuegoUSA10.Replace(MARCA, ESMERALDA));
            RutinaRojoFuegoUsa10 = ASM.Compilar(Resources.MensajeObjetoRecivido_EsmeraldaUSA_RojoFuegoUSA10.Replace(MARCA, ROJOFUEGO));
            Activado = new byte[] { 0x00,0x48,0x00,0x47 };
            Desactivado = new byte[] { 0x03, 0xD1, 0x28, 0x1C, 0x03, 0x21, 0x0D, 0xF7 };
            OffsetOverride = new Variable("Es la posicion donde se pone los bytes de activado y luego el puntero+1 a la rutina");

            OffsetOverride.Add(EdicionPokemon.RojoFuegoUsa, 0x0F6F08,0xF6F80);
            OffsetOverride.Add(EdicionPokemon.VerdeHojaUsa, 0x0F6EE0, 0xF6F58);
            OffsetOverride.Add(EdicionPokemon.RojoFuegoEsp,0xF7284);
            OffsetOverride.Add(EdicionPokemon.VerdeHojaEsp,0xF725C);
            OffsetOverride.Add(EdicionPokemon.EsmeraldaUsa, 0x1973E8);
            OffsetOverride.Add(EdicionPokemon.EsmeraldaEsp, 0x196FEC);

            Activado2EsmeraldaOnly = new byte[] { 0x0, 0x0 };
            Desactivado2EsmeraldaOnly = new byte[] { 0x20 ,0x80 };
            OffsetEsmeralda = new Variable("Un cambio mas para la edicion esmeralda");
            OffsetEsmeralda.Add(EdicionPokemon.EsmeraldaUsa, 0x099738);
            OffsetEsmeralda.Add(EdicionPokemon.EsmeraldaEsp, 0x09974C);
            LengthRutina = RutinaEsmeraldaUsa.AsmBinary.Length;
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
            return romGBA.Data.SearchArray(offsetInicio,offsetInicio+Activado.Length,Activado)>0;
        }
        public static void Activar(RomData rom)
        {
            Activar(rom.Rom, rom.Edicion, rom.Compilacion);
        }

        public static void Activar(RomGba romGBA, EdicionPokemon edicion, Compilacion compilacion)
        {
            int offsetInicio = Variable.GetVariable(OffsetOverride, edicion, compilacion);
            int offsetEsmeralda;
            int rutinaCompilada;
            byte[] rutina=null;
            if (!EstaActivado(romGBA, edicion, compilacion))
            {
                romGBA.Data.SetArray(Activado, offsetInicio);
                offsetInicio += Activado.Length;
                if (edicion.AbreviacionRom == AbreviacionCanon.BPE)
                {
                    rutina = RutinaEsmeraldaUsa.AsmBinary;
                    offsetEsmeralda= Variable.GetVariable(OffsetEsmeralda, edicion, compilacion);
                    romGBA.Data.SetArray(Activado2EsmeraldaOnly, offsetEsmeralda);
                    if (edicion.Idioma == Idioma.Español)
                    {
                        //pongo los valores que toquen
                    }
                }
                else {
                    rutina=RutinaRojoFuegoUsa10.AsmBinary;
                    //cambio offsets ediciones,compilaciones e idiomas
                    if (edicion.Idioma == Idioma.Español)
                    {
                        //pongo los valores que toquen
                        if (edicion.AbreviacionRom == AbreviacionCanon.BPG)
                        {
                       
                        }
                        else 
                        {

                        }
                    }
                    else {
                        if (edicion.AbreviacionRom == AbreviacionCanon.BPG)
                        {
                            if (compilacion == Compilacion.Compilaciones[0])
                            {

                            }
                            else
                            {

                            }
                        }
                        else if (compilacion == Compilacion.Compilaciones[1])
                        {

                        }
                    }
                }

                rutinaCompilada = romGBA.Data.SetArray(rutina);
                romGBA.Data.SetArray(offsetInicio, new OffsetRom(rutinaCompilada + 1).BytesPointer);
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
            if (EstaActivado(romGBA, edicion, compilacion))
            {
                offsetOverride = Variable.GetVariable(OffsetOverride, edicion, compilacion);
                offsetRutina = new OffsetRom(romGBA, offsetOverride + Activado.Length);
                romGBA.Data.Remove(offsetRutina.Offset - 1, LengthRutina);
                romGBA.Data.SetArray(Desactivado, offsetOverride);
                if (edicion.AbreviacionRom == AbreviacionCanon.BPE)
                    romGBA.Data.SetArray(Desactivado2EsmeraldaOnly, Variable.GetVariable(OffsetEsmeralda, edicion, compilacion));
            }
        }
    }
}
