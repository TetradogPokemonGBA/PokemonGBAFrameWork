using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class Parche
    {
        public class ParteAbsoluta
        {
            public ParteAbsoluta()
            {
                PointersRelativos = new List<KeyValuePair<OffsetRom, ParteRelativa>>();
            }

            public OffsetRom Offset { get; set; }
            public List<KeyValuePair<OffsetRom, ParteRelativa>> PointersRelativos { get; private set; }
            public byte[] BytesOn { get; set; }
            public byte[] BytesOff { get; set; }

            public void SetData(RomGba rom)
            {
                byte[] space = new byte[5];//lo añado por si existe una rutina igual pero un poco más larga que no coja esa rutina como la rutina que buscamos.
                byte[] dataOn = (byte[])BytesOn.Clone();
                for (int i = 0; i < PointersRelativos.Count; i++)
                {
                    OffsetRom.SetOffset(dataOn, PointersRelativos[i].Key,new OffsetRom(rom.Data.SetArrayIfNotExist(PointersRelativos[i].Value.Datos.AddArray(space))));
                }
                rom.Data.SetArray(Offset.Offset, dataOn);
            }
        }

        public class ParteRelativa
        {

            public byte[] Datos { get; set; }
        }

        public string GameCode { get; set; }
        public Llista<ParteAbsoluta> PartesAbsolutas { get; private set; }
        public Llista<ParteRelativa> PartesRelativas { get; private set; }

        public Parche()
        {
            PartesAbsolutas = new Llista<ParteAbsoluta>();
            PartesRelativas = new Llista<ParteRelativa>();
            GameCode = "";
        }
        public Parche(RomGba romBase,RomGba romDataPatch,bool forcePatch=false)
        {
            if (!forcePatch&&romBase.Edicion.GameCode != romDataPatch.Edicion.GameCode)
                throw new RomNoCompatibleException(romBase.Edicion.GameCode, romDataPatch.Edicion.GameCode);
            //si los datos del parche sobreescriben datos de la rombase son partesAbsolutas si sobrescriben partes vacias son partes relativas

        }
        public void SetPatch(RomGba rom,bool forcePatch=false)
        {

            if (!forcePatch && rom.Edicion.GameCode != GameCode)
                throw new RomNoCompatibleException(GameCode);

           

            for (int i = 0; i < PartesAbsolutas.Count; i++)
                PartesAbsolutas[i].SetData(rom);


        }

    }
}
