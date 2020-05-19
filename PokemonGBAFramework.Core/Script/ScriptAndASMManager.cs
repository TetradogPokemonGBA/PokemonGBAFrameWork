using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    /// <summary>
    /// Está pensado para agrupar los Scripts sacados de una Rom para cuando se necesiten cargar
    /// </summary>
    public class ScriptAndASMManager
    {
        public ScriptAndASMManager()
        {
            DicScriptsCargados = new SortedList<int, Script>();
            DicASMCargados = new SortedList<int, BloqueASM>();
        }
        public SortedList<int,Script> DicScriptsCargados { get; set; }
        public SortedList<int, BloqueASM> DicASMCargados { get; set; }
        public unsafe Script GetScript(byte* ptrRom,int offsetScript)
        {
            if (!DicScriptsCargados.ContainsKey(offsetScript))
                DicScriptsCargados.Add(offsetScript, new Script(this,ptrRom, offsetScript));
            return DicScriptsCargados[offsetScript];
        }
        public unsafe BloqueASM GetASM(byte* ptrRom, int offsetASM)
        {
            if (!DicASMCargados.ContainsKey(offsetASM))
                DicASMCargados.Add(offsetASM, new BloqueASM(ptrRom, offsetASM));
            return DicASMCargados[offsetASM];
        }

    }
}
