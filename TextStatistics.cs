using System.Text.RegularExpressions;
using System.Collections.Generic;

class TextStatistics
{
    struct Element
    {
        private string Word {get; set; }
        private int Value {get; set; }
        public string GetWord(){return Word;}
        public void SetWord(string Word){this.Word = Word;}
        public int GetValue(){return Value;}
        public void SetValue(int Value){this.Value = Value;}
        public Element(string word, int value)
        {
            this.Word = word;
            this.Value = value;
        }
    }
    public void Processtng(string path)
    {
        string PathToResultFile = "Result.txt";

        //Словарь, куда в начале будут добавляться слова и их количество
        Dictionary<string, int> rating = new Dictionary<string, int>();

        //Выделяет слово
        Regex regex = new Regex(@"\w+");

        //Считвает файл после форматирования
        string text = File.ReadAllText(path);

        MatchCollection matches = regex.Matches(text);
        int ValueLines = 0;

        //Записывает в cловарь новые слова
        foreach(Match match in matches)
        {
            //Если слово уже есть в словаре, то переходит к следующему
            if(rating.ContainsKey(match.ToString()) != false)
            {
                rating[match.ToString()]++;
            }
            //Если слова нету, то добавляет новый элемент в словарь
            else
            {
                rating.TryAdd(match.ToString(), 1);
                ValueLines++;
            }
        }

        Element[] element = new Element[ValueLines];
        int h = 0;

        //Словарь переписывается в массив для сортировки
        foreach(var g in rating)
        {
            element[h].SetWord(g.Key);
            element[h].SetValue(g.Value);
            h++;
        }
        
        //Сортировка массива
        element = QuickSort(element, 0, element.Length - 1);

        //Опустошение итогового файла, на случай если он не пустой
        File.WriteAllText(PathToResultFile, "");

        //Переписывание отсортированного массива в итоговый файл
        for(int i = 0; i < ValueLines; i++)
        {
            string KeyValue = $"{element[i].GetWord(), -20} {element[i].GetValue()}\n";
            File.AppendAllText(PathToResultFile, KeyValue);
        }
        
    }

    //Нахождение опорного элемента
    static int FindPivot(Element[] array, int minIndex, int maxIndex)
    {
        int pivot = minIndex - 1;
        Element temp = new Element();
        for(int i = minIndex; i < maxIndex; i++)
        {
            if(array[i].GetValue() > array[maxIndex].GetValue())
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
    static Element[] QuickSort(Element[] array, int minIndex, int maxIndex)
    {
        if(minIndex >= maxIndex)
        {
            return array;
        }

        int pivot = FindPivot(array, minIndex, maxIndex);
        QuickSort(array, minIndex, pivot - 1);
        QuickSort(array, pivot + 1, maxIndex);
        
        return array;
    }
}