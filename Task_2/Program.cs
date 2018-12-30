using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace Task_2
{
    class Program
    {
        public static List<int> FindUnique(int[] ArrayOne, int[] ArrayTwo)
        {
            List<int> UniqueNumbers = new List<int>();
            List<int> arrOne = new List<int>(ArrayOne);
            List<int> arrTwo = new List<int>(ArrayTwo);
            IEnumerable<int> distinctOne= arrOne.Distinct();
            IEnumerable<int> distinctTwo = arrTwo.Distinct();

            arrOne = AddUniq(distinctOne);
            arrTwo = AddUniq(distinctTwo);

            UniqueNumbers.AddRange(SearchUniq(arrOne, arrTwo));
            UniqueNumbers.AddRange(SearchUniq(arrTwo, arrOne));

            return UniqueNumbers;
        }

        public static List<int> AddUniq(IEnumerable<int> distinctOne)
        {
            List<int> list = new List<int>();
            foreach (int num in distinctOne)
                list.Add(num);
            return list;
        }

        public static List<int> SearchUniq(List<int> array1, List<int> array2)
        {
            List<int> list = new List<int>();
            foreach (var item in array1)
                if (array2.Where(c => c == item).Count() == 0)
                    list.Add(item);
            return list;
        }

        public static List<int> FindUniqueNotEven(int[] Array)
        {
            List<int> UniqueNumbers = new List<int>();
            List<int> array = new List<int>(Array);
            IEnumerable<int> distinctOne = array.Distinct();
            int i = 0;
            foreach (int num in distinctOne)
            {
                if (num % 2 != 0)
                    UniqueNumbers.Add(num);
                i++;
            }
            return UniqueNumbers;
        }

        public static int SumEvenNumber(int[] ArrayOne, int[] ArrayTwo)
        {
            List<int> RepeatNumbers = new List<int>();
            List<int> arrOne = new List<int>(ArrayOne);
            List<int> arrTwo = new List<int>(ArrayTwo);

            arrOne = SearchEven(arrOne);
            arrTwo = SearchEven(arrTwo);

            RepeatNumbers = SearchEvenNotSecondArray(arrOne, arrTwo);
            return RepeatNumbers.Sum();
        }

        public static List<int> SearchEven(List<int> Array)
        {
            List<int> list = new List<int>();
            foreach (int num in Array)
            {
                if (num % 2 != 0)
                    continue;
                else
                    list.Add(num);
            }
            return list;
        }

        public static List<int> SearchEvenNotSecondArray(List<int> array1, List<int> array2)
        {
            List<int> list = new List<int>();
            foreach (var item in array1)
                if (array2.Where(c => c == item).Count() == 0)
                    list.Add(item);
            return list;
        }

        public static void Display(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine("Array[{0}] = {1}", i, arr[i]);
            }
        }

        static void Main(string[] args)
        {
            string path = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
            var readJson = JsonConvert.DeserializeObject<MyClass>(File.ReadAllText(path + @"/jsonFile.json")); 

            Console.WriteLine();
            Console.WriteLine("Numbers of the first array");
            Display(readJson.firstArray);
            Console.WriteLine();
            Console.WriteLine("Numbers of the second array");
            Display(readJson.secondArray);
            Console.WriteLine("-----------------------------------------");

            Array.Sort(readJson.firstArray);
            Array.Sort(readJson.secondArray);

            List<int> firstUnique = FindUnique(readJson.firstArray, readJson.secondArray);
            for (int i = 0; i < firstUnique.Count; i++)
                Console.WriteLine("Unique numbers from both arrays = {0}", firstUnique[i]);
            Console.WriteLine("-----------------------------------------");

            List<int> UniqueNotEven = FindUniqueNotEven(readJson.firstArray);
            for (int i = 0; i < UniqueNotEven.Count; i++)
                Console.WriteLine("Unique odd number from the first array = {0}", UniqueNotEven[i]);
            Console.WriteLine("-----------------------------------------");

            int count = 0;
            for (int i = 0; i < UniqueNotEven.Count; i++)
            {
                for (int j = 0; j < readJson.secondArray.Length; j++)
                    if (UniqueNotEven[i] == readJson.secondArray[j])
                        count++;
                Console.WriteLine("Number {0} is found {1} times in second array", UniqueNotEven[i], count);
            }
            Console.WriteLine("-----------------------------------------");

            Console.WriteLine("The sum of the even numbers of the first array that are not represented in the second array = {0}", SumEvenNumber(readJson.firstArray, readJson.secondArray));
            Console.ReadKey();
        }
    }
}