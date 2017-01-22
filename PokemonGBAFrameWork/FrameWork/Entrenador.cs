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
        public SpritesEntrenadores()
        {
            const byte MAX = 0xFF;
            sprites = new BloqueImagen[MAX];
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

            entrenadores = new SpritesEntrenadores();

            for (int i = 0; i < entrenadores.Total; i++)
                entrenadores[i] = BloqueImagen.GetBloqueImagen(rom.RomGBA, Zona.GetOffset(rom.RomGBA, Variables.SpriteImg,rom.Edicion,rom.Compilacion) + i << 3, Zona.GetOffset(rom.RomGBA, Variables.SpritePaleta, rom.Edicion, rom.Compilacion) + i << 3);
            return entrenadores;
        }
        public static void SetSpritesEntrenadores(RomData rom,SpritesEntrenadores spritesEntrenadores)
        {
            if (rom == null||spritesEntrenadores==null)
                throw new ArgumentNullException();

            for (int i = 0; i < spritesEntrenadores.Total; i++)
            {
                if (spritesEntrenadores[i] == null)
                    throw new NullReferenceException("Siempre tiene que haber imagen de entrenador...");//mas adelante hacer imagen vacia :D
            }

            for (int i = 0; i < spritesEntrenadores.Total; i++)
            {
                //actualizo el pointer de las tablas
                //tabla img
                Offset.SetOffset(rom.RomGBA,Zona.GetOffset(rom.RomGBA, Variables.SpriteImg, rom.Edicion, rom.Compilacion) + i << 3,spritesEntrenadores[i].OffsetInicio);
                //tabla paleta
                Offset.SetOffset(rom.RomGBA,Zona.GetOffset(rom.RomGBA, Variables.SpritePaleta, rom.Edicion, rom.Compilacion) + i << 3, spritesEntrenadores[i].Paletas[0].OffsetPointerPaleta);
                //pongo los datos
                BloqueImagen.SetBloqueImagen(rom.RomGBA, spritesEntrenadores[i]);


            }
     
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
                BloqueBytes bloqueDatosEquipo = BloqueBytes.GetBytes(rom.RomGBA, Offset.GetPointer(bloqueEntrenador.Bytes, (int)Entrenador.Posicion.PointerPokemonData), bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] * tamañoPokemon);
                
                for (int i = 0, f = bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons]; i < f; i++)
                {
                    bytesPokemonEquipo = bloqueDatosEquipo.Bytes.SubArray(i * tamañoPokemon, tamañoPokemon);
                    equipoCargado.PokemonEquipo[i].PokemonIndex = bytesPokemonEquipo[(int)Posicion.IndexPokemon];
                    equipoCargado.PokemonEquipo[i].Nivel = (ushort)(Hex)bytesPokemonEquipo.SubArray((int)Posicion.Nivel, (int)Longitud.Nivel);
                    equipoCargado.PokemonEquipo[i].Ivs = bytesPokemonEquipo[(int)Posicion.Ivs];
                    if(hayItems)
                       equipoCargado.PokemonEquipo[i].Item= (ushort)(Hex)bytesPokemonEquipo.SubArray((int)Posicion.Item, (int)Longitud.Item);
                    if(hayAtaquesCustom)
                    {
                        equipoCargado.PokemonEquipo[i].Move1 = (ushort)(Hex)bytesPokemonEquipo.SubArray((int)Posicion.Move1, (int)Longitud.Ataque);
                        equipoCargado.PokemonEquipo[i].Move2 = (ushort)(Hex)bytesPokemonEquipo.SubArray((int)Posicion.Move2, (int)Longitud.Ataque);
                        equipoCargado.PokemonEquipo[i].Move3 = (ushort)(Hex)bytesPokemonEquipo.SubArray((int)Posicion.Move3, (int)Longitud.Ataque);
                        equipoCargado.PokemonEquipo[i].Move4 = (ushort)(Hex)bytesPokemonEquipo.SubArray((int)Posicion.Move4, (int)Longitud.Ataque);
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

                bool habiaAtaquesCustom = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves] & 0x1) != 0;
                //borro los datos antiguos de los pokemon :D
                BloqueBytes.RemoveBytes(rom.RomGBA, Offset.GetPointer(bloqueEntrenador.Bytes, (int)Entrenador.Posicion.PointerPokemonData), bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons]*(habiaAtaquesCustom?16:8));
                //el numero de pokemon
                bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] = (byte)equipo.NumeroPokemon;
                //hasHeldItem
                //bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasHeldITem];

                //hasCustmoMoves
                //bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves]
                //es calculado aqui :D
                BloqueBytes.SetBytes(rom.RomGBA, bloqueEntrenador);//guardo los cambios
               
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

        public static Hex GetNumeroDeEntrenadores(RomData rom)
        {
            const byte TAMAÑOENTRENADOR = 0x28,POSICIONPOINTERDATOS= 0x24;

            ushort num = 0;           
            Hex posicionEntrenadores = Zona.GetOffset(rom.RomGBA, Variables.Entrenadores, rom.Edicion, rom.Compilacion);

            while (Offset.GetPointer(rom.RomGBA, posicionEntrenadores + num * TAMAÑOENTRENADOR + POSICIONPOINTERDATOS) > 0)
                num++;
            return (uint)num;
        }
        public static Entrenador GetEntrenador(RomData rom,Hex index)
        {

            BloqueBytes bytesEntrenador = GetBytesEntrenador(rom, index);
            Entrenador entranadorCargado = new Entrenador();
           
            //le pongo los datos
            entranadorCargado.EsUnaEntrenadora = (bytesEntrenador.Bytes[(int)Posicion.EsChica] & 0x80) != 0;
            entranadorCargado.MusicaBatalla =(byte)(bytesEntrenador.Bytes[(int)Posicion.Musica] & 0x7F);
            entranadorCargado.MoneyClass = bytesEntrenador.Bytes[(int)Posicion.MoneyClass];
            entranadorCargado.Nombre = BloqueString.GetString(bytesEntrenador, (int)Posicion.Nombre, (int)Longitud.Nombre);
            entranadorCargado.Inteligencia = (uint)(Hex)bytesEntrenador.Bytes.SubArray((int)Posicion.Inteligencia, (int)Longitud.Inteligencia);
            entranadorCargado.Item1= (ushort)(Hex)bytesEntrenador.Bytes.SubArray((int)Posicion.Item1, (int)Longitud.Item);
            entranadorCargado.Item2 = (ushort)(Hex)bytesEntrenador.Bytes.SubArray((int)Posicion.Item2, (int)Longitud.Item);
            entranadorCargado.Item3 = (ushort)(Hex)bytesEntrenador.Bytes.SubArray((int)Posicion.Item3, (int)Longitud.Item);
            entranadorCargado.Item4 = (ushort)(Hex)bytesEntrenador.Bytes.SubArray((int)Posicion.Item4, (int)Longitud.Item);
            entranadorCargado.SpriteIndex = bytesEntrenador.Bytes[(int)Posicion.Sprite];
            entranadorCargado.Pokemon =Equipo.GetEquipo(rom, index);
            
            
            return entranadorCargado;

        }

        public static BloqueBytes GetBytesEntrenador(RomData rom, Hex index)
        {
            const byte TAMAÑOENTRENADOR = 0x28;
            Hex posicionEntrenadores = Zona.GetOffset(rom.RomGBA, Variables.Entrenadores, rom.Edicion, rom.Compilacion);
            Hex poscionEntrenador = posicionEntrenadores + TAMAÑOENTRENADOR * index;
            return BloqueBytes.GetBytes(rom.RomGBA, posicionEntrenadores, TAMAÑOENTRENADOR);
        }

        public static Entrenador[] GetEntrenadores(RomData rom)
        {
            Entrenador[] entrenadores = new Entrenador[GetNumeroDeEntrenadores(rom)];
            for (int i = 0; i < entrenadores.Length; i++)
                entrenadores[i] = GetEntrenador(rom, i);
            return entrenadores;
        }
    }

    /*
     total 40 bytes
pos->rom[byte]&0x1!=0 si es true hay movesets y cambia de 8 a 16  movesetIndex
pos->rom[byte]&0x2!=0 si es true "heldItems"
pos+1->clase(MoneyClass) entrenador byte
pos+2->rom[byte]&0x80!=0 pregunta si es chica
pos+2->rom[byte]&0x7F  musica
pos+3->entrenador sprite byte
pos+4->NombreEntrenador->10bytes
//faltan 2bytes pos+14,pos+15
pos+16->item1 2bytes
pos+18->item2 2bytes
pos+20->item3 2bytes
pos+22->item4 2bytes
//faltan 4bytes 
pos+28->AI entrenador 4 bytes
pos+32->Numero de pokemons (uint...) 4 bytes
pos+36->Pointer to pokemon data 4bytes
         */

    /*
     class money se tiene que buscar....
primero se lee un byte de la rom en la posicion de la classMoney luego mientras ser diferente a 0xFF mirara si coincide con el entrenador si coincide la ha encontrado y el dinero se saca de la classMoney+index+1 el index empieza con 0 y cada vez que no encuentra el index suma 4 y el byte cambia classLocation+index


trainerCash ->1byte->rom[classMoney+index+1]

si no lo encuentra el trainerCash es 0

     */



}
