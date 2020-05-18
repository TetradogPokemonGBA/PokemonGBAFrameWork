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
		
        public ApplyMovement(Word personajeAUsar,BloqueMovimiento datosMovimiento)
		{
			PersonajeAUsar=personajeAUsar;
			DatosMovimiento=datosMovimiento;
			
		}
		
		public ApplyMovement(ScriptManager scriptManager,RomGba rom,int offset):base(scriptManager, rom,offset)
		{
		}
		public ApplyMovement(ScriptManager scriptManager, byte[] bytesScript,int offset):base(scriptManager, bytesScript,offset)
		{}
		public unsafe ApplyMovement(ScriptManager scriptManager, byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
        public BloqueMovimiento DatosMovimiento { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{PersonajeAUsar,DatosMovimiento};
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			PersonajeAUsar=new Word(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			DatosMovimiento=new BloqueMovimiento(ptrRom,new OffsetRom(ptrRom,offsetComando));

		}
		public override byte[] GetBytesTemp()
		{
			byte[] data = new byte[Size];
			data[0] = IdComando;
			Word.SetData(data,1, PersonajeAUsar);
			OffsetRom.Set(data,3, new OffsetRom(DatosMovimiento.IdUnicoTemp));
			return data;
		}
	}
}
