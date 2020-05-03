using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public delegate T GetMethod<T>(RomGba rom,int ordenGameFreak,OffsetRom offsetInicio);
    public delegate T[] GetTodos<T>(RomGba rom,OffsetRom offsetInicio);
}
