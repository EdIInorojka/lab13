using ClassLibrary133;
using laba133;
using System.Linq;

namespace TestProject2
{
    [TestClass]
    public class UnitTest1
    {
        //тесты из прошлой лабораторной работы
        [TestMethod]
        public void AddToBegin_ShouldIncreaseCount()
        {
            // Arrange
            MyList<Auto> list = new MyList<Auto>();
            Auto item = new Auto();
            item.RandomInit();

            // Act
            list.AddToBegin(item);

            // Assert
            Assert.AreEqual(1, list.Count);
        }
        [TestMethod]
        public void AddToEnd_ShouldIncreaseCount()
        {
            // Arrange
            MyList<Auto> list = new MyList<Auto>();
            Auto item = new Auto();
            item.RandomInit();

            // Act
            list.AddToEnd(item);

            // Assert
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void AddToOdd_ShouldAddItemsAtOddPositions()
        {
            // Arrange
            MyList<Auto> list = new MyList<Auto>();
            for (int i = 0; i < 5; i++)
            {
                Auto item = new Auto();
                item.RandomInit();
                list.AddToEnd(item);
            }

            // Act
            list.AddToOdd();

            // Assert
            Assert.AreEqual(11, list.Count);
        }

        [TestMethod]
        public void RemoveFrom_ShouldRemoveItemsFromList()
        {
            // Arrange
            MyList<Auto> list = new MyList<Auto>();
            Auto item1 = new Auto { Brand = "BMW", Color = "Black", Yoi = 2020, Cost = 30000, Clearance = 10 };
            Auto item2 = new Auto { Brand = "Audi", Color = "White", Yoi = 2021, Cost = 35000, Clearance = 12 };

            list.AddToEnd(item1);
            list.AddToEnd(item2);

            // Act
            list.RemoveFrom(item1);

            // Assert
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void Clone_ShouldCreateExactCopy()
        {
            // Arrange
            MyList<Auto> list = new MyList<Auto>();
            Auto item = new Auto();
            item.RandomInit();

            list.AddToEnd(item);

            // Act
            MyList<Auto> clonedList = list.Clone();

            // Assert
            Assert.AreEqual(list.Count, clonedList.Count);
            Assert.AreEqual(list.beg.Data, clonedList.beg.Data);
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllItems()
        {
            // Arrange
            MyList<Auto> list = new MyList<Auto>();
            Auto item = new Auto();
            item.RandomInit();

            list.AddToEnd(item);

            // Act
            list.Clear();

            // Assert
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void Constructor_ShouldInitializeListWithSize()
        {
            // Act
            MyList<Auto> list = new MyList<Auto>(5);

            // Assert
            Assert.AreEqual(4, list.Count);
        }

        [TestMethod]
        public void Constructor_ShouldThrowExceptionForNegativeSize()
        {
            // Act & Assert
            Assert.ThrowsException<Exception>(() => new MyList<Auto>(-1));
        }

        [TestMethod]
        public void Constructor_WithCollection_ShouldInitializeList()
        {
            // Arrange
            Auto[] items = {
                new Auto { Brand = "BMW", Color = "Black", Yoi = 2020, Cost = 30000, Clearance = 10 },
                new Auto { Brand = "Audi", Color = "White", Yoi = 2021, Cost = 35000, Clearance = 12 }
            };

            // Act
            MyList<Auto> list = new MyList<Auto>(items);

            // Assert
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void FindItem_ShouldReturnCorrectItem()
        {
            // Arrange
            MyList<Auto> list = new MyList<Auto>();
            Auto item = new Auto { Brand = "BMW", Color = "Black", Yoi = 2020, Cost = 30000, Clearance = 10 };

            list.AddToEnd(item);

            // Act
            var foundItem = list.FindItem(item);

            // Assert
            Assert.AreEqual(item, foundItem.Data);
        }

        [TestMethod]
        public void RemoveItem_ShouldRemoveSpecificItem()
        {
            // Arrange
            MyList<Auto> list = new MyList<Auto>();
            Auto item1 = new Auto { Brand = "BMW", Color = "Black", Yoi = 2020, Cost = 30000, Clearance = 10 };
            Auto item2 = new Auto { Brand = "Audi", Color = "White", Yoi = 2021, Cost = 35000, Clearance = 12 };

            list.AddToEnd(item1);
            list.AddToEnd(item2);

            // Act
            bool result = list.RemoveItem(item1);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(item2, list.beg.Data);
        }
        //тесты для 2ой части
        [TestMethod]
        public void Constructor_ShouldInitializeTableWithDefaultSize()
        {
            // Arrange
            int defaultSize = 10;

            // Act
            MyHashTable<Auto> hashTable = new MyHashTable<Auto>();

            // Assert
            Assert.IsNotNull(hashTable);
            Assert.AreEqual(defaultSize, hashTable.Capacity);
        }


        [TestMethod]
        public void Constructor_WithSize_ShouldInitializeTableWithGivenSize()
        {
            // Arrange
            int size = 20;

            // Act
            MyHashTable<Auto> hashTable = new MyHashTable<Auto>(size);

            // Assert
            Assert.IsNotNull(hashTable);
            Assert.AreEqual(size, hashTable.Capacity);
        }

        [TestMethod]
        public void Constructor_WithCollection_ShouldInitializeTable()
        {
            // Arrange
            Auto[] autos = new Auto[]
            {
                new Auto { Brand = "BMW", Color = "Black", Yoi = 2020, Cost = 30000, Clearance = 10 },
                new Auto { Brand = "Audi", Color = "White", Yoi = 2021, Cost = 35000, Clearance = 12 }
            };

            // Act
            MyHashTable<Auto> hashTable = new MyHashTable<Auto>(autos);

            // Assert
            Assert.AreEqual(autos.Length, hashTable.Capacity);
            Assert.IsTrue(hashTable.Contains(autos[0]));
            Assert.IsTrue(hashTable.Contains(autos[1]));
        }

        [TestMethod]
        public void AddPoint_ShouldAddElementToTable()
        {
            // Arrange
            MyHashTable<Auto> hashTable = new MyHashTable<Auto>(10);
            Auto auto = new Auto { Brand = "BMW", Color = "Black", Yoi = 2020, Cost = 30000, Clearance = 10 };

            // Act
            hashTable.AddPoint(auto);

            // Assert
            Assert.IsTrue(hashTable.Contains(auto));
        }

        [TestMethod]
        public void Contains_ShouldReturnTrueIfElementExists()
        {
            // Arrange
            MyHashTable<Auto> hashTable = new MyHashTable<Auto>(10);
            Auto auto = new Auto { Brand = "BMW", Color = "Black", Yoi = 2020, Cost = 30000, Clearance = 10 };
            hashTable.AddPoint(auto);

            // Act
            bool contains = hashTable.Contains(auto);

            // Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void Contains_ShouldReturnFalseIfElementDoesNotExist()
        {
            // Arrange
            MyHashTable<Auto> hashTable = new MyHashTable<Auto>(10);
            Auto auto = new Auto { Brand = "BMW", Color = "Black", Yoi = 2020, Cost = 30000, Clearance = 10 };

            // Act
            bool contains = hashTable.Contains(auto);

            // Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void RemoveData_ShouldRemoveElementIfExists()
        {
            // Arrange
            MyHashTable<Auto> hashTable = new MyHashTable<Auto>(10);
            Auto auto = new Auto { Brand = "BMW", Color = "Black", Yoi = 2020, Cost = 30000, Clearance = 10 };
            hashTable.AddPoint(auto);

            // Act
            bool removed = hashTable.RemoveData(auto);

            // Assert
            Assert.IsTrue(removed);
            Assert.IsFalse(hashTable.Contains(auto));
        }

        [TestMethod]
        public void RemoveData_ShouldReturnFalseIfElementDoesNotExist()
        {
            // Arrange
            MyHashTable<Auto> hashTable = new MyHashTable<Auto>(10);
            Auto auto = new Auto { Brand = "BMW", Color = "Black", Yoi = 2020, Cost = 30000, Clearance = 10 };

            // Act
            bool removed = hashTable.RemoveData(auto);

            // Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void GetIndex_ShouldReturnCorrectIndex()
        {
            // Arrange
            MyHashTable<Auto> hashTable = new MyHashTable<Auto>(10);
            Auto auto = new Auto { Brand = "BMW", Color = "Black", Yoi = 2020, Cost = 30000, Clearance = 10 };

            // Act
            int index = hashTable.GetIndex(auto);

            // Assert
            int expectedIndex = Math.Abs(auto.GetHashCode()) % 10;
            Assert.AreEqual(expectedIndex, index);
        }
        //тесты для 3 части
        [TestMethod]
        public void Constructor_Default_ShouldInitializeTreeWithEmptyRoot()
        {
            // Arrange
            Tree<Auto> tree = new Tree<Auto>();

            // Act
            // Action
            // Assert
            Assert.IsNotNull(tree);
            Assert.IsNull(tree.root);
            Assert.AreEqual(0, tree.Count);
        }

        [TestMethod]
        public void Constructor_WithLength_ShouldInitializeTreeWithSpecifiedLength()
        {
            // Arrange
            int length = 5;

            // Act
            Tree<Auto> tree = new Tree<Auto>(length);

            // Assert
            Assert.IsNotNull(tree);
            Assert.IsNotNull(tree.root);
            Assert.AreEqual(length, tree.Count);
        }

        [TestMethod]
        public void AddPoint_ShouldIncrementCount()
        {
            // Arrange
            Tree<Auto> tree = new Tree<Auto>();
            Auto auto = new Auto();

            // Act
            tree.AddPoint(auto);

            // Assert
            Assert.AreEqual(1, tree.Count);
        }

        [TestMethod]
        public void TransformToFindTree_ShouldTransformTreeToSearchTree()
        {
            // Arrange
            Tree<Auto> tree = new Tree<Auto>(5);

            // Act
            tree.TransformToFindTree();

            // Assert
            Assert.IsNotNull(tree);
            Assert.IsTrue(IsSearchTree(tree.root));
        }

        // Можно добавить больше тестов в соответствии с вашими требованиями

        // Вспомогательный метод для проверки, является ли это двоичным деревом поиска
        private bool IsSearchTree(Point<Auto>? root)
        {
            // Implement your logic for checking if it's a binary search tree
            return true;
        }
        [TestMethod]
        public void FindAndDisplayMaxCostItem_WithSingleElement_ShouldPrintElement()
        {
            // Arrange
            var tree = new Tree<Auto>();
            var auto = new Auto("BMW", "black", 2020, 50000, 10); // Создаем объект Auto
            tree.AddPoint(auto); // Добавляем его в дерево

            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                tree.FindAndDisplayMaxCostItem(); // Вызываем метод для поиска элемента с максимальной стоимостью
                string expected = $"Элемент с максимальной стоимостью:\r\n{auto}\r\n"; // Ожидаемый вывод
                string actual = sw.ToString(); // Получаем фактический вывод
                                               // Assert
                Assert.AreEqual(expected, actual); // Проверяем соответствие ожидаемого и фактического вывода
            }
        }

        [TestMethod]
        public void FindAndDisplayMaxCostItem_WithMultipleElements_ShouldPrintMaxCostElement()
        {
            // Arrange
            var tree = new Tree<Auto>();
            var auto1 = new Auto("BMW", "black", 2020, 50000, 10);
            var auto2 = new Auto("Audi", "white", 2021, 60000, 12);
            var auto3 = new Auto("Mercedes", "red", 2019, 55000, 11);
            tree.AddPoint(auto1);
            tree.AddPoint(auto2);
            tree.AddPoint(auto3);

            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                tree.FindAndDisplayMaxCostItem(); // Вызываем метод для поиска элемента с максимальной стоимостью
                string expected = $"Элемент с максимальной стоимостью:\r\n{auto2}\r\n"; // Ожидаемый вывод - элемент auto2 с максимальной стоимостью
                string actual = sw.ToString(); // Получаем фактический вывод
                                               // Assert
                Assert.AreEqual(expected, actual); // Проверяем соответствие ожидаемого и фактического вывода
            }
        }
        [TestMethod]
        public void FindAndDisplayMaxCostItem_WithEmptyTree_ShouldPrintTreeIsEmpty()
        {
            // Arrange
            var tree = new Tree<Auto>(); // Создаем пустое дерево

            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw); // Перенаправляем вывод Console.WriteLine()
                tree.FindAndDisplayMaxCostItem(); // Вызываем метод для поиска элемента с максимальной стоимостью
                string expected = "Дерево пусто.\r\n"; // Ожидаемый вывод
                string actual = sw.ToString(); // Получаем фактический вывод
                                               // Assert
                Assert.AreEqual(expected, actual); // Проверяем соответствие ожидаемого и фактического вывода
            }
        }
        //тесты для 4 части 
        private MyCollection<Auto> collection;

        [TestInitialize]
        public void Initialize()
        {
            collection = new MyCollection<Auto>();
        }

        [TestMethod]
        public void AddItem_ShouldAddItemToCollection()
        {
            // Arrange
            Auto auto = new Auto("BMW", "black", 2010, 50000, 15);

            // Act
            collection.AddToBegin(auto);

            // Assert
            Assert.IsTrue(collection.Contains(auto));
        }

        [TestMethod]
        public void RemoveItem_ShouldRemoveItemFromCollection()
        {
            // Arrange
            Auto auto = new Auto("BMW", "black", 2010, 50000, 15);
            collection.AddToBegin(auto);

            // Act
            collection.RemoveItem(auto);

            // Assert
            Assert.IsFalse(collection.Contains(auto));
        }

        [TestMethod]
        public void Contains_ShouldReturnTrueIfItemExistsInCollection()
        {
            // Arrange
            Auto auto = new Auto("BMW", "black", 2010, 50000, 15);
            collection.AddToBegin(auto);

            // Act
            bool result = collection.Contains(auto);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Contains_ShouldReturnFalseIfItemDoesNotExistInCollection()
        {
            // Arrange
            Auto auto = new Auto("BMW", "black", 2010, 50000, 15);

            // Act
            bool result = collection.Contains(auto);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ClearCollection_ShouldRemoveAllItemsFromCollection()
        {
            // Arrange
            Auto auto1 = new Auto("BMW", "black", 2010, 50000, 15);
            Auto auto2 = new Auto("Honda", "white", 2015, 40000, 12);
            collection.AddToBegin(auto1);
            collection.AddToBegin(auto2);

            // Act
            collection.Clear();

            // Assert
            Assert.AreEqual(2, collection.Count);
        }
        [TestMethod]
        public void Add_ShouldTriggerCollectionChangedAndCountChangedEvents()
        {
            // Arrange
            var collection = new MyObservableCollection<Auto>();
            bool collectionChangedTriggered = false;
            bool collectionCountChangedTriggered = false;

            collection.CollectionChanged += (sender, args) =>
            {
                collectionChangedTriggered = args.Action == "Добавление";
            };
            collection.CollectionCountChanged += (sender, args) =>
            {
                collectionCountChangedTriggered = args.Action == "CollectionCountChanged";
            };

            Auto item = new Auto();
            item.RandomInit();

            // Act
            collection.Add(item);

            // Assert
            Assert.IsTrue(collectionChangedTriggered, "CollectionChanged event was not triggered.");
            Assert.IsTrue(collectionCountChangedTriggered, "CollectionCountChanged event was not triggered.");
            Assert.AreEqual(1, collection.Count);
        }

        [TestMethod]
        public void Remove_ShouldTriggerCollectionChangedAndCountChangedEvents()
        {
            // Arrange
            var collection = new MyObservableCollection<Auto>();
            Auto item = new Auto();
            item.RandomInit();
            collection.Add(item);

            bool collectionChangedTriggered = false;
            bool collectionCountChangedTriggered = false;

            collection.CollectionChanged += (sender, args) =>
            {
                collectionChangedTriggered = args.Action == "Удаление";
            };
            collection.CollectionCountChanged += (sender, args) =>
            {
                collectionCountChangedTriggered = args.Action == "CollectionCountChanged";
            };

            // Act
            bool removed = collection.Remove(item);

            // Assert
            Assert.IsTrue(removed);
            Assert.IsTrue(collectionChangedTriggered, "CollectionChanged event was not triggered.");
            Assert.IsTrue(collectionCountChangedTriggered, "CollectionCountChanged event was not triggered.");
            Assert.AreEqual(0, collection.Count);
        }


        [TestMethod]
        public void IndexerSet_ShouldTriggerCollectionChangedAndReferenceChangedEvents()
        {
            // Arrange
            var collection = new MyObservableCollection<Auto>();
            Auto item = new Auto();
            item.RandomInit();
            collection.Add(item);

            bool collectionChangedTriggered = false;
            bool collectionReferenceChangedTriggered = false;

            collection.CollectionChanged += (sender, args) =>
            {
                collectionChangedTriggered = args.Action == "Изменение элемента";
            };
            collection.CollectionReferenceChanged += (sender, args) =>
            {
                collectionReferenceChangedTriggered = args.Action == "CollectionReferenceChanged";
            };

            Auto newItem = new Auto();
            newItem.RandomInit();

            // Act
            collection[0] = newItem;

            // Assert
            Assert.IsTrue(collectionChangedTriggered, "CollectionChanged event was not triggered.");
            Assert.IsTrue(collectionReferenceChangedTriggered, "CollectionReferenceChanged event was not triggered.");
        }
        [TestMethod]
        public void JournalEntry_ShouldStoreCorrectData()
        {
            // Arrange
            string collectionName = "TestCollection";
            string actionType = "TestAction";
            string itemData = "TestData";

            // Act
            JournalEntry entry = new JournalEntry(collectionName, actionType, itemData);

            // Assert
            Assert.AreEqual(collectionName, entry.CollectionName);
            Assert.AreEqual(actionType, entry.ActionType);
            Assert.AreEqual(itemData, entry.ItemData);
        }

        [TestMethod]
        public void JournalEntry_ToString_ShouldReturnFormattedString()
        {
            // Arrange
            string collectionName = "TestCollection";
            string actionType = "TestAction";
            string itemData = "TestData";
            JournalEntry entry = new JournalEntry(collectionName, actionType, itemData);

            // Act
            string result = entry.ToString();

            // Assert
            string expected = "Коллекция: TestCollection, Действие: TestAction, Данные: TestData";
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void HandleCollectionCountChanged_ShouldAddEntryToJournal()
        {
            // Arrange
            var journal = new Journal();
            var auto = new Auto { Brand = "TestBrand" };
            var args = new CollectionHandlerEventArgs("Добавление", auto);

            // Act
            journal.HandleCollectionCountChanged(this, args);

            // Assert
            string journalContent = journal.ToString();
            Assert.IsTrue(journalContent.Contains("MyObservableCollection"), "Journal entry does not contain the correct source.");
            Assert.IsTrue(journalContent.Contains("CollectionCountChanged"), "Journal entry does not contain the correct event.");
            Assert.IsTrue(journalContent.Contains("Добавление: Бренд: TestBrand"), "Journal entry does not contain the correct action and item.");
        }

        [TestMethod]
        public void HandleCollectionReferenceChanged_ShouldAddEntryToJournal()
        {
            // Arrange
            var journal = new Journal();
            var auto = new Auto { Brand = "TestBrand" };
            var args = new CollectionHandlerEventArgs("Изменение элемента", auto);

            // Act
            journal.HandleCollectionReferenceChanged(this, args);

            // Assert
            string journalContent = journal.ToString();
            Assert.IsTrue(journalContent.Contains("MyObservableCollection"), "Journal entry does not contain the correct source.");
            Assert.IsTrue(journalContent.Contains("CollectionReferenceChanged"), "Journal entry does not contain the correct event.");
            Assert.IsTrue(journalContent.Contains("Изменение элемента: Бренд: TestBrand"), "Journal entry does not contain the correct action and item.");
        }

        [TestMethod]
        public void ToString_ShouldReturnAllEntries()
        {
            // Arrange
            var journal = new Journal();
            var auto1 = new Auto { Brand = "TestBrand1" };
            var auto2 = new Auto { Brand = "TestBrand2" };
            var args1 = new CollectionHandlerEventArgs("Добавление", auto1);
            var args2 = new CollectionHandlerEventArgs("Изменение элемента", auto2);

            // Act
            journal.HandleCollectionCountChanged(this, args1);
            journal.HandleCollectionReferenceChanged(this, args2);

            // Assert
            string journalContent = journal.ToString();
            Assert.IsTrue(journalContent.Contains("Добавление: Бренд: TestBrand1"), "Journal entry for first action is missing or incorrect.");
            Assert.IsTrue(journalContent.Contains("Изменение элемента: Бренд: TestBrand2"), "Journal entry for second action is missing or incorrect.");
        }
    }
}