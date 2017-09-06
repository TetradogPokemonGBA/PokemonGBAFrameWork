/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of ApplyMovement.
	/// </summary>
	public class ApplyMovement:Comando
	{
		public const byte ID=0x4F;
		public const int SIZE=7;
		short personajeAUsar;
		OffsetRom datosMovimiento;
		
		public ApplyMovement(short personajeAUsar,OffsetRom datosMovimiento)
		{
			PersonajeAUsar=personajeAUsar;
			DatosMovimiento=datosMovimiento;
			
		}
		
		public ApplyMovement(RomGba rom,int offset):base(rom,offset)
		{
		}
		public ApplyMovement(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe ApplyMovement(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Aplica los movimientos al persoanje especificado";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "ApplyMovement";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public short PersonajeAUsar
		{
			get{ return personajeAUsar;}
			set{personajeAUsar=value;}
		}
		public OffsetRom DatosMovimiento
		{
			get{ return datosMovimiento;}
			set{datosMovimiento=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{personajeAUsar,datosMovimiento.Offset};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			personajeAUsar=Word.GetWord(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			datosMovimiento=new OffsetRom(ptrRom,new OffsetRom(ptrRom,offsetComando).Offset);
			offsetComando+=OffsetRom.LENGTH;
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			Word.SetWord(ptrRomPosicionado,PersonajeAUsar);
			ptrRomPosicionado+=Word.LENGTH;
			OffsetRom.SetOffset(ptrRomPosicionado,DatosMovimiento);
			ptrRomPosicionado+=OffsetRom.LENGTH;
			
		}
	}
}