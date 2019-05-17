using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class Card
    {
        /*  private int _number;
         * public int number
         * {
         *  get{return _number;}
         * }
         */

        //auto property 
        //prop + tap
        public int Number { get; }//생성자에서만 변경가능


        //ctor + tap 생성자 
        public Card(int number)
        {
            Number = number;
        }


        public override string ToString()
        {

            if (Number == 1 || Number == 3 || Number == 8)
                return Number + "K";
            else if (Number == 20)
                return "10";
            else if (Number > 10)
                return (Number % 10).ToString();
            else
                return Number.ToString();
        }

    }
}
