using System;

namespace прюфер // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        struct Tree
        {
            public int begin;
            public int end;
        }
        //считывание ребер
        static void read(List<Tree> l)
        {
            using (StreamReader sr = new StreamReader("Ребра.txt"))
            {
                while (sr.EndOfStream != true)
                {
                    string[] line = sr.ReadLine().Split('-');
                    //новое дерево
                    Tree t = new Tree()
                    {
                        begin = int.Parse(line[0]),
                        end = int.Parse(line[1])
                    };
                    //новое дерево добавляем в лист
                    l.Add(t);
                }
            }
        }
        //чтение кода
        static int[] read()
        {
            using (StreamReader sr = new StreamReader("Код прюфера.txt"))
            {
                string[] line = sr.ReadLine().Trim().Split(' ');
                int[] arr = new int[line.Length];
                for (int i = 0; i < line.Length; i++)
                {
                    arr[i] = int.Parse(line[i]);
                }
                return arr;
            }
        }
        //запись кода
        static string writePrufer(List<Tree> list)
        {
            string code = ""; //изначально пустой
            int n = list.Count - 1; //количество точек, которые запишутся

            for (int i = 0; i < n; i++)
            {
                int a = min(list);//нахождение ребра с минимальным листом
                code += list[a].begin + " ";//запись
                list.RemoveAt(a); //удаление ребра
            }

            return code;
        }
        //возвращает номер ребра с минимальным листом
        static int min(List<Tree> list)
        {
            bool flag = false; //1ое значение
            bool mark = true;
            int min = 0;

            foreach (Tree t in list) //поиск минимального листа
            {
                if (!flag)//первое значение
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].begin == t.end) //если точка начала = точка конца
                        {
                            mark = false;
                        }
                    }
                    if (mark)
                    {
                        min = t.end;
                        flag = true;
                    }
                }
                else //2 и последующее
                {
                    if (t.end < min) //если конец меньше минимаотного = присвоить
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].begin == t.end)
                            {
                                mark = false;
                            }
                        }
                        if (mark)
                        {
                            min = t.end;

                        }
                    }
                }
                mark = true;
            }

            //номер ребра в дереве
            int num = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (min == list[i].end)
                {
                    num = i;
                }
            }
            return num;
        }
        //вывод на экран
        static void display(List<Tree> l)
        {
            foreach (Tree t in l)
            {
                Console.WriteLine("{0}-{1}", t.begin, t.end);
            }
        }
        //декодирование код -> ребра
        static void writeVerhPruf(List<Tree> t, int[] a)
        {
            //запись всех вершин
            for (int i = 0; i < a.Length; i++)
            {
                Tree x = new Tree();
                x.begin = a[i];
                x.end = 0;
                t.Add(x);
            }

            //запись всех значений графа
            List<int> num = new List<int>();
            for (int i = 0; i < a.Length + 2; i++)
            {
                num.Add(i + 1);
            }

            //восстоновление ребер
            for (int i = 0; i < t.Count; i++)//пока не конец Дерева
            {
                bool flag = false;
                int k = 0;

                for (int j = 0; j < num.Count; j++) //пока не конец списка значениц
                {
                    bool mark = true;

                    for (int z = i; z < a.Length; z++)//ходим по дереву
                    {
                        if (t[z].begin == num[j])//если значение начала = значение Списка ТО ничего не происходит
                        {
                            mark = false;
                        }
                    }

                    if (t[i].begin != num[j] && mark)//если номер НЕ начало и НЕ является началом ТО запоминаем номер и выходим
                    {
                        flag = true;
                        k = j;
                        break;
                    }
                }

                if (flag)// если значение нашлось
                {
                    Tree m = new Tree() { begin = t[i].begin, end = num[k] };
                    t.RemoveAt(i);
                    t.Insert(i, m); //замена на ребро с концом
                    num.RemoveAt(k);//удаление значения
                }
            }
        }
        static void Main(string[] args)
        {
            //КОДИРОВАНИЕ
            List<Tree> list = new List<Tree>();
            read(list);
            //запись в файл
            using (StreamWriter sw = new StreamWriter("Код прюфера.txt", false))
            {
                sw.WriteLine(writePrufer(list));
            }

            //ДЕКОДИРОВАНИЕ

            List<Tree> t = new List<Tree>();
            int[] code = read(); //запись кода
            writeVerhPruf(t, code);//запись ребер
            using (StreamWriter sw = new StreamWriter("Ребра new.txt", false))
            {
                foreach (Tree a in t)
                {
                    sw.WriteLine(a.begin + "-" + a.end);
                }
            }
        }
    }
}