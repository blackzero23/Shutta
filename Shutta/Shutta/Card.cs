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

    }
}
