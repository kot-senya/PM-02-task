using System;

namespace Северо_западный_угол
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
        //матрица затрат на перевозку
        static int[,] summArr(int[,] A1, int[,] A2)
        {
            int[,] A = new int[A1.GetLength(0), A1.GetLength(1)];

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(0); j++)
                {
                    A[i, j] = A1[i, j] + A2[i, j];
                }
            }
            return A;
        }
        //применение метода
        static int[,] metod(ref int[] M, ref int[] N, ref int[,] C)
        {
            int[,] A = new int[C.GetLength(0), C.GetLength(1)];
            int j = 0;
            //цикл работы
            for (int i = 0; i < C.GetLength(0); i++)
            {
                while (M[i] != 0)//если элемент вектора поставки не 0
                {
                    if (j != C.GetLength(1))//пока есть куда шагать вправо, шагаем
                    {
                        if (N[j] >= M[i]) // если потребность > предложения = списание + вниз
                        {
                            A[i, j] = M[i];
                            N[j] -= M[i];
                            M[i] = 0;
                            if (M[i] == 0) //зануление оставшихся элементов столбца
                            {
                                for (int k = j + 1; k < C.GetLength(1); k++)
                                {
                                    A[i, k] = 0;
                                }
                            }
                            break; // выход, чтобы списать у другого постовщика
                        }

                        if (N[j] <= M[i]) // если потребность < предложения = списание + вправо
                        {
                            A[i, j] = N[j];
                            M[i] -= N[j];
                            N[j] = 0;
                            for (int k = i + 1; k < 3; k++) //зануление оставшихся элементов строки
                            {
                                A[k, j] = 0;
                            }
                            j += 1; // новый столбец
                        }
                    }
                    else break;
                }
            }
            return A;
        }
        static void Main(string[] args)
        {
            //чтение из файла
            int[] M = new int[0];
            int[] N = new int[0];
            int[,] A = new int[0, 0];
            int[,] B = new int[0, 0];
            try
            {
                M = readFile1("Вектор мощности поставщиков.txt");
                N = readFile1("Вектор спроса покупателей.txt");
                A = readFile2("Матрица затрат на перевозки.txt");
                B = readFile2("Матрица затрат на хранение еденицы.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (summArr(N) == summArr(M) && A.GetLength(0) == B.GetLength(0) && A.GetLength(1) == B.GetLength(1))
            {
                int[,] C = summArr(A, B); //матрица поставок
                int[,] res = metod(ref M, ref N, ref C);
                Console.WriteLine("Матрица поставок");
                for (int i = 0; i < res.GetLength(0); i++)
                {
                    for (int j = 0; j < res.GetLength(1); j++)
                        Console.Write($"{res[i, j]} ");
                    Console.Write("\n");
                }

                double result = 0, num = 0;
                for (int i = 0; i < res.GetLength(0); i++)
                {
                    for (int j = 0; j < res.GetLength(1); j++)
                        result += C[i, j] * res[i, j];
                    Console.WriteLine($"Сумма поставок {i + 1} постовщика = {result - num} у.д.е.");
                    num = result;
                }
                Console.WriteLine($"Общая сумма поставок = {result} у.д.е.");
            }
            else
            {
                Console.WriteLine("Сумма элементов векторов N и M !=");
            }
        }
    }
}