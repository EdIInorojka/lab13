using ClassLibrary133;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba133
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    // Определяем класс аргументов события
    public class CollectionHandlerEventArgs : EventArgs
    {
        public string Action { get; }
        public object Item { get; }

        public CollectionHandlerEventArgs(string action, object item)
        {
            Action = action;
            Item = item;
        }
    }

    // Класс MyObservableCollection, наследующий MyCollection и реализующий IList
    public class MyObservableCollection<T> : MyCollection<T>, IList<T> where T : IInit, ICloneable, IComparable, new()
    {
       
        // Событие, уведомляющее об изменениях в коллекции
        public event CollectionHandler CollectionChanged;

        // Событие, уведомляющее об изменении количества элементов в коллекции или об изменении ссылок в коллекции
        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionReferenceChanged;

        // Метод для генерации события изменения коллекции
        public virtual void OnCollectionChanged(string action, T item)
        {
            CollectionChanged?.Invoke(this, new CollectionHandlerEventArgs(action, item));
        }

        // Метод для генерации события изменения количества элементов в коллекции или об изменении ссылок в коллекции
        public virtual void OnCollectionCountOrReferenceChanged(string action, T item)
        {
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs(action, item));
            CollectionReferenceChanged?.Invoke(this, new CollectionHandlerEventArgs(action, item));
        }
        
        public virtual void OnCollectionCountChanged(string action, T item)
        {
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs(action, item));
        }
        public virtual void OnCollectionReferenceChanged(string action, T item)
        {
            CollectionReferenceChanged?.Invoke(this, new CollectionHandlerEventArgs(action, item));
        }

        public new void Add(T item)
        {
            base.Add(item);
            OnCollectionChanged("Добавление", item);
            OnCollectionCountChanged("CollectionCountChanged", item);
        }

        public new bool Remove(T item)
        {
            bool removed = base.Remove(item);
            if (removed)
            {
                OnCollectionChanged("Удаление", item);
                OnCollectionCountChanged("CollectionCountChanged", item);
            }
            return removed;
        }
        // Индексатор для доступа к элементам коллекции
        public new T this[int index]
        {
            get
            {
                if (index < 0 || index >= count) throw new ArgumentOutOfRangeException(nameof(index));
                Point<T> current = beg;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                return current.Data;
            }
            set
            {
                if (index < 0 || index >= count) throw new ArgumentOutOfRangeException(nameof(index));
                Point<T> current = beg;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                current.Data = value;
                OnCollectionChanged("Изменение элемента", value);
                OnCollectionReferenceChanged("CollectionReferenceChanged", value); // Бросаем событие CollectionReferenceChanged при изменении ссылки
            }
        }

        // Свойство для получения текущего количества элементов в коллекции
        public int Length => count;

        // Переопределённый метод очистки коллекции
        public new void Clear()
        {
            base.Clear();
            OnCollectionChanged("Очистка", default(T));  // Генерация события при очистке коллекции
            OnCollectionCountOrReferenceChanged("Изменение количества или ссылки", default(T)); // Генерация события при изменении количества элементов или ссылок
        }

        // Метод вставки элемента в коллекцию по индексу
        public new void Insert(int index, T item)
        {
            if (index < 0 || index > count) throw new ArgumentOutOfRangeException(nameof(index));

            if (index == 0)
            {
                AddToBegin(item);
                OnCollectionChanged("Добавление в начало", item);  // Генерация события при добавлении в начало
                OnCollectionCountOrReferenceChanged("Изменение количества или ссылки", item); // Генерация события при изменении количества элементов или ссылок
                return;
            }

            if (index == count)
            {
                AddToEnd(item);
                OnCollectionChanged("Добавление в конец", item);  // Генерация события при добавлении в конец
                OnCollectionCountOrReferenceChanged("Изменение количества или ссылки", item); // Генерация события при изменении количества элементов или ссылок
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
            OnCollectionChanged("Вставка", item);  // Генерация события при вставке элемента
            OnCollectionCountOrReferenceChanged("Изменение количества или ссылки", item); // Генерация события при изменении количества элементов или ссылок
        }

        // Метод удаления элемента по индексу
        public new void RemoveAt(int index)
        {
            if (index < 0 || index >= count) throw new ArgumentOutOfRangeException(nameof(index));

            Point<T> current = beg;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            T removedItem = current.Data;

            if (current.Pred != null)
            {
                current.Pred.Next = current.Next;
            }
            if (current.Next != null)
            {
                current.Next.Pred = current.Pred;
            }
            count--;

            OnCollectionChanged("Удаление по индексу", removedItem);  // Генерация события при удалении по индексу
            OnCollectionCountOrReferenceChanged("Изменение количества или ссылки", removedItem); // Генерация события при изменении количества элементов или ссылок
        }

        // Реализация метода Contains для проверки наличия элемента в коллекции
        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        // Реализация метода CopyTo для копирования элементов коллекции в массив
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

        // Реализация метода IndexOf для получения индекса элемента в коллекции
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
        // Метод для получения перечислителя для перебора элементов коллекции
        public IEnumerator<T> GetEnumerator()
        {
            Point<T> current = beg;
            while (current != null)
            {
                yield return current.Data; // возвращает элемент коллекции в итераторе и перемещает текущую позицию на следующий элемент
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() // метод для перебора значений
        {
            return GetEnumerator();
        }
        public void AddItem(T obj)
        {
            base.Add(obj);
            OnCollectionCountChanged("CollectionCountChanged", obj);
        }

        // Переименованный метод Remove
        public new bool RemoveItem(T obj)
        {
            bool removed = base.Remove(obj);
            if (removed)
            {
                OnCollectionChanged("Удаление", obj);
                OnCollectionCountChanged("Изменение количества или ссылки", obj);
            }
            return removed;
        }
    }
}

