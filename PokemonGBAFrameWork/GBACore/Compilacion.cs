using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class Compilacion : IComparable,IComparable<Compilacion>
    {
        public Compilacion():this(1,0)
        {
        }

        public Compilacion(int version, int subVersion)
        {
            this.Version = version;
            this.SubVersion = subVersion;
        }

        public int Version { get; protected set; }
        public int SubVersion { get; protected set; }
        public virtual string Name
        {
            get
            {
                return Version + "" + SubVersion;
            }
        }
        public override string ToString()
        {
            return String.Format("{0}.{1}", Version, SubVersion);
        }

        int IComparable.CompareTo(object obj)
        {
            return CompareTo(obj as Compilacion);
        }
        int IComparable<Compilacion>.CompareTo(Compilacion other)
        {
            return CompareTo(other);
        }
        protected virtual int CompareTo(Compilacion other)
        {
            int compareTo;
            if (other != null)
            {
                compareTo = Version.CompareTo(other.Version);
                if (compareTo == (int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals)
                {
                    compareTo = SubVersion.CompareTo(other.SubVersion);
                }
            }
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
            return compareTo;
        }

        
    }
}
