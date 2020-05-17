using PokemonGBAFramework.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGBAFramework.Core.BuildScript
{
    public class ByteScriptBuilder
    {
        public ByteScriptBuilder()
        {
            DicScripts = new SortedList<int,Script>();
        }
        SortedList<int,Script> DicScripts { get; set; }
        
        public void Add(Script script)
        {
            if (!DicScripts.ContainsKey(script.IdUnicoTemp))
                DicScripts.Add(script.IdUnicoTemp, script);
            AddRange(script.GetScritps().ToArray());
        }
        public void AddRange(IList<Script> scripts)
        {
            for (int i = 0; i < scripts.Count; i++)
                Add(scripts[i]);
        }
        public IList<KeyValuePair<int, Script>> Set(RomGba romGba)
        {
            return Set(romGba.Data);
        }
        public IList<KeyValuePair<int, Script>> Set(byte[] data)
        {
            return Set(new BloqueBytes(data));
        }
        public IList<KeyValuePair<int,Script>> Set(BloqueBytes data)
        {//pongo en los bytes los offsets temporales
         //luego sustituye esos Offsets temporales por los que tendrán en el bloque teniendo en cuenta que los scripts empiezan en una posicion especial
            List<KeyValuePair<int, Script>> offsetsScript = new List<KeyValuePair<int, Script>>();
            foreach(var script in DicScripts)
            {
                offsetsScript.Add(new KeyValuePair<int, Script>(data.SearchEmptySpaceAndSetArray(script.Value.GetBytesTemp()),script.Value));
            }
            //ahora sustituyo los OffsetsTemporales por los reales
            for(int i = 0; i < offsetsScript.Count; i++)
            {
                OffsetRom.Set(data, new OffsetRom(offsetsScript[i].Value.IdUnicoTemp), offsetsScript[i].Key);
            }

            return offsetsScript;

        }
        public byte[] GetBytes()
        {
            byte[] data = new byte[GetSize()];
            Set(data);
            return data;
        }

        public int GetSize()
        {
            int total = 0;
            foreach(var script in DicScripts)
            {
                total += script.Value.Size;
                total =total.NextOffsetValido();
            }
            return total;
        }

        public static byte[] GetBytesTemp(IList<Script> scripts)
        {
            ByteScriptBuilder byteScript = new ByteScriptBuilder();
            byteScript.AddRange(scripts);
            return byteScript.GetBytes();
        }
    }
}
