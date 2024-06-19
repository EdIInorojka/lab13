using ClassLibrary133;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba133
{
    public class Tree<T> where T : IInit, IComparable, new()
    {
        public Point<T> root = null;
        int count = 0;
        public int Count => count;
        public Tree()
        {
            count = 0;
            root = MakeTree(count, root);
        }

        public Tree(int len)
        {
            count = len;
            root = MakeTree(len, root);
        }
        [ExcludeFromCodeCoverage]
        public void ShowTree()
        {
            if (root == null) { Console.WriteLine("Дерево пустое"); }
            else
            {
                Console.WriteLine("Ваше дерево:");
                Show(root);
            }
        }
        Point<T> MakeTree(int length, Point<T> point) // идеально сбалансированное дерево
        {
            T data = new T();
            data.RandomInit();
            Point<T> newItem = new Point<T>(data);
            if (length == 0) return null;
            int nl = length / 2;
            int nr = length - nl - 1;
            newItem.Pred = MakeTree(nl, newItem.Pred);
            newItem.Next = MakeTree(nr, newItem.Next);
            return newItem;
        }
        [ExcludeFromCodeCoverage]
        void Show(Point<T> point, int spaces = 5)
        {
            if (point != null)
            {
                Show(point.Pred, spaces + 5); // Рекурсивный вызов для левого поддерева
                for (int i = 0; i < spaces; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(point.Data); // Вывод данных текущего узла
                Show(point.Next, spaces + 5); // Рекурсивный вызов для правого поддерева
            }
        }

        public void AddPoint(T data) // дерево поиска
        {
            if (root == null)
            {
                root = new Point<T>(data); // Инициализация корня, если он равен null
                count++;
                return;
            }
            Point<T> point = root;
            Point<T> current = null;
            bool isExist = false;
            while (point != null && !isExist)
            {
                current = point;
                if (point.Data.CompareTo(data) == 0) // элемент уже есть
                {
                    isExist = true;
                }
                else // ищем место
                {
                    if (point.Data.CompareTo(data) < 0)
                    {
                        point = point.Pred;
                    }
                    else
                    {
                        point = point.Next;
                    }
                }
            }
            // нашли место
            if (isExist)
            {
                return; // ничего не добавили
            }
            Point<T> newPoint = new Point<T>(data);
            if (current.Data.CompareTo(data) < 0) // если элемент меньше
            {
                current.Pred = newPoint;
            }
            else
            {
                current.Next = newPoint;
            }
            count++;

        }

        void TransFromToArray(Point<T> point, T[] array, ref int current)
        {
            if (point != null)
            {
                TransFromToArray(point.Pred, array, ref current);
                array[current] = point.Data;
                current++;
                TransFromToArray(point.Next, array, ref current);
            }
        }

        public void TransformToFindTree()
        {
            T[] array = new T[count];
            int current = 0;

            TransFromToArray(root, array, ref current);
            root = new Point<T>(array[0]);
            count = 0;
            for (int i = 0; i < array.Length; i++)
            {
                AddPoint(array[i]);
            }
        }
        public void FindAndDisplayMaxCostItem()
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пусто.");
                return;
            }

            // Ищем узел с максимальной стоимостью
            Point<T> maxCostNode = FindNodeWithMaxCost(root);

            if (maxCostNode != null)
            {
                Console.WriteLine("Элемент с максимальной стоимостью:");
                Console.WriteLine(maxCostNode.Data);
            }
            else
            {
                Console.WriteLine("Не удалось найти элемент с максимальной стоимостью.");
            }
        }

        // Вспомогательный метод для поиска узла с максимальной стоимостью
        private Point<T> FindNodeWithMaxCost(Point<T> point)
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пусто");
                return null;
            }
            if (point == null)
            {
                return null;
            }

            Point<T> leftMax = FindNodeWithMaxCost(point.Pred);
            Point<T> rightMax = FindNodeWithMaxCost(point.Next);

            // Получаем стоимость текущего узла
            double currentCost = GetItemCost(point.Data);

            // Сравниваем стоимость текущего узла с максимальной стоимостью из поддеревьев
            Point<T> maxNode = point;
            double maxCost = currentCost;

            if (leftMax != null)
            {
                double leftMaxCost = GetItemCost(leftMax.Data);
                if (leftMaxCost > maxCost)
                {
                    maxNode = leftMax;
                    maxCost = leftMaxCost;
                }
            }

            if (rightMax != null)
            {
                double rightMaxCost = GetItemCost(rightMax.Data);
                if (rightMaxCost > maxCost)
                {
                    maxNode = rightMax;
                    maxCost = rightMaxCost;
                }
            }

            return maxNode;
        }

        // Метод для получения стоимости элемента типа T
        private double GetItemCost(T item)
        {
            // Предположим, что T представляет класс с полем Cost (например, Auto)
            // Необходимо получить стоимость объекта типа T
            // Реализуйте этот метод в соответствии с вашей структурой данных
            // Например, если T представляет класс Auto, то:
            if (item is Auto)
            {
                var auto = (Auto)(object)item;
                return auto.Cost;
            }

            // Добавьте обработку других типов, если необходимо

            throw new ArgumentException("Невозможно получить стоимость элемента типа T.");
        }
        public void CleanTree()
        {
            root = null;
            count = 0;
        }
    }
}
