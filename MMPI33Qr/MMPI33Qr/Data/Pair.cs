using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrLib.Data
{
    public record Pair(int X, int Y)
    {
        public static implicit operator Pair((int x, int y) pos) => new(pos.x, pos.y);
    }
}
