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
	public class VirtualCallIf:Comando,IScript
	{
		public const byte ID = 0xBC;
		public new const int SIZE = Comando.SIZE+1+OffsetRom.LENGTH;
		public const string NOMBRE = "VirtualCallIf";
		public const string DESCRIPCION = "Llama a la función si se  cumple la condición.";


		public VirtualCallIf() { }
		public VirtualCallIf(Byte condicion, Script funcionPersonalizada)
		{
			Condicion = condicion;
			Script = funcionPersonalizada;
 
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
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;
		public override int Size => SIZE;
		public Byte Condicion { get; set; }
		public Script Script { get; set; }

		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Condicion, Script };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Condicion = ptrRom[offsetComando];
			offsetComando++;
			Script =scriptManager.GetScript(ptrRom, new OffsetRom(ptrRom, offsetComando));
 
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			
			data[0]=IdComando;
			data[1] = Condicion; 
			OffsetRom.Set(data,2,new OffsetRom(Script.IdUnicoTemp));

			return data;
		}
	}
}
