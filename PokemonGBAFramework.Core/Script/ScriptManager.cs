using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    /// <summary>
    /// Está pensado para agrupar los Scripts sacados de una Rom para cuando se necesiten cargar
    /// </summary>
    public class ScriptManager
    {
        public ScriptManager()
        {
            DicScriptsCargados = new SortedList<int, Script>();
        }
        public SortedList<int,Script> DicScriptsCargados { get; set; }

        public unsafe Script Get(byte* ptrRom,int offsetScript)
        {
            if (!DicScriptsCargados.ContainsKey(offsetScript))
                DicScriptsCargados.Add(offsetScript, new Script(this,ptrRom, offsetScript));
            return DicScriptsCargados[offsetScript];
        }


        public int Set(BloqueBytes ptrRom, Script script)
        {
            throw new NotImplementedException();
        }
    }
}
