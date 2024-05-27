using System;
using System.Collections.Generic;

namespace AutomatTheory3._0Lexer
{
    internal class ParserLR
    {
        List<Token> tokens = new List<Token>();
        Stack<Token> lexemStack = new Stack<Token>();
        Stack<int> stateStack = new Stack<int>();
        private static List<KeyValuePair<string, int>> inputOPN = new List<KeyValuePair<string, int>>();
        int nextLex = 0;
        int state = 0;
        bool isEnd = false;

        public ParserLR(List<Token> inputTokwn)
        {
            tokens = inputTokwn;
        }

        private Token GetLexeme(int nextLexeme)
        {
            return tokens[nextLexeme];
        }

        private void Shift()
        {
            lexemStack.Push(GetLexeme(nextLex));
            nextLex++;
        }

        private void GoToState(int state)
        {
            stateStack.Push(state);
            this.state = state;
        }

        private void Reduce(int number, string noterm)
        {
            for (int i = 0; i < number; i++)
            {
                lexemStack.Pop();
                stateStack.Pop();
            }
            state = stateStack.Peek();
            Token k = new Token(tokenType.NOTERM);
            k.Value = noterm;
            lexemStack.Push(k);
        }

        void State0()
        {
            if (lexemStack.Count == 0)
                Shift();
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    switch (lexemStack.Peek().Value)
                    {
                        case "<прог>":
                            if (nextLex == tokens.Count)
                                isEnd = true;
                            break;
                    }
                    break;
                case tokenType.VAR:
                    GoToState(1);
                    break;
                default:
                    throw new Exception($"Ожидалось var, но было получено {lexemStack.Peek()}. State: 0");
            }
        }
        void State1()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    switch (lexemStack.Peek().Value)
                    {
                        case "<список>":
                            GoToState(2);
                            break;
                        case "<опис>":
                            GoToState(3);
                            break;
                        case "<список перем>":
                            GoToState(4);
                            break;
                    }
                    break;
                case tokenType.VAR:
                    Shift();
                    break;
                case tokenType.ID:
                    GoToState(5);
                    break;
                default:
                    throw new Exception($"Ожидалось id, но было получено {lexemStack.Peek()}. State: 1");
            }
        }
        void State2()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    switch (lexemStack.Peek().Value)
                    {
                        case "<список>":
                            Shift();
                            break;
                        case "<тело>":
                            GoToState(6);
                            break;
                        case "<опис>":
                            GoToState(8);
                            break;
                        case "<список перем>":
                            GoToState(4);
                            break;
                    }
                    break;
                case tokenType.ID:
                    GoToState(5);
                    break;
                case tokenType.BEGIN:
                    GoToState(7);
                    break;
                default:
                    throw new Exception($"Ожидалось id или begin  но было получено {lexemStack.Peek()}. State: 2");
            }
        }
        void State3()
        {
            if (lexemStack.Peek().Type == tokenType.NOTERM && lexemStack.Peek().Value == "<опис>")
                Reduce(1, "<список>");
            else
                throw new Exception($"Ожидалось правило <опис>, но было получено {lexemStack.Peek()}. State: 3");
        }
        void State4()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    switch (lexemStack.Peek().Value)
                    {
                        case "<список перем>":
                            Shift();
                            break;
                    }
                    break;
                case tokenType.COLON:
                    GoToState(9);
                    break;
                case tokenType.COMMA:
                    GoToState(10);
                    break;
                default:
                    throw new Exception($"Ожидалось : или ,  но было получено {lexemStack.Peek()}. State: 4");
            }
        }
        void State5()
        {
            if (lexemStack.Peek().Type == tokenType.ID)
                Reduce(1, "<список перем>");
            else
                throw new Exception($"Ожидалось id, но было получено {lexemStack.Peek()}. State: 5");
        }
        void State6()
        {
            if (lexemStack.Peek().Type == tokenType.NOTERM && lexemStack.Peek().Value == "<тело>")
                Reduce(3, "<прог>");
            else
                throw new Exception($"Ожидалось правило <тело>, но было получено {lexemStack.Peek()}. State: 6");
        }
        void State7()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    switch (lexemStack.Peek().Value)
                    {
                        case "<список опер>":
                            GoToState(11);
                            break;
                        case "<опер>":
                            GoToState(12);
                            break;
                        case "<усл>":
                            GoToState(14);
                            break;
                    }
                    break;
                case tokenType.ID:
                    GoToState(13);
                    break;
                case tokenType.BEGIN:
                    Shift();
                    break;
                case tokenType.IF:
                    GoToState(15);
                    break;
                default:
                    throw new Exception($"Ожидалось id или if но было получено {lexemStack.Peek()}. State: 7");
            }
        }
        void State8()
        {
            if (lexemStack.Peek().Type == tokenType.NOTERM && lexemStack.Peek().Value == "<опис>")
                Reduce(2, "<список>");
            else
                throw new Exception($"Ожидалось правило <опис>, но было получено {lexemStack.Peek()}. State: 8");
        }
        void State9()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    switch (lexemStack.Peek().Value)
                    {
                        case "<тип>":
                            GoToState(16);
                            break;
                    }
                    break;
                case tokenType.INTEGER:
                    GoToState(17);
                    break;
                case tokenType.REAL:
                    GoToState(18);
                    break;
                case tokenType.LONGINT:
                    GoToState(19);
                    break;
                case tokenType.COLON:
                    Shift();
                    break;
                default:
                    throw new Exception($"Ожидалось integer, real или longint  но было получено {lexemStack.Peek()}. State: 9");
            }
        }
        void State10()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.ID:
                    GoToState(20);
                    break;
                case tokenType.COMMA:
                    Shift();
                    break;
                default:
                    throw new Exception($"Ожидалось id  но было получено {lexemStack.Peek()}. State: 10");
            }
        }
        void State11()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    switch (lexemStack.Peek().Value)
                    {
                        case "<список опер>":
                            Shift();
                            break;
                        case "<опер>":
                            GoToState(22);
                            break;
                        case "<усл>":
                            GoToState(14);
                            break;
                    }
                    break;
                case tokenType.END:
                    GoToState(21);
                    break;
                case tokenType.ID:
                    GoToState(13);
                    break;
                case tokenType.IF:
                    GoToState(15);
                    break;
                default:
                    throw new Exception($"Ожидалось id, if или end  но было получено {lexemStack.Peek()}. State: 11");
            }
        }
        void State12()
        {
            if (lexemStack.Peek().Type == tokenType.NOTERM && lexemStack.Peek().Value == "<опер>")
                Reduce(1, "<список опер>");
            else
                throw new Exception($"Ожидалось правило <опер>, но было получено {lexemStack.Peek()}. State: 12");
        }
        void State13()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.COLON:
                    GoToState(23);
                    break;
                case tokenType.ID:
                    Shift();
                    break;
                default:
                    throw new Exception($"Ожидалось :  но было получено {lexemStack.Peek()}. State: 13");
            }
        }
        void State14()
        {
            if (lexemStack.Peek().Type == tokenType.NOTERM && lexemStack.Peek().Value == "<усл>")
                Reduce(1, "<опер>");
            else
                throw new Exception($"Ожидалось правило <усл>, но было получено {lexemStack.Peek()}. State: 14");
        }
        void State15()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.LPAR:
                    GoToState(24);
                    break;
                case tokenType.IF:
                    Shift();
                    break;
                default:
                    throw new Exception($"Ожидалось (  но было получено {lexemStack.Peek()}. State: 15");
            }
        }
        void State16()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    switch (lexemStack.Peek().Value)
                    {
                        case "<тип>":
                            Shift();
                            break;
                    }
                    break;
                case tokenType.SEMICOLON:
                    GoToState(25);
                    break;
                default:
                    throw new Exception($"Ожидалось ;  но было получено {lexemStack.Peek()}. State: 15");
            }
        }
        void State17()
        {
            if (lexemStack.Peek().Type == tokenType.INTEGER)
                Reduce(1, "<тип>");
            else
                throw new Exception($"Ожидалось integer, но было получено {lexemStack.Peek()}. State: 17");
        }
        void State18()
        {
            if (lexemStack.Peek().Type == tokenType.REAL)
                Reduce(1, "<тип>");
            else
                throw new Exception($"Ожидалось real, но было получено {lexemStack.Peek()}. State: 18");
        }
        void State19()
        {
            if (lexemStack.Peek().Type == tokenType.LONGINT)
                Reduce(1, "<тип>");
            else
                throw new Exception($"Ожидалось longint, но было получено {lexemStack.Peek()}. State: 19");
        }
        void State20()
        {
            if (lexemStack.Peek().Type == tokenType.ID)
                Reduce(3, "<список перем>");
            else
                throw new Exception($"Ожидалось id, но было получено {lexemStack.Peek()}. State: 20");
        }
        void State21()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.DOT:
                    GoToState(26);
                    break;
                case tokenType.END:
                    Shift();
                    break;
                default:
                    throw new Exception($"Ожидалось .  но было получено {lexemStack.Peek()}. State: 21");
            }
        }
        void State22()
        {
            if (lexemStack.Peek().Type == tokenType.NOTERM && lexemStack.Peek().Value == "<опер>")
                Reduce(2, "<список опер>");
            else
                throw new Exception($"Ожидалось правило <опер>, но было получено {lexemStack.Peek()}. State: 22");
        }
        void State23()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.EQUAL:
                    GoToState(27);
                    break;
                case tokenType.COLON:
                    Shift();
                    break;
                default:
                    throw new Exception($"Ожидалось =  но было получено {lexemStack.Peek()}. State: 23");
            }
        }
        void State24()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.EXPR:
                    GoToState(28);
                    break;
                case tokenType.LPAR:
                    Shift();
                    Expr();
                    break;
                default:
                    throw new Exception($"Ожидалось EXPR  но было получено {lexemStack.Peek()}. State: 24");
            }
        }
        void State25()
        {
            if (lexemStack.Peek().Type == tokenType.SEMICOLON)
                Reduce(4, "<опис>");
            else
                throw new Exception($"Ожидалось ; , но было получено {lexemStack.Peek()}. State: 25");
        }
        void State26()
        {
            if (lexemStack.Peek().Type == tokenType.DOT)
                Reduce(4, "<тело>");
            else
                throw new Exception($"Ожидалось . , но было получено {lexemStack.Peek()}. State: 26");
        }
        void State27()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    switch (lexemStack.Peek().Value)
                    {
                        case "<операнд>":
                            GoToState(29);
                            break;
                    }
                    break;
                case tokenType.LIT:
                    GoToState(31);
                    break;
                case tokenType.ID:
                    GoToState(30);
                    break;
                case tokenType.EQUAL:
                    Shift();
                    break;
                default:
                    throw new Exception($"Ожидалось id или lit  но было получено {lexemStack.Peek()}. State: 27");
            }
        }
        void State28()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.RPAR:
                    GoToState(32);
                    break;
                case tokenType.EXPR:
                    Shift();
                    break;
                default:
                    throw new Exception($"Ожидалось )  но было получено {lexemStack.Peek()}. State: 28");
            }
        }
        void State29()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    switch (lexemStack.Peek().Value)
                    {
                        case "<операнд>":
                            Shift();
                            break;
                        case "<знак>":
                            GoToState(34);
                            break;
                    }
                    break;
                case tokenType.SEMICOLON:
                    GoToState(33);
                    break;
                case tokenType.PLUS:
                    GoToState(35);
                    break;
                case tokenType.MINUS:
                    GoToState(37);
                    break;
                case tokenType.MULTIPLY:
                    GoToState(36);
                    break;
                case tokenType.SPLIT:
                    GoToState(38);
                    break;
                default:
                    throw new Exception($"Ожидалось ; , +, -, * или /  но было получено {lexemStack.Peek()}. State: 29");
            }
        }
        void State30()
        {
            if (lexemStack.Peek().Type == tokenType.ID)
                Reduce(1, "<операнд>");
            else
                throw new Exception($"Ожидалось id , но было получено {lexemStack.Peek()}. State: 30");
        }
        void State31()
        {
            if (lexemStack.Peek().Type == tokenType.LIT)
                Reduce(1, "<операнд>");
            else
                throw new Exception($"Ожидалось lit , но было получено {lexemStack.Peek()}. State: 31");
        }
        void State32()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.THEN:
                    GoToState(39);
                    break;
                case tokenType.RPAR:
                    Shift();
                    break;
                default:
                    throw new Exception($"Ожидалось then  но было получено {lexemStack.Peek()}. State: 32");
            }
        }

        void State33()
        {
            if (lexemStack.Peek().Type == tokenType.SEMICOLON)
                Reduce(5, "<опер>");
            else
                throw new Exception($"Ожидалось ; , но было получено {lexemStack.Peek()}. State: 33");
        }
        void State34()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    switch (lexemStack.Peek().Value)
                    {
                        case "<знак>":
                            Shift();
                            break;
                        case "<операнд>":
                            GoToState(40);
                            break;
                    }
                    break;
                case tokenType.ID:
                    GoToState(30);
                    break;
                case tokenType.LIT:
                    GoToState(31);
                    break;
                default:
                    throw new Exception($"Ожидалось id или lit но было получено {lexemStack.Peek()}. State: 34");
            }
        }
        void State35()
        {
            if (lexemStack.Peek().Type == tokenType.PLUS)
                Reduce(1, "<знак>");
            else
                throw new Exception($"Ожидалось + , но было получено {lexemStack.Peek()}. State: 35");
        }
        void State36()
        {
            if (lexemStack.Peek().Type == tokenType.MULTIPLY)
                Reduce(1, "<знак>");
            else
                throw new Exception($"Ожидалось * , но было получено {lexemStack.Peek()}. State: 36");
        }
        void State37()
        {
            if (lexemStack.Peek().Type == tokenType.MINUS)
                Reduce(1, "<знак>");
            else
                throw new Exception($"Ожидалось - , но было получено {lexemStack.Peek()}. State: 37");
        }
        void State38()
        {
            if (lexemStack.Peek().Type == tokenType.SPLIT)
                Reduce(1, "<знак>");
            else
                throw new Exception($"Ожидалось / , но было получено {lexemStack.Peek()}. State: 38");
        }
        void State39()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    switch (lexemStack.Peek().Value)
                    {
                        case "<блок опер>":
                            GoToState(41);
                            break;
                        case "<опер>":
                            GoToState(43);
                            break;
                        case "<усл>":
                            GoToState(14);
                            break;
                    }
                    break;
                case tokenType.THEN:
                    Shift();
                    break;
                case tokenType.BEGIN:
                    GoToState(42);
                    break;
                case tokenType.ID:
                    GoToState(13);
                    break;
                case tokenType.IF:
                    GoToState(15);
                    break;
                default:
                    throw new Exception($"Ожидалось id, if или begin но было получено {lexemStack.Peek()}. State: 39");
            }
        }
        void State40()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    switch (lexemStack.Peek().Value)
                    {
                        case "<операнд>":
                            Shift();
                            break;
                    }
                    break;
                case tokenType.SEMICOLON:
                    GoToState(44);
                    break; ;
                default:
                    throw new Exception($"Ожидалось ; но было получено {lexemStack.Peek()}. State: 40");
            }
        }
        void State41()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    if (GetLexeme(nextLex).Type == tokenType.ELSE)
                    {
                        Shift();
                        GoToState(45);
                        break;
                    }
                    else
                    {
                        switch (lexemStack.Peek().Value)
                        {
                            case "<блок опер>":
                                Reduce(6, "<усл>");
                                break;
                        }
                    }
                    break;
                case tokenType.ELSE:
                    GoToState(45);
                    break;
                default:
                    throw new Exception($"Ожидался else, но было получено {lexemStack.Peek()}. State: 41");
            }
        }
        void State42()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    switch (lexemStack.Peek().Value)
                    {
                        case "<список опер>":
                            GoToState(46);
                            break;
                        case "<опер>":
                            GoToState(12);
                            break;
                        case "<усл>":
                            GoToState(14);
                            break;
                    }
                    break;
                case tokenType.BEGIN:
                    Shift();
                    break;
                case tokenType.ID:
                    GoToState(13);
                    break;
                case tokenType.IF:
                    GoToState(15);
                    break;
                default:
                    throw new Exception($"Ожидалось id но было получено {lexemStack.Peek()}. State: 42");
            }
        }
        void State43()
        {
            if (lexemStack.Peek().Type == tokenType.NOTERM && lexemStack.Peek().Value == "<опер>")
                Reduce(1, "<блок опер>");
            else
                throw new Exception($"Ожидалось правило <опер>, но было получено {lexemStack.Peek()}. State: 43");
        }
        void State44()
        {
            if (lexemStack.Peek().Type == tokenType.SEMICOLON)
                Reduce(7, "<опер>");
            else
                throw new Exception($"Ожидалось ; , но было получено {lexemStack.Peek()}. State: 44");
        }





        void State45()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    switch (lexemStack.Peek().Value)
                    {
                        case "<блок опер>":
                            GoToState(47);
                            break;
                        case "<опер>":
                            GoToState(43);
                            break;
                        case "<усл>":
                            GoToState(14);
                            break;
                    }
                    break;
                case tokenType.ELSE:
                    Shift();
                    break;
                case tokenType.ID:
                    GoToState(13);
                    break;
                case tokenType.BEGIN:
                    GoToState(42);
                    break;
                case tokenType.IF:
                    GoToState(15);
                    break;
                default:
                    throw new Exception($"Ожидалось id или begin но было получено {lexemStack.Peek()}. State: 45");
            }
        }
        void State46()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.NOTERM:
                    switch (lexemStack.Peek().Value)
                    {
                        case "<список опер>":
                            Shift();
                            break;
                        case "<опер>":
                            GoToState(22);
                            break;
                        case "<усл>":
                            GoToState(14);
                            break;
                    }
                    break;
                case tokenType.END:
                    GoToState(48);
                    break;
                case tokenType.ID:
                    GoToState(13);
                    break;
                case tokenType.IF:
                    GoToState(15);
                    break;
                default:
                    throw new Exception($"Ожидалось end но было получено {lexemStack.Peek()}. State: 46");
            }
        }
        void State47()
        {
            if (lexemStack.Peek().Type == tokenType.NOTERM && lexemStack.Peek().Value == "<блок опер>")
                Reduce(8, "<усл>");
            else
                throw new Exception($"Ожидалось правило <блок опер>, но было получено {lexemStack.Peek()}. State: 47");
        }
        void State48()
        {
            switch (lexemStack.Peek().Type)
            {
                case tokenType.SEMICOLON:
                    GoToState(49);
                    break;
                case tokenType.END:
                    Shift();
                    break;
                default:
                    throw new Exception($"Ожидалось ;  но было получено {lexemStack.Peek()}. State: 48");
            }
        }
        void State49()
        {
            if (lexemStack.Peek().Type == tokenType.SEMICOLON)
                Reduce(4, "<блок опер>");
            else
                throw new Exception($"Ожидалось ; , но было получено {lexemStack.Peek()}. State: 49");
        }
        private void Expr()
        {
            Expression expr = new Expression();
            while (GetLexeme(nextLex).Type != tokenType.THEN)
            {
                expr.TakeToken(GetLexeme(nextLex-1));
                if (GetLexeme(nextLex + 1).Type != tokenType.THEN)
                    Shift();
                else
                    break;
            }
            Token k = new Token(tokenType.EXPR);
            lexemStack.Push(k);
            expr.StartOPN();
        }

        public void Start()
        {
            stateStack.Push(0);
            while (isEnd != true)
            {
                switch (state)
                {
                    case 0:
                        State0();
                        break;
                    case 1:
                        State1();
                        break;
                    case 2:
                        State2();
                        break;
                    case 3:
                        State3();
                        break;
                    case 4:
                        State4();
                        break;
                    case 5:
                        State5();
                        break;
                    case 6:
                        State6();
                        break;
                    case 7:
                        State7();
                        break;
                    case 8:
                        State8();
                        break;
                    case 9:
                        State9();
                        break;
                    case 10:
                        State10();
                        break;
                    case 11:
                        State11();
                        break;
                    case 12:
                        State12();
                        break;
                    case 13:
                        State13();
                        break;
                    case 14:
                        State14();
                        break;
                    case 15:
                        State15();
                        break;
                    case 16:
                        State16();
                        break;
                    case 17:
                        State17();
                        break;
                    case 18:
                        State18();
                        break;
                    case 19:
                        State19();
                        break;
                    case 20:
                        State20();
                        break;
                    case 21:
                        State21();
                        break;
                    case 22:
                        State22();
                        break;
                    case 23:
                        State23();
                        break;
                    case 24:
                        State24();
                        break;
                    case 25:
                        State25();
                        break;
                    case 26:
                        State26();
                        break;
                    case 27:
                        State27();
                        break;
                    case 28:
                        State28();
                        break;
                    case 29:
                        State29();
                        break;
                    case 30:
                        State30();
                        break;
                    case 31:
                        State31();
                        break;
                    case 32:
                        State32();
                        break;
                    case 33:
                        State33();
                        break;
                    case 34:
                        State34();
                        break;
                    case 35:
                        State35();
                        break;
                    case 36:
                        State36();
                        break;
                    case 37:
                        State37();
                        break;
                    case 38:
                        State38();
                        break;
                    case 39:
                        State39();
                        break;
                    case 40:
                        State40();
                        break;
                    case 41:
                        State41();
                        break;
                    case 42:
                        State42();
                        break;
                    case 43:
                        State43();
                        break;
                    case 44:
                        State44();
                        break;
                    case 45:
                        State45();
                        break;
                    case 46:
                        State46();
                        break;
                    case 47:
                        State47();
                        break;
                    case 48:
                        State48();
                        break;
                    case 49:
                        State49();
                        break;
                }
            }
        }
    }
}
