using System.Collections.Generic;

namespace PokemonGBAFramework.Core
{
    public class BaseOrden
    {
        public Word Orden { get; set; }
        public override string ToString()
        {
            return Orden.ToString();
        }
        protected static T GetOrden<T>(RomGba rom, int posicion, byte[] muestraAlgoritmo, int inicioRelativo, OffsetRom inicioOrdenLocal = default) where T:BaseOrden,new()
        {
            T ordenLocal = new T();
            if (Equals(inicioOrdenLocal, default))
                inicioOrdenLocal = GetOffset(rom,muestraAlgoritmo,inicioRelativo);
            try
            {
                ordenLocal.Orden = new Word(rom, inicioOrdenLocal + (posicion - 1) * Word.LENGTH);
            }
            catch
            {
                ordenLocal.Orden = default;
            }

            return ordenLocal;
        }

        protected static OffsetRom GetOffset(RomGba rom, byte[] muestraAlgoritmo, int inicioRelativo)
        {
            return new OffsetRom(rom, GetZona(rom,muestraAlgoritmo,inicioRelativo));
        }

        protected static Zona GetZona(RomGba rom,byte[] muestraAlgoritmo,int inicioRelativo)
        {
            return Zona.Search(rom, muestraAlgoritmo, inicioRelativo);
        }
        protected static KeyValuePair<TOrden, T> GetOrdenado<TOrden,T>(RomGba rom, byte[] muestraAlgoritmo, int inicioRelativo, int posOriginal, GetMethod<T> metodo, OffsetRom offsetInicioMetodo = default, OffsetRom offsetInicioOrdenLocal = default) where TOrden : BaseOrden, new()
        {
            T item = metodo(rom, posOriginal, offsetInicioMetodo);
            return new KeyValuePair<TOrden, T>(GetOrden<TOrden>(rom, posOriginal,muestraAlgoritmo,inicioRelativo, offsetInicioOrdenLocal), item);
        }
        protected static T[] GetOrdenados<TOrden,T>(RomGba rom, byte[] muestraAlgoritmo, int inicioRelativo, GetTodos<T> metodo, OffsetRom offsetMetodo = default, OffsetRom offsetInicio = default) where TOrden : BaseOrden, new()
        {
            int aux;
            T[] items = metodo(rom, offsetMetodo);
            T[] ordenados = new T[items.Length];

            if (Equals(offsetInicio, default))
                offsetInicio = GetOffset(rom,muestraAlgoritmo,inicioRelativo);

            for (int i = 0; i < ordenados.Length; i++)
            {
                aux = GetOrden<TOrden>(rom, i, muestraAlgoritmo, inicioRelativo, offsetInicio).Orden;
                if(aux>=0&&aux<ordenados.Length)
                   ordenados[aux] = items[i];
            }

            return ordenados;
        }
        public static implicit operator Word(BaseOrden orden)=>orden.Orden;
    }
}