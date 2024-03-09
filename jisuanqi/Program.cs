using System.Text.RegularExpressions;

namespace calculator
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
            Console.WriteLine("欢迎使用计算器，支持表达式计算。\n请输入表达式(允许+-*/和英文括号()[]{})：");
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
                    Console.WriteLine($"结果：{Compute(input)}");
                }
                catch
                {
                    Console.WriteLine("输入错误");
                    wait();
                    break;
                }
            }
        }
        static decimal Operate(decimal b, decimal a, string op)
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
        static decimal Compute(string input)
        {
            input = input.Replace("[", "(").Replace("{", "(").Replace("]", ")").Replace("}", ")")+")";
            Stack<decimal> numstack = new Stack<decimal>();//数字栈
            Stack<string> optstack = new Stack<string>();//符号栈
            optstack.Push("(");//默认在符号栈内压入一个左括号

            string[] stringResult = (from Match match in Regex.Matches(input, @"(\d+\.\d+?)|(\()|(\))|(\d+)|(\*)|(\+)|(-)|(/)|(\^)") select match.Value).ToArray();//分离数字和符号

            for (int i = 0; i < stringResult.Length; i++)
            {
                gui(stringResult, i, ref numstack, ref optstack);//不断计算
            }
            return numstack.Peek();
        }
        static void gui(string[] stringResult, int i, ref Stack<decimal> numstack, ref Stack<string> optstack)
        {
            if (stringResult[i] == "(")
            {
                optstack.Push(stringResult[i]);
            }
            else if (stringResult[i] == ")")
            {
                if (optstack.Peek() == "(")
                {
                    optstack.Pop();
                }
                else
                {
                    numstack.Push(Operate(numstack.Pop(), numstack.Pop(), optstack.Pop()));
                    gui(stringResult, i, ref numstack, ref optstack);//递归
                }
            }
            else if (stringResult[i] == "+" || stringResult[i] == "-" || stringResult[i] == "*" || stringResult[i] == "/" || stringResult[i] == "^")
            {
                if (optstack.Peek() == "(")
                {
                    optstack.Push(stringResult[i]);
                }
                else if (getyxj(stringResult[i]) > getyxj(optstack.Peek()))
                {
                    optstack.Push(stringResult[i]);
                }
                else
                {
                    numstack.Push(Operate(numstack.Pop(), numstack.Pop(), optstack.Pop()));
                    gui(stringResult, i, ref numstack, ref optstack);//递归
                }
            }
            else
            {
                numstack.Push(decimal.Parse(stringResult[i]));
            }
        }
    }
}
