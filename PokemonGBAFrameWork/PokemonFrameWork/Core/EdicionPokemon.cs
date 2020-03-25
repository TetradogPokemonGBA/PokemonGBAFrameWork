/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 10:33
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PokemonGBAFrameWork
{
    public enum Idioma
    {
        Español = 'S',
        Ingles = 'E',
        Otro = 'O'
    }
    [Flags]
    public enum AbreviacionCanon
    {
        /// <summary>
        ///Abreviación Rubi
        /// </summary>
        AXV = 8,//asi puede haber hasta 4 compilaciones...por si surjen nuevos idiomas con más compilaciones :3
        /// <summary>
        ///Abreviación Zafiro
        /// </summary>
        AXP = AXV * 2,
        /// <summary>
        ///Abreviación Esmeralda
        /// </summary>
        BPE = AXP * 2,
        /// <summary>
        ///Abreviación Rojo Fuego
        /// </summary>
        BPR = BPE * 2,
        /// <summary>
        ///Abreviación Verde Hoja
        /// </summary>
        BPG = BPR * 2

    }

    /// <summary>
    /// Description of EdicionPokemon.
    /// </summary>
    public class EdicionPokemon : Edicion, IComparable
    {
        /// <summary>
        ///Este es el ID máximo reservado hasta que añada nuevos idiomas
        /// </summary>
        //public const long IDMINRESERVADO = (int)Idioma.Español + (int)Idioma.Ingles + (int)AbreviacionCanon.AXP + (int)AbreviacionCanon.AXV + (int)AbreviacionCanon.BPE + (int)AbreviacionCanon.BPG + (int)AbreviacionCanon.BPR + (int)CompilacionPokemon.Version.Primera + (int)CompilacionPokemon.Version.Segunda + (int)CompilacionPokemon.Version.Ultima;
        //public const long IDRUBIANDZAFIRO= (int)Idioma.Español + (int)Idioma.Ingles + (int)AbreviacionCanon.AXP + (int)AbreviacionCanon.AXV +(int)CompilacionPokemon.Version.Primera + (int)CompilacionPokemon.Version.Segunda + (int)CompilacionPokemon.Version.Ultima;
        //public const long IDESMERALDA = (int)Idioma.Español + (int)Idioma.Ingles + (int)AbreviacionCanon.BPE + (int)CompilacionPokemon.Version.Primera;
        //public const long IDROJOFUEGOANDVERDEHOJA = (int)Idioma.Español + (int)Idioma.Ingles + (int)AbreviacionCanon.BPR + (int)AbreviacionCanon.BPG + (int)CompilacionPokemon.Version.Primera + (int)CompilacionPokemon.Version.Segunda;
        //public const long IDHOENN= (int)Idioma.Español + (int)Idioma.Ingles + (int)AbreviacionCanon.AXP + (int)AbreviacionCanon.AXV + (int)AbreviacionCanon.BPE + (int)CompilacionPokemon.Version.Primera + (int)CompilacionPokemon.Version.Segunda + (int)CompilacionPokemon.Version.Ultima;
        //public const long IDKANTO = IDROJOFUEGOANDVERDEHOJA;
        //Ediciones canon usa
        public static readonly EdicionPokemon RubiUsa10 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "AXV", "POKEMON RUBY"), Idioma.Ingles, AbreviacionCanon.AXV, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon RubiUsa11 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "AXV", "POKEMON RUBY"), Idioma.Ingles, AbreviacionCanon.AXV, CompilacionPokemon.Compilaciones[1]);
        public static readonly EdicionPokemon RubiUsa12 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "AXV", "POKEMON RUBY"), Idioma.Ingles, AbreviacionCanon.AXV, CompilacionPokemon.Compilaciones[2]);


        public static readonly EdicionPokemon ZafiroUsa10 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "AXP", "POKEMON SAPP"), Idioma.Ingles, AbreviacionCanon.AXP, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon ZafiroUsa11 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "AXP", "POKEMON SAPP"), Idioma.Ingles, AbreviacionCanon.AXP, CompilacionPokemon.Compilaciones[1]);
        public static readonly EdicionPokemon ZafiroUsa12 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "AXP", "POKEMON SAPP"), Idioma.Ingles, AbreviacionCanon.AXP, CompilacionPokemon.Compilaciones[2]);
        public static readonly EdicionPokemon EsmeraldaUsa10 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "BPE", "POKEMON EMER"), Idioma.Ingles, AbreviacionCanon.BPE, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon RojoFuegoUsa10 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "BPR", "POKEMON FIRE"), Idioma.Ingles, AbreviacionCanon.BPR, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon RojoFuegoUsa11 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "BPR", "POKEMON FIRE"), Idioma.Ingles, AbreviacionCanon.BPR, CompilacionPokemon.Compilaciones[1]);

        public static readonly EdicionPokemon VerdeHojaUsa10 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "BPG", "POKEMON LEAF"), Idioma.Ingles, AbreviacionCanon.BPG, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon VerdeHojaUsa11 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "BPG", "POKEMON LEAF"), Idioma.Ingles, AbreviacionCanon.BPG, CompilacionPokemon.Compilaciones[1]);

        //Ediciones canon esp
        public static readonly EdicionPokemon RubiEsp10 = new EdicionPokemon(new Edicion((char)Idioma.Español, "AXV", "POKEMON RUBY"), Idioma.Español, AbreviacionCanon.AXV, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon ZafiroEsp10 = new EdicionPokemon(new Edicion((char)Idioma.Español, "AXP", "POKEMON SAPP"), Idioma.Español, AbreviacionCanon.AXP, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon EsmeraldaEsp10 = new EdicionPokemon(new Edicion((char)Idioma.Español, "BPE", "POKEMON EMER"), Idioma.Español, AbreviacionCanon.BPE, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon RojoFuegoEsp10 = new EdicionPokemon(new Edicion((char)Idioma.Español, "BPR", "POKEMON FIRE"), Idioma.Español, AbreviacionCanon.BPR, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon VerdeHojaEsp10 = new EdicionPokemon(new Edicion((char)Idioma.Español, "BPG", "POKEMON LEAF"), Idioma.Español, AbreviacionCanon.BPG, CompilacionPokemon.Compilaciones[0]);
        //todas las edicionesCanon
        public static readonly EdicionPokemon[] EdicionesCanon = new EdicionPokemon[] {
            RubiUsa10,
            RubiUsa11,
            RubiUsa12,
            ZafiroUsa10,
            ZafiroUsa11,
            ZafiroUsa12,
            EsmeraldaUsa10,
            RojoFuegoUsa10,
            RojoFuegoUsa11,
            VerdeHojaUsa10,
            VerdeHojaUsa11,
            RubiEsp10,
            ZafiroEsp10,
            EsmeraldaEsp10,
            RojoFuegoEsp10,
            VerdeHojaEsp10
        };
        public static readonly long[] IdsEdicionesCanon = new long[] {
            RubiUsa10.Id,
            RubiUsa11.Id,
            RubiUsa12.Id,
            ZafiroUsa10.Id,
            ZafiroUsa11.Id,
            ZafiroUsa12.Id,
            EsmeraldaUsa10.Id,
            RojoFuegoUsa10.Id,
            RojoFuegoUsa11.Id,
            VerdeHojaUsa10.Id,
            VerdeHojaUsa11.Id,
            RubiEsp10.Id,
            ZafiroEsp10.Id,
            EsmeraldaEsp10.Id,
            RojoFuegoEsp10.Id,
            VerdeHojaEsp10.Id
        };

        private EdicionPokemon(Edicion edicion)
            : base(edicion.InicialIdioma, edicion.Abreviacion, edicion.NombreCompleto)
        {
        }
        private EdicionPokemon(Edicion edicion, Idioma idioma, AbreviacionCanon abreviacionRomCanon, CompilacionPokemon compilacion = null)
            : base(edicion.InicialIdioma, edicion.Abreviacion, edicion.NombreCompleto)
        {
            Idioma = idioma;
            AbreviacionRom = abreviacionRomCanon;
            Compilacion = compilacion;
            if (compilacion != null)
                Id = GetId(this);
        }
        #region Propiedades
        public Idioma Idioma { get; private set; }
        public AbreviacionCanon AbreviacionRom { get; private set; }
        public bool RegionKanto
        {
            get { return this.AbreviacionRom == AbreviacionCanon.BPG || this.AbreviacionRom == AbreviacionCanon.BPR; }
        }
        public bool RegionHoenn
        {
            get { return !RegionKanto; }
        }
        public bool EstaEnEspañol
        {
            get { return Idioma == Idioma.Español; }
        }
        public bool EstaEnIngles
        {
            get { return Idioma == Idioma.Ingles; }
        }
        public bool EsZafiro
        {
            get { return AbreviacionRom == AbreviacionCanon.AXP; }
        }
        public bool EsRubi
        {
            get { return AbreviacionRom == AbreviacionCanon.AXV; }
        }
        public bool EsRojoFuego
        {
            get { return AbreviacionRom == AbreviacionCanon.BPR; }
        }
        public bool EsVerdeHoja
        {
            get { return AbreviacionRom == AbreviacionCanon.BPG; }
        }
        public bool EsEsmeralda
        {
            get { return AbreviacionRom == AbreviacionCanon.BPE; }
        }

        public bool EstaModificada
        {
            get { return (char)Idioma != InicialIdioma || Abreviacion != AbreviacionRom.ToString(); }
        }

        public bool EsRubiOZafiro => EsRubi || EsZafiro; 
        #endregion
        #region Overrides
        public override bool Equals(object obj)
        {
            EdicionPokemon other = obj as EdicionPokemon;
            bool equals = other != null;
            if (equals)
                equals = this.Idioma == other.Idioma && this.AbreviacionRom == other.AbreviacionRom;
            return equals;
        }
        public override bool Compatible(Edicion edicion)
        {
            EdicionPokemon edicionPokemon = edicion as EdicionPokemon;
            bool compatible = edicionPokemon != null;
            if (compatible)
            {
                switch (AbreviacionRom)
                {
                    case AbreviacionCanon.AXV:
                    case AbreviacionCanon.AXP:
                        compatible = edicionPokemon.EsRubiOZafiro;
                        break;
                    case AbreviacionCanon.BPE:
                        compatible = AbreviacionRom == edicionPokemon.AbreviacionRom;
                        break;
                    case AbreviacionCanon.BPR:
                    case AbreviacionCanon.BPG:
                        compatible = edicionPokemon.RegionKanto;
                        break;
                }
            }
            return compatible;
        }
        #endregion
        public int CompareTo(object obj)
        {
            return ICompareTo(obj as Edicion);
        }
        protected override int ICompareTo(Edicion other)
        {
            EdicionPokemon edicion = other as EdicionPokemon;
            int compareTo;
            if (edicion != null)
            {
                compareTo = Idioma.CompareTo(edicion.Idioma);
                if (compareTo == (int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals)
                    compareTo = AbreviacionRom.CompareTo(edicion.AbreviacionRom);

            }
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals;
            return compareTo;
        }
        public static EdicionPokemon GetEdicionPokemon(RomGba rom)
        {
            EdicionPokemon edicionPokemon = new EdicionPokemon(rom.Edicion);
            bool edicionValida;
            AbreviacionCanon abreviacionRom;
            edicionPokemon.Idioma = GetIdioma(edicionPokemon.InicialIdioma);//mirar de que el idioma se pueda calcular...
            edicionValida = Enum.TryParse(edicionPokemon.Abreviacion, out abreviacionRom);
            edicionPokemon.AbreviacionRom = abreviacionRom;
            //compruebo que este bien
            if (edicionValida && (edicionPokemon.Idioma == Idioma.Español || edicionPokemon.Idioma == Idioma.Ingles))
            {
                edicionValida = ValidaEdicion(rom, edicionPokemon);
            }
            else
                edicionValida = false;

            if (!edicionValida)
            {
                //si esta mal corrijo los campos Idioma y AbreviacionRom

                for (int i = 0; !edicionValida && i < EdicionesCanon.Length; i++)
                {
                    if (ValidaEdicion(rom, EdicionesCanon[i]))
                    { //tengo que saber que edicion y que idioma es
                        edicionPokemon.Idioma = EdicionesCanon[i].Idioma;
                        edicionPokemon.AbreviacionRom = EdicionesCanon[i].AbreviacionRom;
                        edicionValida = true;
                    }
                    //si no es una edicion canon es que ha sido muy modificada y no se leerla
                    if (!edicionValida)
                        throw new FormatoRomNoReconocidoException();

                }
            }
            edicionPokemon.Compilacion = CompilacionPokemon.GetCompilacion(rom, edicionPokemon);
            //pongo el id
            edicionPokemon.Id = GetId(edicionPokemon);
            return edicionPokemon;
        }

        private static Idioma GetIdioma(char inicialIdioma)
        {
            Idioma idioma;
            switch (inicialIdioma)
            {
                case 'S': idioma = Idioma.Español; break;
                case 'E': idioma = Idioma.Ingles; break;
                default: idioma = Idioma.Otro; break;
            }
            return idioma;
        }

        public static long GetId(EdicionPokemon edicionPokemon)
        {
            return (int)edicionPokemon.Idioma*1000 + (int)edicionPokemon.AbreviacionRom + (int)((CompilacionPokemon)edicionPokemon.Compilacion).Compilacion;
        }

        static bool ValidaEdicion(RomGba rom, EdicionPokemon edicionPokemon)
        {
            bool valida = false;
            //tengo que encontrar si es verdad que sea su edicion...
            //diferenciar idioma,edicion
            try
            {
                switch (edicionPokemon.AbreviacionRom)
                {
                    case AbreviacionCanon.AXV:
                    case AbreviacionCanon.AXP:
                        valida = Zona.GetOffsetRom(AtaqueCompleto.ZonaAnimacion, rom, edicionPokemon, CompilacionPokemon.Compilaciones[0]).IsAPointer;
                        if (!valida)
                            valida = Zona.GetOffsetRom(AtaqueCompleto.ZonaAnimacion, rom, edicionPokemon, CompilacionPokemon.Compilaciones[1]).IsAPointer;
                        break;
                    case AbreviacionCanon.BPE:
                    case AbreviacionCanon.BPR:
                    case AbreviacionCanon.BPG:
                        valida = Zona.GetOffsetRom(Pokemon.Descripcion.ZonaDescripcion, rom, edicionPokemon, CompilacionPokemon.Compilaciones[0]).IsAPointer;
                        if (!valida && edicionPokemon.RegionKanto)
                            valida = Zona.GetOffsetRom(Pokemon.Descripcion.ZonaDescripcion, rom, edicionPokemon, CompilacionPokemon.Compilaciones[1]).IsAPointer;
                        break;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);//por ver algo :)
            }
            return valida;

        }
        public static EdicionPokemon GetEdicionCompatible(long id)
        {
            EdicionPokemon edicion;
            Idioma idioma = (Idioma)id;
            CompilacionPokemon.Version version = (CompilacionPokemon.Version)id;
            AbreviacionCanon abreviacion = (AbreviacionCanon)id;

            if (idioma.HasFlag(Idioma.Español))
            {

                if (abreviacion.HasFlag(AbreviacionCanon.BPE))
                {
                    if (version.HasFlag(CompilacionPokemon.Version.Primera))
                        edicion = EsmeraldaEsp10;
                    else throw new IdIncorrectoException("una Compilación");
                }
                else if (abreviacion.HasFlag(AbreviacionCanon.BPR))
                {
                    if (version.HasFlag(CompilacionPokemon.Version.Primera))
                        edicion = RojoFuegoEsp10;
                    else throw new IdIncorrectoException("una Compilación");
                }
                else if (abreviacion.HasFlag(AbreviacionCanon.BPG))
                {
                    if (version.HasFlag(CompilacionPokemon.Version.Primera))
                        edicion = VerdeHojaEsp10;
                    else throw new IdIncorrectoException("una Compilación");
                }
                else if (abreviacion.HasFlag(AbreviacionCanon.AXV))
                {
                    if (version.HasFlag(CompilacionPokemon.Version.Primera))
                        edicion = RubiEsp10;
                    else throw new IdIncorrectoException("una Compilación");
                }
                else if (abreviacion.HasFlag(AbreviacionCanon.AXP))
                {
                    if (version.HasFlag(CompilacionPokemon.Version.Primera))
                        edicion = ZafiroEsp10;
                    else throw new IdIncorrectoException("una Compilación");
                }
                else
                {
                    throw new IdIncorrectoException("una Edición");
                }
            }
            else if (idioma.HasFlag(Idioma.Ingles))
            {
                

                if (abreviacion.HasFlag(AbreviacionCanon.BPE))
                {
                    if (version.HasFlag(CompilacionPokemon.Version.Primera))
                        edicion = EsmeraldaUsa10;
                    else throw new IdIncorrectoException("una Compilación");
                }
                else if (abreviacion.HasFlag(AbreviacionCanon.BPR))
                {
                    if (version.HasFlag(CompilacionPokemon.Version.Primera))
                        edicion = RojoFuegoUsa10;
                    else if (version.HasFlag(CompilacionPokemon.Version.Segunda))
                        edicion = RojoFuegoUsa11;
                    else throw new IdIncorrectoException("una Compilación");
                }
                else if (abreviacion.HasFlag(AbreviacionCanon.BPG))
                {
                    if (version.HasFlag(CompilacionPokemon.Version.Primera))
                        edicion = VerdeHojaUsa10;
                    else if (version.HasFlag(CompilacionPokemon.Version.Segunda))
                        edicion = VerdeHojaUsa11;
                    else throw new IdIncorrectoException("una Compilación");
                }
                else if (abreviacion.HasFlag(AbreviacionCanon.AXV))
                {
                    if (version.HasFlag(CompilacionPokemon.Version.Primera))
                        edicion = RubiUsa10;
                    else if (version.HasFlag(CompilacionPokemon.Version.Segunda))
                        edicion = RubiUsa11;
                    else if (version.HasFlag(CompilacionPokemon.Version.Ultima))
                        edicion = RubiUsa12;
                    else throw new IdIncorrectoException("una Compilación");
                }
                else if (abreviacion.HasFlag(AbreviacionCanon.AXP))
                {
                    if (version.HasFlag(CompilacionPokemon.Version.Primera))
                        edicion = ZafiroUsa10;
                    else if (version.HasFlag(CompilacionPokemon.Version.Segunda))
                        edicion = ZafiroUsa11;
                    else if (version.HasFlag(CompilacionPokemon.Version.Ultima))
                        edicion = ZafiroUsa12;
                    else throw new IdIncorrectoException("una Compilación");
                }
                else
                {
                    throw new IdIncorrectoException("una Edición");
                }
            }
            else
            {
                throw new IdIncorrectoException("un Idioma");
            }
            return edicion;
        }
   
        public static (AbreviacionCanon Edicion,Idioma Idioma,CompilacionPokemon.Version Compilacion) ReadId(long id)
        {
            EdicionPokemon edicionPokemon = GetEdicionCompatible(id);
            return (edicionPokemon.AbreviacionRom, edicionPokemon.Idioma, ((CompilacionPokemon)edicionPokemon.Compilacion).Compilacion);
        }
    }
    public class IdIncorrectoException:Exception
    {
        public IdIncorrectoException(string parteIncorrecta) : base(String.Format("El id no es correcto, se necesita {0} compatible", parteIncorrecta)) { }
    }
}
