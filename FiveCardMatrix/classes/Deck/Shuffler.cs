//        ~~~ Class that implements array shuffling ~~~
//(RandomStringArrayTool.cs, C#)
// Modified from http://dotnetperls.com/shuffle-array
using System;
using System.Collections.Generic;
using System.Linq;

namespace FiveCardMatrix
{
     public static class Shuffler
     {
    
            /// <summary>
            /// Stores the current random number
            ///  Modified from http://dotnetperls.com/shuffle-array
            /// </summary>
            static Random _random = new Random();

            /// <summary>
            /// Return randomized version of the stringarray (generic method created from above)
            /// </summary>
            public static T[] Randomize<T>(T[] arr)
            {
                List<KeyValuePair<int, T>> list = new List<KeyValuePair<int, T>>();
                // Add all strings from array
                // Add new random int each time
                foreach (T item in arr)
                {
                    list.Add(new KeyValuePair<int, T>(_random.Next(), item));
                }
                // Sort the list by the random number
                var sorted = from item in list
                             orderby item.Key
                             select item;
                // Allocate new array
                T[] result = new T[arr.Length];
                // Copy values to array
                int index = 0;
                foreach (KeyValuePair<int, T> pair in sorted)
                {
                    result[index] = pair.Value;
                    index++;
                }
                // Return copied array
                return result;
            }
            public static List<T> RandomizeListOf<T>(List<T> arr)
            {
                return new List<T> (Randomize<T>(arr.ToArray<T>()));
            }
        }
    }
