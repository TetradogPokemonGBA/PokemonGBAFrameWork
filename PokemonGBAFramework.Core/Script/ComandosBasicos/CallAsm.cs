/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 12:25
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using Gabriel.Cat.S.Extension;
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CallAsm.
	/// </summary>
	public class CallAsm:Comando
	{
		public  const byte ID=0x23;
		public  new const int SIZE = Comando.SIZE + OffsetRom.LENGTH;
		public  const string NOMBRE="CallAsm";
		public  const string DESCRIPCION= "Ejecuta codigo ASM en el offset thumb-sub+1";

		public CallAsm() { }
		public CallAsm(BloqueASM bloqueASM)
		{ ASM = bloqueASM; }
		public CallAsm(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public CallAsm(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe CallAsm(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;

		public override string Nombre => NOMBRE;
		public override int Size => SIZE;
		public BloqueASM ASM{get;set;}

		protected override unsafe void CargarCamando(ScriptAndASMManager scriptManager, byte* ptrRom, int offsetComando)
		{
			ASM = scriptManager.GetASM(ptrRom, new OffsetRom(ptrRom, offsetComando));
		}

		public override byte[] GetBytesTemp()
		{
			return new byte[] { IdComando }.AddArray(new OffsetRom(ASM.IdUnicoTemp).BytesPointer);
		}
		

	}
}
