using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Mapa
{//nombre clase no definitivo
    public class Bloque
    {
        public Bloque(int tile, int collision, int elevation)
        {
            this.Tile = (ushort)tile;
            this.Collision = (ushort)collision;
            this.Elevation = (ushort)elevation;
        }

        public ushort Tile { get; set; }
        public ushort Collision { get; set; }
        public ushort Elevation { get; set; }
        public static implicit operator ushort(Bloque bloque)
        {
            return (ushort)((bloque.Tile & 0x3ff) + ((bloque.Collision & 0x3) << 10) + ((bloque.Elevation & 0xf) << 12));
        }
        public static explicit operator Bloque(ushort bloqueShort)
        {
            return new Bloque(bloqueShort&0x3FF,(bloqueShort>>10)&0x3,(bloqueShort>>12)&0xF);
        }
    }
}
