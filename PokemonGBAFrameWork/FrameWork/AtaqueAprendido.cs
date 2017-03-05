using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gabriel.Cat.Extension;
namespace PokemonGBAFrameWork
{
   public class AtaquesAprendidos
    {
        enum Variable
        {
            AtaquesAprendidos
        }
      
        public static readonly byte[] MarcaFin;

        BloqueBytes bytesAtaqueAprendido;


        static AtaquesAprendidos()
        {
            Zona zonaAtaquesAprendidos = new Zona(Variable.AtaquesAprendidos);
            MarcaFin = new byte[] { 0xFF, 0xFF }; 
            //añado las zonas
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x6930C);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x6930C);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x3E968);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x3E968);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x3EA7C);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x3EA7C);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x3B988);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x3B988);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x3B7BC);
            zonaAtaquesAprendidos.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x3B7BC);

            Zona.DiccionarioOffsetsZonas.Add(zonaAtaquesAprendidos);
        }

        public BloqueBytes BytesAtaqueAprendido
        {
            get
            {
                return bytesAtaqueAprendido;
            }

            private set
            {
                bytesAtaqueAprendido = value;
            }
        }
        public static AtaquesAprendidos GetAtaquesAprendidos(RomData rom, Hex indexPokemon)
        { return GetAtaquesAprendidos(rom.RomGBA, rom.Edicion, rom.Compilacion, indexPokemon); }
        public static AtaquesAprendidos GetAtaquesAprendidos(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion,Hex indexPokemon)
        {
            Hex offset = Offset.GetOffset(rom,GetOffsetPrimerPointer(rom,edicion,compilacion) + indexPokemon * (int)Longitud.Offset);
            BloqueBytes bloque = BloqueBytes.GetBytes(rom,offset,MarcaFin);
            return new AtaquesAprendidos() { BytesAtaqueAprendido = bloque };
        }
        private static Hex GetOffsetPrimerPointer(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion)
        {
            return  Zona.GetOffset(rom, Variable.AtaquesAprendidos, edicion, compilacion);
        }
        public static void SetAtaquesAprendidos(RomData rom, Hex indexPokemon, AtaquesAprendidos ataquesAprendidos)
        {  SetAtaquesAprendidos(rom.RomGBA, rom.Edicion, rom.Compilacion, indexPokemon,ataquesAprendidos); }
        public static void SetAtaquesAprendidos(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex indexPokemon,AtaquesAprendidos ataquesAprendidos)
        {
            Hex offset = Offset.GetOffset(rom,Zona.GetOffset(rom, Variable.AtaquesAprendidos, edicion, compilacion) + indexPokemon * (int)Longitud.Offset);
            BloqueBytes bloqueOri = BloqueBytes.GetBytes(rom,offset, MarcaFin);
            BloqueBytes.RemoveBytes(rom, bloqueOri.OffsetInicio, bloqueOri.Bytes.Length);
            if (ataquesAprendidos.BytesAtaqueAprendido.Bytes[ataquesAprendidos.BytesAtaqueAprendido.Bytes.Length - 2] != MarcaFin[0] || ataquesAprendidos.BytesAtaqueAprendido.Bytes[ataquesAprendidos.BytesAtaqueAprendido.Bytes.Length - 1] != MarcaFin[1])
                ataquesAprendidos.BytesAtaqueAprendido.Bytes = ataquesAprendidos.BytesAtaqueAprendido.Bytes.AddArray(MarcaFin);
            Offset.SetOffset(rom, offset, BloqueBytes.SetBytes(rom, ataquesAprendidos.BytesAtaqueAprendido.Bytes));
        }

        internal static int GetTotalPokemon(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            int total = 0;
            while (Offset.IsAPointer(rom, GetOffsetPrimerPointer(rom, edicion, compilacion) + total * (int)Longitud.Offset)) total++;
            return total;
        }
    }
}
