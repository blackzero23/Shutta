using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class StorageMoney
    {
        private int _battingMoney;
        private int _basicMoney;

        public StorageMoney(int basicMoney)
        {
            _basicMoney = basicMoney;
        }

        public void TakeBattingMoney(int battingMoney)
        {
            _battingMoney += battingMoney;
        }

        public int GiveBattiongMoney()
        {
            int _returnToMoney = _battingMoney;

            _battingMoney = 0;

            return _returnToMoney;
        }
    }
}
