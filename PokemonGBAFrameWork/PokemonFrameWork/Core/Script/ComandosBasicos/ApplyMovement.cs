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
		public const string NOMBRE="ApplyMovement";
		public const string DESCRIPCION="Aplica los movimientos al persoanje especificado";
		Word personajeAUsar;
		OffsetRom datosMovimiento;
		
		public ApplyMovement(Word personajeAUsar,OffsetRom datosMovimiento)
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
				return DESCRIPCION;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return NOMBRE;
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word PersonajeAUsar
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
			return new Object[]{personajeAUsar,datosMovimiento};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			personajeAUsar=new Word(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			datosMovimiento=new OffsetRom(ptrRom,offsetComando);

		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado,PersonajeAUsar);
			ptrRomPosicionado+=Word.LENGTH;
			OffsetRom.SetOffset(ptrRomPosicionado,DatosMovimiento);
			
		}
	}
}
