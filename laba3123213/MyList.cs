using ClassLibrary133;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba133
{
    public class MyList<T> where T : IInit, ICloneable, IComparable, new()
    {
        public Point<T> beg = null;
        public Point<T> cur; // Текущий элемент
        public Point<T> end = null;

        public int count;
        public int Count => count;
        public static Point<T> MakeRandomData()
        {
            T data = new T();
            data.RandomInit();
            return new Point<T>(data);
        }
        public static T MakeRandomItem()
        {
            T data = new T();
            data.RandomInit();
            return data;
        }
        public void AddToBegin(T item)
        {
            T newData = (T)item.Clone();
            Point<T> newItem = new Point<T>(newData);
            count++;
            if (beg != null)
            {
                beg.Pred = newItem;
                newItem.Next = beg;
                beg = newItem;
            }
            else
            {
                beg = newItem;
                end = beg;
            }
        }
        public void AddToEnd(T item)
        {
            T newData = (T)item.Clone();
            Point<T> newItem = new Point<T>(newData);
            count++;
            if (end != null)
            {
                end.Next = newItem;
                newItem.Pred = end;
                end = newItem;
            }
            else
            {
                beg = newItem;
                end = beg;
            }
        }

        public void AddToOdd()
        {
            if (beg == null) throw new Exception("Список пуст.");

            cur = beg;
            int index = 1;
            //Добавляем первый элемент
            T first = new T();
            first.RandomInit();
            AddToBegin(first);
            while (cur != null)
            {
                if (index % 2 != 0)
                {
                    // Создаем новый элемент с данными текущего элемента

                    T newData = new T();
                    newData.RandomInit();
                    Point<T> newPoint = new Point<T>(newData);

                    // Добавляем новый элемент после текущего
                    newPoint.Next = cur.Next;
                    if (cur.Next != null)
                        cur.Next.Pred = newPoint;
                    cur.Next = newPoint;
                    newPoint.Pred = cur;
                    count++;
                }
                // Переходим к следующему элементу
                cur = cur.Next;
                index++;
            }
        }

        public void RemoveFrom(T item)
        {
            if (beg == null)
            {
                Console.WriteLine("Список пуст, ничего не нужно удалять");
                return;
            }

            Point<T> current = beg;
            Point<T> foundNode = null;

            // Ищем первый элемент с заданным информационным полем
            while (current != null)
            {
                if (current.Data != null && current.Data.Equals(item))
                {
                    foundNode = current;
                    break; // Нашли элемент, прекращаем поиск
                }

                current = current.Next;
            }

            if (foundNode == null)
            {
                Console.WriteLine("Элемент не найден, ничего не нужно удалять");
                return;
            }

            // Начинаем удаление, начиная с найденного элемента и до конца списка
            if (foundNode == beg)
            {
                // Найденный элемент - первый в списке
                beg = null;
                end = null;
            }
            else
            {
                // Найденный элемент не первый в списке, удаляем его и последующие элементы
                foundNode.Pred.Next = null;
                end = foundNode.Pred;
            }
            // Обновляем счетчик количества элементов
            count = 0;
        }
        public MyList() { }
        public MyList(int size)
        {
            if (size < 0) throw new Exception("Размер не может быть меньше или равен 0");
            beg = MakeRandomData();
            end = beg;
            for (int i = 1; i < size; i++)
            {
                T newItem = MakeRandomItem();
                AddToEnd(newItem);
            }
        }

        public MyList(T[] collection)
        {
            if (collection == null) throw new Exception("Список пуст");
            if (collection.Length == 0) throw new Exception("Длина списка = 0");

            foreach (T item in collection)
            {
                AddToEnd(item);
            }
        }

        public void PrintList()
        {

            if (count >= 0)
            {
                Console.WriteLine("Ваш список:");
                Point<T> curr = beg;
                for (int i = 0; curr != null; i++)
                {
                    Console.WriteLine(curr);
                    curr = curr.Next;
                }
            }
            else Console.WriteLine("Список пуст");

        }
        public Point<T> FindItem(T item)
        {
            Point<T> curr = beg;
            while (curr != null)
            {
                if (curr.Data == null) throw new Exception("Data is null");
                if (curr.Data.Equals(item)) return curr;
                curr = curr.Next;
            }
            return null;
        }
        public bool RemoveItem(T item)
        {
            if (beg == null) throw new Exception("the empty list");
            Point<T> pos = FindItem(item);
            if (pos == null) return false;
            count--;
            //one element
            if (beg == end)
            {
                beg = end = null;
                return true;
            }
            //the first
            if (pos.Pred == null)
            {
                beg = beg?.Next;
                beg.Pred = null;
                return true;
            }
            //the last
            if (pos.Next == null)
            {
                end = end.Pred;
                end.Next = null;
                return true;
            }
            Point<T> next = pos.Next;
            Point<T> pred = pos.Next;
            pos.Next.Pred = pred;
            pos.Pred.Next = next;
            return true;
        }
        public MyList<T> Clone()
        {
            MyList<T> clonedList = new MyList<T>();

            if (beg == null)
            {
                // Исходный список пуст, возвращаем пустой клонированный список
                return clonedList;
            }

            // Клонируем каждый элемент списка и добавляем его в клонированный список
            Point<T> current = beg;
            while (current != null)
            {
                if (current.Data != null && current.Data is ICloneable)
                {
                    // Клонируем элемент с помощью метода Clone()
                    T clonedData = (T)((ICloneable)current.Data).Clone();
                    clonedList.AddToEnd(clonedData); // Добавляем клонированный элемент в новый список
                }
                else
                {
                    // Обработка случаев, когда элемент не реализует ICloneable или Data равен null
                    // Можно выполнить дополнительную логику по необходимости
                    Console.WriteLine("Невозможно склонировать элемент списка.");
                }

                current = current.Next;
            }

            return clonedList;
        }
        // Метод для удаления всего списка (очистки списка)
        public void Clear()
        {
            // Просто переустанавливаем начальный и конечный указатели в null
            beg = null;
            end = null;
            Console.WriteLine("Список удалён");
        }
        public static int InputIntNumber() // проверка на целое число
        {
            bool isCorrert;
            int number;
            do
            {
                isCorrert = int.TryParse(Console.ReadLine(), out number);
                if (!isCorrert) Console.WriteLine("Пожалуйста, введите число: ");
            } while (!isCorrert);
            return number;
        }
    }
}
