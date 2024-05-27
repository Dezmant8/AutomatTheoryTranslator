//using System.Text.RegularExpressions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using System.Threading.Tasks;
//using AutomatTheory3._0Lexer;

//namespace AutomatTheory3._0Lexer
//{
//    public class Expression
//    {
//        List<Token> ExpressionStack = new List<Token>();
//        Stack<string> Operations = new Stack<string>();
//        Stack<int> Prioritis = new Stack<int>();
//        int index = 0;
//        int countLpar = 0;
//        int countRpar = 0;
//        string output = null;
//        Dictionary<string, int> priority = new Dictionary<string, int>()
//        {
//            {"(", 0},
//            {")", 1},
//            {"or", 2},
//            {"and", 3},
//            {">" , 4}, {"<" , 4}, {"=" , 4}
//        };
//        public void TakeToken(Token token)
//        {
//            ExpressionStack.Add(token);
//        }
//        public void StartOPN()
//        {
//            Decstra();
//            if (countLpar != countRpar)
//                throw new Exception("Неверно составлено выражение: неравное количество открывающихся и закрывающихся скобок.");
//            ReversePolishNotation();
//        }
//        private void HighPriority(string operation)
//        {
//            int count = Operations.Count();
//            Stack<string> temp = new Stack<string>();
//            Stack<int> priorityTemp = new Stack<int>();
//            for (int i = 0; i < count; i++)
//            {
//                if (Prioritis.Peek() >= priority[operation])
//                {
//                    output += Operations.Pop();
//                    Prioritis.Pop();
//                }
//                else
//                {
//                    temp.Push(Operations.Pop());
//                    priorityTemp.Push(Prioritis.Pop());
//                }
//            }
//            temp.Reverse();
//            priorityTemp.Reverse();
//            int countTemp = temp.Count();
//            for (int i = 0; i < countTemp; i++)
//            {
//                Operations.Push(temp.Pop());
//                Prioritis.Push(priorityTemp.Pop());
//            }
//            Operations.Push(ExpressionStack[index].DCR);
//            Prioritis.Push(priority[operation]);
//        }

//        private void Decstra()
//        {
//            if (ExpressionStack[index].Type == tokenType.LPAR || ExpressionStack[index].Type == tokenType.ID || ExpressionStack[index].Type == tokenType.LIT)
//            {
//                Prioritis.Push(0);
//                while (index != ExpressionStack.Count())
//                {
//                    if (ExpressionStack[index].Type == tokenType.LIT || ExpressionStack[index].Type == tokenType.ID)
//                    {
//                        output += ExpressionStack[index].DCR + " ";
//                        index++;
//                    }
//                    else if (ExpressionStack[index].Type == tokenType.OR)
//                    {
//                        string operation = "or";

//                        if ((priority[operation] > Prioritis.Peek()) || Operations.Count() ==
//                        0)
//                        {
//                            Operations.Push(ExpressionStack[index].DCR + " ");
//                            Prioritis.Push(priority[operation]);
//                        }
//                        else
//                        {
//                            HighPriority(operation);
//                        }
//                        index++;
//                    }
//                    else if (ExpressionStack[index].Type == tokenType.AND)
//                    {
//                        string operation = "and";
//                        if ((priority[operation] > Prioritis.Peek()) || Operations.Count() ==
//                        0)
//                        {
//                            Operations.Push(ExpressionStack[index].DCR + " ");
//                            Prioritis.Push(priority[operation]);
//                        }
//                        else
//                        {
//                            HighPriority(operation);
//                        }
//                        index++;
//                    }
//                    else if (ExpressionStack[index].Type == tokenType.MORE)
//                    {
//                        string operation = ">";
//                        if ((priority[operation] > Prioritis.Peek()) || Operations.Count() ==
//                        0)
//                        {
//                            Operations.Push(ExpressionStack[index].DCR + " ");
//                            Prioritis.Push(priority[operation]);
//                        }
//                        else
//                        {
//                            HighPriority(operation);
//                        }
//                        index++;
//                    }
//                    else if (ExpressionStack[index].Type == tokenType.LESS)
//                    {
//                        string operation = "<";
//                        if ((priority[operation] > Prioritis.Peek()) || Operations.Count() ==
//                        0)

