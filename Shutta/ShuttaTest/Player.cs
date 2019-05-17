using System.Collections.Generic;
using System.Dynamic;

namespace ShuttaTest
{
    public class Player
    {
        /*
        private List<Card> _cards = new List<Card>();
        private List<string> _results = new List<string>();
        private List<int> _scores = new List<int>();
        private List<int> _levels = new List<int>();
        private int _money;
        */
        public List<Card> Cards { get; set; }
        public List<string> Results { get; set; }

        public List<int> Scores { get; set; }

        public List<int> Levels { get; set; }

        public int Money { get; set; }

        public Player()
        {
            Cards = new List<Card>();
            Results = new List<string>();
            Scores = new List<int>();
            Levels = new List<int>();

        }

    }
}

