/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 03/10/2017
 * Hora: 4:57
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using Gabriel.Cat.S.Extension;
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

        public static Color StatsNormal = Paleta.ToGBAColor(Color.Black);
        public static Color StatsHigher = Paleta.ToGBAColor(Color.Blue);
        public static Color StatsLower = Paleta.ToGBAColor(Color.Red);
        #region RutinaKanto
        public static readonly Variable Offset1Kanto;
        public static readonly Variable Offset2Kanto;
        public static readonly Variable Offset3Kanto;
        public static readonly Variable Offset4Kanto;
        public static readonly Variable Offset5Kanto;
        public static readonly Variable Offset6Kanto;
        public static readonly Variable Offset7Kanto;
        public static readonly Variable Offset8Kanto;

        public static readonly Variable OffsetHeaderAtaqueKanto;
        public static readonly Variable OffsetHeaderDefensaKanto;
        public static readonly Variable OffsetHeaderAtaqueEspecialKanto;
        public static readonly Variable OffsetHeaderDefensaEspecialKanto;
        public static readonly Variable OffsetHeaderVelocidadKanto;
        public static readonly Variable OffsetHeaderRevertirColorANegroKanto;

        public static readonly byte[] HeaderKantoAtaque = { 0x0A, 0x79, 0x32, 0x32, 0x12, 0x06, 0x12, 0x0E };
        public static readonly byte[] HeaderKantoDefensa = { 0x11, 0x68, 0x8A, 0x79, 0x32, 0x32, 0x12, 0x06 };
        public static readonly byte[] HeaderKantoAtaqueEspecial = { 0x42, 0x46, 0x11, 0x68, 0x0A, 0x7A, 0x32, 0x32 };
        public static readonly byte[] HeaderKantoDefensaEspecial = { 0x11, 0x68, 0x8A, 0x7A, 0x32, 0x32, 0x12, 0x06 };
        public static readonly byte[] HeaderKantoVelocidad = { 0x42, 0x46, 0x11, 0x68, 0x0A, 0x7B, 0x32, 0x32 };
        public static readonly byte[] HeaderKantoRevertirColorANegro = { 0x11, 0x68, 0x8A, 0x7B, 0x0F, 0x32, 0x12, 0x06 };

        static readonly byte[] HeaderKantoPart1 = { 0x0, 0x49, 0x08, 0x47 };
        static readonly Llista<KeyValuePair<string, Variable>> VarsKanto;
        #endregion
        #region RutinaEsmeralda
        public static readonly Variable Offset1Esmeralda;
        public static readonly Variable Offset2Esmeralda;
        public static readonly Variable OffsetNatureEsmeralda;
        public static readonly Variable OffsetRightStatsEsmeralda;
        public static readonly Variable OffsetLeftStatsEsmeralda;
        public static readonly Variable OffsetSpecialF7Esmeralda;
        public static readonly Variable DisplayedStringEsmeralda;

        public static readonly Variable OffsetHeaderLeftEsmeralda;
        public static readonly Variable OffsetHeaderRightEsmeralda;
        public static readonly byte[] HeaderLeftEsmeralda = { 0x0C, 0x48, 0x0C, 0x49, 0xEC, 0xF7, 0x41, 0xFA, 0x48, 0x46 };
        public static readonly byte[] HeaderRightEsmeralda = { 0x08, 0x48, 0x09, 0x49, 0xEC, 0xF7, 0xDE, 0xF9 };
        static readonly byte[] HeaderLeftPart1 = { 0x00, 0x00, 0x00, 0x4A, 0x10, 0x47 };
        static readonly byte[] HeaderRightPart1 = { 0x00, 0x4A, 0x10, 0x47 };
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

            OffsetHeaderAtaqueKanto = new Variable("ColorearStats Offset Header Ataque Kanto");
            OffsetHeaderDefensaKanto = new Variable("ColorearStats Offset Header Defensa Kanto");
            OffsetHeaderAtaqueEspecialKanto = new Variable("ColorearStats Offset Header AtaqueEspecial Kanto");
            OffsetHeaderDefensaEspecialKanto = new Variable("ColorearStats Offset Header DefensaEspecial Kanto");
            OffsetHeaderVelocidadKanto = new Variable("ColorearStats Offset Header Velocidad Kanto");
            OffsetHeaderRevertirColorANegroKanto = new Variable("ColorearStats Offset Header RevertirColorANegro Kanto");

            VarsKanto.Add(new KeyValuePair<string, Variable>("OFFSET1", Offset1Kanto));
            VarsKanto.Add(new KeyValuePair<string, Variable>("OFFSET2", Offset2Kanto));
            VarsKanto.Add(new KeyValuePair<string, Variable>("OFFSET3", Offset3Kanto));
            VarsKanto.Add(new KeyValuePair<string, Variable>("OFFSET4", Offset4Kanto));
            VarsKanto.Add(new KeyValuePair<string, Variable>("OFFSET5", Offset5Kanto));
            VarsKanto.Add(new KeyValuePair<string, Variable>("OFFSET6", Offset6Kanto));
            VarsKanto.Add(new KeyValuePair<string, Variable>("OFFSET7", Offset7Kanto));
            VarsKanto.Add(new KeyValuePair<string, Variable>("OFFSET8", Offset8Kanto));

            Offset1Kanto.Add(EdicionPokemon.RojoFuegoUsa, 0x137162, 0x1371DA);
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


            OffsetHeaderAtaqueKanto.Add(EdicionPokemon.RojoFuegoUsa, 0x137134, 0x1371AC);
            OffsetHeaderAtaqueKanto.Add(EdicionPokemon.VerdeHojaUsa, 0x137130, 0x1371A8);
            OffsetHeaderAtaqueKanto.Add(EdicionPokemon.VerdeHojaEsp, 0x1372D4);
            OffsetHeaderAtaqueKanto.Add(EdicionPokemon.RojoFuegoEsp, 0x1372FC);

            OffsetHeaderDefensaKanto.Add(EdicionPokemon.RojoFuegoUsa, 0x137158, 0x1371D0);
            OffsetHeaderDefensaKanto.Add(EdicionPokemon.VerdeHojaUsa, 0x13710C, 0x137184);
            OffsetHeaderDefensaKanto.Add(EdicionPokemon.VerdeHojaEsp, 0x1372F8);
            OffsetHeaderDefensaKanto.Add(EdicionPokemon.RojoFuegoEsp, 0x137320);

            OffsetHeaderAtaqueEspecialKanto.Add(EdicionPokemon.RojoFuegoUsa, 0x13717C, 0x1371F4);
            OffsetHeaderAtaqueEspecialKanto.Add(EdicionPokemon.VerdeHojaUsa, 0x137154, 0x1371CC);
            OffsetHeaderAtaqueEspecialKanto.Add(EdicionPokemon.VerdeHojaEsp, 0x13731C);
            OffsetHeaderAtaqueEspecialKanto.Add(EdicionPokemon.RojoFuegoEsp, 0x137344);

            OffsetHeaderDefensaEspecialKanto.Add(EdicionPokemon.RojoFuegoUsa, 0x1371A4, 0x13721C);
            OffsetHeaderDefensaEspecialKanto.Add(EdicionPokemon.VerdeHojaUsa, 0x13717C, 0x1371F4);
            OffsetHeaderDefensaEspecialKanto.Add(EdicionPokemon.VerdeHojaEsp, 0x137344);
            OffsetHeaderDefensaEspecialKanto.Add(EdicionPokemon.RojoFuegoEsp, 0x13736C);

            OffsetHeaderVelocidadKanto.Add(EdicionPokemon.RojoFuegoUsa, 0x1371C8, 0x137240);
            OffsetHeaderVelocidadKanto.Add(EdicionPokemon.VerdeHojaUsa, 0x1371A0, 0x137218);
            OffsetHeaderVelocidadKanto.Add(EdicionPokemon.VerdeHojaEsp, 0x137368);
            OffsetHeaderVelocidadKanto.Add(EdicionPokemon.RojoFuegoEsp, 0x137390);

            OffsetHeaderRevertirColorANegroKanto.Add(EdicionPokemon.RojoFuegoUsa, 0x1371F0, 0x137268);
            OffsetHeaderRevertirColorANegroKanto.Add(EdicionPokemon.VerdeHojaUsa, 0x1371C8, 0x137240);
            OffsetHeaderRevertirColorANegroKanto.Add(EdicionPokemon.VerdeHojaEsp, 0x137390);
            OffsetHeaderRevertirColorANegroKanto.Add(EdicionPokemon.RojoFuegoEsp, 0x1373B8);
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

            OffsetHeaderLeftEsmeralda = new Variable("ColorearStats HeaderLeft Esmeralda");
            OffsetHeaderRightEsmeralda = new Variable("ColorearStats HeaderRight Esmeralda");

            VarsEsmeralda.Add(new KeyValuePair<string, Variable>("OFFSET1", Offset1Esmeralda));
            VarsEsmeralda.Add(new KeyValuePair<string, Variable>("OFFSET2", Offset2Esmeralda));

            VarsEsmeralda.Add(new KeyValuePair<string, Variable>("OFFSETNATURE", OffsetNatureEsmeralda));
            VarsEsmeralda.Add(new KeyValuePair<string, Variable>("OFFSETRIGHTSTATS", OffsetRightStatsEsmeralda));
            VarsEsmeralda.Add(new KeyValuePair<string, Variable>("OFFSETSPECIALF7", OffsetSpecialF7Esmeralda));
            VarsEsmeralda.Add(new KeyValuePair<string, Variable>("OFFSETLEFTSTATS", OffsetLeftStatsEsmeralda));

            VarsEsmeralda.Add(new KeyValuePair<string, Variable>("DISPLAYEDSTRING", DisplayedStringEsmeralda));

            Offset1Esmeralda.Add(EdicionPokemon.EsmeraldaUsa, 0x1C37A8);
            Offset1Esmeralda.Add(EdicionPokemon.EsmeraldaEsp, 0x133C8);

            Offset2Esmeralda.Add(EdicionPokemon.EsmeraldaUsa, 0x1C386C);
            Offset2Esmeralda.Add(EdicionPokemon.EsmeraldaEsp, 0x1C348C);

            OffsetNatureEsmeralda.Add(EdicionPokemon.EsmeraldaUsa, 0x31E818);
            OffsetNatureEsmeralda.Add(EdicionPokemon.EsmeraldaEsp, 0x324AD4);

            OffsetRightStatsEsmeralda.Add(EdicionPokemon.EsmeraldaUsa, 0x61CE8E);
            OffsetRightStatsEsmeralda.Add(EdicionPokemon.EsmeraldaEsp, 0x61F7AA);

            OffsetSpecialF7Esmeralda.Add(EdicionPokemon.EsmeraldaUsa, 0x1AFC29);
            OffsetSpecialF7Esmeralda.Add(EdicionPokemon.EsmeraldaEsp, 0x1AF849);

            OffsetLeftStatsEsmeralda.Add(EdicionPokemon.EsmeraldaUsa, 0x61CE82);
            OffsetLeftStatsEsmeralda.Add(EdicionPokemon.EsmeraldaEsp, 0x61F79E);

            DisplayedStringEsmeralda.Add(EdicionPokemon.EsmeraldaUsa, 0x02021FC4);//es una variable falta saber como se mira para la version esp
            //falta version en español                                   2020F14C

            OffsetHeaderLeftEsmeralda.Add(EdicionPokemon.EsmeraldaUsa, 0x1C379E);
            OffsetHeaderLeftEsmeralda.Add(EdicionPokemon.EsmeraldaEsp, 0x1C33BE);

            OffsetHeaderRightEsmeralda.Add(EdicionPokemon.EsmeraldaUsa, 0x1C3864);
            OffsetHeaderRightEsmeralda.Add(EdicionPokemon.EsmeraldaEsp, 0x1C3484);

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

        static string SetOffsetsRutina(RomGba rom, string rutina, EdicionPokemon edicion, Compilacion compilacion)
        {
            const int R = 2, G = 1, B = 0;
            string[] COLORES = { "BLUE", "RED", "BLACK" };
            string[] PARTESCOLOR = { "B", "G", "R" };
            char[] varDisplayString;
            byte[] color;
            StringBuilder strRutina;
            strRutina = new StringBuilder(rutina);
            if (edicion.RegionKanto)
            {
                for (int i = 0; i < VarsKanto.Count; i++)
                    strRutina.Replace(VarsKanto[i].Key, new OffsetRom(Variable.GetVariable(VarsKanto[i].Value, edicion, compilacion)).ToString());
                //parte colores BBBBBB,RRRRRR,NNNNNN se ponen los pointers a la información de los colores BGR
                strRutina.Replace("BBBBBB", new OffsetRom(rom.Data.SetArrayIfNotExist(ColorToGBA(StatsHigher))).ToString());
                strRutina.Replace("RRRRRR", new OffsetRom(rom.Data.SetArrayIfNotExist(ColorToGBA(StatsLower))).ToString());
                strRutina.Replace("NNNNNN", new OffsetRom(rom.Data.SetArrayIfNotExist(ColorToGBA(StatsNormal))).ToString());
            }
            else if (edicion.AbreviacionRom == AbreviacionCanon.BPE)
            {
                for (int i = 0,f= VarsEsmeralda.Count - 1; i < f; i++)
                    strRutina.Replace(VarsEsmeralda[i].Key, new OffsetRom(Variable.GetVariable(VarsEsmeralda[i].Value, edicion, compilacion)).ToString());

                varDisplayString = ((Hex)Variable.GetVariable(VarsEsmeralda[VarsEsmeralda.Count - 1].Value, edicion, compilacion)).ToString().PadLeft(8,'0').ToCharArray();
                strRutina.Replace(VarsEsmeralda[VarsEsmeralda.Count-1].Key,new string( varDisplayString));

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
            bool compatible = edicion.RegionKanto || (edicion.AbreviacionRom == AbreviacionCanon.BPE && edicion.Idioma == Idioma.Ingles);

            return compatible;
        }
        public static bool EstaActivado(RomGba romGBA, EdicionPokemon edicion, Compilacion compilacion)
        {
            byte[] rutinaABuscar = null;
            if (edicion.RegionKanto)
            {
                rutinaABuscar = ASM.Compilar(SetOffsetsRutina(romGBA, Properties.Resources.ColorearStats_Ataque_FR, edicion, compilacion)).AsmBinary;
            }
            else if (edicion.AbreviacionRom == AbreviacionCanon.BPE)
            {
                rutinaABuscar = ASM.Compilar(SetOffsetsRutina(romGBA, Properties.Resources.ColorearStats_RightStats_Esmeralda, edicion, compilacion)).AsmBinary;
            }
            else
            {
                //Rubi y Zafiro
            }

            return romGBA.Data.SearchArray(rutinaABuscar.SubArray(20)) > 0;
        }
        public static void Activar(RomData rom)
        {
            Activar(rom.Rom, rom.Edicion, rom.Compilacion);
        }

        public static void Activar(RomGba romGBA, EdicionPokemon edicion, Compilacion compilacion)
        {
            int offsetHeader;
            List<KeyValuePair<string, Variable>> varsKanto;
            if (!EstaActivado(romGBA, edicion, compilacion))
            {
                if (edicion.RegionKanto)
                {

                    varsKanto = new List<KeyValuePair<string, Variable>>();
                    varsKanto.Add(new KeyValuePair<string, Variable>(Properties.Resources.ColorearStats_Ataque_FR, OffsetHeaderAtaqueKanto));
                    varsKanto.Add(new KeyValuePair<string, Variable>(Properties.Resources.ColorearStats_Defensa_FR, OffsetHeaderDefensaKanto));
                    varsKanto.Add(new KeyValuePair<string, Variable>(Properties.Resources.ColorearStats_AtaqueEspecial_FR, OffsetHeaderAtaqueEspecialKanto));
                    varsKanto.Add(new KeyValuePair<string, Variable>(Properties.Resources.ColorearStats_DefensaEspecial_FR, OffsetHeaderDefensaEspecialKanto));
                    varsKanto.Add(new KeyValuePair<string, Variable>(Properties.Resources.ColorearStats_Velocidad_FR, OffsetHeaderVelocidadKanto));
                    varsKanto.Add(new KeyValuePair<string, Variable>(Properties.Resources.ColorearStats_RevertirColorANegro_FR, OffsetHeaderRevertirColorANegroKanto));

                    for (int i = 0; i < varsKanto.Count; i++)
                    {
                        offsetHeader = Variable.GetVariable(varsKanto[i].Value, edicion, compilacion);
                        romGBA.Data.SetArray(HeaderKantoPart1, offsetHeader);
                        offsetHeader += HeaderKantoPart1.Length;
                        romGBA.Data.SetArray(new OffsetRom(romGBA.Data.SetArrayIfNotExist(ASM.Compilar(SetOffsetsRutina(romGBA, varsKanto[i].Key, edicion, compilacion)).AsmBinary) + 1).BytesPointer, offsetHeader);
                    }

                }
                else if (edicion.AbreviacionRom == AbreviacionCanon.BPE)
                {
                    //pongo las rutinas
                    //activo las rutinas

                    offsetHeader = Variable.GetVariable(OffsetHeaderLeftEsmeralda, edicion, compilacion);
                    romGBA.Data.SetArray(HeaderLeftPart1, offsetHeader);
                    offsetHeader += HeaderLeftPart1.Length;
                    romGBA.Data.SetArray(new OffsetRom(romGBA.Data.SetArrayIfNotExist(ASM.Compilar(SetOffsetsRutina(romGBA, Properties.Resources.ColorearStats_LeftStats_Esmeralda, edicion, compilacion)).AsmBinary) + 1).BytesPointer, offsetHeader);

                    offsetHeader = Variable.GetVariable(OffsetHeaderRightEsmeralda, edicion, compilacion);
                    romGBA.Data.SetArray(HeaderRightPart1, offsetHeader);
                    offsetHeader += HeaderRightPart1.Length;
                    romGBA.Data.SetArray(new OffsetRom(romGBA.Data.SetArrayIfNotExist(ASM.Compilar(SetOffsetsRutina(romGBA, Properties.Resources.ColorearStats_RightStats_Esmeralda, edicion, compilacion)).AsmBinary) + 1).BytesPointer, offsetHeader);

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
            int offsetRutinaActual;
            byte[] rutinaActual;
            List<KeyValuePair<KeyValuePair<string, Variable>, byte[]>> vars;
            if (EstaActivado(romGBA, edicion, compilacion))
            {
                vars = new List<KeyValuePair<KeyValuePair<string, Variable>, byte[]>>();
                if (edicion.RegionKanto)
                {
                    vars.Add(new KeyValuePair<KeyValuePair<string, Variable>, byte[]>(new KeyValuePair<string, Variable>(Properties.Resources.ColorearStats_Ataque_FR, OffsetHeaderAtaqueKanto), HeaderKantoAtaque));
                    vars.Add(new KeyValuePair<KeyValuePair<string, Variable>, byte[]>(new KeyValuePair<string, Variable>(Properties.Resources.ColorearStats_Defensa_FR, OffsetHeaderDefensaKanto), HeaderKantoDefensa));
                    vars.Add(new KeyValuePair<KeyValuePair<string, Variable>, byte[]>(new KeyValuePair<string, Variable>(Properties.Resources.ColorearStats_AtaqueEspecial_FR, OffsetHeaderAtaqueEspecialKanto), HeaderKantoAtaqueEspecial));
                    vars.Add(new KeyValuePair<KeyValuePair<string, Variable>, byte[]>(new KeyValuePair<string, Variable>(Properties.Resources.ColorearStats_DefensaEspecial_FR, OffsetHeaderDefensaEspecialKanto), HeaderKantoDefensaEspecial));
                    vars.Add(new KeyValuePair<KeyValuePair<string, Variable>, byte[]>(new KeyValuePair<string, Variable>(Properties.Resources.ColorearStats_Velocidad_FR, OffsetHeaderVelocidadKanto), HeaderKantoVelocidad));
                    vars.Add(new KeyValuePair<KeyValuePair<string, Variable>, byte[]>(new KeyValuePair<string, Variable>(Properties.Resources.ColorearStats_RevertirColorANegro_FR, OffsetHeaderRevertirColorANegroKanto), HeaderKantoRevertirColorANegro));

                }
                else if (edicion.AbreviacionRom == AbreviacionCanon.BPE)
                {
                    vars.Add(new KeyValuePair<KeyValuePair<string, Variable>, byte[]>(new KeyValuePair<string, Variable>(Properties.Resources.ColorearStats_LeftStats_Esmeralda, OffsetHeaderLeftEsmeralda), HeaderLeftEsmeralda));
                    vars.Add(new KeyValuePair<KeyValuePair<string, Variable>, byte[]>(new KeyValuePair<string, Variable>(Properties.Resources.ColorearStats_RightStats_Esmeralda, OffsetHeaderRightEsmeralda), HeaderRightEsmeralda));
                }
                else
                {
                    //Rubi y Zafiro si fuera diferente a las demás ediciones el for se tendria que poner o protegido contra estas ediciones o dentro de cada if
                }

                for (int i = 0; i < vars.Count; i++)
                {
                    rutinaActual = ASM.Compilar(SetOffsetsRutina(romGBA, vars[i].Key.Key, edicion, compilacion)).AsmBinary;
                    offsetRutinaActual = romGBA.Data.SearchArray(rutinaActual.SubArray(20));//mirar de hacer una subArray para que los colores no den problemas
                    //borro la rutina de la rom
                    romGBA.Data.Remove(offsetRutinaActual, rutinaActual.Length);
                    //desactivo la rutina
                    romGBA.Data.SetArray(vars[i].Value, Variable.GetVariable(vars[i].Key.Value, edicion, compilacion));
                }
            }
        }
    }
}
