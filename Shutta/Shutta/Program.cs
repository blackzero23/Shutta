using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    class Program
    {
        public const int SeedMoney = 10000;
        //private const int BettingMoney = 100;

        static void Main(string[] args)
        {
            Console.WriteLine("룰 타입을 선택하세요. (1:Basic 2:Simple)");
            int input = int.Parse(Console.ReadLine());
            RuleType rlueType = (RuleType)input;

            Console.WriteLine("판돈 금액을 정하세요. 최대(1000원)");
            int BettingMoney = int.Parse(Console.ReadLine());

            //오버라이드를 쓰는 전형적인 패턴
            List<Player> players = new List<Player>();

            for (int i = 0; i < 4; i++)
            {
                if (rlueType == RuleType.Basic)
                    players.Add(new BasicPlayer());
                else
                    players.Add(new SimplePlayer());
            }


            foreach (var player in players)
                    player.Money = SeedMoney;

            int round = 1;

            int keepBattingMoney = 0;
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
                    dealer.PutMoney(BettingMoney + keepBattingMoney);
                    keepBattingMoney = 0;
                }

                //카드 돌리기.
                foreach (var player in players)
                {

                player.Cards.Clear(); //카드 초기화.

                 //카드 두장씩.
                for (int i = 0; i < 2; i++)                
                    player.Cards.Add(dealer.DrawCard());


                    Console.WriteLine(  player.GetCardText());
                }

                //승자 찾기
                Player winner = FindWinner(players);

                //무승부라면.
                if (winner == null)
                {
                    Console.WriteLine("무승부!"+"\n");
                    keepBattingMoney += dealer.GetMoney();
                    continue;
                }

                //승자에게 상금 주기
                winner.Money += dealer.GetMoney();


                //각 플레이어의 돈 출력.
                Console.WriteLine("-----------------------");
                foreach (var player in players)
                    Console.WriteLine(player.Money + "\t");
                Console.WriteLine();
            }




        }

        private static Player FindWinner(List<Player> players)
        {

            int[] score = new int[4];

            for (int i = 0; i < 4; i++)
            {
                score[i] = players[i].CaculateScore();

            }
            
            
            int idx = 0;
            for (int i = 0; i < (score.Length)-1; i++)
            {
                if (score[i] < score[i + 1])
                {
                   
                    idx = i + 1;
                    
                }

            }
            return players[idx];


        }

        private static bool isAnyoneOring(List<Player> players)
        {
            foreach (Player player in players)
                if (player.Money == 0)
                    return true;

            return false;
        }
    }
}
