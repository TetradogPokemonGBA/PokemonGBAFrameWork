/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of SetHealingPlace.
	/// </summary>
	public class SetHealingPlace:Comando
	{
		//hacer metodo para sacar el mapa del lugar indicado en el script y de un mapa sacar un lugar, RomData o Mapas y HealingPlaces(tiene que existir en algun lado la asociación)
		//Script de nivel de tipo 3 
		//https://www.pokecommunity.com/showthread.php?t=189304 para los lugares mmm mirar si se pueden sacar porque si los obtengo y luego obtengo los mapas obtendré información personalizada de cada rom que es lo ideal :)
		public const byte ID=0x9F;
		public const int SIZE=3;
		Word lugar;
		
		public SetHealingPlace(Word lugar)
		{
			Lugar=lugar;
			
		}
		
		public SetHealingPlace(RomGba rom,int offset):base(rom,offset)
		{
		}
		public SetHealingPlace(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe SetHealingPlace(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Establece el lugar donde el jugador va una vez que está sin pokemon con vida.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SetHealingPlace";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word Lugar
		{
			get{ return lugar;}
			set{lugar=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{lugar};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			lugar=new Word(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado,Lugar);
		}
	}
}
