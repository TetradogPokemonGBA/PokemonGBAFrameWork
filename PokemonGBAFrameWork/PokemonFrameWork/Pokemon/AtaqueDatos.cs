/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 11/03/2017
 * Time: 5:52
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;

namespace PokemonGBAFrameWork.Ataque
{
    public class Datos : IComparable<Datos>,IElementoBinarioComplejo
    {

        enum Custom
        {
            MakesContact = 1,
            Protect = 2,
            MagicCoat = 4,
            Snatch = 8,
            MirrorMove = 16,
            KingsRock = 32
        }
        enum CamposDatosAtaque
        {
            Effect,
            BasePower,
            Type,
            Accuracy,
            PP,
            EffectAccuracy,
            Target,
            Priority,
            Custom,
            PadByte1,
            Category,
            PadByte3
        }
        public enum Categoria
        {
            Fisico, Especial, Estatus
        }
        public const byte ID = 0x16;
        public static readonly Creditos Creditos;
        public static readonly Zona ZonaDatosAtaques;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Datos>();

        public const int Longitud = 12;
        BloqueBytes blDatosAtaque;
        /* custom
          el primero 1 Makes contact
          el segundo 2 Is affected by Protect
          el tercero 4 is affected by magic coat
          el cuarto 8 is affected by snatch
          el quinto 10 is affected by mirror move
          el sexto 20 is affected by king's rock
		 */
        static Datos()
        {
            Creditos = new Creditos();
            Creditos.Add(Creditos.Comunidades[Creditos.POKEMONCOMMUNITY], "Gamer2020", "PGE");

            ZonaDatosAtaques = new Zona("Datos ataque");

            //datos los pp es el offset de los datos+4 si se cambia el offset de los datos hay que cambiar el de los pps tambien!!!
            ZonaDatosAtaques.Add(0x1CC, EdicionPokemon.EsmeraldaEsp, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.RojoFuegoUsa, EdicionPokemon.VerdeHojaEsp, EdicionPokemon.VerdeHojaUsa);

            ZonaDatosAtaques.Add(0xCC20, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp);
            ZonaDatosAtaques.Add(0xCA54, EdicionPokemon.RubiUsa, EdicionPokemon.ZafiroUsa);

        }

        //los ultimos bytes puede ser la parte "Extra" de PGE -Attack Editor

