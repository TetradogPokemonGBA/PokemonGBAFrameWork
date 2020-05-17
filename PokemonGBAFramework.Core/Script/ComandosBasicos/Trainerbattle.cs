/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Trainerbattle.
	/// </summary>
	public class Trainerbattle:Comando
	{
		public const byte ID = 0x5C;
		public const int SIZE = 14;
		Byte kindOfBattle;

		Word battleToStart;

		Word reserved;

		OffsetRom pointerToTheChallengeText;

		OffsetRom pointerToTheDefeatText;

 
		public Trainerbattle(Byte kindOfBattle, Word battleToStart, Word reserved, OffsetRom pointerToTheChallengeText, OffsetRom pointerToTheDefeatText)
		{
			KindOfBattle = kindOfBattle;

			BattleToStart = battleToStart;

			Reserved = reserved;

			PointerToTheChallengeText = pointerToTheChallengeText;

			PointerToTheDefeatText = pointerToTheDefeatText;

 
		}
   
		public Trainerbattle(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public Trainerbattle(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe Trainerbattle(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Empieza una batalla contra un entrenador";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "Trainerbattle";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
                         

		public Byte KindOfBattle {
			get{ return kindOfBattle; }
			set{ kindOfBattle = value; }
		}

		public Word BattleToStart {
			get{ return battleToStart; }
			set{ battleToStart = value; }
		}

		public Word Reserved {
			get{ return reserved; }
			set{ reserved = value; }
		}

		public OffsetRom PointerToTheChallengeText {
			get{ return pointerToTheChallengeText; }
			set{ pointerToTheChallengeText = value; }
		}

		public OffsetRom PointerToTheDefeatText {
			get{ return pointerToTheDefeatText; }
			set{ pointerToTheDefeatText = value; }
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[] {
				kindOfBattle,
				battleToStart,
				reserved,
				pointerToTheChallengeText,
				pointerToTheDefeatText
			};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			kindOfBattle = ptrRom[offsetComando];

			offsetComando++;

			battleToStart = new Word(ptrRom, offsetComando);

			offsetComando += Word.LENGTH;

			reserved = new Word(ptrRom, offsetComando);

			offsetComando += Word.LENGTH;

			pointerToTheChallengeText = new OffsetRom(ptrRom, offsetComando);

			offsetComando += OffsetRom.LENGTH;

			pointerToTheDefeatText =new OffsetRom(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado = kindOfBattle;

			++ptrRomPosicionado;

			Word.SetData(ptrRomPosicionado, battleToStart);

			ptrRomPosicionado += Word.LENGTH;

			Word.SetData(ptrRomPosicionado, reserved);

			ptrRomPosicionado += Word.LENGTH;

			OffsetRom.Set(ptrRomPosicionado, pointerToTheChallengeText);

			ptrRomPosicionado += OffsetRom.LENGTH;

			OffsetRom.Set(ptrRomPosicionado, pointerToTheDefeatText);
		}
	}
}
