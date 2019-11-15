/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of VirtualCallIf.
	/// </summary>
	public class VirtualCallIf:Comando
	{
		public const byte ID = 0xBC;
		public const int SIZE = 6;
		Byte condicion;
		OffsetRom funcionPersonalizada;
 
		public VirtualCallIf(Byte condicion, OffsetRom funcionPersonalizada)
		{
			Condicion = condicion;
			FuncionPersonalizada = funcionPersonalizada;
 
		}
   
		public VirtualCallIf(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public VirtualCallIf(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe VirtualCallIf(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Llama a la función si se  cumple la condición.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "VirtualCallIf";
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
			funcionPersonalizada =new OffsetRom(ptrRom, offsetComando);
 
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
