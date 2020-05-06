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
		public const int SIZE = 6;
		Byte condicion;
		OffsetRom funcionPersonalizada;
 
		public VirtualGotoIf(Byte condicion, OffsetRom funcionPersonalizada)
		{
			Condicion = condicion;
			FuncionPersonalizada = funcionPersonalizada;
 
		}
   
		public VirtualGotoIf(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public VirtualGotoIf(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe VirtualGotoIf(byte* ptRom, int offset)
			: base(ptRom, offset)
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
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			condicion = ptrRom[offsetComando];
			offsetComando++;
			funcionPersonalizada = new OffsetRom(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado = condicion;
			++ptrRomPosicionado; 
			OffsetRom.SetOffset(ptrRomPosicionado, funcionPersonalizada);
		}
	}
}
