using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    abstract public class Player
    {
        protected List<Card> cards;

        //List<Card> Cards;


        public Player(int name)
        {
            PlayerName = "Player" + name;
            Cards = new List<Card>();
            Results = new List<string>();
            Scores = new List<int>();
            Levels = new List<int>();

        }

        public Player(String name, int seedMoney)
        {
            PlayerName = "Player" + name;
            Money = seedMoney;
            Cards = new List<Card>();
            Results = new List<string>();
            Scores = new List<int>();
            Levels = new List<int>();

        }



        //자동 프로퍼티 auto property
        //card 클래스 내에서만 변경 가능.
        //카드 2장.
        //맴버의 값을 못바꾸고 추가할수없다는 말은아니다.
        //Add(new )카능.

        public List<Card> Cards { get => cards; set => cards = value; }

        //족보 이름
        public List<string> Results { get; set; }


        // 족보 이름에 따른 점수
        public List<int> Scores { get; set; }


        // 족보 이르멩 따른 랭크(10~0)
        public List<int> Levels { get; set; }

        public int Money { get; set; }
        

        public String PlayerName { get; }

       


        public abstract String GetCardText(); //이거때문에 컴퓨터랑 나랑 찢어지는게 좋을듯.
       

        public String GetCardTextALL()
        {
            Console.Write(PlayerName + ": ");
            StringBuilder builder = new StringBuilder();
            foreach (var card in Cards)
            {
                builder.Append(card.ToString() + "\t");
            }

            return builder.ToString();
        }

        public void CaculateScore()
        {
            //3개의 숫자중족보를 토대로 가장 좋은 조건인 두개의 패를 조합해서 만듬.
            int cnt = 0;
            for (int i = 0; i < Cards.Count(); i++)
            {
                int j = (i + 1) % 3;

                if ((Cards[i].Number == 3 && Cards[j].Number == 8)
                    || (Cards[i].Number == 8 && Cards[j].Number == 3))

                    
                {
                    Levels[cnt] = 10;
                    Scores[cnt] = 30;
                    Results[cnt] = "삼팔광땡";
                }

                else if ((Cards[i].Number == 4 && Cards[j].Number == 7) ||
                         (Cards[i].Number == 7 && Cards[j].Number == 4))
                {
                    Levels[cnt] = 2;
                    Results[cnt] = "암행어사";
                }

                else if (Cards[i].Number == 1 && (Cards[j].Number == 3 || Cards[j].Number == 8)
                    || Cards[j].Number == 1 && (Cards[i].Number == 3 || Cards[i].Number == 8))
                {
                    Levels[cnt] = 9;
                    Scores[cnt] = Cards[i].Number + Cards[j].Number;
                    switch (Scores[cnt])
                    {
                        case 4:
                            Scores[cnt] = 28;
                            Results[cnt] = "일삼광땡";
                            break;

                        case 9:
                            Scores[cnt] = 27;
                            Results[cnt] = "일팔광땡";
                            break;
                    }

                }

                else if ((Cards[i].Number == 9 && Cards[j].Number == 4)
                    || (Cards[i].Number == 4 && Cards[j].Number == 9))
                {
                    Levels[cnt] = 4;
                    Scores[cnt] = 27;
                    Results[cnt] = "멍텅구리 구사";
                }

                else if ((Cards[i].Number == 3 && Cards[j].Number == 7)
                    || (Cards[i].Number == 7 && Cards[j].Number == 3))
                {
                    Levels[cnt] = 1;
                    Results[cnt] = "땡잡이";
                }

                else if ((Cards[i].Number % 10) == (Cards[j].Number % 10))
                {
                    Levels[cnt] = 7;
                    switch (Cards[i].Number % 10)
                    {
                        case 1:
                            Scores[cnt] = 17;
                            Results[cnt] = "일땡";
                            break;

                        case 2:
                            Scores[cnt] = 18;
                            Results[cnt] = "이땡";
                            break;

                        case 3:
                            Scores[cnt] = 19;
                            Results[cnt] = "삼땡";
                            break;

                        case 4:
                            Scores[cnt] = 20;
                            Results[cnt] = "사땡";
                            break;

                        case 5:
                            Scores[cnt] = 21;
                            Results[cnt] = "오땡";
                            break;

                        case 6:
                            Scores[cnt] = 22;
                            Results[cnt] = "육땡";
                            break;

                        case 7:
                            Scores[cnt] = 23;
                            Results[cnt] = "칠땡";
                            break;

                        case 8:
                            Scores[cnt] = 24;
                            Results[cnt] = "팔땡";
                            break;

                        case 9:
                            Scores[cnt] = 25;
                            Results[cnt] = "구땡";
                            break;

                        case 0:
                            Levels[cnt] = 8;
                            Scores[cnt] = 26;
                            Results[cnt] = "장땡";
                            break;

                    }
                }

                else if ((Cards[i].Number % 10) == 1 || (Cards[j].Number % 10) == 1)
                {
                    Levels[cnt] = 6;
                    Scores[cnt] = (Cards[i].Number + Cards[j].Number) % 10;
                    switch (Scores[cnt])
                    {
                        case 3:
                            Scores[cnt] = 15;
                            Results[cnt] = "알리";
                            break;

                        case 5:
                            Scores[cnt] = 14;
                            Results[cnt] = "독사";
                            break;

                        case 0:
                            Scores[cnt] = 13;
                            Results[cnt] = "구삥";
                            break;

                        case 1:
                            Scores[cnt] = 12;
                            Results[cnt] = "장삥";
                            break;

                        default:
                            Levels[cnt] = 0;
                            Results[cnt] = Scores[cnt] + "끝";
                            break;
                    }
                }

                else if ((Cards[i].Number % 10) == 4 || (Cards[j].Number % 10) == 4)
                {
                    Levels[cnt] = 5;
                    Scores[cnt] = (Cards[i].Number + Cards[j].Number) % 10;
                    switch (Scores[cnt])
                    {
                        case 4:
                            Scores[cnt] = 11;
                            Results[cnt] = "장사";
                            break;

                        case 0:
                            Scores[cnt] = 10;
                            Results[cnt] = "세륙";
                            break;

                        case 3:
                            Levels[cnt] = 3;
                            Scores[cnt] = 16;
                            Results[cnt] = "구사";
                            break;

                        default:
                            Levels[cnt] = 0;
                            Results[cnt] = Scores[cnt] + "끝";
                            break;
                    }
                }

                else
                {
                    Levels[cnt] = 0;
                    Scores[cnt] = (Cards[i].Number + Cards[j].Number) % 10;
                    Results[cnt] = Scores[cnt] + "끝";

                    if (Scores[cnt] == 0)
                        Results[cnt] = "망통";
                }

                ++cnt;
            }


        }



    }
}
