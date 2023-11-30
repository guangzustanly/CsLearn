﻿namespace LinkedNudewtd
{
    class LinkedNude<T>
    {
        public T value;
        public LinkedNude<T>? last;
        public LinkedNude<T>? next;
        public LinkedNude(T value)
        {
            this.value = value;
        }
    }
    class LinkedList<T>
    {
        public LinkedNude<T>? head;
        public LinkedNude<T>? tail;
        public void Add(T value)
        {
            if (head == null || tail == null)
            {
                head = new LinkedNude<T>(value);
                tail = head;
            }
            else
            {
                tail.next = new LinkedNude<T>(value);
                LinkedNude<T> nude = tail;
                tail = tail.next;
                tail.last = nude;
            }
        }
        public void Remove(T value)
        {
            if (head == null || tail == null)
            {
                return;
            }
            else if (head.value.Equals(value))
            {
                head = head.next;
                if (head == null) { tail = null; }
                else { head.last = null; }
            }
            else if (tail.value.Equals(value))
            {
                tail = tail.last;
                tail.next = null;
            }
            else
            {
                LinkedNude<T> nude = head;
                while (nude.next != null)
                {
                    if (nude.next.value.Equals(value))
                    {
                        nude.next = nude.next.next;
                        nude.next.last = nude;
                        break;
                    }
                    nude = nude.next;
                }
            }
        }
        public void Clear()
        {
            head = null;
            tail = null;
        }
        public bool Contains(T value)
        {
            if (head == null) return false;
            LinkedNude<T> nude = head;
            while (nude != null)
            {
                if (nude.value.Equals(value))
                {
                    return true;
                }
                nude = nude.next;
            }
            return false;
        }
        public int IndexOf(T value)
        {
            if (head == null) return -1;
            LinkedNude<T> nude = head;
            int index = 0;
            while (nude != null)
            {
                if (nude.value.Equals(value))
                {
                    return index;
                }
                nude = nude.next;
                index++;
            }
            return -1;
        }
        public int LastIndexOf(T value)
        {
            if (head == null) return -1;
            LinkedNude<T> nude = tail;
            while (nude != null)
            {
                
            }
            return -1;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> l = new LinkedList<int>();
            l.Add(1);
            l.Remove(1);

            l.Add(1);
            l.Add(2);
            l.Remove(2);

            l.Add(2);
            l.Add(3);
            l.Add(4);
            l.Remove(3);
            l.Add(2);

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"是否存在{i}：{l.Contains(i)}");
                Console.WriteLine($"{i}的从前往后的索引：{l.IndexOf(i)}");
                Console.WriteLine($"{i}的从后往前的索引：{l.LastIndexOf(i)}");
            }
        }
    }
}