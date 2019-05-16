using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class Dealer
    {
        #region money
        private int _money = 0;//안적어줘도 자동 0으로 초기화.

        public void PutMoney(int bettingMoney)
        {
            _money += bettingMoney;           
        }

        public int GetMoney()
        {
            int moneyToReturn = _money;
            _money = 0;

            return moneyToReturn;
        }
        #endregion

        private List<Card> _cards = new List<Card>();

        private int _cardIndex; //0 으로 카드 초기화

        public Card DrawCard()
        {
            return _cards[_cardIndex++];
        }

        public Dealer()
        {
            
            for (int i = 1; i <= 10; i++) //범위 상수를 설정할때 생각.
            {
                for (int j = 0; j < 2; j++)
                {
                    bool isKwang = (j ==0) && (i==1 || i ==3 || i == 8);
                    Card card = new Card(i,isKwang);
                    _cards.Add(card);
                }
            }

            //람다식으로 셔플함.
             _cards = _cards.OrderBy(x => Guid.NewGuid()).ToList();

        }
      
    }
}
