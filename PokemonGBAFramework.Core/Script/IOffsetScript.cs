using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public interface IOffsetScript
    {
        OffsetRom Offset { get; set; }
    }
}
