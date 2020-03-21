/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 11/03/2017
 * Time: 5:52
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFrameWork.Ataque;
using System;
using System.Collections.Generic;

namespace PokemonGBAFrameWork
{
    /// <summary>
    /// Description of Ataque.
    /// </summary>
    public class AtaqueCompleto
	{
        enum LongitudCampos
		{
					
			PointerEfecto = 4,
			ContestData,
			Descripcion = 4,//es un pointer al texto
			ScriptBatalla,
			Animacion
		}
		enum ValoresLimitadoresFin
		{
			Ataque =0x13E0,
		}



		public static readonly Zona ZonaScriptBatalla;
		public static readonly Zona ZonaAnimacion;

        public static readonly Variable VariableLimitadoAtaques;

        static readonly byte[] BytesDesLimitadoAtaques;

        public const int LENGTHLIMITADOR = 16;
		
		static AtaqueCompleto()
		{


            byte[] valoresUnLimited = (((Hex)(int)ValoresLimitadoresFin.Ataque));

            ZonaScriptBatalla = new Zona("ScriptAtaqueBatalla");
			ZonaAnimacion=new Zona("AnimaciónAtaque");
            VariableLimitadoAtaques = new Variable("VariableLimitadorAtaque");

            //por investigar!!!
            //efectos el offset tiene que acabar en 0,4,8,C
            //de momento se tiene que investigar...lo que habia antes eran animaciones...

            //script batalla
            ZonaScriptBatalla.Add(0x162D4,EdicionPokemon.RojoFuegoEsp10,EdicionPokemon.VerdeHojaEsp10);
			ZonaScriptBatalla.Add(EdicionPokemon.RojoFuegoUsa10, 0x16364, 0x16378);
			ZonaScriptBatalla.Add(EdicionPokemon.VerdeHojaUsa10, 0x16364, 0x16378);

			ZonaScriptBatalla.Add(0x148B0,EdicionPokemon.RubiEsp10,EdicionPokemon.ZafiroEsp10);
			ZonaScriptBatalla.Add(0x3E854,EdicionPokemon.EsmeraldaUsa10,EdicionPokemon.EsmeraldaEsp10);
			ZonaScriptBatalla.Add(0x146E4,EdicionPokemon.RubiUsa10,EdicionPokemon.ZafiroUsa10);

			//animacion CON ESTO PUEDO DIFERENCIAR LAS VERSIONES ZAFIRO Y RUBI USA :D
			ZonaAnimacion.Add(0x72608,EdicionPokemon.RojoFuegoEsp10,EdicionPokemon.VerdeHojaEsp10);
			ZonaAnimacion.Add(EdicionPokemon.RojoFuegoUsa10, 0x7250D0, 0x725E4);
			ZonaAnimacion.Add(EdicionPokemon.VerdeHojaUsa10, 0x7250D0, 0x725E4);
			ZonaAnimacion.Add(EdicionPokemon.RubiUsa10, 0x75734, 0x75754);
			ZonaAnimacion.Add(EdicionPokemon.ZafiroUsa10, 0x75738, 0x75758);
			ZonaAnimacion.Add(EdicionPokemon.EsmeraldaUsa10, 0xA3A44);
			ZonaAnimacion.Add(EdicionPokemon.EsmeraldaEsp10, 0xA3A58);
			ZonaAnimacion.Add(EdicionPokemon.RubiEsp10, 0x75BF0);
			ZonaAnimacion.Add(EdicionPokemon.ZafiroEsp10, 0x75BF4);
            //añado la variable limitador
            VariableLimitadoAtaques.Add(EdicionPokemon.VerdeHojaUsa10, 0xD75D0, 0xD75E4);
            VariableLimitadoAtaques.Add(EdicionPokemon.RojoFuegoUsa10, 0xD75FC, 0xD7610);
            VariableLimitadoAtaques.Add(EdicionPokemon.EsmeraldaUsa10, 0x14E504);
            VariableLimitadoAtaques.Add(EdicionPokemon.VerdeHojaEsp10, 0xD7858);
            VariableLimitadoAtaques.Add(EdicionPokemon.EsmeraldaEsp10, 0x14E138);
            VariableLimitadoAtaques.Add(EdicionPokemon.RojoFuegoEsp10, 0xD7884);
            VariableLimitadoAtaques.Add(0xAC8C2, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10);
            VariableLimitadoAtaques.Add(EdicionPokemon.RubiUsa10, 0xAC676, 0xAC696);
            VariableLimitadoAtaques.Add(EdicionPokemon.ZafiroUsa10, 0xAC676, 0xAC696);

            BytesDesLimitadoAtaques = new byte[LENGTHLIMITADOR];
            BytesDesLimitadoAtaques.SetArray(LENGTHLIMITADOR - valoresUnLimited.Length, valoresUnLimited);


        }

	

	}
}
