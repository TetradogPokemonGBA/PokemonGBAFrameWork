namespace PokemonGBAFramework.Core
{
    public abstract class BasePaleta
    {


        public BasePaleta()
        {
            Paleta = new Paleta();
        }

        public Paleta Paleta { get; set; }


        protected static T Get<T>(RomGba rom, int posicion, OffsetRom offsetPaletaNormal, byte[] muestraAlgoritmo, int index) where T:BasePaleta,new()
        {
            if (Equals(offsetPaletaNormal, default))
                offsetPaletaNormal = GetOffset(rom,muestraAlgoritmo,index);
            T paleta = new T();
            int offsetPaletaNormalPokemon = offsetPaletaNormal + Paleta.LENGTHHEADERCOMPLETO * posicion;
            paleta.Paleta = Paleta.GetPaleta(rom, offsetPaletaNormalPokemon);
            return paleta;
        }

        protected static OffsetRom GetOffset(RomGba rom, byte[] muestraAlgoritmo, int index)
        {
            return new OffsetRom(rom, GetZona(rom,muestraAlgoritmo,index));
        }

        protected static int GetZona(RomGba rom, byte[] muestraAlgoritmo, int index)
        {
            return Zona.Search(rom, muestraAlgoritmo, index);
        }
        protected static T[] Get<T>(RomGba rom, byte[] muestraAlgoritmo, int index) where T : BasePaleta, new() => Huella.GetAll<T>(rom,(r,pos,offset)=> Get<T>(rom, pos, offset, muestraAlgoritmo, index), GetOffset(rom, muestraAlgoritmo, index));

        protected static T[] GetOrdenLocal<T>(RomGba rom, byte[] muestraAlgoritmo, int index) where T : BasePaleta, new() => OrdenLocal.GetOrdenados<T>(rom, (r, o) => Get<T>(r, muestraAlgoritmo, index));
        protected static T[] GetOrdenNacional<T>(RomGba rom, byte[] muestraAlgoritmo, int index) where T : BasePaleta, new() => OrdenNacional.GetOrdenados<T>(rom, (r, o) => Get<T>(r, muestraAlgoritmo, index));
        
        public static implicit operator Paleta(BasePaleta paleta)=>paleta.Paleta;
    }
}