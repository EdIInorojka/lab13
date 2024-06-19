using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba133
{
    public class Point<T> where T : IComparable
    {
        public T Data { get; set; }
        public Point<T> Next { get; set; }
        public Point<T> Pred { get; set; }
        public Point()
        {
            this.Data = default(T);
            this.Next = null;
            this.Pred = null;
        }
        public Point(T data)
        {
            this.Data = data;
            this.Next = null;
            this.Pred = null;
        }
        public override string ToString()
        {
            if (Data == null) return string.Empty;
            else return Data.ToString();
        }
        public int CompareTo(Point<T> other)
        {
            if (other == null)
                return 1;

            if (Data == null && other.Data == null)
                return 0;
            else if (Data == null)
                return -1;
            else if (other.Data == null)
                return 1;

            return Data.CompareTo(other.Data);
        }
        public override int GetHashCode()
        {
            return Data == null ? 0 : Data.GetHashCode();
        }
    }
}
