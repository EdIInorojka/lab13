using ClassLibrary133;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba133
{
    public class MyCollection<T> : MyList<T>, IEnumerable<T>, IList<T> where T : IInit, ICloneable, IComparable, new()
    {
        public bool IsReadOnly => throw new NotImplementedException();

        public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public MyCollection() : base() { }
        public MyCollection(int len) : base(len) { }
        public MyCollection(T[] collection) : base(collection) { }

        public IEnumerator<T> GetEnumerator() => this.GetData();


        IEnumerator IEnumerable.GetEnumerator() //метод для перебора значений
        {
            return GetEnumerator();
        }
        public IEnumerator<T> GetData()
        {
            Point<T> current = beg;
            while (current != null)
            {
                yield return current.Data; //возвращает элемент коллекции в итераторе и перемещает текущую позицию на следующий элемент
                current = current.Next;
            }
        }

        public int IndexOf(T item)
        {
            Point<T> current = beg;
            int index = 0;
            while (current != null)
            {
                if (current.Data != null && current.Data.Equals(item))
                {
                    return index;
                }
                current = current.Next;
                index++;
            }
            return -1; // Элемент не найден
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > count) throw new ArgumentOutOfRangeException(nameof(index));

            if (index == 0)
            {
                AddToBegin(item);
                return;
            }

            if (index == count)
            {
                AddToEnd(item);
                return;
            }

            Point<T> current = beg;
            for (int i = 0; i < index - 1; i++)
            {
                current = current.Next;
            }

            T newData = (T)item.Clone();
            Point<T> newItem = new Point<T>(newData)
            {
                Next = current.Next,
                Pred = current
            };
            if (current.Next != null)
            {
                current.Next.Pred = newItem;
            }
            current.Next = newItem;
            count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count) throw new ArgumentOutOfRangeException(nameof(index));

            if (index == 0)
            {
                if (beg != null)
                {
                    beg = beg.Next;
                    if (beg != null)
                    {
                        beg.Pred = null;
                    }
                    count--;
                }
                return;
            }
            Point<T> current = beg;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            if (current.Pred != null)
            {
                current.Pred.Next = current.Next;
            }
            if (current.Next != null)
            {
                current.Next.Pred = current.Pred;
            }
            count--;
        }

        public void Add(T item)
        {
            AddToEnd(item);
        }

        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Целевой массив не может быть пустым.");
            }

            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Недопустимое значение индекса массива.");
            }

            if (array.Length - arrayIndex < count)
            {
                throw new ArgumentException("Целевой массив имеет недостаточную длину для копирования элементов начиная с указанного индекса.");
            }

            Point<T> current = beg;
            while (current != null)
            {
                array[arrayIndex++] = current.Data;
                current = current.Next;
            }
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }
    }
    public class MyEnumerator<T> : IEnumerator<T> where T : IInit, ICloneable, IComparable, new() //свой перечислитель 
    {
        Point<T> beg;
        Point<T> curr;

        public MyEnumerator(MyCollection<T> coll)
        {
            beg = coll.beg;
            curr = beg;
        }
        public T Current => curr.Data;

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (curr.Next == null) { Reset(); return false; }
            else { curr = curr.Next; return true; }
        }

        public void Reset()
        {
            curr = beg;
        }
    }
}
