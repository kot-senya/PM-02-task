using System;
using System.IO;

namespace Джонсон // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        //запись матрицы
        static void read(string path, List<int[]> list)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.EndOfStream != true)
                {
                    string[] line = sr.ReadLine().Split(' ');
                    //новый массив
                    int[] arr = new int[2];
                    //запись
                    arr[0] = int.Parse(line[0]);
                    arr[1] = int.Parse(line[1]);
                    list.Add(arr);
                }
            }
        }
        //максимальный элемент массива
        static int findMax(List<int[]> list)
        {
            int max = 0;
            foreach (int[] arr in list)
            {
                if (arr[0] > max)
                {
                    max = arr[0];
                }
                if (arr[1] > max)
                {
                    max = arr[1];
                }
            }
            return max;
        }
        //сортировка массива
        static void sort(List<int[]> list)
        {
            int max = findMax(list); //поиск максимального значения

            int countStart = 0; //счетчик номер листа с начала
            int countEnd = list.Count; // счетчик номер листа с конца

            for (int i = 0; i < max; i++)//движемся пока не дойдем до максимального значения
            {
                if (countStart < countEnd) //номер листа < общее число листов
                {
                    for (int j = 0; j < countEnd; j++) //движемся по листу
                    {
                        if (list[j][0] == i && j >= countStart) //если левое значение = i  и номер текущего листа >= номер листа
                        {
                            int[] arr = new int[] { list[j][0], list[j][1] }; // новый массив = значение из листа
                            list.Remove(list[j]);// удаление старого массива из лиса
                            list.Insert(countStart, arr); // запись этого массива на новое место
                            countStart++; // увелечение номера листа
                        }
                        else if (list[j][1] == i && j >= countStart && j < countEnd)//если правое значение = i  и номер текущего листа >= номер листа и номер текущего листа < номер листа с конца
                        {
                            int[] arr = new int[] { list[j][0], list[j][1] }; // новый массив = значение из листа
                            list.Remove(list[j]); // удаление старого массива из лиса
                            list.Insert(countEnd - 1, arr);// запись этого массива на новое место
                            countEnd--;//уменьшение номера листа с конца
                        }
                    }
                }
                else
                {
                    break;
                }
            }

        }
        //нахождение максимального времени
        static int findPath(List<int[]> list)
        {
            int[] arr = new int[list.Count]; //новый массив, показывает время занимаемое при обработке 

            arr[0] = list[0][0];

            for (int i = 1; i < arr.Length; i++)
            {
                arr[i] = arr[i - 1] + list[i][0] - list[i - 1][1]; //предыдущее время + время 1 - время 2
            }
            return arr.Max(); //поиск максимального
        }
        static void Main(string[] args)
        {
            List<int[]> list = new List<int[]>();
            read("матрица.txt", list);
            sort(list);
            Console.WriteLine(findPath(list));
        }
    }
}