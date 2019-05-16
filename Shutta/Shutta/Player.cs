using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    abstract public class Player
    {
       

       public Player(int name)
        {
            PlayerName = "Player" + name;
            Cards = new List<Card>();
        }

        //자동 프로퍼티 auto property
        //card 클래스 내에서만 변경 가능.
        //카드 2장.
        //맴버의 값을 못바꾸고 추가할수없다는 말은아니다.
        //Add(new )카능.

        public String PlayerName { get; }

        public List<Card> Cards { get; } 

        public int Money { get; set; }

        public abstract int CaculateScore();

        public String GetCardText()
        {
            Console.Write(PlayerName+ ": ");
            StringBuilder builder = new StringBuilder();
            foreach (var card in Cards)
            {
                builder.Append(card.ToString() + "\t");
            }

            return builder.ToString();
        }
    }
}
