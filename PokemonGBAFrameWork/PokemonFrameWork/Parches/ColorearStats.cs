/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 03/10/2017
 * Hora: 4:57
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of ColorearStats.
	/// </summary>
	public static class ColorearStats
	{
        public static readonly Creditos Creditos;

        public static Color StatsNormal=Color.Black;
        public static Color StatsHigher = Color.Blue;
        public static Color StatsLower = Color.Red;
        #region RutinaKanto
        public static readonly Variable Offset1Kanto;
        public static readonly Variable Offset2Kanto;
        public static readonly Variable Offset3Kanto;
        public static readonly Variable Offset4Kanto;
        public static readonly Variable Offset5Kanto;
        public static readonly Variable Offset6Kanto;
        public static readonly Variable Offset7Kanto;
        public static readonly Variable Offset8Kanto;
        static readonly Llista<KeyValuePair<string, Variable>> VarsKanto;
        #endregion
        #region Esmeralda
        public static readonly Variable Offset1Esmeralda;
        public static readonly Variable Offset2Esmeralda;
        public static readonly Variable OffsetNatureEsmeralda;
        public static readonly Variable OffsetRightStatsEsmeralda;
        public static readonly Variable OffsetLeftStatsEsmeralda;
        public static readonly Variable OffsetSpecialF7Esmeralda;
        public static readonly Variable DisplayedStringEsmeralda;
        static Llista<KeyValuePair<string, Variable>> VarsEsmeralda;
        #endregion

        static ColorearStats()
        {

            Creditos = new Creditos();
            Creditos.Add(Creditos.Comunidades[Creditos.POKEMONCOMMUNITY], "Spherical Ice", "Rutina post: https://www.pokecommunity.com/showpost.php?p=9043890&postcount=816");

            #region Kanto
            VarsKanto = new Llista<KeyValuePair<string, Variable>>();


            Offset1Kanto = new Variable("ColorearStats Offset1");
            Offset2Kanto = new Variable("ColorearStats Offset2");
            Offset3Kanto = new Variable("ColorearStats Offset3");
            Offset4Kanto = new Variable("ColorearStats Offset4");
            Offset5Kanto = new Variable("ColorearStats Offset5");
            Offset6Kanto = new Variable("ColorearStats Offset6");
            Offset7Kanto = new Variable("ColorearStats Offset7");
            Offset8Kanto = new Variable("ColorearStats Offset8");

            VarsKanto.Add(new KeyValuePair<string, Variable>("OFFSET1", Offset1Kanto));
            VarsKanto.Add(new KeyValuePair<string, Variable>("OFFSET2", Offset2Kanto));
            VarsKanto.Add(new KeyValuePair<string, Variable>("OFFSET3", Offset3Kanto));
            VarsKanto.Add(new KeyValuePair<string, Variable>("OFFSET4", Offset4Kanto));
            VarsKanto.Add(new KeyValuePair<string, Variable>("OFFSET5", Offset5Kanto));
            VarsKanto.Add(new KeyValuePair<string, Variable>("OFFSET6", Offset6Kanto));
            VarsKanto.Add(new KeyValuePair<string, Variable>("OFFSET7", Offset7Kanto));
            VarsKanto.Add(new KeyValuePair<string, Variable>("OFFSET8", Offset8Kanto));

            Offset1Kanto.Add(EdicionPokemon.RojoFuegoUsa, 0x137162,0x1371DA);
            Offset1Kanto.Add(EdicionPokemon.VerdeHojaUsa, 0x13713A, 0x1371B2);
            Offset1Kanto.Add(EdicionPokemon.VerdeHojaEsp, 0x137302);
            Offset1Kanto.Add(EdicionPokemon.RojoFuegoEsp, 0x13732A);

            Offset2Kanto.Add(EdicionPokemon.RojoFuegoUsa, 0x03FBE8, 0x03FBFC);
            Offset2Kanto.Add(EdicionPokemon.VerdeHojaUsa, 0x03FBE8, 0x03FBFC);
            Offset2Kanto.Add(EdicionPokemon.VerdeHojaEsp, 0x03FAD4);
            Offset2Kanto.Add(EdicionPokemon.RojoFuegoEsp, 0x03FAD4);

            Offset3Kanto.Add(EdicionPokemon.RojoFuegoUsa, 0x042EB4, 0x042EC8);
            Offset3Kanto.Add(EdicionPokemon.VerdeHojaUsa, 0x042EB4, 0x042EC8);
            Offset3Kanto.Add(EdicionPokemon.VerdeHojaEsp, 0x042DA0);
            Offset3Kanto.Add(EdicionPokemon.RojoFuegoEsp, 0x042DA0);

            Offset4Kanto.Add(EdicionPokemon.RojoFuegoUsa, 0x1371FA, 0x137272);
            Offset4Kanto.Add(EdicionPokemon.VerdeHojaUsa, 0x1371D2, 0x13724A);
            Offset4Kanto.Add(EdicionPokemon.VerdeHojaEsp, 0x13739A);
            Offset4Kanto.Add(EdicionPokemon.RojoFuegoEsp, 0x1373C2);

            Offset5Kanto.Add(EdicionPokemon.RojoFuegoUsa, 0x137188, 0x137200);
            Offset5Kanto.Add(EdicionPokemon.VerdeHojaUsa, 0x137160, 0x1371D8);
            Offset5Kanto.Add(EdicionPokemon.VerdeHojaEsp, 0x137328);
            Offset5Kanto.Add(EdicionPokemon.RojoFuegoEsp, 0x137350);

            Offset6Kanto.Add(EdicionPokemon.RojoFuegoUsa, 0x1371AE, 0x137227);
            Offset6Kanto.Add(EdicionPokemon.VerdeHojaUsa, 0x137187, 0x1371FF);
            Offset6Kanto.Add(EdicionPokemon.VerdeHojaEsp, 0x13734F);
            Offset6Kanto.Add(EdicionPokemon.RojoFuegoEsp, 0x137377);

            Offset7Kanto.Add(EdicionPokemon.RojoFuegoUsa, 0x13713C, 0x1371B5);
            Offset7Kanto.Add(EdicionPokemon.VerdeHojaUsa, 0x137115, 0x13718D);
            Offset7Kanto.Add(EdicionPokemon.VerdeHojaEsp, 0x1372DD);
            Offset7Kanto.Add(EdicionPokemon.RojoFuegoEsp, 0x137305);

            Offset8Kanto.Add(EdicionPokemon.RojoFuegoUsa, 0x1371D4, 0x13724C);
            Offset8Kanto.Add(EdicionPokemon.VerdeHojaUsa, 0x1371AC, 0x137224);
            Offset8Kanto.Add(EdicionPokemon.VerdeHojaEsp, 0x137374);
            Offset8Kanto.Add(EdicionPokemon.RojoFuegoEsp, 0x13739C);
            #endregion
            #region Esmeralda falta acabar...
            VarsEsmeralda = new Llista<KeyValuePair<string, Variable>>();

            Offset1Esmeralda = new Variable("ColorearStats Offset1 Esmeralda");
            Offset2Esmeralda = new Variable("ColorearStats Offset2 Esmeralda");

            OffsetNatureEsmeralda = new Variable("ColorearStats OffsetNature Esmeralda");
            OffsetRightStatsEsmeralda = new Variable("ColorearStats OffsetRightStats Esmeralda");
            OffsetLeftStatsEsmeralda = new Variable("ColorearStats OffsetLeftStats Esmeralda");
            OffsetSpecialF7Esmeralda = new Variable("ColorearStats OffsetSpecialF7 Esmeralda");
            DisplayedStringEsmeralda = new Variable("ColorearStats DisplayedString Esmeralda");

            VarsEsmeralda.Add(new KeyValuePair<string, Variable>("OFFSET1", Offset1Esmeralda));
            VarsEsmeralda.Add(new KeyValuePair<string, Variable>("OFFSET2", Offset2Esmeralda));

            VarsEsmeralda.Add(new KeyValuePair<string, Variable>("OFFSETNATURE", OffsetNatureEsmeralda));
            VarsEsmeralda.Add(new KeyValuePair<string, Variable>("OFFSETRIGHTSTATS", OffsetRightStatsEsmeralda));
            VarsEsmeralda.Add(new KeyValuePair<string, Variable>("OFFSETSPECIALF7", OffsetSpecialF7Esmeralda));
            VarsEsmeralda.Add(new KeyValuePair<string, Variable>("OFFSETLEFTSTATS", OffsetLeftStatsEsmeralda));

            VarsEsmeralda.Add(new KeyValuePair<string, Variable>("DISPLAYEDSTRING", DisplayedStringEsmeralda));

            Offset1Esmeralda.Add(EdicionPokemon.EsmeraldaUsa,0x1C37A8);
            Offset1Esmeralda.Add(EdicionPokemon.EsmeraldaEsp,0x133C8);

            Offset2Esmeralda.Add(EdicionPokemon.EsmeraldaUsa,0x1C386C);
            Offset2Esmeralda.Add(EdicionPokemon.EsmeraldaEsp,0x1C348C);

            OffsetNatureEsmeralda.Add(EdicionPokemon.EsmeraldaUsa, 0x31E818);
            OffsetNatureEsmeralda.Add(EdicionPokemon.EsmeraldaEsp, 0x324AD4);

            OffsetRightStatsEsmeralda.Add(EdicionPokemon.EsmeraldaUsa, 0x61CE8E);
            OffsetRightStatsEsmeralda.Add(EdicionPokemon.EsmeraldaEsp, 0x61F7AA);

            OffsetSpecialF7Esmeralda.Add(EdicionPokemon.EsmeraldaUsa, 0x1AFC29);
            OffsetSpecialF7Esmeralda.Add(EdicionPokemon.EsmeraldaEsp, 0x1AF849);

            OffsetLeftStatsEsmeralda.Add(EdicionPokemon.EsmeraldaUsa, 0x61CE82);
            OffsetLeftStatsEsmeralda.Add(EdicionPokemon.EsmeraldaEsp, 0x61F79E);

            DisplayedStringEsmeralda.Add(EdicionPokemon.EsmeraldaUsa, 0x02021FC4);//es una variable falta saber como se mira para la version esp

            #endregion

        }
        #region Tratamiento Colores
        public static byte[] ColorToGBA(Color color)
        {
            return new byte[] { color.B, color.G, color.R };
        }
        public static Color GBAToColor(byte[] gbaBytes)
        {
            const int R = 2, G = 1, B = 0;
            return Color.FromArgb(255, gbaBytes[R], gbaBytes[G], gbaBytes[B]);
        }
        #endregion

        static string SetOffsetsRutina(RomGba rom,string rutina,EdicionPokemon edicion,Compilacion compilacion)
        {
            const int R = 2, G = 1, B = 0;
            string[] COLORES = { "BLUE", "RED", "BLACK" };
            string[] PARTESCOLOR = { "B", "G", "R" };
            byte[] color;
            StringBuilder strRutina;
            strRutina = new StringBuilder(rutina);
            if (edicion.RegionKanto)
            {
                for (int i = 0; i < VarsKanto.Count; i++)
                    strRutina.Replace(VarsKanto[i].Key, new OffsetRom(Variable.GetVariable(VarsKanto[i].Value, edicion, compilacion)).ToString());
                //parte colores BBBBBB,RRRRRR,NNNNNN
                strRutina.Replace("BBBBBB", (Hex)ColorToGBA(StatsHigher));//se tiene que poner el pointer que apunta a los datos del color!!! por corregir!!! falta hacer que carge los colores de una rom
                strRutina.Replace("RRRRRR", (Hex)ColorToGBA(StatsLower));
                strRutina.Replace("NNNNNN", (Hex)ColorToGBA(StatsNormal));
            }
            else if(edicion.AbreviacionRom==AbreviacionCanon.BPE)
            {
                for (int i = 0; i < VarsEsmeralda.Count; i++)
                    strRutina.Replace(VarsEsmeralda[i].Key, new OffsetRom(Variable.GetVariable(VarsEsmeralda[i].Value, edicion, compilacion)).ToString());

                //parte colores BLUE,RED,BLACK B,G,R
                color = ColorToGBA(StatsNormal);
                for (int i = 0; i < color.Length; i++)
                    strRutina.Replace(COLORES[2] + PARTESCOLOR[i], (Hex)color[i]);
                color = ColorToGBA(StatsLower);
                for (int i = 0; i < color.Length; i++)
                    strRutina.Replace(COLORES[1] + PARTESCOLOR[i], (Hex)color[i]);
                color = ColorToGBA(StatsHigher);
                for (int i = 0; i < color.Length; i++)
                    strRutina.Replace(COLORES[0] + PARTESCOLOR[i], (Hex)color[i]);
            }
            else
            {
                //Rubi y Zafiro
            }
          

            return strRutina.ToString();
        }

        public static bool EstaActivado(RomData rom)
        {
            return EstaActivado(rom.Rom, rom.Edicion, rom.Compilacion);
        }
        public static bool Compatible(EdicionPokemon edicion, Compilacion compilacion)
        {
            bool compatible= edicion.RegionKanto||(edicion.AbreviacionRom==AbreviacionCanon.BPE&&edicion.Idioma==Idioma.Ingles);

            return compatible;
        }
        public static bool EstaActivado(RomGba romGBA, EdicionPokemon edicion, Compilacion compilacion)
        {
            byte[] rutinaABuscar=null;
            if(edicion.RegionKanto)
            {
                rutinaABuscar = ASM.Compilar(SetOffsetsRutina(romGBA,Properties.Resources.ColorearStats_Ataque_FR, edicion, compilacion)).AsmBinary;
            }
            else if(edicion.AbreviacionRom==AbreviacionCanon.BPE)
            {
                rutinaABuscar = ASM.Compilar(SetOffsetsRutina(romGBA, Properties.Resources.ColorearStats_RightStats_Esmeralda, edicion, compilacion)).AsmBinary;
            }
            else
            {
                //Rubi y Zafiro
            }

            return romGBA.Data.SearchArray(rutinaABuscar) > 0;
        }
        public static void Activar(RomData rom)
        {
            Activar(rom.Rom, rom.Edicion, rom.Compilacion);
        }

        public static void Activar(RomGba romGBA, EdicionPokemon edicion, Compilacion compilacion)
        {
            if (!EstaActivado(romGBA, edicion, compilacion))
            {
                if (edicion.RegionKanto)
                {

                }
                else if (edicion.AbreviacionRom == AbreviacionCanon.BPE)
                {

                }
                else
                {
                    //Rubi y Zafiro
                }
            }
        }
        public static void Desactivar(RomData rom)
        {
            Desactivar(rom.Rom, rom.Edicion, rom.Compilacion);
        }

        public static void Desactivar(RomGba romGBA, EdicionPokemon edicion, Compilacion compilacion)
        {
            if (EstaActivado(romGBA, edicion, compilacion))
            {
                if (edicion.RegionKanto)
                {

                }
                else if (edicion.AbreviacionRom == AbreviacionCanon.BPE)
                {

                }
                else
                {
                    //Rubi y Zafiro
                }
            }
        }
    }
}
