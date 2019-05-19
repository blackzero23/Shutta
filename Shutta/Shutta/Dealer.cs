using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class Dealer
    {        
        private List<Card> _cards = new List<Card>();

        private int _cardIndex; //0 으로 카드 초기화

        public Card DrawCard()
        {
            return _cards[_cardIndex++];
        }

        public Dealer()
        {
            for (int i = 1; i <= 20; i++) //범위 상수를 설정할때 생각.
            {
                    Card card = new Card(i);
                    _cards.Add(card); 
            }

            //람다식으로 셔플함.
            _cards = _cards.OrderBy(x => Guid.NewGuid()).ToList();
                        
            //for (int i = 1; i <= 10; i++) //범위 상수를 설정할때 생각.
            //{
            //    for (int j = 0; j < 2; j++)
            //    {
            //        bool isKwang = (j ==0) && (i==1 || i ==3 || i == 8);
            //        Card card = new Card(i,);
            //        _cards.Add(card);
            //    }
            //}            
        }

        public int AskBasicMoney()
        {
            Console.WriteLine("기본 금액을 정해주세요.");
            Console.WriteLine("최소 500원 ~ 최대 1000원\n");
            int bettingMoney;
            while (true)
            {
                bettingMoney = int.Parse(Console.ReadLine());
                if (bettingMoney > 1000 || bettingMoney <500)
                    Console.WriteLine("제대로 입력해주세요.");                
                else
                    break;
            }
            return bettingMoney;
        }

    }
}
