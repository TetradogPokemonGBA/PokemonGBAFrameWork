/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 11:11
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Gabriel.Cat;

namespace PokemonGBAFrameWork
{
    /// <summary>
    /// Se usa para guardar los offsets de los pointers que estan en las rutinas.
    /// </summary>
    public class Zona : Var
    {

        public Zona(string nombre) : base(nombre)
        { }
        public Zona(Enum nombre) : base(nombre)
        { }
        public static OffsetRom GetOffsetRom(Zona zona, RomGba rom)
        {
            return GetOffsetRom(zona, rom, rom.Edicion);
        }
        public static OffsetRom GetOffsetRom(Zona zona, RomGba rom, Edicion edicion)
        {
            return GetOffsetRom(zona, rom, edicion, edicion.Compilacion);
        }
        public static void SetOffsetRom(Zona zona, RomGba rom, Edicion edicion, OffsetRom offsetToSet)
        {
            OffsetRom.SetOffset(rom, GetOffsetRom(zona, rom, edicion), offsetToSet.Offset);
        }

        public static OffsetRom GetOffsetRom(Zona zona, RomGba rom, Edicion edicion, Compilacion compilacion)
        {
            return new OffsetRom(rom, GetValue(zona, edicion, compilacion));
        }
    }
}
