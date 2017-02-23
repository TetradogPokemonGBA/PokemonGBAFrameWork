/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 19/08/2016
 * Time: 16:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 //gracias a Silver314 por la app Yape he podido entender algunos parametros que no sabia que heran :D
using System;
using Gabriel.Cat;
using Gabriel.Cat.Extension;
namespace PokemonGBAFrameWork
{
    /// <summary>
    /// Description of DescripcionPokedex.
    /// </summary>
    public class Descripcion
    {
        /*
	 la altura y el peso pasados a kg y metros solo se tienen que dividir entre 10
         [CD BF BF BE FF 00 00 00 00 00 00 00=>NombreEspecie]
         [0A 00 =>Altura]
         [82 00 =>Peso]
         [1C 4D 44 08]//no se que es...
         [8B 4D 44 08=>descripcionPokedex]
         [00 00=>No se]
         [4C 01=>escala pokemon]
         [0B 00=>Against offset1??]
         [00 01=> escala entrenador]
         [FE FF=>Against offset2??]
         [00 00=>No se]
             */
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
        //en construccion
        Hex offsetInicio;
        BloqueString nombreEspecie;
        //tiene un tamaño maximo en todas las versiones de 0xC
        BloqueString descripcion;
        short peso;
        short altura;
        short escalaPokemon;
        short escalaEntrenador;
        //datos que desconozco
        short numero;
        short direccionPokemon;
        short direccionEntrenador;
        short numero2;
        //falta proporcion con entrenador y peso aun no lo acabo de entender como va...
        static Descripcion()
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
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x8F508, 0x8F528, 0x8F528);
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x8F508, 0x8F528, 0x8F528);
            //añado la zona al diccionario
            Zona.DiccionarioOffsetsZonas.Add(zonaDescripcion);
        }
        public Descripcion()
        {
            nombreEspecie = new BloqueString((int)LongitudCampos.NombreEspecie);
            descripcion = new BloqueString("");
            //pongo los datos que no se...
            numero = 0;
            numero2 = 0;
            
        }
        public Descripcion(BloqueString nombreEspecie, BloqueString descripcion ,short peso,short altura,short escalaPokemon,short escalaEntrenador, short direccionPokemon, short direccionEntrenador) : this()
        {
            //asi controlo los maximos
            this.nombreEspecie.Texto = nombreEspecie.Texto;
            this.descripcion.Texto = descripcion.Texto;
            this.direccionEntrenador = direccionEntrenador;
            this.direccionPokemon = direccionPokemon;
            Peso = peso;
            Altura = altura;
            EscalaEntrenador = escalaEntrenador;
            EscalaPokemon = escalaPokemon;
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

        public BloqueString Pagina
        {
            get
            {
                return descripcion;
            }
        }

        public short Peso
        {
            get
            {
                return peso;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                peso = value;
            }
        }

        public short Altura
        {
            get
            {
                return altura;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                altura = value;
            }
        }

        public short EscalaPokemon
        {
            get
            {
                return escalaPokemon;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                escalaPokemon = value;
            }
        }

        public short EscalaEntrenador
        {
            get
            {
                return escalaEntrenador;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                escalaEntrenador = value;
            }
        }

        public short DireccionPokemon
        {
            get
            {
                return direccionPokemon;
            }

            set
            {
                direccionPokemon = value;
            }
        }

        public short DireccionEntrenador
        {
            get
            {
                return direccionEntrenador;
            }

            set
            {
                direccionEntrenador = value;
            }
        }

        public Hex OffsetFin(bool esEsmeralda = false)
        {
            return OffsetInicio + (esEsmeralda ? (int)LongitudCampos.TotalEsmeralda : (int)LongitudCampos.TotalGeneral);
        }

        internal static bool ValidarZona(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            bool tieneMasCompilaciones = edicion.AbreviacionRom != Edicion.ABREVIACIONESMERALDA && edicion.IdiomaOffsets == Edicion.Idioma.English;//esta pensado para las ediciones USA y ESP las demas no...de momento
            bool valido;
            if (tieneMasCompilaciones)
            {
                valido = ValidarOffset(rom, edicion, Zona.GetOffset(rom, Variables.Descripcion, edicion, compilacion));

            }
            else
                valido = compilacion == CompilacionRom.Compilacion.Primera;
            return valido;
        }
        internal static bool ValidarOffset(RomGBA rom, Edicion edicion, Hex offsetInicioDescripcion)
        {
            Hex offsetByteValidador;
            byte byteValidador;
            bool valido=offsetInicioDescripcion>-1;//si el offset no es valido devuelve -1 
            if (valido)
            {
                offsetByteValidador = offsetInicioDescripcion + (int)LongitudCampos.NombreEspecie + 4/*poner lo que es...*/ + (int)Longitud.Offset - 1;
                byteValidador = rom.Datos[offsetByteValidador];
                valido = (byteValidador == 0x8 || byteValidador == 0x9);
                if (valido && (edicion.AbreviacionRom == Edicion.ABREVIACIONZAFIRO || edicion.AbreviacionRom == Edicion.ABREVIACIONRUBI))
                {
                    offsetByteValidador += (int)Longitud.Offset;
                    byteValidador = rom.Datos[offsetByteValidador];
                    valido = (byteValidador == 0x8 || byteValidador == 0x9);
                }
            }
            return valido;

        }
        internal static bool ValidarIndicePokemon(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex ordenGameFreak)
        {
            return ValidarOffset(rom, edicion, Zona.GetOffset(rom, Variables.Descripcion, edicion, compilacion) + ordenGameFreak * DameTotal(edicion));
        }

        private static int DameTotal(Edicion edicion)
        {
            int total;
            if (edicion.AbreviacionRom != Edicion.ABREVIACIONESMERALDA)
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
        public static void SetDescripcionPokedex(RomGBA rom, Hex ordenGameFreak, Descripcion descripcion, bool borrarPaginasAnteriores = true)
        { SetDescripcionPokedex(rom, Edicion.GetEdicion(rom), ordenGameFreak, descripcion, borrarPaginasAnteriores); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="edicion"></param>
        /// <param name="ordenGameFreak"></param>
        /// <param name="descripcion">actualiza las descripciones en las nuevas posiciones</param>
        /// <param name="borrarPaginasAnteriores">Se usa la direccion de los bloqueString para encontrar los bytes de la anterior pagina</param>
        public static void SetDescripcionPokedex(RomGBA rom, Edicion edicion, Hex ordenGameFreak, Descripcion descripcion, bool borrarPaginasAnteriores = true)
        { SetDescripcionPokedex(rom, edicion, CompilacionRom.GetCompilacion(rom, edicion), ordenGameFreak, descripcion, borrarPaginasAnteriores); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="compilacion"></param>
        /// <param name="ordenGameFreak"></param>
        /// <param name="descripcion">actualiza las descripciones en las nuevas posiciones</param>
        /// <param name="borrarPaginasAnteriores">Se usa la direccion de los bloqueString para encontrar los bytes de la anterior pagina</param>
        public static void SetDescripcionPokedex(RomGBA rom, CompilacionRom.Compilacion compilacion, Hex ordenGameFreak, Descripcion descripcion, bool borrarPaginasAnteriores = true)
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
        public static void SetDescripcionPokedex(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex ordenGameFreak, Descripcion descripcion, bool borrarPaginasAnteriores = true)
        {
            const byte MarcaFinMensaje = 0xFF;
            DescripcionPokedexRubiZafiro descripcionRubiOZafiro;
            long index;
            Hex posicion;
            Hex posicionDescripcion;
            Hex offsetDescripcion = Zona.GetOffset(rom, Variables.Descripcion, edicion, compilacion) + ordenGameFreak * DameTotal(edicion);
            posicionDescripcion = offsetDescripcion;
            //borro el nombre de la especie
            BloqueBytes.RemoveBytes(rom, posicionDescripcion, (int)LongitudCampos.NombreEspecie, 0x0);
            //pongo el nombre de la especie
            BloqueString.SetString(rom, offsetDescripcion, descripcion.NombreEspecie);
            posicionDescripcion += (int)LongitudCampos.NombreEspecie;
            Word.SetWord(rom, posicionDescripcion, descripcion.Altura);
            posicionDescripcion += 2;
            Word.SetWord(rom, posicionDescripcion, descripcion.Peso);
            posicionDescripcion += 2;
           
            //quito las paginas si estan
            //borro pagina1
            if (borrarPaginasAnteriores && descripcion.Pagina.OffsetInicio != 0)
            {
                
                index = rom.Datos.IndexByte(descripcion.Pagina.OffsetInicio, MarcaFinMensaje);

                BloqueBytes.RemoveBytes(rom, descripcion.Pagina.OffsetInicio, index - descripcion.Pagina.OffsetInicio);
            }
            //pongo pagina1
            posicion = BloqueBytes.SearchEmptyBytes(rom, descripcion.Pagina.Texto.Length + 1);
            BloqueString.SetString(rom, posicion, descripcion.Pagina.Texto);
            descripcion.Pagina.OffsetInicio = posicion;
            Offset.SetOffset(rom,posicionDescripcion, posicion);
            posicionDescripcion += 4;
            if (edicion.AbreviacionRom ==Edicion.ABREVIACIONRUBI|| edicion.AbreviacionRom == Edicion.ABREVIACIONZAFIRO)
            {

                descripcionRubiOZafiro = descripcion as DescripcionPokedexRubiZafiro;
                if (descripcionRubiOZafiro == null)
                {
                    //creo una pagina en blanco
                    Offset.SetOffset(rom, offsetDescripcion + (int)LongitudCampos.NombreEspecie + 4 + (int)Longitud.Offset, BloqueBytes.SearchBytes(rom, new byte[] { 0xFF }));
                }
                //borro pagina2
                if (borrarPaginasAnteriores && descripcionRubiOZafiro.Pagina2.OffsetInicio != 0)
                {
                    index = rom.Datos.IndexByte(descripcionRubiOZafiro.Pagina2.OffsetInicio, MarcaFinMensaje);

                    BloqueBytes.RemoveBytes(rom, descripcionRubiOZafiro.Pagina2.OffsetInicio, index - descripcionRubiOZafiro.Pagina2.OffsetInicio);

                }
                
                //pongo pagina2
                posicion = BloqueBytes.SearchEmptyBytes(rom, descripcionRubiOZafiro.Pagina2.Texto.Length + 1);
                BloqueString.SetString(rom, posicion, descripcionRubiOZafiro.Pagina2.Texto);
                descripcionRubiOZafiro.Pagina2.OffsetInicio = posicion;
                Offset.SetOffset(rom, posicionDescripcion, posicion);
                posicionDescripcion += 4;
            }
            else if (edicion.AbreviacionRom != Edicion.ABREVIACIONESMERALDA)
                posicionDescripcion += 4;//me salto la pagina fantasma :D

            Word.SetWord(rom, posicionDescripcion, descripcion.numero);
            posicionDescripcion += 2;
            Word.SetWord(rom, posicionDescripcion, descripcion.EscalaPokemon);
            posicionDescripcion += 2;
            Word.SetWord(rom, posicionDescripcion, descripcion.direccionPokemon);
            posicionDescripcion += 2;
            Word.SetWord(rom,posicionDescripcion, descripcion.EscalaEntrenador);
            posicionDescripcion += 2;
            Word.SetWord(rom, posicionDescripcion, descripcion.direccionEntrenador);
            posicionDescripcion += 2;
            Word.SetWord(rom, posicionDescripcion, descripcion.numero2);


        }
        public static Descripcion GetDescripcionPokedex(RomGBA rom, Hex ordenGameFreak)
        { return GetDescripcionPokedex(rom, Edicion.GetEdicion(rom), ordenGameFreak); }
        public static Descripcion GetDescripcionPokedex(RomGBA rom, Edicion edicion, Hex ordenGameFreak)
        { return GetDescripcionPokedex(rom, edicion, CompilacionRom.GetCompilacion(rom, edicion), ordenGameFreak); }
        public static Descripcion GetDescripcionPokedex(RomGBA rom, CompilacionRom.Compilacion compilacion, Hex ordenGameFreak)
        { return GetDescripcionPokedex(rom, Edicion.GetEdicion(rom), compilacion, ordenGameFreak); }
        public static Descripcion GetDescripcionPokedex(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex ordenGameFreak)
        {

            if (rom == null || edicion == null || ordenGameFreak < 0) throw new ArgumentException();
            BloqueBytes bytesDescripcion;
            BloqueString nombreEspecie;
            Hex offsetPagina;
            BloqueString descripcion;
            BloqueString descripcion2;
            Descripcion descripcionPokedex;
            short peso, altura, tamañoPokemon, tamañoEntrenador;
            short direccionEntrenador, direccionPokemon,numero,numero2;
            bytesDescripcion = BloqueBytes.GetBytes(rom, Zona.GetOffset(rom, Variables.Descripcion, edicion, compilacion) + ordenGameFreak * DameTotal(edicion), DameTotal(edicion));
            //primero va la especie y acaba en FF si es mas corto que el tamaño maximo
            //no funciona bien de momento...
            nombreEspecie = BloqueString.GetString(bytesDescripcion,0,(int)LongitudCampos.NombreEspecie,true);
            nombreEspecie.MaxCaracteres = (int)LongitudCampos.NombreEspecie;
            peso = Word.GetWord(bytesDescripcion.Bytes, (int)LongitudCampos.NombreEspecie + 2);
            altura = Word.GetWord(bytesDescripcion.Bytes, (int)LongitudCampos.NombreEspecie);

            numero = Word.GetWord(bytesDescripcion.Bytes, bytesDescripcion.Bytes.Length - 12 );
            numero2 = Word.GetWord(bytesDescripcion.Bytes, bytesDescripcion.Bytes.Length - 2);

            tamañoPokemon = Word.GetWord(bytesDescripcion.Bytes,bytesDescripcion.Bytes.Length - 10);
            tamañoEntrenador = Word.GetWord(bytesDescripcion.Bytes, bytesDescripcion.Bytes.Length - 6);
           
            /* //que lio -.- aun no me sale...
         [CD BF BF BE FF 00 00 00 00 00 00 00=>NombreEspecie]
         [0A 00 =>Altura]
         [82 00 =>Peso]
         [1C 4D 44 08=>descripcionPokedex]
         [8B 4D 44 08=>descripcionPokedex]2 //solo en Rubi y Zafiro en Rojo y Verde apuntan a 0xFF y si les pones texto no se pueden usar en la pokedex...será que en la pokedex no hay mas de una pagina...
         [00 00=>No se]
         [4C 01=>escala pokemon]
         [0B 00=>Against offset1??]
         [00 01=> escala entrenador]
         [FE FF=>Against offset2??]
         [00 00=>No se]
             */
            direccionPokemon = Word.GetWord(bytesDescripcion.Bytes, bytesDescripcion.Bytes.Length - 8);
            direccionEntrenador = Word.GetWord(bytesDescripcion.Bytes, bytesDescripcion.Bytes.Length - 4);
            //luego va la descripcion que es un pointer
            offsetPagina = Offset.GetOffset(bytesDescripcion, (int)LongitudCampos.NombreEspecie + (int)Longitud.Offset);
            
            descripcion = BloqueString.GetString(rom, offsetPagina);
            if (edicion.AbreviacionRom == Edicion.ABREVIACIONRUBI|| edicion.AbreviacionRom == Edicion.ABREVIACIONZAFIRO)
            {
                offsetPagina = Offset.GetOffset(bytesDescripcion, (int)LongitudCampos.NombreEspecie + (int)Longitud.Offset + (int)Longitud.Offset);
                descripcion2 = BloqueString.GetString(rom,offsetPagina);


                descripcionPokedex = new DescripcionPokedexRubiZafiro(nombreEspecie, descripcion, descripcion2,peso,altura,tamañoPokemon,tamañoEntrenador,direccionPokemon,direccionEntrenador);
            }
            else
            {
                descripcionPokedex = new Descripcion(nombreEspecie, descripcion,peso,altura,tamañoPokemon,tamañoEntrenador, direccionPokemon, direccionEntrenador);//mas adelante poner todos los campos
            }
            //pongo los datos que aun no se que son...
            descripcionPokedex.numero = numero;
            descripcionPokedex.numero2 = numero2;
            return descripcionPokedex;
        }
        public static int TotalEntradas(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            int total=0;

            while (PokemonGBAFrameWork.Descripcion.ValidarIndicePokemon(rom, edicion, compilacion, total))
                total+=30;
            while (!PokemonGBAFrameWork.Descripcion.ValidarIndicePokemon(rom, edicion, compilacion, total))
                total -= 10;
            while (PokemonGBAFrameWork.Descripcion.ValidarIndicePokemon(rom, edicion, compilacion, total))
                total++;

            return total;
        }

        public static int GetTotalBytes(Edicion edicion)
        {
            int total;
            if (edicion.AbreviacionRom == Edicion.ABREVIACIONESMERALDA)
                total = (int)LongitudCampos.TotalEsmeralda;
            else total = (int)LongitudCampos.TotalGeneral;
            return total;
        }
        /// <summary>
        /// Crea donde toca la descripcion
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="edicion"></param>
        /// <param name="compilacion"></param>
        /// <param name="pokemon"></param>
        public static void CreateDescripcionPokedex(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Pokemon pokemon)
        {
            Descripcion descripcion;
            if (edicion.AbreviacionRom == Edicion.ABREVIACIONRUBI || edicion.AbreviacionRom == Edicion.ABREVIACIONZAFIRO)
                descripcion = new DescripcionPokedexRubiZafiro();
            else
                descripcion = new Descripcion();
            pokemon.Descripcion = descripcion;
            SetDescripcionPokedex(rom, pokemon.OrdenPokedexNacional, descripcion, false);
        }
    }
    public class DescripcionPokedexRubiZafiro : Descripcion
    {
        BloqueString pagina2;
        public DescripcionPokedexRubiZafiro():this(new BloqueString((int)LongitudCampos.NombreEspecie), new BloqueString(), new BloqueString(), 0, 0, 0, 0,0,0)
        {}
        public DescripcionPokedexRubiZafiro(BloqueString nombreEspecie, BloqueString pagina1, BloqueString pagina2, short peso, short altura, short escalaPokemon, short escalaEntrenador,short direccionPokemon,short direccionEntrenador) : base(nombreEspecie, pagina1,peso,altura,escalaPokemon,escalaEntrenador,direccionPokemon,direccionEntrenador)
        {
            this.pagina2 = new BloqueString(pagina2.OffsetInicio, pagina2.Texto);
        }
        public BloqueString Pagina2
        {
            get { return pagina2; }
        }
    }
}
