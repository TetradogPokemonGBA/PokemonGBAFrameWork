using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class BloqueTienda
    {
        public const ushort MARCAFIN = 0;
        public BloqueTienda()
        {
            Objetos = new List<Word>();
        }
        public unsafe BloqueTienda(byte* ptrRom,int offset):this()
        {
            Word aux;
           
            do
            {
                aux = new Word(ptrRom, offset);
                offset += Word.LENGTH;
                Objetos.Add(aux);
            } while (aux > MARCAFIN);

            IdUnicoTemp = Script.GetIdUnicoTemp();
        }
        public int IdUnicoTemp { get; set; }
        public List<Word> Objetos { get; set; }
        public bool EstaFinPuesto => Objetos.Count > 0 && Objetos[Objetos.Count - 1] == 0;

        public void PonerFin()
        {
            QuitarFin();
            Objetos.Add(MARCAFIN);
        }
        public void QuitarFin()
        {
            while (Objetos.Contains(MARCAFIN))
                Objetos.Remove(MARCAFIN);
        }
        public byte[] GetBytes()
        {
            PonerFin();
            return new byte[0].AddArray(Objetos.Select((obj) => obj.Data).ToArray());
        }
    }
}
