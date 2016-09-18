using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Fra;


namespace CalC
{
    class Program
    {
        static Random ran = new Random();
        static Dictionary<string, int> priorities = null;
        const string operators = "+-*/=";
        const string space = " ";
        static Program()
        {
            priorities = new Dictionary<string, int>();
            priorities.Add("#", -1);
            priorities.Add("+", 0);
            priorities.Add("-", 0);
            priorities.Add("*", 1);
            priorities.Add("/", 1);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("-n表示生成四则算式的数目");
            Console.WriteLine("-a表示用户是否输入答案（1输入，0不输入）");
            Console.WriteLine("-c表示是否显示正确答案（1显示，0不显示）");
            Console.WriteLine("-t表示是否生成题目文件（1生成，0不生成)");
            Console.WriteLine("-m表示文件生成题目的数量（若生成题目文件该参数必须输入,否则不输入）");
            try
            {
                if (args.Length != 8 && args.Length != 10)
                    throw new Exception("参数数目不正确");
                else if (args.Length == 8)
                {
                    if (args[0] != "-n" || args[2] != "-a" || args[4] != "-c" || args[6] != "-t")
                        throw new Exception("参数名称不正确");
                    if (int.Parse(args[1]) <= 0)
                        throw new Exception("题目数必须为正");
                    if (int.Parse(args[7]) == 1)
                        throw new Exception("参数数目不正确");
                    if ((int.Parse(args[3]) != 0 && int.Parse(args[3]) != 1) ||
                            (int.Parse(args[5]) != 0 && int.Parse(args[5]) != 1))
                        throw new Exception("参数值必须为0或1");
                }
                else
                {
                    if (args[0] != "-n" || args[2] != "-a" || args[4] != "-c" || args[6] != "-t" || args[8] != "-m")
                        throw new Exception("参数名称不正确");
                    if (int.Parse(args[1]) <= 0 || int.Parse(args[9]) <= 0)
                        throw new Exception("题目数必须为正");
                    if (int.Parse(args[7]) == 0)
                        throw new Exception("参数数目不正确");
                    if ((int.Parse(args[3]) != 0 && int.Parse(args[3]) != 1) ||
                            (int.Parse(args[5]) != 0 && int.Parse(args[5]) != 1))
                        throw new Exception("参数值必须为0或1");
                }

                int numOfQue = int.Parse(args[1]);
                string[] inputAnswer = new string[numOfQue];
                string[] correctAnswer = new string[numOfQue];
                string[] ques = new string[numOfQue];
                List<string> result = new List<string>();
                for (int i = 0; i < numOfQue; i++)
                {

                    result = produceQue();
                    ques[i] = result[0];
                    try
                    {
                        correctAnswer[i] = Calculate(result);
                        Console.WriteLine("{0,-20}", ques[i] + operators[4] + space);
                    }
                    catch (FractionException e)
                    {
                        i--;
                    }
                }
                Console.WriteLine();
                int input = int.Parse(args[3]);
                if (input == 1)
                {
                    Console.Write("输入答案： ");
                    for (int i = 0; i < numOfQue; i++)
                    {
                        Console.Write("{0,-20}", ques[i] + operators[4] + space);
                        inputAnswer[i] = Console.ReadLine();
                    }

                    int numOfCorrect = 0;
                    for (int i = 0; i < numOfQue; i++)
                    {
                        if (inputAnswer[i] == correctAnswer[i])
                            numOfCorrect++;
                    }
                    Console.WriteLine("您共答对" + numOfCorrect + "道题");
                }
                input = int.Parse(args[5]);
                if (input == 1)
                {
                    Console.Write("正确答案: ");
                    for (int i = 0; i < numOfQue; i++)
                        Console.Write("{0,-20}", ques[i] + operators[4] + space + correctAnswer[i]);
                    Console.WriteLine();
                }

                input = int.Parse(args[7]);
                if (input == 1)
                {
                    string filename = "que.txt";//这里是你的已知文件
                    FileStream fs = File.Create(filename);  //创建文件
                    fs.Close();
                    StreamWriter sw = new StreamWriter(filename);
                    input = int.Parse(args[9]);
                    for (int i = 0; i < input; i++)
                    {
                        string que = "";
                        que = produceQue()[0];
                        sw.Write("{0,-20}", que + operators[4] + space);
                        if (i % 10 == 9)
                            sw.Write("\r\n");
                    }
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("请输入正确参数");
            }
        }


        static List<string> produceQue()
        {
            List<string> str = new List<string>();
            List<int> isOperand = new List<int>();
            int count = 0;
            count = ran.Next(1, 3);
            int[] num = new int[count + 1];
            int[] den = new int[count + 1];
            string[] operand = new string[count + 1];
            int[] index = new int[count];
            int numOfBrackets = 0;
            for (int i = 0; i < count + 1; i++)
            {
                num[i] = ran.Next(2, 5);
                if (ran.Next(1, 10) < 8)
                    den[i] = 1;
                else
                {
                    den[i] = ran.Next(1, 5);
                    numOfBrackets = ran.Next(1, count);
                }
                operand[i] = new Fraction(num[i], den[i]).ToString();
                if (i < count)
                    index[i] = ran.Next(0, 4);
            }
            int[] start = new int[numOfBrackets];
            int[] end = new int[numOfBrackets];
            for (int i = 0; i < numOfBrackets; i++)
            {
                start[i] = ran.Next(1, count + 1);
                end[i] = ran.Next(start[i] + 1, count + 2);
            }
            for (int i = 0; i < numOfBrackets; i++)
            {
                for (int m = 0; m < numOfBrackets; m++)
                {
                    if (start[i] == end[m])
                    {
                        start[i] = -1;
                        end[m] = -1;
                    }
                }
            }
            int j = 1;
            for (int i = 0; i < count + 1; i++)
            {
                str.Add(operand[i]);
                isOperand.Add(i + 1);
                if (i < count)
                {
                    str.Add(operators[index[i]].ToString());
                    isOperand.Add(0);
                }
            }
            for (int i = 0; i < numOfBrackets; i++)
            {
                if (start[i] != -1)
                {
                    int left = isOperand.FindIndex(s => s == start[i]);
                    str.Insert(left, "(");
                    isOperand.Insert(left, -1);
                }
                if (end[i] != -1)
                {
                    int right = isOperand.FindIndex(s => s == end[i]);
                    str.Insert(right + 1, ")");
                    isOperand.Insert(right + 1, -1);
                }
            }
            str.Insert(0, "");
            for (int i = 1; i < str.Count; )
            {
                str[0] += str[i++] + space;
            }
            return str;
        }




        static string Compute(Fraction leftNum, Fraction rightNum, int op)
        {
            switch (op)
            {
                case 0:
                    return leftNum + rightNum;
                case 1:
                    return leftNum - rightNum;
                case 2:
                    return leftNum * rightNum;
                case 3:
                    return leftNum / rightNum;
                default:
                    return "";
            }
        }

        static bool IsOperator(string op)
        {

            return operators.IndexOf(op) >= 0;
        }

        static bool IsLeftAssoc(string op)
        {
            return op == "+" || op == "-" || op == "*" || op == "/" || op == "%";
        }

        static Queue<object> PreOrderToPostOrder(List<string> expression)
        {
            var result = new Queue<object>();
            var operatorStack = new Stack<string>();
            operatorStack.Push("#");
            string top, cur, tempChar;
            string tempNum;

            for (int i = 1; i < expression.Count; )
            {
                cur = expression[i++];
                top = operatorStack.Peek();

                if (cur == "(")
                {
                    operatorStack.Push(cur);
                }
                else
                {
                    if (IsOperator(cur))
                    {
                        while (IsOperator(top) && ((IsLeftAssoc(cur) && priorities[cur] <= priorities[top])) || (!IsLeftAssoc(cur) && priorities[cur] < priorities[top]))
                        {
                            result.Enqueue(operatorStack.Pop());
                            top = operatorStack.Peek();
                        }
                        operatorStack.Push(cur);
                    }
                    else if (cur == ")")
                    {
                        while (operatorStack.Count > 0 && (tempChar = operatorStack.Pop()) != "(")
                        {
                            result.Enqueue(tempChar);
                        }
                    }
                    else
                    {
                        tempNum = cur;
                        result.Enqueue(tempNum);
                    }
                }
            }
            while (operatorStack.Count > 0)
            {
                cur = operatorStack.Pop();
                if (cur == "#") continue;
                if (operatorStack.Count > 0)
                {
                    top = operatorStack.Peek();
                }

                result.Enqueue(cur);
            }

            return result;
        }

        static string Calculate(List<string> expression)
        {

            var rpn = PreOrderToPostOrder(expression);
            var operandStack = new Stack<string>();
            string left, right;
            object cur;
            while (rpn.Count > 0)
            {
                cur = rpn.Dequeue();
                int index = operators.IndexOf(cur.ToString());

                if (index >= 0)
                {
                    right = operandStack.Pop();
                    left = operandStack.Pop();
                    operandStack.Push(Compute(left, right, index));
                }
                else
                {
                    operandStack.Push(cur.ToString());
                }
            }
            return operandStack.Pop();
        }
    }
}
