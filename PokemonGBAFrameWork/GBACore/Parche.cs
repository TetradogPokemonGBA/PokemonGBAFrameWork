using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;
using Gabriel.Cat.S.Binaris;
namespace PokemonGBAFrameWork
{
    public class Parche:IElementoBinarioComplejo
    {

        public class ParteAbsoluta:IElementoBinarioComplejo
        {
            private static readonly ElementoBinario serializador = ElementoComplejoBinarioNullable.GetElement<ParteAbsoluta>();
            public ParteAbsoluta()
            {
                PointersRelativos = new List<KeyValuePair<OffsetRom, ParteRelativa>>();
                OffsetCompatibles = new LlistaOrdenada<string, OffsetRom>();
            }


            public List<KeyValuePair<OffsetRom, ParteRelativa>> PointersRelativos { get; private set; }
            public byte[] BytesOn { get; set; }
            public byte[] BytesOff { get; set; }
            /// <summary>
            /// GameCode,Offset data to replace
            /// </summary>
            public LlistaOrdenada<string, OffsetRom> OffsetCompatibles { get; private set; }

            ElementoBinario IElementoBinarioComplejo.Serialitzer => serializador;

            public void SetData(RomGba rom)
            {
                byte[] dataOn = (byte[])BytesOn.Clone();
                for (int i = 0; i < PointersRelativos.Count; i++)
                {
                    OffsetRom.SetOffset(dataOn, PointersRelativos[i].Key, new OffsetRom(rom.Data.SetArrayIfNotExist(PointersRelativos[i].Value.GetBytes(rom))));
                }
                rom.Data.SetArray(OffsetCompatibles[rom.Edicion.GameCode].Offset, dataOn);
            }
            public void RemoveData(RomGba rom)
            {
                rom.Data.SetArray(OffsetCompatibles[rom.Edicion.GameCode].Offset, BytesOff);
            }
            public bool IsOn(RomGba rom)
            {
                return rom.Data.SearchArray(OffsetCompatibles[rom.Edicion.GameCode].Offset, BytesOn.Length, BytesOn) > 0;
            }
        }

        public class ParteRelativa:IElementoBinarioComplejo
        {
            private static readonly ElementoBinario serializador = ElementoComplejoBinarioNullable.GetElement<ParteRelativa>();
            public byte[] Datos { get; set; }
            public Llista<KeyValuePair<OffsetRom, ParteRelativa>> PartesRelativas { get; private set; }
            /// <summary>
            /// GameCode->OffsetDatos,OffsetAPoner
            /// </summary>
            public LlistaOrdenada<string, Llista<KeyValuePair<OffsetRom, OffsetRom>>> Compatibilidad { get; private set; }
            public ParteRelativa()
            {
                PartesRelativas = new Llista<KeyValuePair<OffsetRom, ParteRelativa>>();
                Compatibilidad = new LlistaOrdenada<string, Llista<KeyValuePair<OffsetRom, OffsetRom>>>();
            }
            ElementoBinario IElementoBinarioComplejo.Serialitzer => serializador;
            public byte[] GetBytes(RomGba rom)
            {
                byte[] space = new byte[5];//lo añado por si existe una rutina igual pero un poco más larga que no coja esa rutina como la rutina que buscamos.
                Llista<KeyValuePair<OffsetRom, OffsetRom>> lstCompativilidad = Compatibilidad[rom.Edicion.GameCode];
                byte[] datos = PartesRelativas.Count > 0 ? (byte[])Datos.Clone() : Datos;

                for (int i = 0; i < PartesRelativas.Count; i++)
                {
                    OffsetRom.SetOffset(datos, PartesRelativas[i].Key, new OffsetRom(rom.Data.SetArrayIfNotExist(PartesRelativas[i].Value.Datos.AddArray(space))));

                }
                for (int i = 0; i < lstCompativilidad.Count; i++)
                {
                    OffsetRom.SetOffset(datos, lstCompativilidad[i].Key.Offset, lstCompativilidad[i].Value);
                }

                return datos;
            }
            public void RemoveData(RomGba rom)
            {
                byte[] data = GetBytes(rom);
                int offset = rom.Data.SearchArray(data);
                if (offset > 0)
                    rom.Data.Remove(offset, data.Length);
            }
        }

