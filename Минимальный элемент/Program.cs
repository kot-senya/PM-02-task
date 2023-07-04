using System;

namespace минимальный_элемент // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        //чтение
        static int[] readFile1(string path)
        {
            int[] arr = new int[0];
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.EndOfStream != true)
                {
                    string[] line = sr.ReadLine().Split(' ');
                    arr = convert(line);
                }
            }
            return arr;
        }
        static int[,] readFile2(string path)
        {
            List<int[]> list = new List<int[]>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.EndOfStream != true)
                {
                    string[] line = sr.ReadLine().Split(' ');
                    list.Add(convert(line));
                }
            }
            int[,] arr = convert(list);
            return arr;
        }
        static int[] convert(string[] arr)
        {
            int[] newArr = new int[arr.Length];
            for (int i = 0; i < newArr.Length; i++)
            {
                newArr[i] = Convert.ToInt32(arr[i]);
            }
            return newArr;
        }
        static int[,] convert(List<int[]> list)
        {
            int[,] newArr = new int[list.Count, list[0].Length];

            for (int i = 0; i < newArr.GetLength(0); i++)
            {
                for (int j = 0; j < newArr.GetLength(0); j++)
                {
                    newArr[i, j] = list[i][j];
                }
            }
            return newArr;
        }
        //сумма элементов массива
        static int summArr(int[] arr)
        {
            int result = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                result = result + arr[i];
            }

            return result;
        }
        //копирование массива
        static int[,] replay(int[,] arr)
        {
            int[,] Array = new int[arr.GetLength(0), arr.GetLength(1)];

            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    Array[i, j] = arr[i, j];
                }
            }
            return Array;
        }
        //проверяет все ли элементы массива пустые
        static bool checkOn(int[] arr)
        {
            bool flag = false;
            foreach (int i in arr)
            {
                if (i != 0)
                {
                    flag = true;
                }
            }
            return flag;
        }
        //находит минимальный элемент массива
        static void minimum(int[,] array, out int k, out int l)
        {
            k = 0;
            l = 0;
            int min = -1;
            int flag = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (flag == 0 && array[i, j] > 0) //если еще не найден минимальный элемент
                    {
                        min = array[i, j]; // присваиваем
                        k = i;
                        l = j;
                        flag = 1;
                    }
                    if (flag == 1 && min > array[i, j] && array[i, j] > 0) // если мин найден и > 0
                    {
                        min = array[i, j]; // присваиваем
                        k = i;
                        l = j;
                    }
                }
            }
        }
        //заполнение пустых ячеек
        static void full(int k, int l, ref int[] N, ref int[] M, ref int[,] A)
        {
            if (N[l] >= M[k] && M[k] > 0) // потребность > спроса && спрос > 0
            {
                A[k, l] = M[k];
                N[l] -= M[k];
                M[k] = 0;

                if (M[k] == 0) //если спрос = 0, обнуление строк
                {
                    for (int i = 0; i < A.GetLength(1); i++)
                    {
                        if (i != l && !(A[k, i] > 0))
                        {
                            A[k, i] = 0;
                        }
                    }
                }
            }
            if (M[k] >= N[l] && N[l] > 0) // потребность < спрос && потребность > 0
            {
                A[k, l] = N[l];
                M[k] -= N[l];
                N[l] = 0;

                if (N[l] == 0) // если потребность = 0, обнуление столбцов
                {
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        if (i != k && !(A[i, l] > 0))
                        {
                            A[i, l] = 0;
                        }
                    }
                }
            }
        }
        //метод
        static int[,] metod(int[] M, int[] N, int[,] C)
        {
            int[,] A = new int[M.Length, N.Length];
            while (checkOn(N) && checkOn(M))//проверка массива на пустоту
            {
                int k;
                int l;
                //нахождение минимального элемента
                minimum(C, out k, out l);
                C[k, l] = 0; // обнуление элемента в массиве поставки
                //заполнение пустующих ячеек
                full(k, l, ref N, ref M, ref A); // заполнение элемента в массиве затрат
            }
            return A;
        }

        static void Main(string[] args)
        {
            int[] M = new int[0];
            int[] N = new int[0];            
            int[,] C = new int[0, 0];
            try
            {
                M = readFile1("Вектор мощности поставщиков.txt");
                N = readFile1("Вектор спроса покупателей.txt");
                C = readFile2("Матрица затрат на перевозки.txt");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            int[,] A = new int[C.GetLength(0), C.GetLength(1)];
                //применимаость метода
                if (summArr(N) == summArr(M))
                {
                    int[,] cCopy = replay(C);
                    A = metod(M, N, C);
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        for (int j = 0; j < A.GetLength(1); j++)
                            Console.Write($"{A[i, j],3} ");
                        Console.Write("\n");
                    }
                    Console.WriteLine("Сумма денежных затрат:");
                    int sum = 0;
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            sum += A[i, j] * cCopy[i, j];
                        }
                    }
                    Console.WriteLine(sum);
                }
                else
                {
                    Console.WriteLine("Сумма элементов векторов N и M !=");
                }
        }
    }
}