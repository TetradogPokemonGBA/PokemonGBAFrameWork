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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offsetABuscar"></param>
        /// <param name="romOrigen"></param>
        /// <param name="romDestino"></param>
        /// <returns>null si no lo encuentra</returns>
        public delegate OffsetRom GetOffsetCompatible(OffsetRom offsetABuscar, RomGba romOrigen, RomGba romDestino);

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
        public static event GetOffsetCompatible OffsetCompatibleStatic;
        public static Llista<GetOffsetCompatible> MetodosOffsetCompatible { get; private set; }
        public string Autor { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public LlistaOrdenada<string, string> GameCodeCompatibles { get; private set; }
        public Llista<ParteAbsoluta> PartesAbsolutas { get; private set; }
        public Llista<ParteRelativa> PartesRelativas { get; private set; }
        
        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;
        public event GetOffsetCompatible OffsetCompatible;

        static Parche()
        {
            MetodosOffsetCompatible = new Llista<GetOffsetCompatible>(new GetOffsetCompatible[] {Metodo1GetOffsetCompatible, Metodo2GetOffsetCompatible, Metodo3GetOffsetCompatible, Metodo4GetOffsetCompatible });

        }
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
        public LlistaOrdenada<OffsetRom,OffsetRom> GetOffsetDicCompatibilidad(Edicion edicionOrigen=null)
        {
            LlistaOrdenada<OffsetRom, OffsetRom> dicComatibilidad = new LlistaOrdenada<OffsetRom, OffsetRom>();
            string gameCodeOrigen = edicionOrigen != null ? edicionOrigen.GameCode : GameCodeCompatibles[0].Value;
            for (int i = 0; i < PartesAbsolutas.Count; i++)
                if (!dicComatibilidad.ContainsKey(PartesAbsolutas[i].OffsetCompatibles[gameCodeOrigen]))
                    dicComatibilidad.Add(PartesAbsolutas[i].OffsetCompatibles[gameCodeOrigen],null);
            for (int i = 0; i < PartesRelativas.Count; i++)
                for (int j = 0; j < PartesRelativas[i].Compatibilidad[gameCodeOrigen].Count; j++)
                    if (!dicComatibilidad.ContainsKey(PartesRelativas[i].Compatibilidad[gameCodeOrigen][j].Value))
                    dicComatibilidad.Add(PartesRelativas[i].Compatibilidad[gameCodeOrigen][j].Value, null);
            return dicComatibilidad;
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
            LlistaOrdenada<OffsetRom, OffsetRom> dicOffsets=GetOffsetDicCompatibilidad(romWithPatch.Edicion);
            if (compatible)
            {
                for (int i = 0; i < dicOffsets.Count; i++)
                    dicOffsets.AddOrReplace(dicOffsets[i].Key, TryGetOffsetCompatible(dicOffsets[i].Key, romWithPatch, romToAddCompatiblity));

            }
            if(FaltaOffsetsPorPoner(dicOffsets))
            {
                CompletaDic(dicOffsets, this.OffsetCompatible,romWithPatch,romToAddCompatiblity);
                CompletaDic(dicOffsets, Parche.OffsetCompatibleStatic, romWithPatch, romToAddCompatiblity);
            }
            compatible=!FaltaOffsetsPorPoner(dicOffsets);
           if(compatible)
            {
                //pongo los offsets para el gamecode compatible :)
                //mirar de hacer un diccionario generico gameCode,dicOffsets
            }
                return compatible;
        }

        private void CompletaDic(LlistaOrdenada<OffsetRom, OffsetRom> dicOffsets, GetOffsetCompatible offsetCompatible, RomGba romWithPatch, RomGba romToAddCompatiblity)
        {
            for (int i = 0; i < dicOffsets.Count && offsetCompatible != null; i++)
                if (dicOffsets[i].Value == null)
                    dicOffsets.AddOrReplace(dicOffsets[i].Key, offsetCompatible(dicOffsets[i].Key, romWithPatch, romToAddCompatiblity));
        }

        private bool FaltaOffsetsPorPoner(LlistaOrdenada<OffsetRom, OffsetRom> dicOffsets)
        {
            bool nofalta = true;
            for (int i = 0; i < dicOffsets.Count && nofalta; i++)
                nofalta = dicOffsets[i].Value != null;
            return !nofalta;
        }
        static OffsetRom TryGetOffsetCompatible(OffsetRom offsetABuscarCompatible, RomGba romWithPatch, RomGba romToAddCompatiblity)
        {
            OffsetRom offsetCompatible = null;
            for (int i = 0; i < MetodosOffsetCompatible.Count && offsetCompatible == null; i++)
                offsetCompatible = MetodosOffsetCompatible[i](offsetABuscarCompatible, romWithPatch, romToAddCompatiblity);
            return offsetCompatible;
        }
        public static OffsetRom Metodo1GetOffsetCompatible(OffsetRom offsetABuscarCompatible,RomGba romWithPatch, RomGba romToAddCompatiblity)
        {
            OffsetRom offsetCompatible = null;
            //lo busco mirando los datos a los que apunta
            return offsetCompatible;

        }
        public static OffsetRom Metodo2GetOffsetCompatible(OffsetRom offsetABuscarCompatible, RomGba romWithPatch, RomGba romToAddCompatiblity)
        {
            OffsetRom offsetCompatible = null;
            //lo busco mirando rutina que apunta a este offset
            return offsetCompatible;

        }
        public static OffsetRom Metodo3GetOffsetCompatible(OffsetRom offsetABuscarCompatible, RomGba romWithPatch, RomGba romToAddCompatiblity)
        {
            OffsetRom offsetCompatible = null;
            //lo busco mirando bytes sin pointers de la rutina actual un poco por arriba
            return offsetCompatible;

        }
        public static OffsetRom Metodo4GetOffsetCompatible(OffsetRom offsetABuscarCompatible, RomGba romWithPatch, RomGba romToAddCompatiblity)
        {
            OffsetRom offsetCompatible = null;
            //lo busco mirando bytes sin pointers de la rutina actual un  poco por abajo
            return offsetCompatible;

        }

    }
}
