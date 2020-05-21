/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Warp7.
	/// </summary>
	public class Warp7:Comando
	{
		public const byte ID = 0xD7;
		public const int SIZE = 8;
		Byte bancoAIr;
		Byte mapaAIr;
		Byte salidaAIr;
		Word coordenadaX;
		Word coordenadaY;
 
		public Warp7(Byte bancoAIr, Byte mapaAIr, Byte salidaAIr, Word coordenadaX, Word coordenadaY)
		{
			BancoAIr = bancoAIr;
			MapaAIr = mapaAIr;
			SalidaAIr = salidaAIr;
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
 
		}
   
		public Warp7(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public Warp7(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe Warp7(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "bajo investigación.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "Warp7";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte BancoAIr {
			get{ return bancoAIr; }
			set{ bancoAIr = value; }
		}
		public Byte MapaAIr {
			get{ return mapaAIr; }
			set{ mapaAIr = value; }
		}
		public Byte SalidaAIr {
			get{ return salidaAIr; }
			set{ salidaAIr = value; }
		}
		public Word CoordenadaX {
			get{ return coordenadaX; }
			set{ coordenadaX = value; }
		}
		public Word CoordenadaY {
			get{ return coordenadaY; }
			set{ coordenadaY = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ bancoAIr, mapaAIr, salidaAIr, coordenadaX, coordenadaY };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			bancoAIr = ptrRom[offsetComando];
			offsetComando++;
			mapaAIr = ptrRom[offsetComando];
			offsetComando++;
			salidaAIr = ptrRom[offsetComando];
			offsetComando++;
			coordenadaX = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			coordenadaY = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 data[0]=IdComando;
			*ptrRomPosicionado = bancoAIr;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = mapaAIr;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = salidaAIr;
			++ptrRomPosicionado; 
			Word.SetData(data, , CoordenadaX);
 
			Word.SetData(data, , CoordenadaY);
		}
	}
}
