namespace PokemonGBAFramework.Core.Mapa.Elements
{
    public abstract class SpriteBase
	{
		protected SpriteBase(byte x, byte y)
		{
			X = x;
			Y = y;
		}

		public byte X { get; set; }
		public byte Y { get; set; }
		public abstract byte[] GetBytes();
	}

}
