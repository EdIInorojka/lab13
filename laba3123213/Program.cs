using ClassLibrary133;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace laba133
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyObservableCollection<Auto> collection1 = new MyObservableCollection<Auto>();
            MyObservableCollection<Auto> collection2 = new MyObservableCollection<Auto>();
            // Создание двух объектов типа Journal
            Journal journal1 = new Journal();
            Journal journal2 = new Journal();
            // Подписка первого объекта Journal на события CollectionCountChanged и CollectionReferenceChanged из первой коллекции
            collection1.CollectionCountChanged += journal1.HandleCollectionCountChanged;
            collection1.CollectionReferenceChanged += journal1.HandleCollectionReferenceChanged;

            // Подписка второго объекта Journal на события CollectionReferenceChanged из обеих коллекций
            collection1.CollectionReferenceChanged += journal2.HandleCollectionReferenceChanged;
            collection2.CollectionReferenceChanged += journal2.HandleCollectionReferenceChanged;

            int ans = 0;
            do
            {
                Commands();
                Console.Write("Введите номер: ");
                ans = InputIntNumber();
                // Создание двух коллекций MyObservableCollection
               switch(ans)
                {
                    
                    case 1:
                        {                            
                            Console.WriteLine("Коллекции и журналы созданы, подписки добавлены");
                        break;
                        }
                    case 2:
                        {
                            // Добавление элементов в коллекции
                            collection1.Add(new Auto("BMW", "black", 2020, 50000, 5));
                            collection1.Add(new Auto("Honda", "white", 2019, 40000, 6));
                            collection2.Add(new Auto("Volkswagen", "green", 2018, 30000, 7));
                            collection2.Add(new Auto("Ford", "purple", 2017, 20000, 8));
                            Console.WriteLine("Добавление произведено");
                            break;
                        }
                    case 3:
                        {
                            // Удаление некоторых элементов из коллекций
                            collection1.Remove(collection1[0]);
                            collection2.Remove(collection2[1]);
                            Console.WriteLine("Элементы удалены");

                            break;
                        }
                    case 4:
                        {
                            // Присвоение некоторым элементам коллекций новые значения
                            collection1[0] = new Auto("Audi", "pink", 2016, 10000, 9);
                            collection2[0] = new Auto("BMW", "black", 2020, 50000, 5);
                            Console.WriteLine("Присвоение произведено");
                            break;
                        }
                        case 5:
                        {
                            // Вывод данных об объектах Journal
                            Console.WriteLine("Данные журнала 1:");
                            Console.WriteLine(journal1);
                            Console.WriteLine();

                            Console.WriteLine("Данные журнала 2:");
                            Console.WriteLine(journal2);
                            Console.WriteLine("Элементы добавлены");
                            break;
                        }
                }
            } while (ans != 6);
        }
        public static void Commands()
        {
            Console.WriteLine("1) Создать коллекции, журналы и подписки к ним");
            Console.WriteLine("2) Добавить элементы в коллекцию");
            Console.WriteLine("3) Удаление элементов из коллекции");
            Console.WriteLine("4) Присвоение первым элементам коллекции новые значения");
            Console.WriteLine("5) Вывод");
            Console.WriteLine("6) Выход");
        }
        public static int InputIntNumber() // проверка на целое число
        {
            bool isCorrert;
            int number;
            do
            {
                isCorrert = int.TryParse(Console.ReadLine(), out number);
                if (!isCorrert) Console.Write("Пожалуйста, введите число: ");
            } while (!isCorrert);
            return number;
        }
    }
}
