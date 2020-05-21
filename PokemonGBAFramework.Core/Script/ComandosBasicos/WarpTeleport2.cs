/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of WarpTeleport2.
	/// </summary>
	public class WarpTeleport2:Comando
	{
		public const byte ID = 0xD1;
		public const int SIZE = 8;
		Byte banco;
		Byte mapa;
		Byte salida;
		Word coordenadaX;
		Word coordenadaY;
 
		public WarpTeleport2(Byte banco, Byte mapa, Byte salida, Word coordenadaX, Word coordenadaY)
		{
			Banco = banco;
			Mapa = mapa;
			Salida = salida;
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
 
		}
   
		public WarpTeleport2(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public WarpTeleport2(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe WarpTeleport2(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Transporta al jugador al mapa especificado con el efecto de teletransportación";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "WarpTeleport2";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte Banco {
			get{ return banco; }
			set{ banco = value; }
		}
		public Byte Mapa {
			get{ return mapa; }
			set{ mapa = value; }
		}
		public Byte Salida {
			get{ return salida; }
			set{ salida = value; }
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
			return new Object[]{ banco, mapa, salida, coordenadaX, coordenadaY };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			banco = ptrRom[offsetComando];
			offsetComando++;
			mapa = ptrRom[offsetComando];
			offsetComando++;
			salida = ptrRom[offsetComando];
			offsetComando++;
			coordenadaX = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			coordenadaY = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 data[0]=IdComando;
			*ptrRomPosicionado = banco;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = mapa;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = salida;
			++ptrRomPosicionado; 
			Word.SetData(data, , CoordenadaX);
 
			Word.SetData(data, , CoordenadaY);
		}
	}
}
