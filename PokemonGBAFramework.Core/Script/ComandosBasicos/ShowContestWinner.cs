/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of ShowContestWinner.
	/// </summary>
	public class ShowContestWinner:Comando
	{
		public const byte ID=0x77;
		public new const int SIZE=Comando.SIZE+1;
		public const string NOMBRE = "ShowContestWinner";
		public const string DESCRIPCION = "Muestra al vencedor el concurso.(Solo para la región de Hoenn en la de Kanto actua como nop)";
		public ShowContestWinner()
		{
			
		}
		public ShowContestWinner(byte contest)
		{
			Contest = contest;
		}
		public ShowContestWinner(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public ShowContestWinner(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe ShowContestWinner(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return DESCRIPCION;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return NOMBRE;
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public byte Contest { get; set; }

		protected override unsafe void CargarCamando(ScriptAndASMManager scriptManager, byte* ptrRom, int offsetComando)
		{
			Contest = ptrRom[offsetComando];
		}
		protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.Zafiro|Edicion.Pokemon.Rubi|Edicion.Pokemon.Esmeralda;
		}
		public override byte[] GetBytesTemp()
		{
			return new byte[] { IdComando, Contest };
		}

	}
}
