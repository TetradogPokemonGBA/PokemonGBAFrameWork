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
            List<KeyValuePair<int, int>> lstOffsets = new List<KeyValuePair<int, int>>();
            //faltan los bloques string,movement,shop?
            foreach(var script in DicScripts)
            {
                offsetsScript.Add(new KeyValuePair<int, Script>(data.SearchEmptySpaceAndSetArray(script.Value.GetBytesTemp()),script.Value));
                lstOffsets.Add(new KeyValuePair<int, int>(offsetsScript[offsetsScript.Count-1].Key, offsetsScript[offsetsScript.Count - 1].Value.IdUnicoTemp));
                foreach(var texto in script.Value.GetStrings())
                {
                    lstOffsets.Add(new KeyValuePair<int, int>(data.SearchEmptySpaceAndSetArray(BloqueString.ToByteArray(texto.Texto)), texto.IdUnicoTemp));
                }
                foreach (var move in script.Value.GetMovimientos())
                {
                    lstOffsets.Add(new KeyValuePair<int, int>(data.SearchEmptySpaceAndSetArray(move.GetBytes()), move.IdUnicoTemp));
                }
                foreach (var braille in script.Value.GetBrailles())
                {
                    lstOffsets.Add(new KeyValuePair<int, int>(data.SearchEmptySpaceAndSetArray(braille.GetBytes()), braille.IdUnicoTemp));
                }
                foreach (var tienda in script.Value.GetTiendas())
                {
                    lstOffsets.Add(new KeyValuePair<int, int>(data.SearchEmptySpaceAndSetArray(tienda.GetBytes()), tienda.IdUnicoTemp));
                }
                foreach (var trainerbattle in script.Value.GetTrainerBattles())
                {
                    lstOffsets.Add(new KeyValuePair<int, int>(data.SearchEmptySpaceAndSetArray(trainerbattle.ChallengeText.GetBytes()), trainerbattle.ChallengeText.IdUnicoTemp));
                    lstOffsets.Add(new KeyValuePair<int, int>(data.SearchEmptySpaceAndSetArray(trainerbattle.DefeatText.GetBytes()), trainerbattle.DefeatText.IdUnicoTemp));
                }
                //falta  otros
            }
          
            //ahora sustituyo los OffsetsTemporales por los reales
            for(int i = 0; i < lstOffsets.Count; i++)
            {
                OffsetRom.Set(data, new OffsetRom(lstOffsets[i].Value), lstOffsets[i].Key);
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
                foreach (var texto in script.Value.GetStrings())
                {
                    total += BloqueString.ToByteArray(texto.Texto).Length;
                    total = total.NextOffsetValido();

                }
                foreach (var move in script.Value.GetMovimientos())
                {
                    total += move.GetBytes().Length;
                    total = total.NextOffsetValido();
                }
                foreach (var braille in script.Value.GetBrailles())
                {
                    total += braille.GetBytes().Length;
                    total = total.NextOffsetValido();
                }
                foreach (var tienda in script.Value.GetTiendas())
                {
                    total += tienda.GetBytes().Length;
                    total = total.NextOffsetValido();
                }
                foreach (var trainerbattle in script.Value.GetTrainerBattles())
                {
                    total += trainerbattle.ChallengeText.GetBytes().Length;
                    total = total.NextOffsetValido();
                    total += trainerbattle.DefeatText.GetBytes().Length;
                    total = total.NextOffsetValido();
                }
                //falta  otros
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
