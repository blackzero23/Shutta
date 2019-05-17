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


            //너무적은 금액은 못걸도록 해야됨 너무 오래감.
            Console.WriteLine("판돈 금액을 정하세요. 최대(1000원)");
            int BettingMoney = int.Parse(Console.ReadLine());
            



           

            //오버라이드를 쓰는 전형적인 패턴
            List<Player> players = new List<Player>();

            for (int i = 0; i < 4; i++)
            {
                if (rlueType == RuleType.Basic)
                    players.Add(new BasicPlayer(i));
                else
                    players.Add(new SimplePlayer(i));
            }


            foreach (var player in players)
                    player.Money = SeedMoney;

            int round = 1;

            int keepBattingMoney = 0;
            while (true)

            //딜러 매 라운드마다 만드는걸로.
            Dealer dealer = new Dealer();

            while (true)
            {
                //한명이 오링이 면 게임 종료
                if (isAnyoneOring(players))                  
                    break;

                Console.WriteLine($"Round {round}");
                round++;

                dealer.initCard();


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

                    //카드 세장씩.
                    for (int i = 0; i < 3; i++)
                        player.Cards.Add(dealer.DrawCard());
                    Console.WriteLine(player.GetCardText());
                }
              
                //승자 찾기
                List<Player> winner = FindWinner(players);

                //무승부라면. 체크.
                while (true)
                {
                    if (winner.Count() >= 2)
                    {
                        Console.WriteLine(" 무승부! ");
                        Console.WriteLine("-승자들 끼리 다시 승부-");

                        //무승부를 위한 딜러..
                        //시간 되면 딜러 자체 클래스 재조정. 

                        Dealer SecondDealer = new Dealer(); 

                        foreach (var player in winner)
                        {
                            player.Cards.Clear(); //카드 초기화.
                            
                            //카드 세장씩.
                            for (int i = 0; i < 3; i++)
                                player.Cards.Add(SecondDealer.DrawCard());
                            Console.WriteLine(player.GetCardText());
                        }

                        winner = FindWinner(winner);
                    }
                    else
                        break;
                }               
                //승자에게 상금 주기

                winner[0].Money += dealer.GetMoney();

                //각 플레이어의 돈 출력.
                Console.WriteLine("-----------------------");
                foreach (var player in players)                   
                    Console.WriteLine(player.PlayerName +" : "
                        +player.Money + "\t");
                Console.WriteLine();
                
            }

        }


        private static void DealtCard(List<Player> players,Dealer dealer)
        {
            foreach (var player in players)
            {
                player.Cards.Clear(); //카드 초기화.

                //카드 세장씩.
                for (int i = 0; i < 3; i++)
                    player.Cards.Add(dealer.DrawCard());
                Console.WriteLine(player.GetCardText());
            }

        }


        private static List<Player> FindWinner(List<Player> players)
        {

            int[] score = new int[players.Count()];
            int[] ranking = Enumerable.Repeat(1, players.Count()).ToArray<int>();
            
            for (int i = 0; i < players.Count(); i++)
            {
                score[i] = players[i].CaculateScore();
            }

            //조건. 1등에 다 등수가 같을 경우 같은 애들 2명 리스트로 담아서 반환.

            //등수 구함.
            for (int i = 0; i < players.Count(); i++)
            {
                for (int j = 0; j < players.Count(); j++)               
                    if (score[i] < score[j])
                        ranking[i]++;              
            }

            //1등 체크
           
            List<Player> NumberOnePlayer = new List<Player>();
            for (int i = 0; i < players.Count(); i++)
            {
                if (ranking[i] == 1)             
                    NumberOnePlayer.Add(players[i]);
            }
      

            return NumberOnePlayer;
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
