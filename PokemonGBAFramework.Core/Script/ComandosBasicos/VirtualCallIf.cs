/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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
   
		public VirtualCallIf(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public VirtualCallIf(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe VirtualCallIf(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			condicion = ptrRom[offsetComando];
			offsetComando++;
			funcionPersonalizada =new OffsetRom(ptrRom, offsetComando);
 
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 data[0]=IdComando;
			*ptrRomPosicionado = condicion;
			++ptrRomPosicionado; 
			OffsetRom.Set(ptrRomPosicionado, funcionPersonalizada);
		}
	}
}
