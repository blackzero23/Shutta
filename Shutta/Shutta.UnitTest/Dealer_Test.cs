using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shutta.UnitTest
{
    [TestClass]
    public class Dealer_Test
    {
        [TestMethod]
        //테스트 함수는 한글로 권장
        public void 스물장의_카드에는_광이_3장_들어있어야_함()
        {
            Dealer dealer = new Dealer();

            List<Card> cards = new List<Card>();
            for (int i = 0; i < 20; i++)
            {
                Card card = dealer.DrawCard();
                cards.Add(card);
            }

            int kwangCount = 0;
            //for (int i = 0; i < cards.Count; i++)
            //{
            //    if (cards[i].IsKwang)
            //        kwangCount++;
            //}

            foreach (Card card in cards)
                if (card.IsKwang)
                    kwangCount++;

            Assert.AreEqual(3, kwangCount);//기대하는값 , 실제값.
        }

        [TestMethod]
        //테스트 함수는 한글로 권장
        public void 스물장의_카드에는_1이_2장_들어있어야됨()
        {
            Dealer dealer = new Dealer();

            List<Card> cards = new List<Card>();
            for (int i = 0; i < 20; i++)
            {
                Card card = dealer.DrawCard();
                cards.Add(card);
            }

            int countof1 = 0;
            
            foreach (Card card in cards)
                if (card.Number == 1)
                    countof1++;

            Assert.AreEqual(2, countof1);//기대하는값 , 실제값.
        }


        [TestMethod]
        //테스트 함수는 한글로 권장
        public void 무승부시_판돈이_보존되어야된다()
        {
             const int SeedMoney = 500;
             const int BettingMoney = 100;


        Console.WriteLine("룰 타입을 선택하세요. (1:Basic 2:Simple");
            int input = int.Parse(Console.ReadLine());
            RuleType rlueType = (RuleType)input;


            //오버라이드를 쓰는 전형적인 패턴
            List<Player> players = new List<Player>();

            for (int i = 0; i < 2; i++)
            {
                if (rlueType == RuleType.Basic)
                    players.Add(new BasicPlayer());
                else
                    players.Add(new SimplePlayer());
            }


            foreach (var player in players)
                player.Money = SeedMoney;

            int round = 1;
            while (true)
            {
                //한명이 오링이 면 게임 종료
                if (isAnyoneOring(players))
                    break;

                Console.WriteLine($"Round {round}");
                round++;


                //딜러 매 라운드마다 만드는걸로.
                Dealer dealer = new Dealer();

                //학교 출석
                foreach (var player in players)
                {
                    player.Money -= BettingMoney;
                    dealer.PutMoney(BettingMoney);
                }

                //카드 돌리기.
                foreach (var player in players)
                {

                    player.Cards.Clear(); //카드 초기화.

                    //카드 두장씩.
                    for (int i = 0; i < 2; i++)
                        player.Cards.Add(dealer.DrawCard());


                    Console.WriteLine(player.GetCardText());
                }

                //승자 찾기
                Player winner = FindWinner(players);

                //무승부라면.
                if (winner == null)
                    continue;

                //승자에게 상금 주기
                winner.Money += dealer.GetMoney();


                //각 플레이어의 돈 출력.
                Console.WriteLine("-----------------------");
                foreach (var player in players)
                    Console.WriteLine(player.Money + "\t");
                Console.WriteLine();
            }








            //int countof1 = 0;

            //foreach (Card card in cards)
            //    if (card.Number == 1)
            //        countof1++;

            //Assert.AreEqual(2, countof1);//기대하는값 , 실제값.



          

        }

    }
}
