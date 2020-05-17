/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of VirtualGoto.
	/// </summary>
	public class VirtualGoto:Comando,IEndScript
	{
		public const byte ID = 0xB9;
		public new const  int SIZE = 5;
		OffsetRom funcionPersonalizada;
 
		public VirtualGoto(OffsetRom funcionPersonalizada)
		{
			FuncionPersonalizada = funcionPersonalizada;
 
		}
   
		public VirtualGoto(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public VirtualGoto(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe VirtualGoto(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Salta asta la función especificada.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "VirtualGoto";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public OffsetRom FuncionPersonalizada {
			get{ return funcionPersonalizada; }
			set{ funcionPersonalizada = value; }
		}

		#region IEndScript implementation
		public bool IsEnd => true;
		#endregion
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ funcionPersonalizada };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			funcionPersonalizada =new OffsetRom(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			OffsetRom.Set(ptrRomPosicionado, funcionPersonalizada);
		}
	}
}