//                        {
//                            Operations.Push(ExpressionStack[index].DCR + " ");
//                            Prioritis.Push(priority[operation]);
//                        }
//                        else
//                        {
//                            HighPriority(operation);
//                        }
//                        index++;
//                    }
//                    else if (ExpressionStack[index].Type == tokenType.EQUAL)
//                    {
//                        string operation = "=";
//                        if ((priority[operation] > Prioritis.Peek()) || Operations.Count() ==
//                        0)

//                        {
//                            Operations.Push(ExpressionStack[index].DCR + " ");
//                            Prioritis.Push(priority[operation]);
//                        }
//                        else
//                        {
//                            HighPriority(operation);
//                        }
//                        index++;
//                    }


//                    else if (ExpressionStack[index].Type == tokenType.LPAR)
//                    {
//                        string operation = "(";
//                        countLpar++;

//                        if ((priority[operation] > Prioritis.Peek()) || Operations.Count() == 0)
//                        {
//                            Operations.Push(ExpressionStack[index].DCR);
//                            Prioritis.Push(priority[operation]);
//                        }
//                        else
//                        {
//                            Operations.Push(operation);
//                            Prioritis.Push(priority[operation]);
//                        }
//                        index++;
//                    }

//                    else if (ExpressionStack[index].Type == tokenType.RPAR)
//                    {
//                        string operation = ")";
//                        countRpar++;

//                        if (((priority[operation] > Prioritis.Peek() || Operations.Count() == 0)))
//                        {
//                            Operations.Push(ExpressionStack[index].DCR);
//                            Prioritis.Push(priority[operation]);
//                        }
//                        else
//                        {
//                            HighPriority(operation);
//                            Operations.Pop();
//                            Operations.Pop();
//                            Prioritis.Pop();
//                            Prioritis.Pop();
//                        }
//                        index++;
//                    }
//                    else if (ExpressionStack[index].Type == tokenType.ID || ExpressionStack[index].Type == tokenType.LIT)
//                    {
//                        Operations.Pop();
//                        Operations.Pop();
//                        Prioritis.Pop();
//                        Prioritis.Pop();
//                    }
//                    else
//                    {
//                        throw new Exception("Неверно составлено выражение.");
//                    }
//                }
//                int countOperations = Operations.Count();
//                for (int i = 0; i < countOperations; i++)//Выталкивание всех оставшихся операций в стеке
//                {
//                    output += Operations.Pop();
//                }
//            }
//            else
//                throw new Exception("Неверно составлено выражение.");
//        }

//        public void ReversePolishNotation()
//        {
//            Dictionary<int, string> M = new Dictionary<int, string>();
//            Stack<string> stackOperand = new Stack<string>();
//            int key = 1;
//            for (int i = 0; i < output.Count(); i++)
//            {
//                string currentChar = output[i].ToString();
//                switch (currentChar)
//                {

//                    case ("or"):
//                        {
//                            M.Add(key, stackOperand.Pop() + " " + stackOperand.Pop() + " " + "or ");
//                            stackOperand.Push("M" + key.ToString());
//                            key++;
//                            break;
//                        }

//                    case ("and"):
//                        {
//                            M.Add(key, stackOperand.Pop() + " " + stackOperand.Pop() + " " + "and ");
//                            stackOperand.Push("M" + key.ToString());
//                            key++;
//                            break;
//                        }

//                    case ("<"):
//                        {
//                            M.Add(key, stackOperand.Pop() + " " + stackOperand.Pop() + " " + "< ");
//                            stackOperand.Push("M" + key.ToString());
//                            key++;
//                            break;
//                        }

//                    case (">"):
//                        {
//                            M.Add(key, stackOperand.Pop() + " " + stackOperand.Pop() + " " + "> ");
//                            stackOperand.Push("M" + key.ToString());
//                            key++;
//                            break;
//                        }
//                    case ("="):
//                        {
//                            M.Add(key, stackOperand.Pop() + " " + stackOperand.Pop() + " " + "= ");
//                            stackOperand.Push("M" + key.ToString());
//                            key++;
//                            break;
//                        }

