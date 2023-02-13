using System.Data;
using UnityEngine;
using UnityEngine.UI;


namespace General
{
    public class DisplayBonuses
    {
        private Text _text;
        private int _totalPoints;
        
        public DisplayBonuses(GameObject score, int totalPoints)
        {
            if (totalPoints <= 0)
            {
                throw new DataException("total points count must be more than 0");
            }
            
            _text = score.GetComponentInChildren<Text>();
            _totalPoints = totalPoints;
            Display(0);
        }
        
        public void Display(int points)
        {
            _text.text = $"Вы набрали {points} из {_totalPoints}";
        }
    }
}