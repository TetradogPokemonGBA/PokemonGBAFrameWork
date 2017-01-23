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
    public class SpritesEntrenadores
    {
     
        enum Variables
        {
            SpriteImg,
            SpritePaleta
        }
        BloqueImagen[] sprites;//de momento no se si hay FF o es solo el maximo
        static SpritesEntrenadores()
        {
            Zona zonaImg = new Zona(Variables.SpriteImg);
            Zona zonaPaleta = new Zona(Variables.SpritePaleta);
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
            Zona.DiccionarioOffsetsZonas.Add(zonaImg);
            Zona.DiccionarioOffsetsZonas.Add(zonaPaleta);
        }
        public SpritesEntrenadores(int numeroDeEntrenadores)
        {
            sprites = new BloqueImagen[numeroDeEntrenadores];
        }
        public BloqueImagen this[int index]
        {
            get { return sprites[index]; }
            set
            {
                sprites[index] = value;
            }
        }
        public BloqueImagen this[Entrenador entrenador]
        {
            get { return this[entrenador.SpriteIndex]; }
        }
        public int Total
        {
            get { return sprites.Length; }
        }
        public static SpritesEntrenadores GetSpritesEntrenadores(RomData rom)
        {
            SpritesEntrenadores entrenadores;

            if (rom == null)
                throw new ArgumentNullException();

            entrenadores = new SpritesEntrenadores(GetNumeroDeSpritesDeEntrenador(rom));

            for (int i = 0; i < entrenadores.Total; i++)
                entrenadores[i] = BloqueImagen.GetBloqueImagen(rom.RomGBA, Zona.GetOffset(rom.RomGBA, Variables.SpriteImg,rom.Edicion,rom.Compilacion) + i*2 *(int)Longitud.Offset, Zona.GetOffset(rom.RomGBA, Variables.SpritePaleta, rom.Edicion, rom.Compilacion) + i *2* (int)Longitud.Offset);
            return entrenadores;
        }

        public static int GetNumeroDeSpritesDeEntrenador(RomData rom)
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
            if (rom == null|| rom.SpritesEntrenadores == null)
                throw new ArgumentNullException();

            for (int i = 0; i < rom.SpritesEntrenadores.Total; i++)
            {
                if (rom.SpritesEntrenadores[i] == null)
                    throw new NullReferenceException("Siempre tiene que haber imagen de entrenador...");//mas adelante hacer imagen vacia :D
            }

            for (int i = 0; i < rom.SpritesEntrenadores.Total; i++)
            {
                //actualizo el pointer de las tablas
                //tabla img
                Offset.SetOffset(rom.RomGBA,Zona.GetOffset(rom.RomGBA, Variables.SpriteImg, rom.Edicion, rom.Compilacion) + i << 3, rom.SpritesEntrenadores[i].OffsetInicio);
                //tabla paleta
                Offset.SetOffset(rom.RomGBA,Zona.GetOffset(rom.RomGBA, Variables.SpritePaleta, rom.Edicion, rom.Compilacion) + i << 3, rom.SpritesEntrenadores[i].Paletas[0].OffsetPointerPaleta);
                //pongo los datos
                BloqueImagen.SetBloqueImagen(rom.RomGBA, rom.SpritesEntrenadores[i]);


            }
     
        }

        public void SetSpritesEntrenadores(RomGBA romGBA, SpritesEntrenadores spritesEntrenadores)
        {
            SetSpritesEntrenadores(new RomData(romGBA) { SpritesEntrenadores = spritesEntrenadores });
        }

    }
    public class Entrenador//ocupa 40bytes
    {
        public class Equipo//5A??max
        {
            public class Pokemon//ocupa 8 o 16 bytes dependiendo de si tiene o no movimientos custom el entrenador activado
            {
                ushort pokemonIndex;//cuarto byte
                byte ivs;//primer byte
                ushort level;//segundo byte no se porque son dos bytes...tendria que ser 1...
                ushort item;//sexto byte
                            //a partir del byte 8//puede que los movimientos no esten cambiados por lo tanto no estarian...
                ushort move1;
                ushort move2;
                ushort move3;
                ushort move4;

                public ushort PokemonIndex
                {
                    get
                    {
                        return pokemonIndex;
                    }

                    set
                    {
                        pokemonIndex = value;
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
                //falta los evs....o va con los ivs...quizas hace falta poner en la rom una rutina...
            }
            enum Posicion
            {
                Ivs=0,
                Nivel = 1,
                IndexPokemon =3,
                Item=5,
                Move1=7,
                Move2=9,
                Move3=11,
                Move4=13
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
                
                for (int i = 0, f = bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons]; i < f; i++)
                {
                    bytesPokemonEquipo = bloqueDatosEquipo.Bytes.SubArray(i * tamañoPokemon, tamañoPokemon);
                    equipoCargado.PokemonEquipo[i] = new Pokemon();
                    equipoCargado.PokemonEquipo[i].PokemonIndex =(ushort) Serializar.ToShort(bytesPokemonEquipo.SubArray((int)Posicion.IndexPokemon, (int)Longitud.PokemonIndex).Reverse().ToArray());
                    equipoCargado.PokemonEquipo[i].Nivel = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Nivel, (int)Longitud.Nivel).Reverse().ToArray());
                    equipoCargado.PokemonEquipo[i].Ivs = bytesPokemonEquipo[(int)Posicion.Ivs];
                    if(hayItems)
                       equipoCargado.PokemonEquipo[i].Item= Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Item, (int)Longitud.Item).Reverse().ToArray());
                    if(hayAtaquesCustom)
                    {
                        equipoCargado.PokemonEquipo[i].Move1 = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Move1, (int)Longitud.Ataque).Reverse().ToArray());
                        equipoCargado.PokemonEquipo[i].Move2 = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Move2, (int)Longitud.Ataque).Reverse().ToArray());
                        equipoCargado.PokemonEquipo[i].Move3 = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Move3, (int)Longitud.Ataque).Reverse().ToArray());
                        equipoCargado.PokemonEquipo[i].Move4 = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Move4, (int)Longitud.Ataque).Reverse().ToArray());
                    }
                }

                return equipoCargado;
            }
            public static void SetEquipo(RomData rom,Hex indexEntrenador,Equipo equipo)
            {
                SetEquipo(rom, GetBytesEntrenador(rom, indexEntrenador), equipo);
            }
            public static void SetEquipo(RomData rom,BloqueBytes bloqueEntrenador,Equipo equipo)
            {

                bool habiaAtaquesCustom;
                bool hayAtaquesCustom;
                BloqueBytes bloqueEqupo;
                BloqueBytes bloquePokemon;
                int tamañoPokemon;
                habiaAtaquesCustom = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves] & 0x1) != 0;
                tamañoPokemon = bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] * (habiaAtaquesCustom ? 16 : 8);
                //borro los datos antiguos de los pokemon :D
                BloqueBytes.RemoveBytes(rom.RomGBA, Offset.GetPointer(bloqueEntrenador.Bytes, (int)Entrenador.Posicion.PointerPokemonData),tamañoPokemon);
                //el numero de pokemon
                bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] = (byte)equipo.NumeroPokemon;
                //me falta saber como va eso de las operaciones AND, XOR,OR y sus negaciones
                //hasHeldItem
                bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasHeldITem]=(byte)(equipo.HayObjetosEquipados()?0x2:0x0);

                //hasCustmoMoves
                if (equipo.HayAtaquesCustom())
                    bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves]++;
                //es calculado aqui :D
                BloqueBytes.SetBytes(rom.RomGBA, bloqueEntrenador);//guardo los cambios
                hayAtaquesCustom = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves] & 0x1) != 0;
                tamañoPokemon = bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] * (hayAtaquesCustom ? 16 : 8);
                bloqueEqupo = new BloqueBytes(BloqueBytes.SearchEmptyBytes(rom.RomGBA, bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons]), new byte[tamañoPokemon]);
                //pongo los pokemon en el bloque :D
                for(int i=0,index=0;i<equipo.PokemonEquipo.Length;i++)
                {
                    if (equipo[i] != null)//como pueden ser null pues
                    {
                        bloquePokemon = new BloqueBytes(index * tamañoPokemon, new byte[tamañoPokemon]);
                        //pongo los datos
                        bloquePokemon.Bytes.SetArray((int)Posicion.IndexPokemon, (Hex)(short)equipo.PokemonEquipo[i].PokemonIndex);
                        bloquePokemon.Bytes.SetArray((int)Posicion.Nivel, (Hex)(short)equipo.PokemonEquipo[i].Nivel);
                        bloquePokemon.Bytes.SetArray((int)Posicion.Item, (Hex)(short)equipo.PokemonEquipo[i].Item);
                        bloquePokemon.Bytes[(int)Posicion.Ivs] = equipo.PokemonEquipo[i].Ivs;
                        if (hayAtaquesCustom)
                        {
                            bloquePokemon.Bytes.SetArray((int)Posicion.Move1, (Hex)(short)equipo.PokemonEquipo[i].Move1);
                            bloquePokemon.Bytes.SetArray((int)Posicion.Move2, (Hex)(short)equipo.PokemonEquipo[i].Move2);
                            bloquePokemon.Bytes.SetArray((int)Posicion.Move3, (Hex)(short)equipo.PokemonEquipo[i].Move3);
                            bloquePokemon.Bytes.SetArray((int)Posicion.Move4, (Hex)(short)equipo.PokemonEquipo[i].Move4);
                        }
                        bloqueEqupo.Bytes.SetArray(bloquePokemon.OffsetInicio, bloquePokemon.Bytes);
                        index++;
                    }
                }
                BloqueBytes.SetBytes(rom.RomGBA, bloqueEqupo);
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
        byte moneyClass;
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
        public byte MoneyClass
        {
            get
            {
                return moneyClass;
            }

            set
            {
                moneyClass = value;
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
            {//falta controlar el maximo :)
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

            private set
            {
                equipo = value;
            }
        }

        public uint CalcularDinero(RomGBA rom)
        {
            uint tamañoPokemonBytes = 8;
            if (Pokemon.HayAtaquesCustom())
            {
                tamañoPokemonBytes = 16;
            }
            return (MoneyClass * (uint)(rom.Datos[((uint)Pokemon.NumeroPokemon * tamañoPokemonBytes + Pokemon.OffsetToDataPokemon - tamañoPokemonBytes + 2)] << 2));
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
            entranadorCargado.MusicaBatalla =(byte)(bytesEntrenador.Bytes[(int)Posicion.Musica] & 0x7F);
            entranadorCargado.MoneyClass = bytesEntrenador.Bytes[(int)Posicion.MoneyClass];
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
            Hex poscionEntrenador = posicionEntrenadores+TAMAÑOENTRENADOR + TAMAÑOENTRENADOR * index;
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
            
            bloqueEntrenador.Bytes[(int)Posicion.MoneyClass]= entrenador.MoneyClass;
            entrenador.Nombre.OffsetInicio = bloqueEntrenador.OffsetInicio + (int)Posicion.Nombre;
            BloqueString.SetString(rom.RomGBA, entrenador.Nombre);
            bloqueEntrenador.Bytes.SetArray((int)Posicion.Inteligencia, (Hex)entrenador.Inteligencia);
            bloqueEntrenador.Bytes.SetArray((int)Posicion.Item1, (Hex)(short)entrenador.Item1);
            bloqueEntrenador.Bytes.SetArray((int)Posicion.Item2, (Hex)(short)entrenador.Item2);
            bloqueEntrenador.Bytes.SetArray((int)Posicion.Item3, (Hex)(short)entrenador.Item3);
            bloqueEntrenador.Bytes.SetArray((int)Posicion.Item4, (Hex)(short)entrenador.Item4);
            bloqueEntrenador.Bytes[(int)Posicion.Sprite]= entrenador.SpriteIndex;
            //pongo los datos
            Equipo.SetEquipo(rom, bloqueEntrenador,entrenador.Pokemon);
        }
        public static void SetEntrenadores(RomData romData)
        {
            for (int i = 0; i < romData.Entrenadores.Count; i++)
                SetEntrenador(romData, i, romData.Entrenadores[i]);
        }
    }

  

    /*
     class money se tiene que buscar....
primero se lee un byte de la rom en la posicion de la classMoney luego mientras ser diferente a 0xFF mirara si coincide con el entrenador si coincide la ha encontrado y el dinero se saca de la classMoney+index+1 el index empieza con 0 y cada vez que no encuentra el index suma 4 y el byte cambia classLocation+index


trainerCash ->1byte->rom[classMoney+index+1]

si no lo encuentra el trainerCash es 0

     */



}
