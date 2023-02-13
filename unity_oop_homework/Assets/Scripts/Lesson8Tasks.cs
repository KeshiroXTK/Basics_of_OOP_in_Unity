using System;
using UnityEngine;

namespace General
{
    public class Lesson8Tasks: MonoBehaviour
    {
        private void Awake()
        {
            Action<Liquid> actionWithLiquid = liquid => liquid.Drink();

            //Контрвариантность
            Action<Juice> actionWithJuice = actionWithLiquid;
            
            Func<Liquid, Juice> liquidToJuice = liquid => new Juice();
           
            //Ковариантность
            Func<Juice, Liquid> action2 = liquidToJuice;
        }
    }

    public class Liquid
    {
        public void Drink()
        {
            Debug.Log("You drink liquid");
        }
    }

    public class Juice : Liquid
    {
        
    }
    
    
}