using System.Text.RegularExpressions;

namespace jisuanqi
{
    internal class Program
    {
        static void wait()
        {
            Console.WriteLine("按下任意键以退出");
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("欢迎使用计算器，支持表达式计算。\n请输入表达式（允许+-*/和英文括号()[]{},如果要输入小数，请输入分数。如0.5请用1/2代替）：");
            for (int i = 0; i < 101; i++)
            {
                string? input = Console.ReadLine();
                if (input == "" || input == null)
                {
                    Console.WriteLine("你什么也没输入");
                    wait();
                    break;
                }
                try
                {
                    Console.WriteLine($"结果：{yunsuan(input)}");
                }
                catch
                {
                    Console.WriteLine("输入错误");
                    wait();
                    break;
                }
            }
        }
        static decimal jisuan(decimal b, decimal a, string op)
        {
            decimal result = 0;
            switch (op)
            {
                case "+":
                    result = a + b;
                    break;
                case "-":
                    result = a - b;
                    break;
                case "*":
                    result = a * b;
                    break;
                case "/":
                    result = a / b;
                    break;
                case "^":
                    result = (decimal)Math.Pow((double)a, (double)b);
                    break;
            }
            return result;
        }
        static int getyxj(string op)
        {
            int result = 0;
            switch (op)
            {
                case "+":
                case "-":
                    result = 1;
                    break;
                case "*":
                case "/":
                    result = 2;
                    break;
                case "^":
                    result = 3;
                    break;
            }
            return result;
        }
        static decimal yunsuan(string input)
        {
            input = input.Replace("[", "(").Replace("{", "(").Replace("]", ")").Replace("}", ")");
            Stack<decimal> numstack = new Stack<decimal>();//数字栈
            Stack<string> charstack = new Stack<string>();//符号栈
            charstack.Push("(");//默认在符号栈内压入一个左括号

            string[] stringResult = (from Match match in Regex.Matches(input, @"(\()|(\))|(\d+)|(\*)|(\+)|(-)|(/)|(\^)") select match.Value).ToArray();//分离数字和符号

            string[] mid = new string[stringResult.Length + 1];
            for (int q = 0; q < stringResult.Length; q++)
            {
                mid[q] = stringResult[q];
            }
            mid[stringResult.Length] = ")";
            stringResult = mid;//上面几步是扩容分离出来的符号和数字字符串数组，并在结尾默认加一个右括号

            for (int i = 0; i < stringResult.Length; i++)
            {
                suan(stringResult, i, ref numstack, ref charstack);//不断计算
            }
            return numstack.Peek();
        }
        static void suan(string[] stringResult, int i, ref Stack<decimal> numstack, ref Stack<string> charstack)
        {
            if (stringResult[i] == "(")
            {
                charstack.Push(stringResult[i]);
            }
            else if (stringResult[i] == ")")
            {
                if (charstack.Peek() == "(")
                {
                    charstack.Pop();
                }
                else
                {
                    numstack.Push(jisuan(numstack.Pop(), numstack.Pop(), charstack.Pop()));
                    suan(stringResult, i, ref numstack, ref charstack);//递归
                }
            }
            else if (stringResult[i] == "+" || stringResult[i] == "-" || stringResult[i] == "*" || stringResult[i] == "/" || stringResult[i] == "^")
            {
                if (charstack.Peek() == "(")
                {
                    charstack.Push(stringResult[i]);
                }
                else if (getyxj(stringResult[i]) > getyxj(charstack.Peek()))
                {
                    charstack.Push(stringResult[i]);
                }
                else
                {
                    numstack.Push(jisuan(numstack.Pop(), numstack.Pop(), charstack.Pop()));
                    suan(stringResult, i, ref numstack, ref charstack);//递归
                }
            }
            else
            {
                numstack.Push(decimal.Parse(stringResult[i]));
            }
        }
    }
}
