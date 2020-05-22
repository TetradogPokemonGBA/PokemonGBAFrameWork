/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of PokenavCall.
	/// </summary>
	public class PokenavCall:Comando,IString
	{
		public const byte ID = 0xDF;
		public new const int SIZE = 5;
		public const string DESCRIPCION= "Muestra una llamada del Pokenav.";
		public const string NOMBRE= "PokenavCall";


		public PokenavCall(BloqueString text)
		{
			Texto = text;
 
		}
   
		public PokenavCall(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public PokenavCall(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe PokenavCall(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;
		public override int Size => SIZE;
		public BloqueString Texto {	get;set;}
 
		protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.Esmeralda;
		}
		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Texto };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Texto =BloqueString.Get(ptrRom, new OffsetRom(ptrRom, offsetComando));
 
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0] = IdComando;
			OffsetRom.Set(data,1,new OffsetRom(Texto.IdUnicoTemp));
			return data;
 
		}
	}
}
