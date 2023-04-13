using System.Text.RegularExpressions;
using System.Collections.Generic;

class TextStatistics
{
    struct Element
    {
        private string Word;
        public string GetWord(){return Word;}
        public void SetWord(string Word){this.Word = Word;}
        private int Value;
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
        string PathToResultFile = "rating.txt";

        Dictionary<string, int> rating = new Dictionary<string, int>();//Словарь, куда в начале будут добавляться слова и их количество
        Regex regex = new Regex(@"\w+");//Выделяет слово

        string text = File.ReadAllText(path);
        MatchCollection matches = regex.Matches(text);
        int ValueLines = 0;
        foreach(Match match in matches)//Записывает в Словарь новые слова
        {
            if(rating.ContainsKey(match.ToString()) != false)//Если слово уже есть в Словарь, то прибавляет 1
            {
                rating[match.ToString()] += 1;
            }
            else
            {
                rating.TryAdd(match.ToString(), 1);//Если слова нету, то добавляет новый элемент в словарь
                ValueLines++;
            }
        }
        if(!File.Exists(PathToResultFile))//Сохраняет словарь в файл. Если файла нету, то создает его
        {
            File.Create(PathToResultFile);
        }
        else
        {
            File.WriteAllText(PathToResultFile, "");
        }

        Element[] element = new Element[ValueLines];//Словарь переписывается в массив для сортировки
        //Buffer(ValueLines);
        
        int h = 0;
        foreach(var g in rating)
        {
            element[h].SetWord("\"" + g.Key + "\"");
            element[h].SetValue(g.Value);
            h++;
        }
        for(int i = 0; i < ValueLines; i++)
        {
            string KeyValue = $"{element[i].GetWord(), -20} {element[i].GetValue()}\n";
            File.AppendAllText(PathToResultFile, KeyValue);
        }
        
        element = QuickSort(element, 0, element.Length - 1);

        File.WriteAllText(PathToResultFile, "");

        for(int i = 0; i < ValueLines; i++)
        {
            string KeyValue = $"{element[i].GetWord(), -20} {element[i].GetValue()}\n";
            File.AppendAllText(PathToResultFile, KeyValue);
        }
        
    }

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