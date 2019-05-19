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
        private const int MaxPlayer = 2;
        
        static void Main(string[] args)
        {

            Console.WriteLine("사용자 이름을 입력해주세요 : ");
            string playerName = Console.ReadLine();

            Dealer dealer = new Dealer();
            int basicMoney = dealer.AskBasicMoney();                            

            List<Player> players = new List<Player>();

            players.Add(new UserPlayer(playerName, SeedMoney));
            players.Add(new AiPlayer("컴퓨터", SeedMoney));              
                
            int round = 1;
                       
            while (true)
            {
                //한명이 오링이 면 게임 종료
                if (isAnyoneOring(players))
                    break;

                Console.WriteLine($"Round {round}");
                round++;

                StorageMoney betStorage = new StorageMoney(basicMoney);

                foreach (var player in players)
                {
                    player.Money -= basicMoney;
                    betStorage.TakeBattingMoney(basicMoney);
                 }

                //첫 패돌리고
                DrawCard(players, dealer, 2);
                foreach (var player in players)
                    player.GetCardText();

                //컴퓨터 의사 먼저 확인 배팅 확인. 컴퓨터는자신의 가장높은숫자와  사용자의 앞 패의 숫자랑만 비교한다.
                //자신이 나올수 있는 가장 높은 점수. 와 앞패로 가질수있는 가장 높은 점수 계산해서 비교.
                //예) 점수가 10높으면 콜 점수가 5높으면
                //사용자 패를 보여주고 족보에 맞추어 계산한걸 보여준다.
                //사용자 배팅 의사 확인.
                List<int> maxValueIndexs = new List<int>();
                for (int i = 0; i < MaxPlayer; i++)
                {
                    players[i].CaculateScore();
                    maxValueIndexs.Add(FindMaxValueIndex(players[i]));
                }



                ShowCard();
                //패돌리고
                DrawCard(players, dealer, 1);
                //컴퓨터 의사 먼저 확인
                //배팅콜?

                //승자 찾고 무승부면 승자 나올때까지 게임 다시 .

                //승자에게 돈지급.



                //게임이 끝나면 각플레이더들 카드 초기화
                foreach (var player in players)
                    player.Cards.Clear();

               

                CardListInformation(players[0]);

               

                //승자 찾기                

               

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

        private static void ShowCard(List<Player> players)
        {
            //1번째 는 나.

            Console.WriteLine("\n-----------------------\n");
            Console.WriteLine(players[0].GetCardTextALL());
            Console.WriteLine(players[1].GetCardComText());
            Console.WriteLine("\n-----------------------\n");
        }

        private static void CardListInformation(Player player)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(player.Results[i] + " : " + player.Scores[i]);
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

        private static void DrawCard(List<Player> players, Dealer dealer,int drawCardCnt)
        {                       
            foreach (var player in players)
            {
                //player.Cards.Clear(); //카드 초기화.
                for (int i = 0; i < drawCardCnt; i++)
                    player.Cards.Add(dealer.DrawCard());
            }

        }

        private static void Batting(int BettingMoney, List<Player> players, Dealer dealer)
        {
            
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
