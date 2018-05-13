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
                    OffsetRom.SetOffset(dataOn, PointersRelativos[i].Key,new OffsetRom(rom.Data.SetArrayIfNotExist(PointersRelativos[i].Value.GetBytes(rom))));
                }
                rom.Data.SetArray(Offset.Offset, dataOn);
            }
        }

        public class ParteRelativa
        {

            public byte[] Datos { get; set; }
            public Llista<KeyValuePair<OffsetRom, ParteRelativa>> PartesRelativas { get; private set; }

            public ParteRelativa()
            {
                PartesRelativas = new Llista<KeyValuePair<OffsetRom, ParteRelativa>>();
            }
            public byte[] GetBytes(RomGba rom)
            {
                byte[] space = new byte[5];//lo añado por si existe una rutina igual pero un poco más larga que no coja esa rutina como la rutina que buscamos.

                byte[] datos =PartesRelativas.Count>0? (byte[]) Datos.Clone():Datos;
                for(int i=0;i<PartesRelativas.Count;i++)
                {
                    OffsetRom.SetOffset(datos, PartesRelativas[i].Key, new OffsetRom(rom.Data.SetArrayIfNotExist(PartesRelativas[i].Value.Datos.AddArray(space))));

                }

                return datos;
            }
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
            const int SEPARACIONMIN = OffsetRom.LENGTH;

            if (!forcePatch&&romBase.Edicion.GameCode != romDataPatch.Edicion.GameCode)
                throw new RomNoCompatibleException(romBase.Edicion.GameCode, romDataPatch.Edicion.GameCode);
            //si los datos del parche sobreescriben datos de la rombase son partesAbsolutas si sobrescriben partes vacias son partes relativas
            //no empieza otra parte si la distancia no supera el minimo

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
