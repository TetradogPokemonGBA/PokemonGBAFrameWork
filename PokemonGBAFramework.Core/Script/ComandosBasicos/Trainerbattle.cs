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
		public new const int SIZE = Comando.SIZE+1+Word.LENGTH+Word.LENGTH+OffsetRom.LENGTH+OffsetRom.LENGTH;
		public const string NOMBRE = "Trainerbattle";
		public const string DESCRIPCION = "Empieza una batalla contra un entrenador";

		public Trainerbattle() { }
		public Trainerbattle(Byte kindOfBattle, Word battleToStart, Word reserved, BloqueString challengeText, BloqueString defeatText)
		{
			KindOfBattle = kindOfBattle;

			BattleToStart = battleToStart;

			Reserved = reserved;

			ChallengeText = challengeText;

			DefeatText = defeatText;

 
		}
   
		public Trainerbattle(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public Trainerbattle(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe Trainerbattle(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
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


		public Byte KindOfBattle { get; set; }

		public Word BattleToStart { get; set; }

		public Word Reserved { get; set; }

		public BloqueString ChallengeText { get; set; }

		public BloqueString DefeatText { get; set; }

		

		public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[] {
				new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(KindOfBattle)),
				new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(BattleToStart)),
				new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Reserved)),
				new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(ChallengeText)),
				new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(DefeatText))
			};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			KindOfBattle = ptrRom[offsetComando];

			offsetComando++;

			BattleToStart = new Word(ptrRom, offsetComando);

			offsetComando += Word.LENGTH;

			Reserved = new Word(ptrRom, offsetComando);

			offsetComando += Word.LENGTH;

			ChallengeText =BloqueString.Get(ptrRom, new OffsetRom(ptrRom, offsetComando));

			offsetComando += OffsetRom.LENGTH;

			DefeatText =BloqueString.Get(ptrRom ,new OffsetRom(ptrRom, offsetComando));
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			
			data[0]=IdComando;
			data[1] = KindOfBattle;
			Word.SetData(data,2, BattleToStart);
			Word.SetData(data,5, Reserved);
			OffsetRom.Set(data,7, new OffsetRom(ChallengeText.IdUnicoTemp));
			OffsetRom.Set(data,11, new OffsetRom(DefeatText.IdUnicoTemp));

			return data;
		}
	}
}