        public Datos()
        {
            blDatosAtaque = new BloqueBytes(Longitud);
        }
        public Datos(byte effect, byte basePower, byte type, byte accuracy, byte pp, byte effectAccuracy, byte target, byte priority, bool makeContact, bool isAffectedByProtect, bool isAffectedByMagicCoat, bool isAffectedBySnatch, bool isAffectedByMirrorMove, bool isAffectedByKingsRock, byte padByte1, Categoria category, byte padByte3) : this()
        {
            Effect = effect;
            BasePower = basePower;
            Type = type;
            Accuracy = accuracy;
            PP = pp;
            EffectAccuracy = effectAccuracy;
            Target = target;
            Priority = priority;

            MakesContact = makeContact;
            IsAffectedByProtect = isAffectedByProtect;
            IsAffectedBySnatch = isAffectedBySnatch;
            IsAffectedByMagicCoat = isAffectedByMagicCoat;
            IsAffectedByMirrorMove = isAffectedByMirrorMove;
            IsAffectedByKingsRock = isAffectedByKingsRock;

            PadByte1 = padByte1;
            Category = category;
            PadByte3 = padByte3;
        }
        #region Propiedades
        public bool MakesContact
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] % 2 != 0;//si es impar es que hay el 1 de make contact
            }
            set
            {
                if (value)
                {
                    if (!MakesContact)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom]++;

                }
                else
                {
                    if (MakesContact)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom]--;
                }
            }
        }
        public bool IsAffectedByProtect
        {
            get
            {
                return IsCustomEnabled(1);
            }
            set
            {
                if (value)
                {
                    if (!IsAffectedByProtect)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.Protect;
                }
                else
                {
                    if (IsAffectedByProtect)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.Protect;
                }

            }
        }
        public bool IsAffectedByMagicCoat
        {
            get
            {
                return IsCustomEnabled(2);
            }
            set
            {
                if (value)
                {
                    if (!IsAffectedByMagicCoat)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.MagicCoat;
                }
                else
                {
                    if (IsAffectedByMagicCoat)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.MagicCoat;
                }

            }
        }
        public bool IsAffectedBySnatch
        {
            get
            {
                return IsCustomEnabled(3);
            }
            set
            {
                if (value)
                {
                    if (!IsAffectedBySnatch)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.Snatch;
                }
                else
                {
                    if (IsAffectedBySnatch)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.Snatch;
                }

            }
        }
        public bool IsAffectedByMirrorMove
        {
            get
            {
                return IsCustomEnabled(4);
            }
            set
            {
                if (value)
                {
                    if (!IsAffectedByMirrorMove)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.MirrorMove;
                }
                else
                {
                    if (IsAffectedByMirrorMove)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.MirrorMove;
                }

            }
        }
        public bool IsAffectedByKingsRock
        {
            get
            {
                return IsCustomEnabled(5);
            }
            set
            {
                if (value)
                {
                    if (!IsAffectedByKingsRock)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.KingsRock;
                }
                else
                {
                    if (IsAffectedByKingsRock)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.KingsRock;
                }

            }
        }

        public byte Effect
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Effect];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Effect] = value;
            }
        }

        public byte BasePower
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.BasePower];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.BasePower] = value;
            }
        }

        public byte Type
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Type];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Type] = value;
            }
        }

        public byte Accuracy
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Accuracy];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Accuracy] = value;
            }
        }

        public byte PP
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.PP];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.PP] = (byte)(value % 30);//el maximo es 30
            }
        }

        public byte EffectAccuracy
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.EffectAccuracy];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.EffectAccuracy] = value;
            }
        }

        public byte Target
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Target];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Target] = value;
            }
        }

        public byte Priority
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Priority];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Priority] = value;
            }
        }



        public byte PadByte1
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.PadByte1];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.PadByte1] = value;
            }
        }

        public Categoria Category
        {
            get
            {
                return (Categoria)blDatosAtaque.Bytes[(int)CamposDatosAtaque.Category];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Category] = (byte)value;
            }
        }

        public byte PadByte3
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.PadByte3];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.PadByte3] = value;
            }
        }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;
        #endregion
        private bool IsCustomEnabled(int indexCustomToCalculate)
        {
            bool isCustomEnabled;
            int aux = blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom];
            Custom[] customs = Enum.GetValues(typeof(Custom)) as Custom[];
            for (int i = customs.Length - 1; i > indexCustomToCalculate; i--)
                if (aux >= (int)customs[i])
                    aux -= (int)customs[i];
            isCustomEnabled = aux >= (int)customs[indexCustomToCalculate];
            return isCustomEnabled;
        }

        public int CompareTo(Datos other)
        {
            int compareTo;
            if (other != null)
            {
                compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals;
                for (int i = 0; i < blDatosAtaque.Bytes.Length && compareTo == (int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals; i++)
                    compareTo = blDatosAtaque.Bytes[i].CompareTo(other.blDatosAtaque.Bytes[i]);

            }
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
            return compareTo;
        }
        public static Datos[] GetDatos(RomGba rom)
        {
            Datos[] datos = new Datos[Descripcion.GetTotal(rom)];
            for (int i = 0; i < datos.Length; i++)
                datos[i] = GetDatos(rom, i);
            return datos;
        }
        public static Datos GetDatos(RomGba rom, int posicion)
        {
            return new Datos() { blDatosAtaque = BloqueBytes.GetBytes(rom.Data, Zona.GetOffsetRom(ZonaDatosAtaques, rom).Offset + posicion * Longitud, Longitud) };
        }
        public static void SetDatos(RomGba rom, int posicion, Datos datosAtaque)
        {
            rom.Data.SetArray(Zona.GetOffsetRom(ZonaDatosAtaques, rom).Offset + posicion * Longitud, datosAtaque.blDatosAtaque.Bytes);
        }
        public static void SetDatos(RomGba rom, IList<Datos> datos)
        {
            if (datos.Count > AtaqueCompleto.MAXATAQUESSINASM)//mas adelante adapto el hack de Jambo
                throw new ArgumentOutOfRangeException("datos");
            int total = Descripcion.GetTotal(rom);
            if (total < datos.Count)
                AtaqueCompleto.QuitarLimite(rom, datos.Count);
            //elimino los datos
            rom.Data.Remove(Zona.GetOffsetRom(ZonaDatosAtaques, rom).Offset, total * Longitud);
            //reubico
            OffsetRom.SetOffset(rom, Zona.GetOffsetRom(ZonaDatosAtaques, rom), rom.Data.SearchEmptyBytes(datos.Count * Longitud));
            //pongo los datos
            for (int i = 0; i < datos.Count; i++)
                SetDatos(rom, i, datos[i]);
        }


    }
}
