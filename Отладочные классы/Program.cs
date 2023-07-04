using System;
using System.Diagnostics;

namespace отладочные_классы // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Вывод в окно ВЫВОД
            Debug.WriteLine("Запись в окно Вывод только при отладке");
            Trace.WriteLine("Запись в окно Вывод всегда");



            //файл для слушителя
            string path = "слушатель.txt";

            //новый слушитель - выводит сообщения в консоль
            TextWriterTraceListener userConsole = new TextWriterTraceListener(Console.Out);

            //новый слушитель - выводит сообщения в новый, созданный файл
            TextWriterTraceListener userFile = new TextWriterTraceListener(File.Open(path, FileMode.CreateNew));

            //объевление слушителей - после этого информация выводится для слушителя
            Trace.Listeners.Add(userConsole);
            Trace.Listeners.Add(userFile);
        }
    }
}