using ClassLibrary133;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba133
{
    public class MyHashTable<T> where T : IInit, ICloneable, IComparable, new()
    {
        Point<T>[] table;
        public int Capacity => table.Length;

        public MyHashTable(int len = 10)
        {
            table = new Point<T>[len];
        }
        public MyHashTable()
        {
            //table = null;
            table = new Point<T>[10];
        }
        public MyHashTable(T[] collection)
        {
            if (collection == null || collection.Length == 0)
            {
                Console.WriteLine("Хэш таблица пуста");
                table = new Point<T>[10];
            }
            else
            {
                int len = collection.Length;
                table = new Point<T>[len];

                foreach (T item in collection)
                {
                    AddPoint(item);
                }
            }
        }
        public void PrintTable()
        {
            Console.WriteLine("Ваша таблица:");
            for (int i = 0; i < table.Length; i++)
            {
                Console.Write(i + 1 + ": "); // Вывод номера индекса без переноса строки
                if (table[i] != null)
                {
                    Console.WriteLine(table[i].Data); // Вывод данных элемента table[i]

                    // Вывод элементов списка (цепочки), начиная с table[i].Next
                    Point<T> curr = table[i].Next;
                    while (curr != null)
                    {
                        Console.WriteLine(curr.Data); // Вывод данных текущего элемента списка
                        curr = curr.Next;
                    }
                }
                else
                {
                    Console.WriteLine(); // Переход на новую строку, если table[i] == null
                }
            }
        }

        public void AddPoint(T data)
        {
            int index = GetIndex(data);
            if (table[index] == null)
            {
                table[index] = new Point<T>(data);
            }
            else
            {
                Point<T> curr = table[index];
                // Проверяем наличие элемента в списке по индексу index
                while (curr != null)
                {
                    if (curr.Data.Equals(data))
                    {
                        // Если элемент уже существует в списке, прекращаем выполнение метода
                        return;
                    }
                    if (curr.Next == null)
                    {
                        // Дошли до конца списка, добавляем новый элемент
                        curr.Next = new Point<T>(data);
                        curr.Next.Pred = curr;
                        break;
                    }
                    curr = curr.Next;
                }
            }
        }
        public bool Contains(T data)
        {
            int index = GetIndex(data);

            if (table[index] == null)
            {
                Console.WriteLine("Элемент не найден"); // Если цепочка по индексу пуста
                return false;
            }

            Point<T> curr = table[index];
            int i = 0;
            while (curr != null)
            {
                if (curr.Data.Equals(data))
                {
                    Console.WriteLine($"Элемент найден в строке с номером {index + 1}, на позиции {i + 1}");
                    return true;
                }
                curr = curr.Next;
                i++;
            }

            Console.WriteLine("Элемент не найден");
            return false;
        }

        public bool RemoveData(T data)
        {
            int index = GetIndex(data);

            if (table[index] == null)
            {
                Console.WriteLine("Элемент не удалён, так как цепочка пуста");
                return false;
            }

            // Проверяем головной элемент цепочки
            if (table[index].Data.Equals(data))
            {
                if (table[index].Next == null)
                {
                    // Единственный элемент в цепочке
                    table[index] = null;
                }
                else
                {
                    // Перемещаем указатель на следующий элемент
                    table[index] = table[index].Next;
                    if (table[index] != null)
                    {
                        table[index].Pred = null;
                    }
                }
                Console.WriteLine("Элемент удалён");
                return true;
            }
            else
            {
                // Поиск элемента в цепочке
                Point<T> curr = table[index];
                while (curr != null)
                {
                    if (curr.Data.Equals(data))
                    {
                        Point<T> pred = curr.Pred;
                        Point<T> next = curr.Next;

                        // Удаление элемента из цепочки
                        if (pred != null)
                        {
                            pred.Next = next;
                        }
                        if (next != null)
                        {
                            next.Pred = pred;
                        }

                        curr.Pred = null;
                        Console.WriteLine("Элемент удалён");
                        return true;
                    }
                    curr = curr.Next;
                }
            }

            Console.WriteLine("Элемент не удалён, так как не найден в цепочке");
            return false;
        }

        public int GetIndex(T data)
        {
            return Math.Abs(data.GetHashCode()) % Capacity;
        }
    }
}
