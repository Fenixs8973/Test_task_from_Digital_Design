using System.Diagnostics;
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

        MethodInfo mi = type.GetMethod("ProcessingPrivate", BindingFlags.NonPublic | BindingFlags.Instance);

        object text = new object();
        text = WarAndPeace;
        
        //создаем объект
        Stopwatch stopwatch = new Stopwatch();
        //засекаем время начала операции
        stopwatch.Start();
        //Вызываем приватный метод
        var result = (Dictionary<string, int>)mi.Invoke(TS, new object[] { text }); 
        stopwatch.Stop();
        Console.WriteLine("Время работы приватного метода: " + stopwatch.ElapsedMilliseconds);     

        stopwatch = new Stopwatch();
        stopwatch.Start();
        TS.ProcessingPublic(WarAndPeace);
        stopwatch.Stop();
        Console.WriteLine("Время работы публичного метода: " + stopwatch.ElapsedMilliseconds); 
        
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