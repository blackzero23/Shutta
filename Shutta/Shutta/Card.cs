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

        //card 클래스 내에서만 변경 가능.
        public bool IsKwang { get; private set; }


        //ctor + tap 생성자 
        public Card(int number, bool isKwang)
        {
            Number = number;
            IsKwang = isKwang;
        }

        public override string ToString()
        {
            if (IsKwang)
                return Number + "K";
            else
                return Number.ToString();
        }
    }
}
