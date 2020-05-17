/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of ApplyMovement.
	/// </summary>
	public class ApplyMovement:Comando
	{
		public const byte ID=0x4F;
		public new const int SIZE=Comando.SIZE+Word.LENGTH+OffsetRom.LENGTH;
		public const string NOMBRE="ApplyMovement";
		public const string DESCRIPCION="Aplica los movimientos al persoanje especificado";

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
        public Word PersonajeAUsar { get; set; }
        public OffsetRom DatosMovimiento { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{PersonajeAUsar,DatosMovimiento};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			PersonajeAUsar=new Word(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			DatosMovimiento=new OffsetRom(ptrRom,offsetComando);

		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
            ptrRomPosicionado += base.Size;
            Word.SetData(ptrRomPosicionado,PersonajeAUsar);
			ptrRomPosicionado+=Word.LENGTH;
			OffsetRom.Set(ptrRomPosicionado,DatosMovimiento);
			
		}
	}
}
