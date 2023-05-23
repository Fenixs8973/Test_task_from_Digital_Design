class MyClass
{
    static void Main(string[] args)
    {
        string path = @"War_and_peace.txt";

        //Отчистка файла от разметки
        TextProcessingFB2 TP = new TextProcessingFB2();
        TP.Processing(path);

        //Подсчет, сортировка и сохранение слов и их колличества
        TextStatistics TS = new TextStatistics();
        path = @"War_and_peace_buffer.txt";
        TS.Processtng(path);
        File.Delete(path);
        Console.WriteLine("Программа закончила выполнение");
    }
}