        public static readonly ElementoBinario Serializador = ElementoComplejoBinarioNullable.GetElement<Parche>();

        public string Autor { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public LlistaOrdenada<string, string> GameCodeCompatibles { get; private set; }
        public Llista<ParteAbsoluta> PartesAbsolutas { get; private set; }
        public Llista<ParteRelativa> PartesRelativas { get; private set; }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

        public Parche()
        {
            PartesAbsolutas = new Llista<ParteAbsoluta>();
            PartesRelativas = new Llista<ParteRelativa>();
            GameCodeCompatibles = new LlistaOrdenada<string, string>();
        }
        public Parche(RomGba romBase, RomGba romDataPatch, bool forcePatch = false)
        {
            const int SEPARACIONMIN = OffsetRom.LENGTH + 1;

            if (!forcePatch && romBase.Edicion.GameCode != romDataPatch.Edicion.GameCode)//tienen que ser la misma edición para crear el parche y luego se puede probar de añadir compatibilidad
                throw new RomNoCompatibleException(romBase.Edicion.GameCode, romDataPatch.Edicion.GameCode);
            //si los datos del parche sobreescriben datos de la rombase son partesAbsolutas si sobrescriben partes vacias son partes relativas
            //no empieza otra parte si la distancia no supera el minimo

        }

        public void SetPatch(RomGba rom, bool forcePatch = false)
        {

            if (!forcePatch && GameCodeCompatibles.ContainsKey(rom.Edicion.GameCode))
                throw new RomNoCompatibleException(GameCodeCompatibles.Values);

            for (int i = 0; i < PartesAbsolutas.Count; i++)
                PartesAbsolutas[i].SetData(rom);

        }
        public void UnPatch(RomGba rom)
        {
            for (int i = 0; i < PartesAbsolutas.Count; i++)
                PartesAbsolutas[i].RemoveData(rom);
            for (int i = 0; i < PartesRelativas.Count; i++)
                PartesRelativas[i].RemoveData(rom);
        }
        public bool IsCompatible(RomGba rom)
        {
            return GameCodeCompatibles.ContainsKey(rom.Edicion.GameCode);
        }
        public bool IsOn(RomGba rom)
        {
            return PartesAbsolutas.Count > 0 ? PartesAbsolutas[0].IsOn(rom) : PartesRelativas.Count > 0 ? rom.Data.SearchArray(PartesRelativas[0].GetBytes(rom)) > 0 : true;
        }
        public bool TryAddCompatiblity(RomGba romWithPatch, RomGba romToAddCompatiblity)
        {
            bool compatible = romWithPatch.Edicion.Compatible(romToAddCompatiblity.Edicion);
            if (compatible)
            {
                //busco offsets absolutos del parche
                //miro datos del rom con el parche para sacar una array unica en la rom y la busco en la otra,si la encuentro ya tengo el offset :D
                //mirar de buscar la info de los pointers y luego volver a veces sirve esa estrategia :D
                //si no encuentro todos los offsets absolutos no es compatible...

            }
            return compatible;
        }
        static OffsetRom Metodo1GetOffsetCompatible(OffsetRom offsetABuscarCompatible,RomGba romWithPatch, RomGba romToAddCompatiblity)
        {
            OffsetRom offsetCompatible = null;
            //lo busco mirando los datos a los que apunta
            return offsetCompatible;

        }
        static OffsetRom Metodo2GetOffsetCompatible(OffsetRom offsetABuscarCompatible, RomGba romWithPatch, RomGba romToAddCompatiblity)
        {
            OffsetRom offsetCompatible = null;
            //lo busco mirando rutina que apunta a este offset
            return offsetCompatible;

        }
        static OffsetRom Metodo3GetOffsetCompatible(OffsetRom offsetABuscarCompatible, RomGba romWithPatch, RomGba romToAddCompatiblity)
        {
            OffsetRom offsetCompatible = null;
            //lo busco mirando bytes sin pointers de la rutina actual un poco por arriba
            return offsetCompatible;

        }
        static OffsetRom Metodo4GetOffsetCompatible(OffsetRom offsetABuscarCompatible, RomGba romWithPatch, RomGba romToAddCompatiblity)
        {
            OffsetRom offsetCompatible = null;
            //lo busco mirando bytes sin pointers de la rutina actual un  poco por abajo
            return offsetCompatible;

        }

    }
}
