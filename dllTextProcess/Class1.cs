using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace TextProcessingDll
{
    public class TextStatistics
    {
        struct Element
        {
            private string Word { get; set; }
            private int Value { get; set; }
            public string GetWord() { return Word; }
            public void SetWord(string Word) { this.Word = Word; }
            public int GetValue() { return Value; }
            public void SetValue(int Value) { this.Value = Value; }
            public Element(string word, int value)
            {
                this.Word = word;
                this.Value = value;
            }
        }

        static Element[] element;
        
        //Публичный метод обработки
        public Dictionary<string, int> ProcessingPublic(string text)
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
                element[h].SetWord(g.Key);
                element[h].SetValue(g.Value);
                h++;
            }

            //Сортировка массива
            var thread = new Thread(() => QuickSort(element, 0, element.Length - 1));


            rating = new Dictionary<string, int>();

            foreach (var i in element)
            {
                rating.TryAdd(i.GetWord(), i.GetValue());
            }

            return rating;
        }

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
                element[h].SetWord(g.Key);
                element[h].SetValue(g.Value);
                h++;
            }

            //Сортировка массива
            QuickSort(element, 0, element.Length - 1);


            rating = new Dictionary<string, int>();

            foreach (var i in element)
            {
                rating.TryAdd(i.GetWord(), i.GetValue());
            }

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
                if (array[i].GetValue() > array[maxIndex].GetValue())
                {
                    pivot++;
                    temp.SetValue(array[pivot].GetValue());
                    temp.SetWord(array[pivot].GetWord());

                    array[pivot].SetValue(array[i].GetValue());
                    array[pivot].SetWord(array[i].GetWord());

                    array[i].SetValue(temp.GetValue());
                    array[i].SetWord(temp.GetWord());
                }
            }
            pivot++;

            temp.SetValue(array[pivot].GetValue());
            temp.SetWord(array[pivot].GetWord());

            array[pivot].SetValue(array[maxIndex].GetValue());
            array[pivot].SetWord(array[maxIndex].GetWord());

            array[maxIndex].SetValue(temp.GetValue());
            array[maxIndex].SetWord(temp.GetWord());

            return pivot;
        }

        //Быстра сортировка
        static void QuickSort(Element[] array, int minIndex, int maxIndex)
        {
            Console.WriteLine("QuickSort");
            if (minIndex >= maxIndex)
            {
                return;
            }

            int pivot = FindPivot(array, minIndex, maxIndex);
            
            QuickSort(array, minIndex, pivot - 1);
            QuickSort(array, pivot + 1, maxIndex);

            if(element.Length == array.Length)
                element = array;

            return;
        }
    }

}
