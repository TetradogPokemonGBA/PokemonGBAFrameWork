using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Objeto
{
    public class Datos:PokemonFrameWorkItem
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
        public const byte ID = 0xA;
        public static readonly Zona ZonaDatosObjeto;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Datos>();

        BloqueString blNombre;
        Word index;
        Word price;
        byte holdEffect;
        byte parameter;
        BloqueString blDescripcion;
        OffsetRom pointerFieldUsage;
        byte keyItemValue;
        byte bagKeyItem;
        BolsilloObjetos bolsillo;
        byte tipo;
        DWord battleUsage;
        OffsetRom pointerBattleUsage;
        DWord extraParameter;
        static Datos()
        {
            ZonaDatosObjeto = new Zona("Datos Objeto");
            //datos item
            ZonaDatosObjeto.Add(EdicionPokemon.ZafiroEsp10, 0xA9B3C);
            ZonaDatosObjeto.Add(EdicionPokemon.RubiEsp10, 0xA9B3C);
            ZonaDatosObjeto.Add(EdicionPokemon.RojoFuegoEsp10, 0x1C8);
            ZonaDatosObjeto.Add(EdicionPokemon.VerdeHojaEsp10, 0x1C8);
            ZonaDatosObjeto.Add(EdicionPokemon.EsmeraldaEsp10, 0x1C8);

            ZonaDatosObjeto.Add(EdicionPokemon.ZafiroUsa10, 0xA98F0, 0xA9910, 0xA9910);
            ZonaDatosObjeto.Add(EdicionPokemon.RubiUsa10, 0xA98F0, 0xA9910, 0xA9910);
            ZonaDatosObjeto.Add(EdicionPokemon.EsmeraldaUsa10, 0x1C8);
            ZonaDatosObjeto.Add(EdicionPokemon.RojoFuegoUsa10, 0x1C8);
            ZonaDatosObjeto.Add(EdicionPokemon.VerdeHojaUsa10, 0x1C8);
        }
        public Datos()
        {
            blNombre = new BloqueString((int)LongitudCampos.NombreCompilado);
            blDescripcion = new BloqueString();
        }
        #region Propiedades
        public BloqueString Nombre
        {
            get
            {
                return blNombre;
            }
        }

        public Word Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }

        public Word Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        public byte HoldEffect
        {
            get
            {
                return holdEffect;
            }
            set
            {
                holdEffect = value;
            }
        }

        public byte Parameter
        {
            get
            {
                return parameter;
            }
            set
            {
                parameter = value;
            }
        }

        public byte BagKeyItem
        {
            get
            {
                return bagKeyItem;
            }
            set
            {
                bagKeyItem = value;
            }
        }
        public BloqueString Descripcion
        {
            get
            {
                return blDescripcion;
            }
        }
        public OffsetRom PointerFieldUsage
        {
            get
            {
                return pointerFieldUsage;
            }
            set
            {
                pointerFieldUsage = value;
            }
        }
        public byte KeyItemValue
        {
            get
            {
                return keyItemValue;
            }
            set
            {
                keyItemValue = value;
            }
        }

        public BolsilloObjetos Bolsillo
        {
            get
            {
                return bolsillo;
            }
            set
            {
                bolsillo = value;
            }
        }

        public byte Tipo
        {
            get
            {
                return tipo;
            }
            set
            {
                tipo = value;
            }
        }

        public DWord BattleUsage
        {
            get
            {
                return battleUsage;
            }
            set
            {
                battleUsage = value;
            }
        }
        public OffsetRom PointerBattleUsage
        {
            get
            {
                return pointerBattleUsage;
            }
            set
            {
                pointerBattleUsage = value;
            }
        }

        public DWord ExtraParameter
        {
            get
            {
                return extraParameter;
            }
            set
            {
                extraParameter = value;
            }
        }

        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;
        #endregion
        public static int GetTotal(RomGba rom)
        {
            const byte MARCAFINNOMBRE = 0xFF, EMPTYBYTENAME = 0x0;

            bool nombreComprobadoCorrectamente;
            BloqueBytes datosItem;
            int posicionDesripcionObjeto = (int)LongitudCampos.NombreCompilado + (int)LongitudCampos.Index + (int)LongitudCampos.Price + (int)LongitudCampos.HoldEffect + (int)LongitudCampos.Parameter;

            int totalItems = 0;
            int offsetInicio = Zona.GetOffsetRom(ZonaDatosObjeto, rom).Offset;
            int offsetActual = offsetInicio;
            //cada objeto como minimo tiene un pointer si no lo tiene es que no tiene el formato bien :) ademas el nombre si no llega al final acaba en FF :D
            bool acabado = false;

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
        public static Datos[] GetDatos(RomGba rom)
        {
            Datos[] datos = new Datos[GetTotal(rom)];
            for (int i = 0; i < datos.Length; i++)
                datos[i] = GetDatos(rom, i);
            return datos;
        }
        public static Datos GetDatos(RomGba rom, int index)
        {
            EdicionPokemon edicion = (EdicionPokemon)rom.Edicion;
            int offsetDatos = Zona.GetOffsetRom(ZonaDatosObjeto, rom).Offset + index * (int)LongitudCampos.Total;
            OffsetRom aux;
            byte[] blDatos = rom.Data.SubArray(offsetDatos, (int)LongitudCampos.Total);
            Datos datos = new Datos();

            datos.Nombre.Texto = BloqueString.GetString(blDatos, 0);
            datos.index = new Word(blDatos, 14);
            datos.price = new Word(blDatos, 16);
            datos.holdEffect = blDatos[17];
            datos.parameter = blDatos[18];
            datos.blDescripcion = BloqueString.GetString(rom, new OffsetRom(blDatos, 20).Offset);
            datos.keyItemValue = blDatos[24];
            datos.bagKeyItem = blDatos[25];
            datos.bolsillo = (BolsilloObjetos)blDatos[26];
            datos.tipo = blDatos[27];
            aux = new OffsetRom(blDatos, 28);
            if (aux.IsAPointer)
                datos.pointerBattleUsage = aux;

            datos.battleUsage = new DWord(blDatos, 32);

            aux = new OffsetRom(blDatos, 36);
            if (aux.IsAPointer)
                datos.pointerFieldUsage = aux;
            //lo que hacia que fuera tan lento cargando era el uso de try catch!!
            datos.extraParameter = new DWord(blDatos, 40);
            if (edicion.EsEsmeralda)
                datos.IdFuente = EdicionPokemon.IDESMERALDA;
            else if (edicion.EsRubiOZafiro)
                datos.IdFuente = EdicionPokemon.IDRUBIANDZAFIRO;
            else
                datos.IdFuente = EdicionPokemon.IDROJOFUEGOANDVERDEHOJA;

            if (edicion.Idioma == Idioma.Ingles)
                datos.IdFuente = datos.IdFuente - (int)Idioma.Español;
            else datos.IdFuente = datos.IdFuente - (int)Idioma.Ingles;

            datos.IdElemento = (ushort)index;

            return datos;
        }
        public static void SetDatos(RomGba rom, int index, Datos datos)
        {
            int offsetDatos = Zona.GetOffsetRom(ZonaDatosObjeto, rom).Offset + index * (int)LongitudCampos.Total;


            try
            {
                BloqueString.Remove(rom, offsetDatos);
            }
            catch { }

            BloqueString.SetString(rom, offsetDatos, datos.Nombre);
            offsetDatos += (int)LongitudCampos.NombreCompilado;
            Word.SetData(rom, offsetDatos, datos.Index);//por mirar

            offsetDatos += (int)LongitudCampos.Index;
            Word.SetData(rom, offsetDatos, datos.Index);//por mirar
            offsetDatos += (int)LongitudCampos.Price;
            rom.Data[offsetDatos] = datos.HoldEffect;
            offsetDatos++;
            rom.Data[offsetDatos] = datos.Parameter;
            offsetDatos++;
            try
            {
                BloqueString.Remove(rom, new OffsetRom(rom, offsetDatos).Offset);
            }
            catch { }
            rom.Data.SetArray(offsetDatos, new OffsetRom(BloqueString.SetString(rom, datos.Descripcion)).BytesPointer);
            offsetDatos += OffsetRom.LENGTH;
            rom.Data[offsetDatos] = datos.KeyItemValue;
            offsetDatos++;
            rom.Data[offsetDatos] = datos.BagKeyItem;
            offsetDatos++;
            rom.Data[offsetDatos] = (byte)datos.Bolsillo;
            offsetDatos++;
            rom.Data[offsetDatos] = datos.Tipo;
            if (datos.PointerBattleUsage != null)
                OffsetRom.SetOffset(rom.Data.Bytes, offsetDatos, datos.PointerBattleUsage);
            offsetDatos += OffsetRom.LENGTH;
            DWord.SetData(rom, offsetDatos, datos.BattleUsage);
            offsetDatos += DWord.LENGTH;
            if (datos.PointerBattleUsage != null)
                OffsetRom.SetOffset(rom.Data.Bytes, offsetDatos, datos.PointerFieldUsage);
            offsetDatos += OffsetRom.LENGTH;
            DWord.SetData(rom, offsetDatos, datos.ExtraParameter);
        }
        public static void SetDatos(RomGba rom, IList<Datos> datos)
        {
            OffsetRom offsetData;

            int totalActual = GetTotal(rom);
            if (totalActual != datos.Count)
            {
                offsetData = Zona.GetOffsetRom(ZonaDatosObjeto, rom);

                //borrar los datos
                rom.Data.Remove(offsetData.Offset, totalActual * (int)LongitudCampos.Total);
                //pongo un nuevo offset en la zona para que quepa todo :D
                OffsetRom.SetOffset(rom, offsetData, rom.Data.SearchEmptyBytes(datos.Count *(int)LongitudCampos.Total));
            }
            for (int i = 0; i < datos.Count; i++)
                SetDatos(rom, i, datos[i]);
        }
    }
}