//                    default:
//                        {
//                            if (Regex.IsMatch(currentChar.ToString(), "^[a-zA-Z]+$") || Regex.IsMatch(currentChar.ToString(), "^[0-9]+$") || output == " ) ")
//                            {
//                                string temp = null;
//                                while (output[i] != ' ')
//                                {
//                                    temp += output[i].ToString();
//                                    i++;
//                                }
//                                stackOperand.Push(temp);
//                            }
//                            else if (currentChar == " ")
//                            {
//                            }
//                            else
//                            {
//                                throw new System.Exception();
//                            }
//                            break;
//                        }
//                }
//            }
//            Form1.form1.ExprClear();
//            Form1.form1.ExprResult("Обратная польская нотация:");
//            Form1.form1.ExprResult(output);
//            Form1.form1.ExprResult("Матричный вид:");
//            //int countOutput = stackOperand.Count;
//            //for (int i = 0; i < countOutput; i++)
//            //{
//            //    Form1._form1.Conclusion(stackOperand.Pop());
//            //}
//            int countM = M.Count;
//            for (int i = 1; i < countM + 1; i++)
//            {
//                Form1.form1.ExprResult("M" + i + ":" + M[i]);
//            }
//            Form1.form1.ExprResult(" ");
//        }
//    }
//}
using AutomatTheory3._0Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class Expression
{
    List<Token> ExpressionStack = new List<Token>();
    Stack<string> Operations = new Stack<string>();
    Stack<int> Priorities = new Stack<int>();
    int index = 0;
    int countLpar = 0;
    int countRpar = 0;
    string output = "";
    Dictionary<string, int> priority = new Dictionary<string, int>()
    {
        {"(", 0},
        {")", 1},
        {"or", 2},
        {"and", 3},
        {">", 4}, {"<", 4}, {"=", 4}
    };

    public void TakeToken(Token token)
    {
        ExpressionStack.Add(token);
    }

    public void StartOPN()
    {
        Decstra();
        if (countLpar != countRpar)
            throw new Exception("Incorrect expression: unequal number of opening and closing parentheses.");
        //если стек не пуст, то ошибка
        //if (Operations.Count>0 )
        //    throw new Exception("Incrrect expression: не достаточное количество операторов или паеременных");
        ReversePolishNotation();
    }

    private void HighPriority(string operation)
    {
        while (Operations.Count > 0 && Priorities.Peek() >= priority[operation])
        {
            output += Operations.Pop() + " ";
            Priorities.Pop();
        }
        Operations.Push(operation);
        Priorities.Push(priority[operation]);
    }

    private void Decstra()
    {
        bool expectOperand = true;
        while (index < ExpressionStack.Count)
        {
            
            Token token = ExpressionStack[index];
            switch (token.Type)
            {
                case tokenType.ID:
                case tokenType.LIT:
                    if (!expectOperand)
                    {
                        throw new Exception("Incorrect expression: missing operator between operands.");
                    }
                    output += token.Value + " ";
                    expectOperand = false; // After an operand, we expect an operator
                    break;
                    //output += token.Value + " ";
                    //break;

                case tokenType.AND:
                case tokenType.OR:
                case tokenType.MORE:
                case tokenType.LESS:
                case tokenType.EQUAL:
                    if (expectOperand)
                    {
                        throw new Exception("Incorrect expression: unexpected operator.");
                    }
                    string operation = token.Value;
                    if (Operations.Count == 0 || priority[operation] > Priorities.Peek())
                    {
                        Operations.Push(operation);
                        Priorities.Push(priority[operation]);
                    }
                    else
                    {
                        HighPriority(operation);
                    }
                    expectOperand = true;
                    break;

                case tokenType.LPAR:
                    Operations.Push(token.Value);
                    Priorities.Push(priority[token.Value]);
                    countLpar++;
                    expectOperand = true;
                    break;

                case tokenType.RPAR:
                    if (expectOperand)
                    {
                        throw new Exception("Incorrect expression: unexpected right parenthesis.");
                    }
                    while (Operations.Count > 0 && Operations.Peek() != "(")
                    {
                        output += Operations.Pop() + " ";
                        Priorities.Pop();
                    }
                    if (Operations.Count > 0)
                    {
                        Operations.Pop(); // Pop the '('
                        Priorities.Pop();
                    }
                    countRpar++;
                    // Check if the next token is "Then"
                    if (index + 1 < ExpressionStack.Count && ExpressionStack[index + 1].Type == tokenType.THEN)
                    {
                        index++;
                        return; // Stop processing further tokens
                    }
                    break;
                default:
                    throw new Exception("Incorrect expression: invalid token.");
            }
            index++;
            
        }
        // Ensure no missing operators between parts of the expression
        if (expectOperand)
        {
            throw new Exception("Incorrect expression: missing operators between parts.");
        }

        while (Operations.Count > 0)
        {
            output += Operations.Pop() + " ";
        }

    }

    
    //public void ReversePolishNotation()
    //{
    //    Dictionary<int, string> M = new Dictionary<int, string>();
    //    Stack<string> stackOperand = new Stack<string>();
    //    int key = 1;
    //    string[] outputTokens = output.Trim().Split(' ');

    //    foreach (var token in outputTokens)
    //    {
    //        if (token == "or" || token == "and" || token == ">" || token == "<" || token == "=")
    //        {
    //            if (stackOperand.Count < 2)
    //                throw new Exception("Incorrect expression.");
    //            string right = stackOperand.Pop();
    //            string left = stackOperand.Pop();
    //            M.Add(key, left + " " + right + " " + token);
    //            stackOperand.Push("M" + key);
    //            key++;
    //        }
    //        else
    //        {
    //            stackOperand.Push(token);
    //        }
    //    }
        
        
    //    Form1.form1.ExprResult("Обратная польская нотация:\r\n ");
    //    Form1.form1.ExprResult(output.Trim()+ "\r\n ");

    //    Form1.form1.ExprResult("Матричный вид:\r\n ");
    //    foreach (var kvp in M)
    //    {
    //        Form1.form1.ExprResult($"M{kvp.Key}: {kvp.Value}\r\n ");
    //    }
    //    Form1.form1.ExprResult("\r\n ");
    //    //вывод матрицы сначала оператор, потом уже переменные
    //}
    public void ReversePolishNotation()
    {
        Dictionary<int, string> M = new Dictionary<int, string>();
        Stack<string> stackOperand = new Stack<string>();
        int key = 1;
        string[] outputTokens = output.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var token in outputTokens)
        {
            if (token == "or" || token == "and" || token == ">" || token == "<" || token == "=")
            {
                if (stackOperand.Count < 2)
                    throw new Exception("Incorrect expression.");
                string right = stackOperand.Pop();
                string left = stackOperand.Pop();
                M.Add(key, $"{token} {left} {right}");
                stackOperand.Push($"M{key}");
                key++;
            }
            else
            {
                stackOperand.Push(token);
            }
        }

        Form1.form1.ExprResult("Reverse Polish Notation: \r\n");
        Form1.form1.ExprResult(output.Trim() + "\r\n");

        Form1.form1.ExprResult("Matrix View: \r\n");
        foreach (var kvp in M)
        {
            Form1.form1.ExprResult($"M{kvp.Key}: {kvp.Value} \r\n");
        }
        Form1.form1.ExprResult("\r\n ");
    }

    //public void ReversePolishNotation()
    //{
    //    Dictionary<int, string> M = new Dictionary<int, string>();
    //    Stack<string> stackOperand = new Stack<string>();
    //    int key = 1;
    //    string[] outputTokens = output.Trim().Split(' ');

    //    foreach (var token in outputTokens)
    //    {
    //        switch (token)
    //        {
    //            case "or":
    //            case "and":
    //            case ">":
    //            case "<":
    //            case "=":
    //                if (stackOperand.Count < 2)
    //                    throw new Exception("Incorrect expression.");
    //                string right = stackOperand.Pop();
    //                string left = stackOperand.Pop();
    //                M.Add(key, left + " " + right + " " + token);
    //                stackOperand.Push("M" + key);
    //                key++;
    //                break;

    //            default:
    //                stackOperand.Push(token);
    //                break;
    //        }
    //    }
    //    Form1.form1.ExprClear();
    //    Form1.form1.ExprResult("Обратная польская нотация:");
    //    Form1.form1.ExprResult(output);

    //    Form1.form1.ExprResult("Матричный вид:");
    //    foreach (var kvp in M)
    //    {
    //        Console.WriteLine($"M{kvp.Key}: {kvp.Value}");
    //    }
    //}
}

