using Gabriel.Cat;
using Gabriel.Cat.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//información obtenida de https://github.com/Jambo51/Trainer_Editor
namespace PokemonGBAFrameWork
{
    public class Entrenadores:ObjectAutoId
    {
        enum Longitud
        {
            Nombre = 0xD,
            RateMoney=4,//por mirar...
        }
        //31AEB8
        enum Variables
        {
            SpriteImg,
            SpritePaleta,
            RatesMoney,
            NombreClaseEntrenador,
        }
        List<BloqueImagen> sprites;//de momento no se si hay FF o es solo el maximo
        List<BloqueString> nombres;
        List<byte> ratesMoney;
        static Entrenadores()
        {
            Zona zonaImg = new Zona(Variables.SpriteImg);
            Zona zonaPaleta = new Zona(Variables.SpritePaleta);
            Zona zonaNombres = new Zona(Variables.NombreClaseEntrenador);
            Zona zonaRatesMoney = new Zona(Variables.RatesMoney);
            //añado las zonas :)
            zonaRatesMoney.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x4E6A8);
            zonaRatesMoney.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x4E6A8);
            zonaRatesMoney.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x2593C);
            zonaRatesMoney.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x2593C);
            zonaRatesMoney.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x259CC,0x259E0);
            zonaRatesMoney.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x259CC, 0x259E0);
           
            //añado las zonas :D
            zonaNombres.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0xF7088, 0xF70A8);
            zonaNombres.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0xF7088, 0xF70A8);

            zonaNombres.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0xD8074, 0xD8088);
            zonaNombres.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0xD80A0, 0xD80B4);

            zonaNombres.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x183B4);
            zonaNombres.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x183B4);

            zonaNombres.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x40FE8);
            zonaNombres.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x40FE8);

            zonaNombres.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0xD7BC8);
            zonaNombres.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0xD7BF4);

           
            //pongo las zonas :D
            //img
            zonaImg.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x34628);
            zonaImg.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x34628);
            zonaImg.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x3473C, 0x34750);
            zonaImg.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x3473C, 0x34750);

            zonaImg.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x31ADC);
            zonaImg.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x31ADC);

            zonaImg.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x31CA8);
            zonaImg.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x31CA8);

            zonaImg.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x5DF78);
            zonaImg.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x5DF78);

            //paletas
            zonaPaleta.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x34638);
            zonaPaleta.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x34638);
            zonaPaleta.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x3474C, 0x34760);
            zonaPaleta.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x3474C, 0x34760);

            zonaPaleta.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x31AF0);
            zonaPaleta.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x31AF0);

            zonaPaleta.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x31CBC);
            zonaPaleta.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x31CBC);

            zonaPaleta.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x5B784);
            zonaPaleta.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x5B784);

            Zona.DiccionarioOffsetsZonas.Add(zonaRatesMoney);
            Zona.DiccionarioOffsetsZonas.Add(zonaNombres);
            Zona.DiccionarioOffsetsZonas.Add(zonaImg);
            Zona.DiccionarioOffsetsZonas.Add(zonaPaleta);
        }
        public Entrenadores()
        {
            sprites = new List<BloqueImagen>();
            nombres = new List<BloqueString>();
            ratesMoney = new List<byte>();
        }

       

        public List<byte> RatesMoney
        {
            get { return ratesMoney; }
        }
       public List<BloqueImagen> Sprites
        {
            get { return sprites; }
        }
        public List<BloqueString> Nombres
        {
            get { return nombres; }
        }
        public int Total
        {
            get { return sprites.Count; }
        }
        public static Entrenadores GetEntrenadoresClases(RomData rom)
        {
            Entrenadores entrenadores;

            Hex offsetInicioNombres;
            Hex offsetInicioImgs;
            Hex offsetInicioPaletas;
            Hex offsetInicioRateMoney;
            bool compatibleConRateMoney;
            if (rom == null)
                throw new ArgumentNullException();

            compatibleConRateMoney = rom.Edicion.AbreviacionRom != Edicion.ABREVIACIONRUBI && rom.Edicion.AbreviacionRom != Edicion.ABREVIACIONZAFIRO;

            offsetInicioNombres = Zona.GetOffset(rom.RomGBA, Variables.NombreClaseEntrenador, rom.Edicion, rom.Compilacion);
            offsetInicioImgs = Zona.GetOffset(rom.RomGBA, Variables.SpriteImg, rom.Edicion, rom.Compilacion);
            offsetInicioPaletas = Zona.GetOffset(rom.RomGBA, Variables.SpritePaleta, rom.Edicion, rom.Compilacion);
            if (compatibleConRateMoney)
                offsetInicioRateMoney = Zona.GetOffset(rom.RomGBA, Variables.RatesMoney, rom.Edicion, rom.Compilacion);
            entrenadores = new Entrenadores();

            for (int i = 0,f= GetNumeroDeClasesDeEntrenador(rom); i < f; i++)
            {
                entrenadores.Sprites.Add(BloqueImagen.GetBloqueImagen(rom.RomGBA, offsetInicioImgs + i * 2 * (int)PokemonGBAFrameWork.Longitud.Offset, offsetInicioPaletas + i * 2 * (int)PokemonGBAFrameWork.Longitud.Offset));
                entrenadores.Nombres.Add(BloqueString.GetString(rom.RomGBA, offsetInicioNombres + (i * (int)Longitud.Nombre), (int)Longitud.Nombre, true));
                if (compatibleConRateMoney)
                    entrenadores.RatesMoney.Add((rom.RomGBA.Datos.SubArray((int)offsetInicioRateMoney + (i * (int)Longitud.RateMoney), 1)[0]));//por mirar como se lee :)
            }
                return entrenadores;
        }

        public static int GetNumeroDeClasesDeEntrenador(RomData rom)
        {//obtiene bien el numero :D
            Hex offsetTablaEntrenadorImg = Zona.GetOffset(rom.RomGBA, Variables.SpriteImg, rom.Edicion, rom.Compilacion);
            Hex offsetTablaEntrenadorPaleta = Zona.GetOffset(rom.RomGBA, Variables.SpritePaleta, rom.Edicion, rom.Compilacion);
            Hex imgActual = offsetTablaEntrenadorImg, paletaActual = offsetTablaEntrenadorPaleta;
            int numero = 0;
      
            while (Offset.GetOffset(rom.RomGBA, imgActual) > -1 && Offset.GetOffset(rom.RomGBA, paletaActual) > -1)
            {
                numero++;
                imgActual += 8;
                paletaActual += 8;
            }
            return numero-1;
        }

        public static void SetSpritesEntrenadores(RomData rom)
        {
            if (rom == null|| rom.Entrenadores == null)
                throw new ArgumentNullException();

            Hex offsetInicioNombres;
            Hex offsetInicioImgs;
            Hex offsetInicioPaletas;
            Hex offsetInicioRateMoney;

            bool compatibleConRateMoney = rom.Edicion.AbreviacionRom != Edicion.ABREVIACIONRUBI && rom.Edicion.AbreviacionRom != Edicion.ABREVIACIONZAFIRO;
            offsetInicioNombres = Zona.GetOffset(rom.RomGBA, Variables.NombreClaseEntrenador, rom.Edicion, rom.Compilacion);
            offsetInicioImgs = Zona.GetOffset(rom.RomGBA, Variables.SpriteImg, rom.Edicion, rom.Compilacion);
            offsetInicioPaletas = Zona.GetOffset(rom.RomGBA, Variables.SpritePaleta, rom.Edicion, rom.Compilacion);

            if(compatibleConRateMoney)
                offsetInicioRateMoney= Zona.GetOffset(rom.RomGBA, Variables.RatesMoney, rom.Edicion, rom.Compilacion);
            if (compatibleConRateMoney&&rom.EntrenadoresClases.RatesMoney.Count < rom.EntrenadoresClases.Total)
                throw new ArgumentException("falta el rate money de algunas clases...");

            for (int i = 0; i < rom.EntrenadoresClases.Total; i++)
            {
                if (rom.EntrenadoresClases.Sprites[i] == null)
                    throw new NullReferenceException("Siempre tiene que haber imagen de entrenador...");
                else if (rom.EntrenadoresClases.Nombres[i] == null)
                    throw new NullReferenceException("Siempre tiene que haber Nombre de entrenador...");
                

            }

            for (int i = 0; i < rom.EntrenadoresClases.Total; i++)
            {
                //actualizo el pointer de las tablas
                //tabla img
                Offset.SetOffset(rom.RomGBA,offsetInicioImgs + i *(int)PokemonGBAFrameWork.Longitud.Offset, rom.EntrenadoresClases.Sprites[i].OffsetInicio);
                //tabla paleta
                Offset.SetOffset(rom.RomGBA,offsetInicioPaletas + i * (int)PokemonGBAFrameWork.Longitud.Offset, rom.EntrenadoresClases.Sprites[i].Paletas[0].OffsetPointerPaleta);
                rom.EntrenadoresClases.Nombres[i].OffsetInicio = offsetInicioNombres + i * (int)Longitud.Nombre;
                //pongo los datos
                BloqueImagen.SetBloqueImagen(rom.RomGBA, rom.EntrenadoresClases.Sprites[i]);
                BloqueString.SetString(rom.RomGBA, rom.EntrenadoresClases.Nombres[i]);
                if (compatibleConRateMoney)
                    rom.RomGBA.Datos.SetArray(offsetInicioRateMoney + (i * (int)Longitud.RateMoney), Serializar.GetBytes(rom.EntrenadoresClases.RatesMoney[i]).AddArray(new byte[] { 0x0, 0x0 }));
            }
     
        }

        public void SetSpritesEntrenadores(RomGBA romGBA, Entrenadores spritesEntrenadores)
        {
            SetSpritesEntrenadores(new RomData(romGBA) { EntrenadoresClases = spritesEntrenadores });
        }

    }
    public class Entrenador:ObjectAutoId//ocupa 40bytes
    {
        public class Equipo
        {
            public class Pokemon//ocupa 8 o 16 bytes dependiendo de si tiene o no movimientos custom el entrenador activado
            {
                ushort especie;//quinto byte
                byte ivs;//primer byte
                ushort level;//segundo byte no se porque son dos bytes...tendria que ser 1...
                ushort item;//septimo byte
                //a partir del byte 9//puede que los movimientos no esten cambiados por lo tanto no estarian...
                ushort move1;
                ushort move2;
                ushort move3;
                ushort move4;
                

                
                public ushort Especie
                {
                    get
                    {
                        return especie;
                    }

                    set
                    {
                        especie = value;
                    }
                }

                public byte Ivs
                {
                    get
                    {
                        return ivs;
                    }

                    set
                    {
                        ivs = value;
                    }
                }

                public ushort Nivel
                {
                    get
                    {
                        return level;
                    }

                    set
                    {
                        level = value;
                    }
                }

                public ushort Item
                {
                    get
                    {
                        return item;
                    }

                    set
                    {
                        item = value;
                    }
                }

                public ushort Move1
                {
                    get
                    {
                        return move1;
                    }

                    set
                    {
                        move1 = value;
                    }
                }

                public ushort Move2
                {
                    get
                    {
                        return move2;
                    }

                    set
                    {
                        move2 = value;
                    }
                }

                public ushort Move3
                {
                    get
                    {
                        return move3;
                    }

                    set
                    {
                        move3 = value;
                    }
                }

                public ushort Move4
                {
                    get
                    {
                        return move4;
                    }

                    set
                    {
                        move4 = value;
                    }
                }
            }

            enum Posicion
            {
                Ivs = 0,
                //posible byte en blanco
                Nivel = 1,
                //byte en blanco
                Especie = 4,
                Item = 6,

                Move1 = 8,
                Move2 = 10,
                Move3 = 12,
                Move4 = 14
            }
            enum Longitud
            {
                Nivel=2,
                Item=2,
                Ataque=2,
                PokemonIndex = 2,
            }
            Hex offsetToDataPokemon;
            //uint numeroDePokemons;
            Pokemon[] pokemonEquipo;//inicializarla a 6 para que sea el maximo :D luego el total será contar !=null
            public Equipo()
            {
                const int MAXPOKEMONEQUIPO = 6;
                pokemonEquipo = new Pokemon[MAXPOKEMONEQUIPO];
            }
            public Hex OffsetToDataPokemon
            {
                get
                {
                    return offsetToDataPokemon;//se guarda como pointer pero se usa como offset :D
                }

                set
                {
                    if (value < 0 || value > (int)PokemonGBAFrameWork.Longitud.TreintaYDosMegas) throw new ArgumentOutOfRangeException();
                    offsetToDataPokemon = value;
                }
            }

            public Pokemon[] PokemonEquipo
            {
                get
                {
                    return pokemonEquipo;
                }
            }
            public Pokemon this[int index]
            {
                get { return pokemonEquipo[index]; }
                set
                {
                    pokemonEquipo[index] = value;
                }
            }
            public int NumeroPokemon
            {
                get
                {
                    int num = 0;
                    for (int i = 0; i < pokemonEquipo.Length; i++)
                        if (pokemonEquipo[i] != null)
                            num++;
                    return num;
                }
            }
            public bool HayAtaquesCustom()
            {
                const byte NOASIGNADO = 0x0;
                bool hayAtaquesCustom = false;
                for (int i = 0; i < pokemonEquipo.Length && !hayAtaquesCustom; i++)
                    if (pokemonEquipo[i] != null)
                        hayAtaquesCustom = pokemonEquipo[i].Move1 != NOASIGNADO || pokemonEquipo[i].Move2 != NOASIGNADO || pokemonEquipo[i].Move3 != NOASIGNADO || pokemonEquipo[i].Move4 != NOASIGNADO;
                return hayAtaquesCustom;
            }
            public bool HayObjetosEquipados()
            {
                const byte NOASIGNADO = 0x0;
                bool hayObjetosEquipados = false;
                for (int i = 0; i < pokemonEquipo.Length && !hayObjetosEquipados; i++)
                    if (pokemonEquipo[i] != null)
                        hayObjetosEquipados = pokemonEquipo[i].Item != NOASIGNADO;
                return hayObjetosEquipados;
            }

            public static Equipo GetEquipo(RomData rom, Hex indexEntrenador)
            {
                return GetEquipo(rom, GetBytesEntrenador(rom, indexEntrenador));
            }
            public static Equipo GetEquipo(RomData rom,BloqueBytes bloqueEntrenador)
            {
                if (rom == null || bloqueEntrenador == null)
                    throw new ArgumentNullException();

                byte[] bytesPokemonEquipo;
                Equipo equipoCargado = new Equipo();
                bool hayItems = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasHeldITem] & 0x2) != 0;
                bool hayAtaquesCustom = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves] & 0x1) != 0;
                int tamañoPokemon = hayAtaquesCustom ? 16 : 8;
                BloqueBytes bloqueDatosEquipo = BloqueBytes.GetBytes(rom.RomGBA, Offset.GetOffset(bloqueEntrenador.Bytes, (int)Entrenador.Posicion.PointerPokemonData), bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] * tamañoPokemon);
                equipoCargado.OffsetToDataPokemon = bloqueDatosEquipo.OffsetInicio;
               
                    for (int i = 0, f = bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons]; i < f; i++)
                    {
                        bytesPokemonEquipo = bloqueDatosEquipo.Bytes.SubArray(i * tamañoPokemon, tamañoPokemon);
                        equipoCargado.PokemonEquipo[i] = new Pokemon();
                        equipoCargado.PokemonEquipo[i].Especie =(ushort) (Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Especie, (int)Longitud.PokemonIndex)));
                        equipoCargado.PokemonEquipo[i].Nivel = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Nivel, (int)Longitud.Nivel).ReverseArray());
                        equipoCargado.PokemonEquipo[i].Ivs = bytesPokemonEquipo[(int)Posicion.Ivs];
                        if (hayItems)
                            equipoCargado.PokemonEquipo[i].Item = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Item, (int)Longitud.Item));
                        if (hayAtaquesCustom)
                        {
                            equipoCargado.PokemonEquipo[i].Move1 = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Move1, (int)Longitud.Ataque).ReverseArray());
                            equipoCargado.PokemonEquipo[i].Move2 = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Move2, (int)Longitud.Ataque).ReverseArray());
                            equipoCargado.PokemonEquipo[i].Move3 = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Move3, (int)Longitud.Ataque).ReverseArray());
                            equipoCargado.PokemonEquipo[i].Move4 = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Move4, (int)Longitud.Ataque).ReverseArray());
                        }
                    }

                return equipoCargado;
            }
            public static void SetEquipo(RomData rom,Hex indexEntrenador,Equipo equipo)
            {
                SetEquipo(rom, GetBytesEntrenador(rom, indexEntrenador), equipo);
            }
            public static void SetEquipo(RomData rom, BloqueBytes bloqueEntrenador, Equipo equipo)
            {
                if (rom == null || bloqueEntrenador == null || equipo == null)
                    throw new ArgumentNullException();
                if (equipo.NumeroPokemon == 0)
                    throw new ArgumentException("Se necesita como minimo poner un pokemon en el equipo del entrenador!");

                bool habiaAtaquesCustom;
                bool hayAtaquesCustom;
                BloqueBytes bloqueEquipo;
                BloqueBytes bloquePokemon;
                int tamañoEquipoPokemon;
                int tamañoPokemon;
                habiaAtaquesCustom = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves] & 0x1) != 0;
                tamañoPokemon = (habiaAtaquesCustom ? 16 : 8);
                tamañoEquipoPokemon = bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] * tamañoPokemon;
                //borro los datos antiguos de los pokemon :D
                BloqueBytes.RemoveBytes(rom.RomGBA, Offset.GetPointer(bloqueEntrenador.Bytes, (int)Entrenador.Posicion.PointerPokemonData), tamañoEquipoPokemon);
                //el numero de pokemon
                bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] = (byte)equipo.NumeroPokemon;
                //me falta saber como va eso de las operaciones AND, XOR,OR y sus negaciones
                //hasHeldItem
                bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasHeldITem] = (byte)(equipo.HayObjetosEquipados() ? 0x2 : 0x0);

                //hasCustmoMoves
                if (equipo.HayAtaquesCustom())
                    bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves]++;
                //es calculado aqui :D
                BloqueBytes.SetBytes(rom.RomGBA, bloqueEntrenador);//guardo los cambios
                hayAtaquesCustom = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves] & 0x1) != 0;
                tamañoPokemon = (habiaAtaquesCustom ? 16 : 8);
                tamañoEquipoPokemon = bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] * tamañoPokemon;
                bloqueEquipo = new BloqueBytes(BloqueBytes.SearchEmptyBytes(rom.RomGBA, bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons]), new byte[tamañoEquipoPokemon]);
                //pongo los pokemon en el bloque :D
                for (int i = 0, index = 0; i < equipo.PokemonEquipo.Length; i++)
                {
                    if (equipo[i] != null)//como pueden ser null pues
                    {
                        bloquePokemon = new BloqueBytes(index * tamañoPokemon, new byte[tamañoPokemon]);
                        //pongo los datos
                        bloquePokemon.Bytes.SetArray((int)Posicion.Especie, Serializar.GetBytes(equipo.PokemonEquipo[i].Especie).ReverseArray());
                        bloquePokemon.Bytes.SetArray((int)Posicion.Nivel, Serializar.GetBytes(equipo.PokemonEquipo[i].Nivel).ReverseArray());
                        bloquePokemon.Bytes.SetArray((int)Posicion.Item, Serializar.GetBytes(equipo.PokemonEquipo[i].Item));
                   //     bloquePokemon.Bytes[(int)Posicion.EsDeLaTerceraGeneracion] =(byte)( equipo.PokemonEquipo[i].EsDeLaTerceraGeneracion ? 0x1 : 0x0);
                        bloquePokemon.Bytes[(int)Posicion.Ivs] = equipo.PokemonEquipo[i].Ivs;
                        if (hayAtaquesCustom)
                        {
                            bloquePokemon.Bytes.SetArray((int)Posicion.Move1, Serializar.GetBytes(equipo.PokemonEquipo[i].Move1).ReverseArray());
                            bloquePokemon.Bytes.SetArray((int)Posicion.Move2, Serializar.GetBytes(equipo.PokemonEquipo[i].Move2).ReverseArray());
                            bloquePokemon.Bytes.SetArray((int)Posicion.Move3, Serializar.GetBytes(equipo.PokemonEquipo[i].Move3).ReverseArray());
                            bloquePokemon.Bytes.SetArray((int)Posicion.Move4, Serializar.GetBytes(equipo.PokemonEquipo[i].Move4).ReverseArray());
                        }
                        bloqueEquipo.Bytes.SetArray(bloquePokemon.OffsetInicio, bloquePokemon.Bytes);
                        index++;
                    }
                }
                BloqueBytes.SetBytes(rom.RomGBA, bloqueEquipo);
                Offset.SetOffset(rom.RomGBA, bloqueEntrenador.OffsetFin - (int)PokemonGBAFrameWork.Longitud.Offset, bloqueEquipo.OffsetInicio);//actualizo el offset de los datos :D
            }

        }

       

        enum Variables
        {
            Entrenadores
        }
        enum Posicion
        {
            HasCustomMoves = 0,
            HasHeldITem = 0,
            MoneyClass = 1,
            EsChica = 2,
            Musica = 2,
            Sprite = 3,
            Nombre = 4,
            //faltan [14,15]
            Item1 = 16,
            Item2 = 18,
            Item3 = 20,
            Item4 = 22,
            //faltan [23,27]
            Inteligencia = 28,
            NumeroPokemons = 32,
            PointerPokemonData = 36

        }
        enum Longitud
        {
            Nombre=10,
            Inteligencia=4,
            Item = 2,
        }
        public const byte MAXMUSIC = 0x7F;

        byte trainerClass;
        bool esUnaEntrenadora;
        byte musicaBatalla;
        byte spriteIndex;
        BloqueString nombre;//max 10
        //faltan 2 bytes [14,15]
        ushort item1;
        ushort item2;
        ushort item3;
        ushort item4;
        //faltan 4 bytes
        uint inteligencia;
        Equipo equipo;//mirar si como minimo se tiene que tener un pokemon!
        static Entrenador()
        {
            Zona zonaEntrenador = new Zona(Variables.Entrenadores);
            //añado las zonas :D
            zonaEntrenador.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x3587C);
            zonaEntrenador.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x3587C);

            zonaEntrenador.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0xD890);
            zonaEntrenador.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0xD890);

            zonaEntrenador.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0xDA5C);
            zonaEntrenador.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0xDA5C);

            zonaEntrenador.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0xFC00,0xFC14);
            zonaEntrenador.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0xFC00,0xFC14);

            zonaEntrenador.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0xFB70);
            zonaEntrenador.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0xFB70);

            Zona.DiccionarioOffsetsZonas.Add(zonaEntrenador);
        }
        public byte TrainerClass
        {
            get
            {
                return trainerClass;
            }

            set
            {
                trainerClass = value;
            }
        }

        public bool EsUnaEntrenadora
        {
            get
            {
                return esUnaEntrenadora;
            }

            set
            {
                esUnaEntrenadora = value;
            }
        }

        public byte MusicaBatalla
        {
            get
            {
                return musicaBatalla;
            }

            set
            {
                if (value > MAXMUSIC) throw new ArgumentOutOfRangeException();
                musicaBatalla = value;
            }
        }

        public byte SpriteIndex
        {
            get
            {
                return spriteIndex;
            }

            set
            {
                spriteIndex = value;
            }
        }

        public BloqueString Nombre
        {
            get
            {
                return nombre;
            }

            private set
            {
                nombre = value;
            }
        }

        public ushort Item1
        {
            get
            {
                return item1;
            }

            set
            {
                item1 = value;
            }
        }

        public ushort Item2
        {
            get
            {
                return item2;
            }

            set
            {
                item2 = value;
            }
        }

        public ushort Item3
        {
            get
            {
                return item3;
            }

            set
            {
                item3 = value;
            }
        }

        public ushort Item4
        {
            get
            {
                return item4;
            }

            set
            {
                item4 = value;
            }
        }

        public uint Inteligencia
        {
            get
            {
                return inteligencia;
            }

            set
            {
                inteligencia = value;
            }
        }

        public Equipo Pokemon
        {
            get
            {
                return equipo;
            }

             set
            {
                if (value == null) throw new ArgumentNullException();
                equipo = value;
            }
        }

        public uint CalcularDinero(RomData rom)
        {
            uint tamañoPokemonBytes = 8;
            if (Pokemon.HayAtaquesCustom())
            {
                tamañoPokemonBytes = 16;
            }
            return (TrainerClass * (uint)(rom.RomGBA.Datos[((uint)Pokemon.NumeroPokemon * tamañoPokemonBytes + Pokemon.OffsetToDataPokemon - tamañoPokemonBytes + 2)] << 2));
        }
        public override string ToString()
        {
            return Nombre;
        }
        public static Hex GetNumeroDeEntrenadores(RomData rom)
        {
            const byte TAMAÑOENTRENADOR = 0x28,POSICIONPOINTERDATOS= 0x24;

            ushort num = 1;           
            Hex posicionEntrenadores = Zona.GetOffset(rom.RomGBA, Variables.Entrenadores, rom.Edicion, rom.Compilacion);
            Hex posicionActual = posicionEntrenadores + POSICIONPOINTERDATOS+TAMAÑOENTRENADOR;
            while (Offset.GetOffset(rom.RomGBA, posicionActual) > 0)
            {
                num++;
                posicionActual += TAMAÑOENTRENADOR;
            }
            return (uint)num-1;
        }
        public static Entrenador GetEntrenador(RomData rom,Hex index)
        {

            BloqueBytes bytesEntrenador = GetBytesEntrenador(rom, index);
            Entrenador entranadorCargado = new Entrenador();
           
            //le pongo los datos
            entranadorCargado.EsUnaEntrenadora = (bytesEntrenador.Bytes[(int)Posicion.EsChica] & 0x80) != 0;
            entranadorCargado.MusicaBatalla =(byte)(bytesEntrenador.Bytes[(int)Posicion.Musica] & MAXMUSIC);
            entranadorCargado.TrainerClass = bytesEntrenador.Bytes[(int)Posicion.MoneyClass];//quizas es la clase de entrenador :D y no el rango de dinero que da...
            entranadorCargado.Nombre = BloqueString.GetString(bytesEntrenador, (int)Posicion.Nombre, (int)Longitud.Nombre,true);
            entranadorCargado.Inteligencia = Serializar.ToUInt(bytesEntrenador.Bytes.SubArray((int)Posicion.Inteligencia, (int)Longitud.Inteligencia));
            entranadorCargado.Item1= Serializar.ToUShort(bytesEntrenador.Bytes.SubArray((int)Posicion.Item1, (int)Longitud.Item));
            entranadorCargado.Item2 = Serializar.ToUShort(bytesEntrenador.Bytes.SubArray((int)Posicion.Item2, (int)Longitud.Item));
            entranadorCargado.Item3 = Serializar.ToUShort(bytesEntrenador.Bytes.SubArray((int)Posicion.Item3, (int)Longitud.Item));
            entranadorCargado.Item4 = Serializar.ToUShort(bytesEntrenador.Bytes.SubArray((int)Posicion.Item4, (int)Longitud.Item));
            entranadorCargado.SpriteIndex = bytesEntrenador.Bytes[(int)Posicion.Sprite];
            entranadorCargado.Pokemon = Equipo.GetEquipo(rom, bytesEntrenador);
    
            
            return entranadorCargado;

        }

        public static BloqueBytes GetBytesEntrenador(RomData rom, Hex index)
        {
            const byte TAMAÑOENTRENADOR = 0x28;
            Hex posicionEntrenadores = Zona.GetOffset(rom.RomGBA, Variables.Entrenadores, rom.Edicion, rom.Compilacion);
            Hex poscionEntrenador = posicionEntrenadores+TAMAÑOENTRENADOR + TAMAÑOENTRENADOR * index;//el primero me lo salto porque no es un entrenador...
            return BloqueBytes.GetBytes(rom.RomGBA, poscionEntrenador, TAMAÑOENTRENADOR);
        }

        public static Entrenador[] GetEntrenadores(RomData rom)
        {
            Entrenador[] entrenadores = new Entrenador[GetNumeroDeEntrenadores(rom)];
            for (int i = 0; i < entrenadores.Length; i++)
                    entrenadores[i] = GetEntrenador(rom, i);

              
            return entrenadores;
        }
        public static void SetEntrenador(RomData rom,Hex index,Entrenador entrenador)
        {
            BloqueBytes bloqueEntrenador = GetBytesEntrenador(rom, index);
            bloqueEntrenador.Bytes[(int)Posicion.Musica] = entrenador.MusicaBatalla;

            if (entrenador.EsUnaEntrenadora)
            {
                bloqueEntrenador.Bytes[(int)Posicion.EsChica] += 0x80;//va asi???por mirar
            }
            
            bloqueEntrenador.Bytes[(int)Posicion.MoneyClass]= entrenador.TrainerClass;
            entrenador.Nombre.OffsetInicio = bloqueEntrenador.OffsetInicio + (int)Posicion.Nombre;
            BloqueString.SetString(rom.RomGBA, entrenador.Nombre);
            bloqueEntrenador.Bytes.SetArray((int)Posicion.Inteligencia, Serializar.GetBytes(entrenador.Inteligencia));
            bloqueEntrenador.Bytes.SetArray((int)Posicion.Item1, Serializar.GetBytes(entrenador.Item1));
            bloqueEntrenador.Bytes.SetArray((int)Posicion.Item2, Serializar.GetBytes(entrenador.Item2));
            bloqueEntrenador.Bytes.SetArray((int)Posicion.Item3, Serializar.GetBytes(entrenador.Item3));
            bloqueEntrenador.Bytes.SetArray((int)Posicion.Item4, Serializar.GetBytes(entrenador.Item4));
            bloqueEntrenador.Bytes[(int)Posicion.Sprite]= entrenador.SpriteIndex;
            //pongo los datos
           // Equipo.SetEquipo(rom, bloqueEntrenador,entrenador.Pokemon);
        }
        public static void SetEntrenadores(RomData romData)
        {
            for (int i = 0; i < romData.Entrenadores.Count; i++)
                SetEntrenador(romData, i, romData.Entrenadores[i]);
        }
    }

  





}
