﻿using System;
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
        public const int SeedMoney = 500;
        private const int BettingMoney = 100;
        private const int MaxPlayer = 4;

        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();

            //PlayerSetup()

            for (int i = 0; i < MaxPlayer; i++)
                players.Add(new Player());

            foreach (var player in players)
            {
                player.Money = SeedMoney;
                for (int i = 0; i < 3; i++)
                {
                    player.Levels.Add(0);
                    player.Scores.Add(0);
                    player.Results.Add("");
                }
            }

            int round = 1;
            while(true)
            {
                //한명이 오링이 면 게임 종료
                if (isAnyoneOring(players))
                    break;

                Console.WriteLine($"Round {round}" );
                round++;


                //딜러 매 라운드마다 만드는걸로.
                Dealer dealer = new Dealer();

                PutPlayerInCards(dealer, players);
                foreach (var player in players)
                    CheckPae(player);

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

                        for (int i = 0; i < 3; i++)
                        {                 
                            player.Cards.Add(dealer.DrawCard());
                        }
                                

                    Console.WriteLine(player.GetCardText());
                }


                //승자 찾기
                Player winner = FindWinner(players);

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

           
            //무승부 구현 해야됨.
            if (score0 > score1)
                return players[0];
            else
                return players[1];



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

        private static void CheckPae(Player player)
        {
            int cnt = 0;
            for (int i = 0; i < 3; i++)
            {
                int j = (i + 1) % 3;

                if ((player.Cards[i].Number == 3 && player.Cards[j].Number == 8) || (player.Cards[i].Number == 8 && player.Cards[j].Number == 3))
                {
                    player.Levels[cnt] = 10;
                    player.Scores[cnt] = 30;
                    player.Results[cnt] = "삼팔광땡";
                }

                else if ((player.Cards[i].Number == 4 && player.Cards[j].Number == 7) ||
                         (player.Cards[i].Number == 7 && player.Cards[j].Number == 4))
                {
                    player.Levels[cnt] = 2;
                    player.Results[cnt] = "암행어사";
                }

                else if (player.Cards[i].Number == 1 && (player.Cards[j].Number == 3 || player.Cards[j].Number == 8) || player.Cards[j].Number == 1 && (player.Cards[i].Number == 3 || player.Cards[i].Number == 8))
                {
                    player.Levels[cnt] = 9;
                    player.Scores[cnt] = player.Cards[i].Number + player.Cards[j].Number;
                    switch (player.Scores[cnt])
                    {
                        case 4:
                            player.Scores[cnt] = 28;
                            player.Results[cnt] = "일삼광땡";
                            break;

                        case 9:
                            player.Scores[cnt] = 27;
                            player.Results[cnt] = "일팔광땡";
                            break;
                    }

                }

                else if ((player.Cards[i].Number == 9 && player.Cards[j].Number == 4) || (player.Cards[i].Number == 4 && player.Cards[j].Number == 9))
                {
                    player.Levels[cnt] = 4;
                    player.Scores[cnt] = 27;
                    player.Results[cnt] = "멍텅구리 구사";
                }

                else if ((player.Cards[i].Number == 3 && player.Cards[j].Number == 7) || (player.Cards[i].Number == 7 && player.Cards[j].Number == 3))
                {
                    player.Levels[cnt] = 1;
                    player.Results[cnt] = "땡잡이";
                }

                else if ((player.Cards[i].Number % 10) == (player.Cards[j].Number % 10))
                {
                    player.Levels[cnt] = 7;
                    switch (player.Cards[i].Number % 10)
                    {
                        case 1:
                            player.Scores[cnt] = 17;
                            player.Results[cnt] = "일땡";
                            break;

                        case 2:
                            player.Scores[cnt] = 18;
                            player.Results[cnt] = "이땡";
                            break;

                        case 3:
                            player.Scores[cnt] = 19;
                            player.Results[cnt] = "삼땡";
                            break;

                        case 4:
                            player.Scores[cnt] = 20;
                            player.Results[cnt] = "사땡";
                            break;

                        case 5:
                            player.Scores[cnt] = 21;
                            player.Results[cnt] = "오땡";
                            break;

                        case 6:
                            player.Scores[cnt] = 22;
                            player.Results[cnt] = "육땡";
                            break;

                        case 7:
                            player.Scores[cnt] = 23;
                            player.Results[cnt] = "칠땡";
                            break;

                        case 8:
                            player.Scores[cnt] = 24;
                            player.Results[cnt] = "팔땡";
                            break;

                        case 9:
                            player.Scores[cnt] = 25;
                            player.Results[cnt] = "구땡";
                            break;

                        case 0:
                            player.Levels[cnt] = 8;
                            player.Scores[cnt] = 26;
                            player.Results[cnt] = "장땡";
                            break;

                    }
                }

                else if ((player.Cards[i].Number % 10) == 1 || (player.Cards[j].Number % 10) == 1)
                {
                    player.Levels[cnt] = 6;
                    player.Scores[cnt] = (player.Cards[i].Number + player.Cards[j].Number) % 10;
                    switch (player.Scores[cnt])
                    {
                        case 3:
                            player.Scores[cnt] = 15;
                            player.Results[cnt] = "알리";
                            break;

                        case 5:
                            player.Scores[cnt] = 14;
                            player.Results[cnt] = "독사";
                            break;

                        case 0:
                            player.Scores[cnt] = 13;
                            player.Results[cnt] = "구삥";
                            break;

                        case 1:
                            player.Scores[cnt] = 12;
                            player.Results[cnt] = "장삥";
                            break;

                        default:
                            player.Levels[cnt] = 0;
                            player.Results[cnt] = player.Scores[cnt] + "끝";
                            break;
                    }
                }

                else if ((player.Cards[i].Number % 10) == 4 || (player.Cards[j].Number % 10) == 4)
                {
                    player.Levels[cnt] = 5;
                    player.Scores[cnt] = (player.Cards[i].Number + player.Cards[j].Number) % 10;
                    switch (player.Scores[cnt])
                    {
                        case 4:
                            player.Scores[cnt] = 11;
                            player.Results[cnt] = "장사";
                            break;

                        case 0:
                            player.Scores[cnt] = 10;
                            player.Results[cnt] = "세륙";
                            break;

                        case 3:
                            player.Levels[cnt] = 3;
                            player.Scores[cnt] = 16;
                            player.Results[cnt] = "구사";
                            break;

                        default:
                            player.Levels[cnt] = 0;
                            player.Results[cnt] = player.Scores[cnt] + "끝";
                            break;
                    }
                }

                else
                {
                    player.Levels[cnt] = 0;
                    player.Scores[cnt] = (player.Cards[i].Number + player.Cards[j].Number) % 10;
                    player.Results[cnt] = player.Scores[cnt] + "끝";

                    if (player.Scores[cnt] == 0)
                        player.Results[cnt] = "망통";
                }

                ++cnt;
            }

        }
    }
}
