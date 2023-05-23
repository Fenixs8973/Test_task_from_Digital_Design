using System.IO;
using System.Reflection;
class MyClass
{
    static void Main(string[] args)
    {
        string path = @"War_and_peace.txt";

        string WarAndPeace = File.ReadAllText(path);
 
        TextProcessingDll.TextStatistics TS = new TextProcessingDll.TextStatistics();
        
        var type = typeof(TextProcessingDll.TextStatistics);

        MethodInfo mi = type.GetMethod("Processing", BindingFlags.NonPublic | BindingFlags.Instance);//.GetMethod("Processtng");

        object text = new object();
        text = WarAndPeace;
        
        var result = (Dictionary<string, int>)mi.Invoke(TS, new object[] { text });      

        path = @"Result.txt";
        //Переписывание отсортированного массива в итоговый файл
        foreach (var i in result)
        {
            string KeyValue = $"{i.Key,-20} {i.Value}\n";
            File.AppendAllText(path, KeyValue);
        }
        Console.WriteLine("Программа закончила выполнение");
    }
}