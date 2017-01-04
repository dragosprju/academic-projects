using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Calculator
{
    static class Postfix
    {

        private static List<string> expElems = new List<string>();
        private static Stack<string> stack = new Stack<string>();
        //private static List<string> queue = expElems; // also used as queue

        private static bool HasHigherPresedence(string opQuestioned, string opComparedWith)
        {
            string[] ops = { opQuestioned, opComparedWith };
            int[] lvls = new int[2];
            
            for (int i = 0; i <= 1; i++)
            {
                /*if (ops[i].Equals("(") || ops[i].Equals(")"))
                {
                    lvls[i] = 1;
                }
                else*/
                if (ops[i].Equals("*") || ops[i].Equals("/"))
                {
                    lvls[i] = 1;
                }
                else if (ops[i].Equals("(") || ops[i].Equals(")"))
                {
                    lvls[i] = 2;
                }
            }

            if (lvls[0] > lvls[1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsOperand(string op)
        {
            double result;

            if (double.TryParse(op, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        private static bool IsOperator(string op)
        {
            if (op == "+" || op == "-" || op == "*" || op == "/")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsOpeningParanthesis(string op)
        {
            if (op == "(")
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        private static bool IsClosingParanthesis(string op)
        {
            if (op == ")")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static double Operate(double value1, double value2, string op)
        {
            if (op == "+")
            {
                return value1 + value2;
            }
            else if (op == "-")
            {
                return value1 - value2;
            }
            else if (op == "*")
            {
                return value1 * value2;
            }
            else if (op == "/")
            {
                return value1 / value2;
            }
            else
            {
                throw new Exception("Just went into the impossible if in Operate(). Operator: '" + op + "'.");
            }
        }

        public static string Parse(string exp)
        {
            string result = "";

            stack.Clear();
            expElems.Clear();
            expElems.AddRange(exp.Split(' '));

            foreach (string element in expElems)
            {
                if (IsOperand(element))
                {
                    result += element + " ";
                }
                else if (IsOperator(element))
                {
                    while (stack.Count != 0 && !IsOpeningParanthesis(stack.Peek()) && HasHigherPresedence(stack.Peek(), element))
                    {
                        result += stack.Pop() + " ";
                    }
                    stack.Push(element);
                }
                else if (IsOpeningParanthesis(element))
                {
                    stack.Push(element);
                }
                else if (IsClosingParanthesis(element))
                {
                    while (stack.Count != 0 && !IsOpeningParanthesis(stack.Peek()))
                    {
                        result += stack.Pop() + " ";
                    }
                    stack.Pop();
                }

            }
            while (stack.Count != 0)
            {
                result += stack.Pop() + " ";
            }

            return result;
        }

        public static string Calculate(string exp)
        {
            double val1, val2;
            double rez = 0;

            expElems.Clear();
            stack.Clear();

            expElems.AddRange(exp.Split(' '));
            
            foreach(string element in expElems)
            {
                if (IsOperand(element))
                {
                    stack.Push(element);
                }
                else if (IsOperator(element))
                {
                    val2 = double.Parse(stack.Pop(), CultureInfo.InvariantCulture); // AICI E 2!!
                    val1 = double.Parse(stack.Pop(), CultureInfo.InvariantCulture);
                    rez = Operate(val1, val2, element);
                    stack.Push(rez.ToString().Replace(",","."));
                }
            }

            return rez.ToString().Replace(",", ".");
        }
    }
}
