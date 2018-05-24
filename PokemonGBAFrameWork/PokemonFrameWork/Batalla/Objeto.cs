using Gabriel.Cat.S.Binaris;
using PokemonGBAFrameWork.Objeto;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class ObjetoCompleto:IElementoBinarioComplejo
    {

        //estas dos zonas descubiertas con LSAs Complete Item Editor usando los offsets que da para facilitar la investigación
        //creditos a LocksmithArmy por la app :)
        public static readonly Zona ZonaFieldEffect;
        public static readonly Zona ZonaBattleScript;
        public static readonly ElementoBinario Serializador = ElementoBinarioNullable.GetElementoBinario(typeof(ObjetoCompleto));

        static ObjetoCompleto()
        {
        

            ZonaFieldEffect = new Zona("Field effect");
            ZonaBattleScript = new Zona("Battle Script");




            //field effect
            ZonaFieldEffect.Add(EdicionPokemon.RojoFuegoUsa, 0x3DC510, 0x3DC580);
            ZonaFieldEffect.Add(EdicionPokemon.VerdeHojaUsa, 0x3DC34C, 0x3DC3BC);
            ZonaFieldEffect.Add(EdicionPokemon.VerdeHojaEsp, 0x3D6274);
            ZonaFieldEffect.Add(EdicionPokemon.RojoFuegoEsp, 0x3D6438);
            ZonaFieldEffect.Add(EdicionPokemon.EsmeraldaUsa, 0x584E88);
            ZonaFieldEffect.Add(EdicionPokemon.EsmeraldaEsp, 0x587884);
            ZonaFieldEffect.Add(EdicionPokemon.RubiUsa, 0x3C5580, 0x3C559C);
            ZonaFieldEffect.Add(EdicionPokemon.ZafiroUsa, 0x3C55D8, 0x3C55F8);
            ZonaFieldEffect.Add(EdicionPokemon.RubiEsp, 0x3C9018);
            ZonaFieldEffect.Add(EdicionPokemon.ZafiroEsp, 0x3C8D54);
            //Battle script
            ZonaBattleScript.Add(EdicionPokemon.EsmeraldaUsa, 0x5839F0);
            ZonaBattleScript.Add(EdicionPokemon.EsmeraldaEsp, 0x5863EC);
            //No estoy seguro me guio por un pointer que lleva a unos datos que luego busco en las roms y vuelvo a atrás :)
            ZonaBattleScript.Add(EdicionPokemon.RojoFuegoUsa, 0x3DEF08, 0x3DEF78);
            ZonaBattleScript.Add(EdicionPokemon.VerdeHojaUsa, 0x3DED44, 0x3DEDB4);
            ZonaBattleScript.Add(EdicionPokemon.VerdeHojaEsp, 0x3D8C6C);
            ZonaBattleScript.Add(EdicionPokemon.RojoFuegoEsp, 0x3D8E30);
            ZonaBattleScript.Add(EdicionPokemon.RubiUsa, 0x3C55B4, 0x3C55D0);
            ZonaBattleScript.Add(EdicionPokemon.ZafiroUsa, 0x3C560C, 0x3C562C);
            ZonaBattleScript.Add(EdicionPokemon.ZafiroEsp, 0xC9554);
            ZonaBattleScript.Add(EdicionPokemon.RubiEsp, 0xC9554);
        }
        public ObjetoCompleto()
        {

        }
        #region Propiedades
    
        public Sprite Sprite { get; set; }
        public Datos Datos { get; set; }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

        #endregion


        public override string ToString()
        {
            return Datos.Nombre;
        }



        public static ObjetoCompleto GetObjeto(RomGba rom, int index)
        {

            ObjetoCompleto objeto = new ObjetoCompleto();
            objeto.Datos = Datos.GetDatos(rom, index);
            objeto.Sprite = Sprite.GetSprite(rom, index);
            return objeto;

        }

        public static ObjetoCompleto[] GetObjetos(RomGba rom)
        {
            ObjetoCompleto[] objetos = new ObjetoCompleto[Datos.GetTotal(rom)];
            for (int i = 0; i < objetos.Length; i++)
                objetos[i] = GetObjeto(rom, i);
            return objetos;
        }

        public static void SetObjeto(RomGba rom, int index, ObjetoCompleto objeto)
        {

            Objeto.Datos.SetDatos(rom, index, objeto.Datos);
            Objeto.Sprite.SetSprite(rom, index, objeto.Sprite);


        }

        public static void SetObjetos(RomGba rom, IList<ObjetoCompleto> objetos)
        {
            IList<Sprite> sprites = new List<Sprite>();
            IList<Datos> datos = new List<Datos>();
            for (int i = 0; i < objetos.Count; i++)
            {
                sprites.Add(objetos[i].Sprite);
                datos.Add(objetos[i].Datos);
            }
            Datos.SetDatos(rom, datos);
            Sprite.SetSprite(rom, sprites);

        }


    }


}
