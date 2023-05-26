using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Diagnostics;


namespace TextProcessingDll
{
    public class TextStatistics
    {
        struct Element
        {
            public string Word { get; set; }
            public int Value { get; set; }
        }

        //Публичный метод обработки
        public Dictionary<string, int> ProcessingLinq(string text)
        {
            //Словарь, куда в начале будут добавляться слова и их количество
            Dictionary<string, int> rating = new Dictionary<string, int>();

            //Выделяет слово
            Regex regex = new Regex(@"\w+");

            MatchCollection matches = regex.Matches(text);
            int ValueLines = 0;

            //Записывает в cловарь новые слова
            foreach (Match match in matches)
            {
                //Если слово уже есть в словаре, то переходит к следующему
                if (rating.ContainsKey(match.ToString()) != false)
                {
                    rating[match.ToString()]++;
                }
                //Если слова нету, то добавляет новый элемент в словарь
                else
                {
                    rating.Add(match.ToString(), 1);
                    ValueLines++;
                }
            }

            Stopwatch stopwatch = new Stopwatch();
            //засекаем время начала операции
            stopwatch.Start();
            //Сортировка
            var rating1 = rating.OrderByDescending(x => x.Value);
            stopwatch.Stop();
            Console.WriteLine("Время LINQ сортировки: " + stopwatch.ElapsedMilliseconds); 
            rating = new Dictionary<string, int>();
            foreach(var i in rating1)
                rating.TryAdd(i.Key, i.Value);
            //rating = result as Dictionary<string, int>; 

            return rating;
        }

        static Element[] element;
        //Приватный метод обработки
        private Dictionary<string, int> ProcessingPrivate(string text)
        {
            //Словарь, куда в начале будут добавляться слова и их количество
            Dictionary<string, int> rating = new Dictionary<string, int>();

            //Выделяет слово
            Regex regex = new Regex(@"\w+");

            MatchCollection matches = regex.Matches(text);
            int ValueLines = 0;

            //Записывает в cловарь новые слова
            foreach (Match match in matches)
            {
                //Если слово уже есть в словаре, то переходит к следующему
                if (rating.ContainsKey(match.ToString()) != false)
                {
                    rating[match.ToString()]++;
                }
                //Если слова нету, то добавляет новый элемент в словарь
                else
                {
                    rating.Add(match.ToString(), 1);
                    ValueLines++;
                }
            }

            element = new Element[ValueLines];
            int h = 0;

            //Словарь переписывается в массив для сортировки
            foreach (var g in rating)
            {
                element[h].Word = g.Key;
                element[h].Value = g.Value;
                h++;
            }

            Stopwatch stopwatch = new Stopwatch();
            //засекаем время начала операции
            stopwatch.Start();
            //Сортировка массива
            element = QuickSort(element, 0, element.Length - 1);
            stopwatch.Stop();
            Console.WriteLine("Время личной сортировки: " + stopwatch.ElapsedMilliseconds);

            rating = new Dictionary<string, int>();
            foreach(Element i in element)
                rating.TryAdd(i.Word, i.Value);

            return rating;
        }

        //Блок сортировка
        //Нахождение опорного элемента
        static int FindPivot(Element[] array, int minIndex, int maxIndex)
        {
            int pivot = minIndex - 1;
            Element temp = new Element();
            for (int i = minIndex; i < maxIndex; i++)
            {
                if (array[i].Value > array[maxIndex].Value)
                {
                    pivot++;
                    temp.Value = array[pivot].Value;
                    temp.Word = array[pivot].Word;

                    array[pivot].Value = array[i].Value;
                    array[pivot].Word = array[i].Word;

                    array[i].Value = temp.Value;
                    array[i].Word = temp.Word;
                }
            }
            pivot++;

            temp.Value = array[pivot].Value;
            temp.Word = array[pivot].Word;

            array[pivot].Value = array[maxIndex].Value;
            array[pivot].Word = array[maxIndex].Word;

            array[maxIndex].Value = temp.Value;
            array[maxIndex].Word = temp.Word;

            return pivot;
        }

        //Быстра сортировка
        static Element[] QuickSort(Element[] array, int minIndex, int maxIndex)
        {
            Console.WriteLine("QuickSort");
            if (minIndex >= maxIndex)
            {
                return array;
            }

            int pivot = FindPivot(array, minIndex, maxIndex);
            
            QuickSort(array, minIndex, pivot - 1);
            QuickSort(array, pivot + 1, maxIndex);

            return array;
        }
    }

}
