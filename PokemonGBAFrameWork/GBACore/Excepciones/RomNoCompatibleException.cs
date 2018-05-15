using System;
using System.Collections.Generic;

namespace PokemonGBAFrameWork
{
    public class RomNoCompatibleException : Exception
    {
        public  ICollection<string> Values { get; private set; }

        public RomNoCompatibleException(string gameCode):base(String.Format("Rom edition must be {0}", gameCode)) { }

        public RomNoCompatibleException(ICollection<string> values)
        {
           Values = values;
        }

        public RomNoCompatibleException(string gameCodeBase, string gameCodeData) : base(String.Format("RomData edition must be {0} and not {1}", gameCodeBase,gameCodeData))
        {
        }
    }
}
