using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexesSystem
{
    public interface IPiece
    {
        //int PlayerID { get;}

        //string Name { get; }

        bool Moved { get; set; }

        PieceType PieceType { get; }
    }
}
