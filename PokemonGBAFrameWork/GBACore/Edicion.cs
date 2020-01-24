using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class Edicion:IComparable,IComparable<Edicion>,IClauUnicaPerObjecte
    {
        enum Variables
        {
            Idioma = Abreviacion + LongitudCampos.Abreviacion,
            Abreviacion = 172,
            NombreCompleto = 160
        }
        public enum LongitudCampos
        {
            Idioma = 1,
            Abreviacion = 3,
            NombreCompleto = 12
        }
        public static readonly Edicion Desconocida = new Edicion();
        char idioma;
        string abreviacion;
        string nombreCompleto;
        Compilacion compilacion;
        long id;
        public Edicion(char idioma, string abreviacion, string nombreCompleto,int version=1,int subVersion=0)
        {
            InicialIdioma = idioma;
            Abreviacion = abreviacion;
            NombreCompleto = nombreCompleto;
            Compilacion = new Compilacion(version,subVersion);
        }
        private Edicion()
        { }

        public char InicialIdioma
        {
            get
            {

                return idioma;
            }
            set
            {

                idioma = value;
            }
        }

        public string Abreviacion
        {
            get
            {
                return abreviacion;
            }
            set
            {

                if (String.IsNullOrEmpty(value))
                    value = "GBA";
                else if (value.Length > (int)LongitudCampos.Abreviacion)
                    value = value.Substring(0, (int)LongitudCampos.Abreviacion);

                abreviacion = value;

            }
        }

        public string NombreCompleto
        {
            get
            {
                return nombreCompleto;
            }
            set
            {

                if (String.IsNullOrEmpty(value))
                    value = "Rom GBA";
                else if (value.Length > (int)LongitudCampos.NombreCompleto)
                    value = value.Substring(0, (int)LongitudCampos.NombreCompleto);

                nombreCompleto = value;
            }
        }

        IComparable IClauUnicaPerObjecte.Clau => this;

        public string GameCode { get { return Abreviacion+InicialIdioma+Compilacion.Name; } }

        public Compilacion Compilacion { get => compilacion; protected set => compilacion = value; }
        public long Id { get => id; set => id = value; }

        public virtual bool Compatible(Edicion edicion)
        {
            return GameCode == edicion.GameCode;
        }
        public Edicion Clone()
        {
            Edicion clon = new Edicion();
            clon.InicialIdioma = InicialIdioma;
            clon.Abreviacion = Abreviacion;
            clon.NombreCompleto = NombreCompleto;
            return clon;
        }
        public override string ToString()
        {
            return string.Format("[Edicion NombreCompleto={0}]", nombreCompleto);
        }
        #region Equals and GetHashCode implementation
        public override bool Equals(object obj)
        {
            Edicion other = obj as Edicion;
            bool equals = other != null;
            if (equals)
                equals = this.idioma == other.idioma && this.abreviacion == other.abreviacion && this.nombreCompleto == other.nombreCompleto;
            return equals;
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            unchecked
            {
                hashCode += 1000000007 * idioma.GetHashCode();
                if (abreviacion != null)
                    hashCode += 1000000009 * abreviacion.GetHashCode();
                if (nombreCompleto != null)
                    hashCode += 1000000021 * nombreCompleto.GetHashCode();
            }
            return hashCode;
        }

        public static bool operator ==(Edicion lhs, Edicion rhs)
        {
            bool equals;
            if (ReferenceEquals(lhs, rhs))
                equals = true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                equals = false;
            else equals = lhs.Equals(rhs);
            return equals;
        }

        public static bool operator !=(Edicion lhs, Edicion rhs)
        {
            return !(lhs == rhs);
        }

        #endregion
        public static Edicion GetEdicion(RomGba rom)
        {
            if (rom == null)
                throw new ArgumentNullException();

            Edicion edicion = new Edicion();
            edicion.InicialIdioma = (char)rom[(int)Variables.Idioma];
            edicion.Abreviacion = System.Text.Encoding.ASCII.GetString(rom.Data.SubArray((int)Variables.Abreviacion, (int)LongitudCampos.Abreviacion));
            edicion.NombreCompleto = System.Text.Encoding.ASCII.GetString(rom.Data.SubArray((int)Variables.NombreCompleto, (int)LongitudCampos.NombreCompleto));

            return edicion;
        }

        int IComparable<Edicion>.CompareTo(Edicion other)
        {
            return ICompareTo(other);
        }
        int IComparable.CompareTo(object obj)
        {
            return ICompareTo(obj as Edicion);
        }
        protected virtual int ICompareTo(Edicion other)
        {
            int compareTo;
            if (other != null)
            {
                compareTo = this.abreviacion.CompareTo(other.abreviacion);
                if(compareTo==0)
                {
                    compareTo= this.idioma.CompareTo(other.idioma);
                    if (compareTo == 0)
                    {
                        compareTo = this.nombreCompleto.CompareTo(other.nombreCompleto);
                    }
                }
            }
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;

            return compareTo;
        }

    
    }
}
