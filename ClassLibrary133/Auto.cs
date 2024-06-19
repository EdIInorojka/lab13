using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary133
{
    public class Auto : IInit, IComparable, ICloneable
    {
        protected string brand; //бренд машины
        protected string color; //цвет 
        protected double yoi; // year of issue (год выпуска)
        protected double cost; //стоимость
        protected double clearance;//просвет

        string[] brandarr = { "BMW", "Honda", "Volkswagen", "Ford", "Audi" };//список из имеющихся брендов авто
        string[] colorarr = { "black", "white", "green", "purple", "pink" }; //список цветов

        public string Brand { get; set; }
        public string Color { get; set; }
        public double Yoi
        {
            get => yoi;
            set
            {
                if (value < 0 || value > 2024) { yoi = 0; }
                else yoi = value;
            }
        }
        public double Cost
        {
            get => cost;
            set
            {
                if (value < 0) { cost = 0; }
                else cost = value;
            }
        }
        public double Clearance
        {
            get => clearance;
            set
            {
                if (value < 0) { clearance = 0; }
                else clearance = value;
            }
        }

        public Random rnd = new Random();

        public Auto() //конструктор без параметров
        {
            Brand = "no brand";
            Color = "no color";
            Yoi = 0;
            Cost = 0;
            Clearance = 0;
        }

        public Auto(string brand, string color, double yoi, double cost, double clearance) //конструктор с параметрами
        {
            Brand = brand;
            Color = color;
            Yoi = yoi;
            Cost = cost;
            Clearance = clearance;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Auto a) return this.Brand == a.Brand && this.Color == a.Color && this.Yoi == a.Yoi && this.Cost == a.Cost && this.Clearance == a.Clearance;
            return false;
        }

        public override string ToString()
        {
            return $"Бренд: {Brand}, цвет: {Color}, год выпуска: {Yoi}, стоимость: {Cost}, дорожный просвет: {Clearance}";
        }
        [ExcludeFromCodeCoverage]
        public virtual void Init()
        {
            Console.Write("Введите бренд: ");
            Brand = Console.ReadLine();

            Console.Write("Введите цвет: ");
            Color = Console.ReadLine();

            Console.Write("Введите год выпуска: ");
            Yoi = InputDoubleNumber();

            Console.Write("Введите стоимость: ");
            Cost = InputDoubleNumber();

            Console.Write("Введите размер просвета: ");
            Clearance = InputDoubleNumber();
        }
        [ExcludeFromCodeCoverage]
        public virtual void RandomInit()
        {
            Brand = brandarr[rnd.Next(brandarr.Length)];
            Color = colorarr[rnd.Next(colorarr.Length)];
            Yoi = rnd.Next(1950, 2024);
            Cost = rnd.Next(1, 1000000);
            Clearance = rnd.Next(0, 100);
        }
        [ExcludeFromCodeCoverage]
        public virtual void Show() // виртуальный метод
        {
            Console.WriteLine($"Auto; Бренд: {Brand}, цвет: {Color}, год выпуска: {Yoi}, стоимость: {Cost}, дорожный просвет: {Clearance}");
        }
        [ExcludeFromCodeCoverage]
        public void Shownw()// невиртуальный метод
        {
            Console.WriteLine($"Бренд: {Brand}, цвет: {Color}, год выпуска: {Yoi}, стоимость: {Cost}, дорожный просвет: {Clearance}");
        }
        [ExcludeFromCodeCoverage]
        public static double InputDoubleNumber() // проверка на целое число
        {
            bool isCorrert;
            double number;
            do
            {
                isCorrert = double.TryParse(Console.ReadLine(), out number);
                if (!isCorrert) Console.WriteLine("Пожалуйста, введите число: ");
            } while (!isCorrert);
            return number;
        }
        public int CompareTo(object obj)
        {
            if (obj == null) return -1;
            if (typeof(Auto) != obj.GetType()) return 1;

            Auto auto = obj as Auto;

            return this.Cost.CompareTo(auto.Cost);
        }
        public object Clone()
        {
            return new Auto(Brand, Color, Yoi, Cost, Clearance);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 31;
                hash = hash * 23 + (Brand.GetHashCode() + Color.GetHashCode() + Yoi.GetHashCode() + Cost.GetHashCode() + Clearance.GetHashCode());
                return hash;
            }
        }
        //object ICloneable.Clone()
        //{
        //    return Clone(); // Вызов вашего существующего метода Clone
        //}
        public int CompareTo<T>(T data) where T : IInit, ICloneable, new()
        {
            throw new NotImplementedException();
        }
    }
}
