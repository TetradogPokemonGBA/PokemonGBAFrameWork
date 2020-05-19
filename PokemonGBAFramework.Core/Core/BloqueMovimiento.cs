using System.Collections.Generic;

namespace PokemonGBAFramework.Core
{
    public class BloqueMovimiento
    {

        public const byte MARCAFIN = 0xFE;

        public BloqueMovimiento()
        {
            List = new List<byte>();
            IdUnicoTemp = Script.GetIdUnicoTemp();
        }

        public unsafe BloqueMovimiento(byte* ptrRom, OffsetRom offsetRom):this()
        {
            int offset = offsetRom;

            while (ptrRom[offset] != MARCAFIN)
            {
                List.Add(ptrRom[offset++]);
            }
        }
        public int IdUnicoTemp { get; set; }
        public List<byte> List { get; private set; }
        public bool EstaCorrecto
        {
            get
            {
                int index = List.IndexOf(MARCAFIN);
                return index < 0 ? true : index == List.Count - 1;
            }
        }
        public bool TieneMarcaFinPuesta => List.Count > 0 ? List[List.Count - 1] == MARCAFIN : false;
        public void QuitaMarcaFin()
        {
            if (TieneMarcaFinPuesta)
                List.RemoveAt(List.Count - 1);
        }
        public void PonMarcaFin()
        {
            if (!TieneMarcaFinPuesta)
                List.Add(MARCAFIN);
        }
        public byte[] GetBytes()
        {
            if(!EstaCorrecto)
            {
                while (List.Contains(MARCAFIN)) List.Remove(MARCAFIN);
            }
            PonMarcaFin();
            return List.ToArray();
        }
    }
}