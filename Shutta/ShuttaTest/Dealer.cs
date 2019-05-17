using System;
using System.Collections.Generic;
using System.Linq;

namespace ShuttaTest
{
    public class Dealer
    {
        private List<Card> _cards = new List<Card>();
        private int _money;

        public Dealer()
        {
            
            for (int number = 1; number <= 20; number++)
            {
            
                    Card card = new Card(number);
                    _cards.Add(card);
                
                
            }
            _cards = _cards.OrderBy(x => Guid.NewGuid()).ToList();
        }

        private int _cardIndex;
        public Card DrawCard()
        {
            Card card = _cards[_cardIndex++];

            return card;
        }
    }
}