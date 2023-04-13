class MyStruct
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

    public void asd()
    {
        Element[] array = new Element[5];
        array[0] = new Element("tree", 2);
        array[1] = new Element("milk", 3); 
        array[2] = new Element("car", 4);
        array[3] = new Element("bank", 10);
        array[4] = new Element("cat", 32);

        Console.WriteLine(array[0].GetWord());
    }
}