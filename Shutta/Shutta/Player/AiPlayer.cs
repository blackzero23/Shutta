using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    class AiPlayer : Player
    {
        public AiPlayer(string name, int seedMoney) : base(name, seedMoney)
        {
        }

        public override String GetCardText() //이거때문에 컴퓨터랑 나랑 찢어지는게 좋을듯.
        {
            Console.Write(PlayerName + ": ");

            return cards[0].ToString() + "\t";
        }

    }
}
