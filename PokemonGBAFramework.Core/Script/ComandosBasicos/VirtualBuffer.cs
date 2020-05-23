/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of VirtualBuffer.
	/// </summary>
	public class VirtualBuffer:Comando,IString
	{
		public const byte ID=0xBF;
		public new const int SIZE=Comando.SIZE+1+OffsetRom.LENGTH;
		public const string NOMBRE = "VirtualBuffer";
		public const string DESCRIPCION = "Almacena el texto en el buffer especificado.";


		public VirtualBuffer() { }
		public VirtualBuffer(Byte buffer,BloqueString texto)
		{
			Buffer=buffer;
			Texto=texto;
			
		}
		
		public VirtualBuffer(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public VirtualBuffer(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe VirtualBuffer(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;
		public override int Size => SIZE;
		public Byte Buffer { get; set; }
		public BloqueString Texto { get; set; }

		public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Buffer)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Texto))};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Buffer=*(ptrRom+offsetComando);
			offsetComando++;
			Texto=BloqueString.Get(ptrRom, new OffsetRom(ptrRom,offsetComando));
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 
			data[0]=IdComando;
			data[1]=Buffer;
			OffsetRom.Set(data,2,new OffsetRom(Texto.IdUnicoTemp));

			return data;
		}
	}
}
