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
            int input = 1;
            while (input == 1)
            {
                Console.Write("请输入生成四则运算题的数目: ");
                int numOfQue = int.Parse(Console.ReadLine());
                string[] inputAnswer = new string[numOfQue];
                string[] correctAnswer = new string[numOfQue];
                string[] ques = new string[numOfQue];
                List<string> result = new List<string>();
                for (int i = 0; i < numOfQue; i++)
                {
                    result = produceQue();
                    ques[i] = result[0];
                 
                    correctAnswer[i] = Calucate(result);
                }
                Console.WriteLine();
                Console.Write("是否输入答案(输入1表示用户输入答案，否则不输入): ");
                input = int.Parse(Console.ReadLine());
                if (input == 1)
                {
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


                Console.Write("是否显示正确答案(输入1表示显示正确答案，否则不显示): ");
                input = int.Parse(Console.ReadLine());
                if (input == 1)
                {
                    for (int i = 0; i < numOfQue; i++)
                        Console.Write("{0,-20}", ques[i] + operators[4] + space + correctAnswer[i]);
                    Console.WriteLine();
                }
                Console.Write("是否继续生成四则运算题的数目(输入1继续生成，否则不生成): ");
                input = int.Parse(Console.ReadLine());
                Console.Clear();
            }

            Console.Write("是否生成题目文件(输入1生成，否则不生成): ");
            input = int.Parse(Console.ReadLine());
            if (input == 1)
            {
                Console.Write("输入生成题目的数量: ");
                string filename = "que.txt";//这里是你的已知文件
                FileStream fs = File.Create(filename);  //创建文件
                fs.Close();
                StreamWriter sw = new StreamWriter(filename);
                input = int.Parse(Console.ReadLine());
                for (int i = 0; i < input; i++)
                {
                    string que = "";
                    que = produceQue()[0];
                    sw.Write("{0,-20}",que + operators[4] + space);
                    if (i % 10 == 9)
                        sw.Write("\r\n");
                }
                sw.Close();
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
                int left = isOperand.FindIndex(s=>s==start[i]);
                str.Insert(left, "(");
                isOperand.Insert(left, -1);
                int right = isOperand.FindIndex(s =>s==end[i]);
                str.Insert(right + 1, ")");
                isOperand.Insert(right + 1, -1);
            }
            str.Insert(0, "");
            for (int i = 1; i < str.Count;)
            {
                str[0] += str[i++] + space;
            }
            return str;
        }




        static string Compute(Fraction leftNum, Fraction rightNum, int op)
        {
            switch (op)
            {
                case 0: return leftNum + rightNum;
                case 1: return leftNum - rightNum;
                case 2: return leftNum * rightNum;
                case 3: return leftNum / rightNum;
                default: return "";
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

        static string Calucate(List<string> expression)
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
