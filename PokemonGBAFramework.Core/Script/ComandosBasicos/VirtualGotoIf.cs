/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of VirtualGotoIf.
	/// </summary>
	public class VirtualGotoIf:Comando
	{
		public const byte ID = 0xBB;
		public new const int SIZE = 6;
		Byte condicion;
		OffsetRom funcionPersonalizada;
 
		public VirtualGotoIf(Byte condicion, OffsetRom funcionPersonalizada)
		{
			Condicion = condicion;
			FuncionPersonalizada = funcionPersonalizada;
 
		}
   
		public VirtualGotoIf(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public VirtualGotoIf(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe VirtualGotoIf(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Salta asta la función si cumple con la condición.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "VirtualGotoIf";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte Condicion {
			get{ return condicion; }
			set{ condicion = value; }
		}
		//si es un script poner el script directamente :) e implementar la interficie IDeclaracion
		public OffsetRom FuncionPersonalizada {
			get{ return funcionPersonalizada; }
			set{ funcionPersonalizada = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ condicion, funcionPersonalizada };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			condicion = ptrRom[offsetComando];
			offsetComando++;
			funcionPersonalizada = new OffsetRom(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			*ptrRomPosicionado = condicion;
			++ptrRomPosicionado; 
			OffsetRom.Set(ptrRomPosicionado, funcionPersonalizada);
		}
	}
}
