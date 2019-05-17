using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shutta;

namespace Shutta
{
    
    class Program 
    {
        public const int SeedMoney = 10000;
        private const int MaxPlayer = 4;
        
        static void Main(string[] args)
        {

            //너무적은 금액은 못걸도록 해야됨 너무 오래감.
            Console.WriteLine("판돈 금액을 정하세요. 최대(1000원)");
            int BettingMoney = int.Parse(Console.ReadLine());
            


            //오버라이드를 쓰는 전형적인 패턴
            List<Player> players = new List<Player>();

            for (int i = 0; i < MaxPlayer; i++)
            {               
              players.Add(new OriginalPlayer(i));
            }
            
            //초기화 ??
            foreach (var player in players)
            {
                //초기 목돈.
                player.Money = SeedMoney;
                for (int i = 0; i < 3; i++)
                {
                    player.Levels.Add(0);
                    player.Scores.Add(0);
                    player.Results.Add("");
                }
            }

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
                Batting(BettingMoney, players, dealer);

                //카드 돌리기.

                DrawCard(players, dealer);


                List<int> maxValueIndexs = new List<int>();
                

                

                //승자 찾기                

                for (int i = 0; i < players.Count(); i++)
                {
                    players[i].CaculateScore();
                    maxValueIndexs.Add(FindMaxValueIndex(players[i]));
                    Console.Write(players[i].PlayerName + " : ");
                    Console.Write(players[i].Results[maxValueIndexs[i]]);
                    Console.WriteLine(" : " + players[i].Scores[maxValueIndexs[i]]);
                }

                List<Player> winner = FindWinner(players, maxValueIndexs);

                //무승부라면. 체크.
                winner = CheckCommonWinners(winner);


                Console.WriteLine("-----------------------");
                Console.WriteLine("승자 "+ winner[0].PlayerName);
                Console.WriteLine("-----------------------");

                //승자에게 상금 주기
                winner[0].Money += dealer.GetMoney();

                //각 플레이어의 돈 출력.
                Console.WriteLine("-----------------------");
                foreach (var player in players)
                    Console.WriteLine(player.PlayerName + " : "
                        + player.Money + "\t");
                Console.WriteLine();
            }

        }

        private static int FindMaxValueIndex(Player player)
        {
            
            int maxValueIndex = new int();
            
            for (int j = 0; j < 3; j++)
            {
                int k = (j + 1) % 3;
                if (player.Scores[j] < player.Scores[k])
                    maxValueIndex = k;
            }

            return maxValueIndex;

        }

        private static List<Player> CheckCommonWinners(List<Player> winner)
        {
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

                    List<int> maxValueIndexs = new List<int>();
                    for (int i = 0; i<winner.Count();i++)
                    {
                        winner[i].CaculateScore();
                        maxValueIndexs.Add(FindMaxValueIndex(winner[i]));
                    }

                    winner = FindWinner(winner, maxValueIndexs);
                }
                else
                    break;
            }

     

            return winner;
        }

        private static void DrawCard(List<Player> players, Dealer dealer)
        {
            foreach (var player in players)
            {
                player.Cards.Clear(); //카드 초기화.

                //카드 세장씩.
                for (int i = 0; i < 3; i++)
                    player.Cards.Add(dealer.DrawCard());

                Console.WriteLine(player.GetCardText());               
            }

            Console.WriteLine("\n-----------------------\n");
        }

        private static void Batting(int BettingMoney, List<Player> players, Dealer dealer)
        {
            foreach (var player in players)
            {
                player.Money -= BettingMoney;
                dealer.PutMoney(BettingMoney);
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

       

        private static List<Player> FindWinner(List<Player> players, List<int> maxValueIndexs)
        {

            int[] score = new int[players.Count()];
            int[] ranking = Enumerable.Repeat(1, players.Count()).ToArray<int>();
            
            for (int i = 0; i < players.Count(); i++)
            {
                score[i] = players[i].Scores[maxValueIndexs[i]];
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

        private static void PutPlayerInCards(Dealer dealer, List<Player> players)
        {
            foreach (Player player in players)
            {

                for (int i = 0; i < 3; i++)
                    player.Cards.Add(dealer.DrawCard());
            }
        }
    }
}
