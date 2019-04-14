using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6
{
    class Program
    {

//        Домашнее задание
//1. Реализовать простейшую хэш-функцию.На вход функции подается строка, на выходе
//получается сумма кодов символов.
//2. Переписать программу, реализующее двоичное дерево поиска:
//a.Добавить в него обход дерева различными способами.
//b.Реализовать поиск в нём.
//c. * Добавить в программу обработку командной строки с помощью которой можно
//указывать, из какого файла считывать данные, каким образом обходить дерево.
//3. Разработать базу данных студентов, состоящую из полей «Имя», «Возраст», «Табельный
//номер», в которой использовать все знания, полученные на уроках.Данные следует

        static void Main(string[] args)
        {
            
            
            while(true)
            {
                Console.Clear();
                menu();
                Console.WriteLine("Ввелите номер задачи");
                var taskNumber = Console.ReadLine();
                switch (Convert.ToInt16(taskNumber))
                {
                    case 1:
                        Task1();
                        break;
                    case 2:
                        Task2();
                        break;
                    case 3:
                        Task3();
                        break;
                    default:
                        Console.WriteLine("Неверный ввод");
                        break;
                }
            }
        }
        static void menu()
        {
            Console.WriteLine("Домашнее задание" +
            "1.Реализовать простейшую хэш-функцию.На вход функции подается строка, на выходе\n" +
            "получается сумма кодов символов. \n" +
            "2.Переписать программу, реализующее двоичное дерево поиска: \n" +
            "a.Добавить в него обход дерева различными способами. \n" +
            "b.Реализовать поиск в нём. \n" +
            "c. * Добавить в программу обработку командной строки с помощью которой можно\n" +
            "указывать, из какого файла считывать данные, каким образом обходить дерево. \n" +
            "3.Разработать базу данных студентов, состоящую из полей «Имя», «Возраст», «Табельный\n" +
            "номер», в которой использовать все знания, полученные на уроках.Данные следует\n");
        }
        static void Task1()
        {
            /*
            "1.Реализовать простейшую хэш-функцию.На вход функции подается строка, на выходе\n" +
            "получается сумма кодов символов. \n" +*/
            Console.WriteLine("Введите строку:");
            string str = "Тестовая строка";
            str = Console.ReadLine();
            
            ulong control=0;            
            int j = 0;
            ulong number = 0;
            //0101 0101 0101 0101 0101 0101 0101 0101  - 32 бита, найдем это число в десятичном виде
            while (j<32)
            {
                if (number == 1)
                {
                    control += number * (ulong)Math.Pow(2, j);
                    number = 0;
                }
                else
                    number = 1;
                j++;
            }            

            ulong result = 0;
            int symbol;
            foreach (var c in str)
            {               
                result += (Convert.ToUInt64(c) ^ control)*((ulong)Math.Pow(10,Math.Log10(c)));                
            }
            Console.WriteLine(result);
            Console.ReadLine();
        }
        static void Task2()
        {
            /*
            "2.Переписать программу, реализующее двоичное дерево поиска: \n" +
            "a.Добавить в него обход дерева различными способами. \n" +
            "b.Реализовать поиск в нём. \n" +
            "c. * Добавить в программу обработку командной строки с помощью которой можно\n"
            "указывать, из какого файла считывать данные, каким образом обходить дерево. \n"*/
            int[] myArr = new int[30];
            
            for(int i=0; i< myArr.Length; i++)
            {
                myArr[i] = i + 1;
            }
            Node Tree = new Node();
            Tree.FillTree(myArr);            
            Tree.PrintTree(Console.WindowWidth / 2, Console.CursorTop);
            Tree.FindValue(Console.WindowWidth / 2, Console.CursorTop+10, 7);
            Console.ReadLine();
        }
        public class Node
        {
            public int Value { get; set; }
            public Node Left;
            public Node Right;
            
            public int Size { get; set; }
            public void FillTree(int[] arr)
            {
                int leftSize=0;
                int rightSize=0;
                int currentIndex=0;
                Size = arr.Length;
                if (arr.Length>0)
                {
                    currentIndex = arr.Length / 2;
                    leftSize = currentIndex - 1;
                    rightSize = arr.Length - currentIndex-1;
                }
                if (rightSize > 0)
                {
                    Right = new Node();
                    Right.FillTree(GetPathOfArr(arr, currentIndex + 1, arr.Length - 1));
                }
                if (leftSize >= 0) 
                {
                    Left = new Node();
                    Left.FillTree(GetPathOfArr(arr, 0, leftSize));
                }
                
                Value = arr[currentIndex];
            }

            private int[] GetPathOfArr(int[] arr, int begin, int end)
            {
                int[] newArr = new int[end - begin + 1];
                int index = 0;
                for (int i = begin; i <= end; i++)
                {
                    newArr[index] = arr[i];
                    index++;
                }
                return newArr;
            }

            public void PrintTree(int x, int y)
            {      
                if(Left !=null)
                {
                    
                    Console.SetCursorPosition(x - (Left.Size / 2) * 2-1, y);
                    Console.Write("+");
                    while (Console.CursorLeft < x)
                        Console.Write("-");
                    
                    Left.PrintTree(x - (Left.Size / 2)*2-1, y + 2);
                }
                Console.SetCursorPosition(x, y);               
                if (Right != null)
                {
                    Console.SetCursorPosition(x+1,y);
                    while (Console.CursorLeft < x + (Right.Size / 2) * 2+1)
                        Console.Write("-");
                    Console.Write("+");
                    Right.PrintTree(x + (Right.Size / 2)*2+1, y + 2);
                }
                Console.SetCursorPosition(x, y);
                Console.WriteLine(Value);
            }

            public void FindValue(int x, int y, int value)
            {
                if (Left != null)
                {

                    Console.SetCursorPosition(x - (Left.Size / 2) * 2 - 1, y);
                    Console.Write("+");
                    while (Console.CursorLeft < x)
                        Console.Write("-");

                    Left.FindValue(x - (Left.Size / 2) * 2 - 1, y + 2, value);
                }
                Console.SetCursorPosition(x, y);
                if (Right != null)
                {
                    Console.SetCursorPosition(x + 1, y);
                    while (Console.CursorLeft < x + (Right.Size / 2) * 2 + 1)
                        Console.Write("-");
                    Console.Write("+");
                    Right.FindValue(x + (Right.Size / 2) * 2 + 1, y + 2, value);
                }
                if(Value == value)
                {
                    ConsoleColor oldColour = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine(Value);
                    Console.ForegroundColor = oldColour;
                }   
                else
                {
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine(Value);
                }
            }
        }        

        static void Task3()
        {
            /*
            "3.Разработать базу данных студентов, состоящую из полей «Имя», «Возраст», «Табельный\n"
            "номер», в которой использовать все знания, полученные на уроках.Данные следует\n");*/
            Student MyDataBase = new Student();

            List<string> Names = new List<string>(){
                "Иван",
                "Василий",
                "Кирилл",
                "Роман",
                "Дмитрий",
                "Алексей",
                "Андрей",
                "Сергей",
                "Александр",
                "Михаил"
            };

            List<string> Surnames = new List<string>(){
                "Иванов",
                "Петров",
                "Сидоров",
                "Кондратьев",
                "Баранов",
                "Буронов",
                "Степанов",
                "Калягин",
                "Алексеев",
                "Смирнов"
            };

            Random ageRnd = new Random();
            Random numRnd = new Random();
            for(int i=0; i< 10; i++)
            {
                string name = Names[numRnd.Next()%10] + " " + Surnames[ageRnd.Next() % 10];
                int age = ageRnd.Next() % 11;
                int studNumber = i+1;
                MyDataBase.FillDataBase(
                    name,
                    age,
                    studNumber);
            }
            Console.WriteLine("Введите вомер студента для поиска от 1 до 10:");
            int answer;
            answer = Convert.ToInt16(Console.ReadLine());
            if(0<answer && answer < 10)
                MyDataBase.FindStudent(answer);
            Console.ReadLine();
        }

        public class Student
        {
            //Данные
            public string Name { get; set; }                                    //Имя студента
            public int Age { get; set; }                                        //Возраст студента
            public int StudentNumber { get; set; }                              //Номер студента
            
            public Student LeftBranch;                                          //Левая подветка
            public Student RightBranch;                                         //Правая подветка
            private int size = 0;                                               //Размер текущей ветки
            public int Size { get { return size; } }

            public void FillDataBase(string name, int age, int studentNumber)
            {
                if (Name == null && Age == 0 && StudentNumber == 0)             //Если текущий объект не заполнен, то нужно его заполнить
                {
                    Name = name;
                    Age = age;
                    StudentNumber = studentNumber;
                    this.size += 1;
                }
                //Если левый объект пустой а правый не пустой или оба объекта не пустые и размер левой ветки меньше правой, заполняем левую ветку
                else if((LeftBranch==null && RightBranch!=null)||(LeftBranch!=null&&RightBranch!=null&&LeftBranch.Size<RightBranch.Size))
                {
                    if(LeftBranch == null)
                        LeftBranch = new Student();
                    this.size += 1;
                    LeftBranch.FillDataBase(name, age, studentNumber);
                }
                //Если левое дерево не пустое а правое пустое или оба объекта не пустые и размер левой ветки больше правой ветки, то заполняем правую ветку
                else if((LeftBranch!=null&&RightBranch==null)||(LeftBranch!=null&&RightBranch!=null&& LeftBranch.Size > RightBranch.Size))
                {
                    if(RightBranch==null)
                        RightBranch = new Student();
                    this.size += 1;
                    RightBranch.FillDataBase(name, age, studentNumber);
                }
                else                                                            //Когда обе ветки пустые в приоритете левая ветка
                {
                    if (LeftBranch == null)
                        LeftBranch = new Student();
                    this.size += 1;
                    LeftBranch.FillDataBase(name, age, studentNumber);
                }
            }

            public void FindStudent(int StudentNumber)                          //Метод для поиска студента в дереве
            {
                if(this.StudentNumber == StudentNumber)                         
                {
                    Console.WriteLine("Name:\t\t" + Name);
                    Console.WriteLine("Age:\t\t" + Age);
                    Console.WriteLine("StudNumber:\t" + StudentNumber);
                }
                else                                                            //Если текущий узел не является требуемым, проболжаем искать в его ветвях.
                {
                    if(LeftBranch!=null)
                        LeftBranch.FindStudent(StudentNumber);
                    if(RightBranch!=null)
                        RightBranch.FindStudent(StudentNumber);
                }
            }
        }
    }
}
