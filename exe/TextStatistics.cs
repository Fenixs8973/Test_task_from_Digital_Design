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

        MethodInfo mi = type.GetMethod("Processtng", BindingFlags.NonPublic | BindingFlags.Instance);//.GetMethod("Processtng");

        object[] text = new object[] { WarAndPeace.ToCharArray() };

        //Invoke - вызов метода
        Dictionary<string, int> result = null;

        try
        {
            result = mi.Invoke(TS, new object[] { WarAndPeace.ToCharArray() }) as Dictionary<string, int>;
        }
        catch
        {

        }

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