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
		//https://www.pokecommunity.com/showthread.php?t=189304 para los lugares
		public const byte ID=0x9F;
		public const int SIZE=3;
		short lugar;
		
		public SetHealingPlace(short lugar)
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
		public short Lugar
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
			lugar=Word.GetWord(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			Word.SetWord(ptrRomPosicionado,Lugar);
			ptrRomPosicionado+=Word.LENGTH;
			
		}
	}
}
