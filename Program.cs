class MyClass
{
    static void Main(string[] args)
    {
        string path = @"War_and_peace.txt";
        TextProcessingFB2 TP = new TextProcessingFB2();//Clearing text from markup characters
        TP.Processing(path);

        TextStatistics TS = new TextStatistics();
        path = @"War_and_peace_buffer.txt";
        TS.Processtng(path);
    }
}