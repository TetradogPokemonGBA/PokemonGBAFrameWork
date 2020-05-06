/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of StartContest.
	/// </summary>
	public class StartContest:Comando
	{
		public const byte ID=0x8C;
		public const int SIZE=1;
		
		public StartContest()
		{
			
		}
		
		public StartContest(RomGba rom,int offset):base(rom,offset)
		{
		}
		public StartContest(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe StartContest(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Empeza el concurso pokemon";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "StartContest";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.Zafiro|Edicion.Pokemon.Rubi|Edicion.Pokemon.Esmeralda;
		}
	
	}
}
