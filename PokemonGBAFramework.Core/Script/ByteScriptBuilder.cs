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
            {
                DicScripts.Add(script.IdUnicoTemp, script);
                AddRange(script.GetScripts().ToArray());
            }
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
            Script script;
            KeyValuePair<int, Script> aux;
            List<KeyValuePair<int, Script>> offsetsScript = new List<KeyValuePair<int, Script>>();
            List<KeyValuePair<int, int>> lstOffsets = new List<KeyValuePair<int, int>>();
            int inicio = data.Length < OffsetRom.DIECISEISMEGAS/2 ? 0 : 0x800000;
            Script[] scripts = DicScripts.Values.ToArray();

            //faltan los bloques string,movement,shop?
            for(int i=scripts.Length-1;i>=0;i--)
            {
                script = scripts[i];
                aux = new KeyValuePair<int, Script>(data.SearchEmptySpaceAndSetArray(script.GetBytesTemp(), inicio), script);
                offsetsScript.Add(aux);
                lstOffsets.Add(new KeyValuePair<int, int>(aux.Key, aux.Value.IdUnicoTemp));
                foreach(var texto in script.GetStrings())
                {
                    lstOffsets.Add(new KeyValuePair<int, int>(data.SearchEmptySpaceAndSetArray(BloqueString.ToByteArray(texto.Texto), inicio), texto.IdUnicoTemp));
                }
                foreach (var move in script.GetMovimientos())
                {
                    lstOffsets.Add(new KeyValuePair<int, int>(data.SearchEmptySpaceAndSetArray(move.GetBytes(), inicio), move.IdUnicoTemp));
                }
                foreach (var braille in script.GetBrailles())
                {
                    lstOffsets.Add(new KeyValuePair<int, int>(data.SearchEmptySpaceAndSetArray(braille.GetBytes(), inicio), braille.IdUnicoTemp));
                }
                foreach (var tienda in script.GetTiendas())
                {
                    lstOffsets.Add(new KeyValuePair<int, int>(data.SearchEmptySpaceAndSetArray(tienda.GetBytes(), inicio), tienda.IdUnicoTemp));
                }
                foreach (var trainerbattle in script.GetTrainerBattles())
                {
                    lstOffsets.Add(new KeyValuePair<int, int>(data.SearchEmptySpaceAndSetArray(trainerbattle.ChallengeText.GetBytes(), inicio), trainerbattle.ChallengeText.IdUnicoTemp));
                    lstOffsets.Add(new KeyValuePair<int, int>(data.SearchEmptySpaceAndSetArray(trainerbattle.DefeatText.GetBytes(), inicio), trainerbattle.DefeatText.IdUnicoTemp));
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
