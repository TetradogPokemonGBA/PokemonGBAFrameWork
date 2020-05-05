using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class DatosObjeto
    {
        public enum LongitudCampos
        {
            Total = 44,
            Nombre = 25,
            //explicacion de los 44 bytes
            NombreCompilado = 14,
            Index = 2,
            Price = 2,
            HoldEffect = 1,
            Parameter = 1,
            Descripcion = 4,//es un pointer a una zona que acaba en FF
            pointerFieldUsage = 4,
            KeyItemValue = 1,
            BagKeyItem = 1,
            Bolsillo = 1,
            Tipo = 1,
            BattleUsage = 4,
            pointerBattleUsage = 4,
            extraParameter = 4

        }
        public enum BolsilloObjetos
        {

            Desconocido,
            Items,
            Pokeballs,
            MTMO,
            Bayas,
            ObjetosClave
        }

        public static readonly byte[] MuestraAlgoritmo = { 0x00, 0x19, 0x80, 0x7C, 0x10 };
        public static readonly int IndexRelativo = -MuestraAlgoritmo.Length - 96;

        public DatosObjeto()
        {
            Nombre = new BloqueString((int)LongitudCampos.NombreCompilado);
            Descripcion = new BloqueString();
        }
        #region Propiedades
        public BloqueString Nombre { get; set; }

        public Word Index { get; set; }

        public Word Price { get; set; }

        public byte HoldEffect { get; set; }

        public byte Parameter { get; set; }

        public byte BagKeyItem { get; set; }
        public BloqueString Descripcion { get; set; }
        public OffsetRom PointerFieldUsage { get; set; }
        public byte KeyItemValue { get; set; }

        public BolsilloObjetos Bolsillo { get; set; }

        public byte Tipo { get; set; }

        public DWord BattleUsage { get; set; }
        public OffsetRom PointerBattleUsage { get; set; }

        public DWord ExtraParameter { get; set; }

        #endregion
        public static int GetTotal(RomGba rom, OffsetRom offsetDatosObjeto = default)
        {
            const byte MARCAFINNOMBRE = 0xFF;
            const byte EMPTYBYTENAME = 0x0;

            bool acabado;
            bool nombreComprobadoCorrectamente;
            BloqueBytes datosItem;
            int posicionDesripcionObjeto = (int)LongitudCampos.NombreCompilado + (int)LongitudCampos.Index + (int)LongitudCampos.Price + (int)LongitudCampos.HoldEffect + (int)LongitudCampos.Parameter;

            int totalItems = 0;
            int offsetInicio = Equals(offsetDatosObjeto, default) ? GetOffset(rom) : offsetDatosObjeto;
            int offsetActual = offsetInicio;
            //cada objeto como minimo tiene un pointer si no lo tiene es que no tiene el formato bien :) ademas el nombre si no llega al final acaba en FF :D


            do
            {
                //mirar de actualizarlo para validar los pointers en otro lado...
                datosItem = BloqueBytes.GetBytes(rom.Data, offsetActual, (int)LongitudCampos.Total);
                //miro que el nombre acaba bien :)
                nombreComprobadoCorrectamente = false;
                acabado = new OffsetRom(rom, offsetActual).IsAPointer;//si lo que leo no es un pointer continuo
                for (int i = 0; i < (int)LongitudCampos.NombreCompilado && !acabado; i++)
                {
                    if (datosItem.Bytes[i] == MARCAFINNOMBRE)
                    {
                        if (!nombreComprobadoCorrectamente) nombreComprobadoCorrectamente = true;

                    }

                    else if (nombreComprobadoCorrectamente && datosItem.Bytes[i] != EMPTYBYTENAME)
                        acabado = true;//si continua es que esta mal :D
                }
                //miro el pointer
                if (!acabado)
                {
                    if (new OffsetRom(datosItem.Bytes, posicionDesripcionObjeto).IsAPointer)
                    {
                        totalItems++;//lo ha leido bien :D
                        offsetActual += (int)LongitudCampos.Total;
                    }
                    else acabado = true;
                }
            } while (!acabado);
            return totalItems;
        }

        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }

        public static Zona GetZona(RomGba rom)
        {
            return Zona.Search(rom, MuestraAlgoritmo, IndexRelativo);
        }

        public static DatosObjeto[] Get(RomGba rom, OffsetRom offsetDatosObjeto = default, int totalObjetos = -1)
        {
            DatosObjeto[] datos = new DatosObjeto[totalObjetos < 0 ? GetTotal(rom) : totalObjetos];

            if (Equals(offsetDatosObjeto, default))
                offsetDatosObjeto = GetOffset(rom);

            for (int i = 0; i < datos.Length; i++)
                datos[i] = Get(rom, i, offsetDatosObjeto);

            return datos;
        }
        public static DatosObjeto Get(RomGba rom, int index, OffsetRom offsetDatosObjeto = default)
        {
            OffsetRom aux;
            int offsetDatos = (Equals(offsetDatosObjeto, default) ? GetOffset(rom) : offsetDatosObjeto) + index * (int)LongitudCampos.Total;
            byte[] blDatos = rom.Data.SubArray(offsetDatos, (int)LongitudCampos.Total);
            DatosObjeto datos = new DatosObjeto();

            datos.Nombre.Texto = BloqueString.Get(blDatos, 0);
            datos.Index = new Word(blDatos, 14);
            datos.Price = new Word(blDatos, 16);
            datos.HoldEffect = blDatos[17];
            datos.Parameter = blDatos[18];
            datos.Descripcion = BloqueString.Get(rom, new OffsetRom(blDatos, 20).Offset);
            datos.KeyItemValue = blDatos[24];
            datos.BagKeyItem = blDatos[25];
            datos.Bolsillo = (BolsilloObjetos)blDatos[26];
            datos.Tipo = blDatos[27];
            aux = new OffsetRom(blDatos, 28);
            if (aux.IsAPointer)
                datos.PointerBattleUsage = aux;

            datos.BattleUsage = new DWord(blDatos, 32);

            aux = new OffsetRom(blDatos, 36);
            if (aux.IsAPointer)
                datos.PointerFieldUsage = aux;
            //lo que hacia que fuera tan lento cargando era el uso de try catch!!
            datos.ExtraParameter = new DWord(blDatos, 40);

            return datos;
        }
    }
}