using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;

namespace General
{
    public class Lesson5Tasks : MonoBehaviour
    {
        private void Awake()
        {
            //использование расширение
            
            Debug.Log("message".SymbolCount('s'));
            

            //частотность в списке
            
            var numbers = new List<int>(new [] {1, 0, 5, 6, 3, 4, 1, 6});

            var counts = CountElements(numbers);

            foreach (var count in counts)
            {
                Debug.Log($"{count.Key} - {count.Value}" );
            }
            
            Debug.Log("--- by Linq ---");
            
            var groups = from n in numbers
                group n by n into g
                select new { Key = g.Key, Count = g.Count() };
            
            foreach (var group in groups)
            {
                Debug.Log($"{group.Key} - {group.Count}" );
            }
            
            //Или
            
            var numbersGroups = numbers.GroupBy(n => n)
                .Select(g => new
                { 
                    Key = g.Key, 
                    Count = g.Count(),
                });
            
            
            //упрощеное выражение
            
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"four",4 },
                {"two",2 },
                { "one",1 },
                {"three",3 },
            };

            Func<KeyValuePair<string, int>, int> orderFunc = new Func<KeyValuePair<string, int>, int> (OrderFunc);
            
            //var d = dict.OrderBy(delegate(KeyValuePair<string,int> pair) { return pair.Value; });
            var d = dict.OrderBy(orderFunc);
            var d2 = dict.OrderBy(pair => pair.Value);
            
            foreach (var pair in d)
            {
                Debug.Log($"{pair.Key} - {pair.Value}");
            }

            int OrderFunc(KeyValuePair<string, int> pair)
            {
                return pair.Value;
            }

        }


        private Dictionary<T, int> CountElements<T>(List<T> list)
        {
            var counts = new Dictionary<T, int>();

            for (int i = 0; i < list.Count; i++)
            {
                var element = list[i];
                if (counts.ContainsKey(element))
                {
                    counts[element]++;
                }
                else
                {
                    counts.Add(element, 1);
                }
            }

            return counts;
        }
        
    }
}