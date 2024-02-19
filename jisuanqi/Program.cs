using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace jisuanqi
{
    internal class Program
    {
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
            }
            return result;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("欢迎使用计算器，支持表达式计算。\n请输入表达式（允许+-*/和英文括号(),如果括号要嵌套，请在每一层都使用小括号）：");
            for (int i = 0; i < 101; i++)
            {
                string? input = Console.ReadLine();
                if (input == "" || input == null)
                {
                    Console.WriteLine("你什么也没输入");
                    break;
                }
                try
                {
                    Console.WriteLine(yunsuan(input));
                }
                catch
                {
                    Console.WriteLine("输入错误");
                    break;
                }
            }
        }
        static decimal yunsuan(string input)
        {
            Stack<decimal> numstack = new Stack<decimal>();//数字栈
            Stack<string> charstack = new Stack<string>();//符号栈
            charstack.Push("(");

            string[] stringResult = (from Match match in Regex.Matches(input, @"(\()|(\))|(\d+)|(\*)|(\+)|(-)|(/)|(\[)|(\])") select match.Value).ToArray();

            string[] mid = new string[stringResult.Length + 1];
            for (int q = 0; q < stringResult.Length; q++)
            {
                mid[q] = stringResult[q];
            }
            mid[stringResult.Length] = ")";
            stringResult = mid;

            for (int i = 0; i < stringResult.Length; i++)
            {
                suan(stringResult, i, ref numstack, ref charstack);
            }
            return numstack.Peek();
        }
        static void suan(string[] stringResult, int i, ref Stack<decimal> numstack, ref Stack<string> charstack)
        {
            if (stringResult[i] == "+" || stringResult[i] == "-")
            {
                if (charstack.Peek() == "(")
                {
                    charstack.Push(stringResult[i]);
                }
                else
                {
                    numstack.Push(jisuan(numstack.Pop(), numstack.Pop(), charstack.Pop()));
                    suan(stringResult, i, ref numstack, ref charstack);
                }
            }
            else if (stringResult[i] == "*" || stringResult[i] == "/")
            {
                if (charstack.Peek() == "(" || charstack.Peek() == "+" || charstack.Peek() == "-")
                {
                    charstack.Push(stringResult[i]);
                }
                else
                {
                    numstack.Push(jisuan(numstack.Pop(), numstack.Pop(), charstack.Pop()));
                    suan(stringResult, i, ref numstack, ref charstack);
                }
            }
            else if (stringResult[i] == "(")
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
                    suan(stringResult, i, ref numstack, ref charstack);
                }
            }
            else
            {
                numstack.Push(decimal.Parse(stringResult[i]));
            }
        }
    }
}
