namespace ShujuChuli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("欢迎使用数据处理器");
            Console.Write("请输入数据（实例：1,2,3）(英文逗号！！！):");
            string a = Console.ReadLine();
            List<decimal> list = new List<decimal>();
            decimal he=0;//和
            foreach (string s in a.Split(','))
            {
                he += decimal.Parse(s);
                list.Add(decimal.Parse(s));
            }
            decimal pjs = he/list.Count;
            Console.WriteLine($"平均数为：{pjs}");

            decimal s2 = 0;//方差
            foreach (decimal s in list)
            {
                s2 += (pjs - s) * (pjs - s);
            }
            s2 /= list.Count;
            Console.WriteLine($"方差为：{s2}");

            list.Sort();//排序
            if (list.Count%2 == 0 )//如果有偶数个元素
            {
                Console.WriteLine($"中位数为：{(list[list.Count / 2] + list[list.Count / 2 - 1]) / 2}");
            }
            else if (list.Count%2==1 )//如果有奇数个元素
            {
                Console.WriteLine($"中位数为：{list[(list.Count-1)/2]}");
            }
        }
    }
}
