using Gabriel.Cat.S.Binaris;
using Poke;
using PokemonGBAFramework;
using PokemonGBAFrameWork.Objeto;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class ObjetoCompleto
    {
        public const byte ID = 0x9;
        //estas dos zonas descubiertas con LSAs Complete Item Editor usando los offsets que da para facilitar la investigación
        //creditos a LocksmithArmy por la app :)
        public static readonly Zona ZonaFieldEffect;
        public static readonly Zona ZonaBattleScript;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<ObjetoCompleto>();
        
        static ObjetoCompleto()
        {
        

            ZonaFieldEffect = new Zona("Field effect");
            ZonaBattleScript = new Zona("Battle Script");




            //field effect
            ZonaFieldEffect.Add(EdicionPokemon.RojoFuegoUsa10, 0x3DC510, 0x3DC580);
            ZonaFieldEffect.Add(EdicionPokemon.VerdeHojaUsa10, 0x3DC34C, 0x3DC3BC);
            ZonaFieldEffect.Add(EdicionPokemon.VerdeHojaEsp10, 0x3D6274);
            ZonaFieldEffect.Add(EdicionPokemon.RojoFuegoEsp10, 0x3D6438);
            ZonaFieldEffect.Add(EdicionPokemon.EsmeraldaUsa10, 0x584E88);
            ZonaFieldEffect.Add(EdicionPokemon.EsmeraldaEsp10, 0x587884);
            ZonaFieldEffect.Add(EdicionPokemon.RubiUsa10, 0x3C5580, 0x3C559C);
            ZonaFieldEffect.Add(EdicionPokemon.ZafiroUsa10, 0x3C55D8, 0x3C55F8);
            ZonaFieldEffect.Add(EdicionPokemon.RubiEsp10, 0x3C9018);
            ZonaFieldEffect.Add(EdicionPokemon.ZafiroEsp10, 0x3C8D54);
            //Battle script
            ZonaBattleScript.Add(EdicionPokemon.EsmeraldaUsa10, 0x5839F0);
            ZonaBattleScript.Add(EdicionPokemon.EsmeraldaEsp10, 0x5863EC);
            //No estoy seguro me guio por un pointer que lleva a unos datos que luego busco en las roms y vuelvo a atrás :)
            ZonaBattleScript.Add(EdicionPokemon.RojoFuegoUsa10, 0x3DEF08, 0x3DEF78);
            ZonaBattleScript.Add(EdicionPokemon.VerdeHojaUsa10, 0x3DED44, 0x3DEDB4);
            ZonaBattleScript.Add(EdicionPokemon.VerdeHojaEsp10, 0x3D8C6C);
            ZonaBattleScript.Add(EdicionPokemon.RojoFuegoEsp10, 0x3D8E30);
            ZonaBattleScript.Add(EdicionPokemon.RubiUsa10, 0x3C55B4, 0x3C55D0);
            ZonaBattleScript.Add(EdicionPokemon.ZafiroUsa10, 0x3C560C, 0x3C562C);
            ZonaBattleScript.Add(EdicionPokemon.ZafiroEsp10, 0xC9554);
            ZonaBattleScript.Add(EdicionPokemon.RubiEsp10, 0xC9554);
        }
        public ObjetoCompleto()
        {

        }
        #region Propiedades
    
        public Sprite Sprite { get; set; }
        public Datos Datos { get; set; }

  
        #endregion


        public override string ToString()
        {
            return Datos.Nombre;
        }



        public static PokemonGBAFramework.Batalla.Objeto GetObjeto(RomGba rom, int index)
        {
 
            ObjetoCompleto objeto = new ObjetoCompleto();
            PokemonGBAFramework.Batalla.Objeto obj = new PokemonGBAFramework.Batalla.Objeto();
            objeto.Datos = Datos.GetDatos(rom, index);
            objeto.Sprite = Sprite.GetSprite(rom, index);

            obj.Imagen = objeto.Sprite.Imagen.GetImg();
            objeto.Datos.SetValues(obj);
            return obj;

        }

        public static Paquete GetObjetos(RomGba rom)
        {
            return rom.GetPaquete("Objetos",(r,i)=>GetObjeto(r,i),Datos.GetTotal(rom));
        }

     


    }


}
