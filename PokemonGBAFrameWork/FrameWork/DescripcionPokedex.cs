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
using Gabriel.Cat.Extension;
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
            PaginasRubiZafiro = 2,
            PaginasGeneral = 1,
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
        public DescripcionPokedex(BloqueString nombreEspecie, BloqueString descripcion) : this()
        {
            //asi controlo los maximos
            this.nombreEspecie.Texto = nombreEspecie.Texto;
            this.descripcion.Texto = descripcion.Texto;
        }
        //calcular OffsetInicio con el offset de la descripcion

        public Hex OffsetInicio
        {
            get
            {
                return offsetInicio;
            }
            set
            {
                offsetInicio = value;
            }
        }

        public BloqueString NombreEspecie
        {
            get
            {
                return nombreEspecie;
            }
        }

        public BloqueString Descripcion
        {
            get
            {
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
            if (tieneMasCompilaciones)
            {
                valido = ValidarOffset(rom, edicion, Zona.GetOffset(rom, Variables.Descripcion, edicion, compilacion));

            }
            else
                valido = compilacion == CompilacionRom.Compilacion.Primera;
            return valido;
        }
        internal static bool ValidarOffset(RomPokemon rom, Edicion edicion, Hex offsetInicioDescripcion)
        {
            Hex offsetByteValidador = offsetInicioDescripcion + (int)LongitudCampos.NombreEspecie + 4/*poner lo que es...*/ + (int)Longitud.Offset - 1;
            byte byteValidador = rom.Datos[offsetByteValidador];
            bool valido = (byteValidador == 0x8 || byteValidador == 0x9);
            if (valido && (edicion.Abreviacion == Edicion.ABREVIACIONZAFIRO || edicion.Abreviacion == Edicion.ABREVIACIONRUBI))
            {
                offsetByteValidador += (int)Longitud.Offset;
                byteValidador = rom.Datos[offsetByteValidador];
                valido = (byteValidador == 0x8 || byteValidador == 0x9);
            }
            return valido;

        }
        internal static bool ValidarIndicePokemon(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex ordenGameFreak)
        {
            return ValidarOffset(rom, edicion, Zona.GetOffset(rom, Variables.Descripcion, edicion, compilacion) + ordenGameFreak * DameTotal(edicion));
        }

        private static int DameTotal(Edicion edicion)
        {
            int total;
            if (edicion.Abreviacion != Edicion.ABREVIACIONESMERALDA)
                total = (int)LongitudCampos.TotalGeneral;
            else total = (int)LongitudCampos.TotalEsmeralda;
            return total;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="ordenGameFreak"></param>
        /// <param name="descripcion">actualiza las descripciones en las nuevas posiciones</param>
        /// <param name="borrarPaginasAnteriores">Se usa la direccion de los bloqueString para encontrar los bytes de la anterior pagina</param>
        public static void SetDescripcionPokedex(RomPokemon rom, Hex ordenGameFreak, DescripcionPokedex descripcion, bool borrarPaginasAnteriores = true)
        { SetDescripcionPokedex(rom, Edicion.GetEdicion(rom), ordenGameFreak, descripcion, borrarPaginasAnteriores); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="edicion"></param>
        /// <param name="ordenGameFreak"></param>
        /// <param name="descripcion">actualiza las descripciones en las nuevas posiciones</param>
        /// <param name="borrarPaginasAnteriores">Se usa la direccion de los bloqueString para encontrar los bytes de la anterior pagina</param>
        public static void SetDescripcionPokedex(RomPokemon rom, Edicion edicion, Hex ordenGameFreak, DescripcionPokedex descripcion, bool borrarPaginasAnteriores = true)
        { SetDescripcionPokedex(rom, edicion, CompilacionRom.GetCompilacion(rom, edicion), ordenGameFreak, descripcion, borrarPaginasAnteriores); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="compilacion"></param>
        /// <param name="ordenGameFreak"></param>
        /// <param name="descripcion">actualiza las descripciones en las nuevas posiciones</param>
        /// <param name="borrarPaginasAnteriores">Se usa la direccion de los bloqueString para encontrar los bytes de la anterior pagina</param>
        public static void SetDescripcionPokedex(RomPokemon rom, CompilacionRom.Compilacion compilacion, Hex ordenGameFreak, DescripcionPokedex descripcion, bool borrarPaginasAnteriores = true)
        { SetDescripcionPokedex(rom, Edicion.GetEdicion(rom), compilacion, ordenGameFreak, descripcion, borrarPaginasAnteriores); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="edicion"></param>
        /// <param name="compilacion"></param>
        /// <param name="ordenGameFreak"></param>
        /// <param name="descripcion">actualiza las descripciones en las nuevas posiciones</param>
        /// <param name="borrarPaginasAnteriores">Se usa la direccion de los bloqueString para encontrar los bytes de la anterior pagina</param>
        public static void SetDescripcionPokedex(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex ordenGameFreak, DescripcionPokedex descripcion, bool borrarPaginasAnteriores = true)
        {
         /*   DescripcionPokedexRubiZafiro descripcionRZ;
            long index;
            Hex posicion;
            Hex offsetDescripcion = Zona.GetOffset(rom, Variables.Descripcion, edicion, compilacion) + ordenGameFreak * DameTotal(edicion);
            //borro el nombre de la especie
            BloqueBytes.RemoveBytes(rom, offsetDescripcion, (int)LongitudCampos.NombreEspecie, 0x0);
            //pongo el nombre de la especie
            BloqueString.SetString(rom, offsetDescripcion, descripcion.NombreEspecie + (char)MarcaFinMensaje);
            //quito las paginas si estan
            //borro pagina1
            if (borrarPaginasAnteriores && descripcion.Descripcion.OffsetInicio != 0)
            {
                index = rom.Datos.IndexByte(descripcion.Descripcion.OffsetInicio, MarcaFinMensaje);

                BloqueBytes.RemoveBytes(rom, descripcion.Descripcion.OffsetInicio, index - descripcion.Descripcion.OffsetInicio > DameTamañoDescripcion(edicion) ? DameTamañoDescripcion(edicion) : index - descripcion.Descripcion.OffsetInicio);
            }
            //pongo pagina1
            posicion = BloqueBytes.SearchEmptyBytes(rom, (descripcion.Descripcion.Texto.Length + 1 > (int)DameTamañoDescripcion(edicion)) ? (int)DameTamañoDescripcion(edicion) : descripcion.Descripcion.Texto.Length + 1);
            BloqueString.SetString(rom, posicion, descripcion.Descripcion.Texto + (char)MarcaFinMensaje);
            descripcion.Descripcion.OffsetInicio = posicion;
            if (edicion.Abreviacion == Edicion.ABREVIACIONRUBI || edicion.Abreviacion == Edicion.ABREVIACIONZAFIRO)
            {

                descripcionRZ = descripcion as DescripcionPokedexRubiZafiro;
                if (descripcionRZ == null)
                {
                    //creo una pagina en blanco
                    Offset.SetOffset(rom, offsetDescripcion + (int)LongitudCampos.NombreEspecie + 4 + (int)Longitud.Offset, BloqueBytes.SearchBytes(rom, new byte[] { 0xFF }));
                }
                //borro pagina2
                if (borrarPaginasAnteriores && descripcionRZ.Descripcion2.OffsetInicio != 0)
                {
                    index = rom.Datos.IndexByte(descripcionRZ.Descripcion2.OffsetInicio, MarcaFinMensaje);

                    BloqueBytes.RemoveBytes(rom, descripcionRZ.Descripcion2.OffsetInicio, index - descripcionRZ.Descripcion2.OffsetInicio > DameTamañoDescripcion(edicion) ? DameTamañoDescripcion(edicion) : index - descripcionRZ.Descripcion2.OffsetInicio);

                }
                //pongo pagina2
                posicion = BloqueBytes.SearchEmptyBytes(rom, (descripcionRZ.Descripcion2.Texto.Length + 1 > (int)DameTamañoDescripcion(edicion)) ? (int)DameTamañoDescripcion(edicion) : descripcionRZ.Descripcion2.Texto.Length + 1);
                BloqueString.SetString(rom, posicion, descripcionRZ.Descripcion2.Texto + (char)MarcaFinMensaje);
                descripcionRZ.Descripcion2.OffsetInicio = posicion;
            }
            //falta poner el resto de datos...
            */
        }
        public static DescripcionPokedex GetDescripcionPokedex(RomPokemon rom, Hex ordenGameFreak)
        { return GetDescripcionPokedex(rom, Edicion.GetEdicion(rom), ordenGameFreak); }
        public static DescripcionPokedex GetDescripcionPokedex(RomPokemon rom, Edicion edicion, Hex ordenGameFreak)
        { return GetDescripcionPokedex(rom, edicion, CompilacionRom.GetCompilacion(rom, edicion), ordenGameFreak); }
        public static DescripcionPokedex GetDescripcionPokedex(RomPokemon rom, CompilacionRom.Compilacion compilacion, Hex ordenGameFreak)
        { return GetDescripcionPokedex(rom, Edicion.GetEdicion(rom), compilacion, ordenGameFreak); }
        public static DescripcionPokedex GetDescripcionPokedex(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex ordenGameFreak)
        {
            if (rom == null || edicion == null || ordenGameFreak < 0) throw new ArgumentException();
            BloqueBytes bytesDescripcion;
            BloqueString nombreEspecie;
            long longitudString;
            Hex offsetPagina;
            BloqueString descripcion;
            BloqueString descripcion2;
            DescripcionPokedex descripcionPokedex;

            bytesDescripcion = BloqueBytes.GetBytes(rom, Zona.GetOffset(rom, Variables.Descripcion, edicion, compilacion) + ordenGameFreak * DameTotal(edicion), DameTotal(edicion));
            //primero va la especie y acaba en FF si es mas corto que el tamaño maximo
            longitudString = bytesDescripcion.Bytes.IndexByte(MarcaFinMensaje);
            if (longitudString < 0 || longitudString > (int)LongitudCampos.NombreEspecie)
                longitudString = (int)LongitudCampos.NombreEspecie;
            nombreEspecie = BloqueString.GetString(bytesDescripcion, 0, longitudString);

            //luego va la descripcion que es un pointer
            offsetPagina = Offset.GetOffset(bytesDescripcion, (int)LongitudCampos.NombreEspecie + 4);
            longitudString = rom.Datos.IndexByte(offsetPagina, MarcaFinMensaje)-offsetPagina;
            descripcion = BloqueString.GetString(rom, offsetPagina, longitudString);
            if (edicion.Abreviacion == Edicion.ABREVIACIONRUBI || edicion.Abreviacion == Edicion.ABREVIACIONZAFIRO)
            {
                offsetPagina = Offset.GetOffset(bytesDescripcion, (int)LongitudCampos.NombreEspecie + 4 + (int)Longitud.Offset);
                longitudString = rom.Datos.IndexByte(offsetPagina, MarcaFinMensaje) - offsetPagina;
                descripcion2 = BloqueString.GetString(rom,offsetPagina, longitudString);


                descripcionPokedex = new DescripcionPokedexRubiZafiro(nombreEspecie, descripcion, descripcion2);
            }
            else
            {
                descripcionPokedex = new DescripcionPokedex(nombreEspecie, descripcion);//mas adelante poner todos los campos
            }
            return descripcionPokedex;
        }

        public static int TotalEntradas(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            int total=0;
            while (DescripcionPokedex.ValidarIndicePokemon(rom, edicion, compilacion, total))//en un futuro optimizarlo un poco mas
                total++;
            return total;
        }
    }
    public class DescripcionPokedexRubiZafiro : DescripcionPokedex
    {
        BloqueString pagina2;
        public DescripcionPokedexRubiZafiro(BloqueString nombreEspecie, BloqueString pagina1, BloqueString pagina2) : base(nombreEspecie, pagina1)
        {
            this.pagina2 = new BloqueString(pagina2.OffsetInicio, pagina2.Texto);
        }
        public BloqueString Descripcion2
        {
            get { return pagina2; }
        }
    }
}
