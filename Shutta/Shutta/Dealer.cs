using System;
using System.Collections;
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

        internal void PutMoney(int bettingMoney)
        {
            _money += bettingMoney;           
        }

        internal int GetMoney()
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
            Card card = _cards[_cardIndex++];

            return card;
            
        }

        public Dealer()
        {

            for (int number = 1; number <= 20; number++)
            {

                Card card = new Card(number);
                _cards.Add(card);


            }
            _cards = _cards.OrderBy(x => Guid.NewGuid()).ToList();
        }

    }
}
