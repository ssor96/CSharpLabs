using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab1
{
    public class MyGenericList<T> : IList<T>
    {
        class Element
        {
            public Element Next = null;
            public T Data;

            public Element(T item)
            {
                Data = item;
            }
        }

        private Element head = null;

        private Element At(int index)
        {
            int idx = 0;
            var cur = head;
            while (cur != null)
            {
                if (idx == index) return cur;
                cur = cur.Next;
                idx++;
            }
            throw new ArgumentOutOfRangeException(index.ToString() + " is not a valid index");
        }

        public T this[int index]
        {
            get => At(index).Data;
            set => At(index).Data = value;
        }

        public int Count
        {
            get
            {
                int res = 0;
                var cur = head;
                while (cur != null)
                {
                    cur = cur.Next;
                    res++;
                }
                return res;
            }
        }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            var newEl = new Element(item);
            if (head == null)
            {
                head = newEl;
                return;
            }
            var cur = head;
            while (cur.Next != null)
            {
                cur = cur.Next;
            }
            cur.Next = newEl;
        }

        public void Clear()
        {
            head = null;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array is null");
            }
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("array index is less than 0");
            }
            var cur = head;
            while (cur != null)
            {
                if (arrayIndex >= array.GetLength(0))
                {
                    throw new ArgumentException("array is too small");
                }
                array[arrayIndex++] = cur.Data;
                cur = cur.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var cur = head;
            while (cur != null)
            {
                yield return cur.Data;
                cur = cur.Next;
            }
        }

        public int IndexOf(T item)
        {
            var cur = head;
            int idx = 0;
            while (cur != null)
            {
                if (cur.Data.Equals(item)) return idx;
                cur = cur.Next;
                idx++;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            var newEl = new Element(item);
            if (index == 0)
            {
                newEl.Next = head;
                head = newEl;
                return;
            }
            int idx = 0;
            var cur = head;
            while (idx + 1 < index && cur != null)
            {
                cur = cur.Next;
                idx++;
            }
            if (idx + 1 != index || cur == null) throw new ArgumentOutOfRangeException(index.ToString() + " is not a valid index");
            newEl.Next = cur.Next;
            cur.Next = newEl;
        }

        public bool Remove(T item)
        {
            var idx = IndexOf(item);
            if (idx != -1)
            {
                RemoveAt(idx);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (head == null)
            {
                throw new ArgumentOutOfRangeException(index.ToString() + " is not a valid index");
            }
            if (index == 0)
            {
                head = head.Next;
                return;
            }
            int idx = 0;
            var cur = head;
            while (idx + 1 < index && cur != null)
            {
                cur = cur.Next;
                idx++;
            }
            if (idx + 1 != index || cur.Next == null) throw new ArgumentOutOfRangeException(index.ToString() + " is not a valid index");
            cur.Next = cur.Next.Next;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        override public string ToString()
        {
            string res = "[";
            var cur = head;
            while (cur != null)
            {
                if (cur != head) res += ", "; 
                res += cur.Data.ToString();
                cur = cur.Next;
            }
            res += "]";
            return res;
        }
    }
}
