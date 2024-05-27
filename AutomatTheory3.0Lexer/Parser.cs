using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatTheory3._0Lexer
{
    internal class Parser
    {
        //private List<tokenType> type;
        private List<Token> tokens;
        private Token token;
        private tokenType lexeme;
        private int index;

        public Parser(List<Token> token)
        {
            tokens = token;
            index = 0;
            this.token = tokens[index];
            this.lexeme = this.token.Type;
        }

        private void Next()
        {
            if (index < tokens.Count - 1)
            {
                token = tokens[++index];
                lexeme = this.token.Type;
            }
        }

        //private void DescriptionList()
        //{
        //    Description();
        //}
        //private void Description()
        //{
        //    if (lexeme != tokenType.VAR)
        //    {
        //        throw new Exception($"Expected var, encountered {lexeme}");
        //    }
        //    if (lexeme != tokenType.DOT)
        //    {
        //        throw new Exception($"Expected . , encountered {lexeme}");
        //    }
        //}





        public void prog()
        {
            if (lexeme != tokenType.VAR)
                throw new Exception($"Ожидался var, а встретился {lexeme}");
            Next();
            list();
            body();
        }

        private void list()
        {
            discr();
            listLL();
        }

        private void listLL()
        {
            switch (lexeme)
            {
                case tokenType.BEGIN:
                    //пустой символ
                    break;
                case tokenType.ID:
                    list_right_ric();
                    break;
            }

        }

        private void list_right_ric()
        {
            discr();
            listLL();
        }

        private void discr()
        {
            var_list();
            if (lexeme != tokenType.COLON)
                throw new Exception($"Ожидался :, а встретился {lexeme}");
            Next();
            Type();
            if (lexeme != tokenType.SEMICOLON)
                throw new Exception($"Ожидался ;, а встретился {lexeme}");
            Next();
        }

        private void Type()
        {
            if (lexeme != tokenType.INTEGER &&
                lexeme != tokenType.REAL &&
                lexeme != tokenType.LONGINT)
                throw new Exception($"Ожидался тип переменной, а встретился {lexeme}");
            Next();
        }

        private void var_list()
        {
            if (lexeme != tokenType.ID)
                throw new Exception($"Ожидался id, а встретился {lexeme}");
            Next();
            var_listLL();
        }

        private void var_listLL()
        {
            switch (lexeme)
            {
                case tokenType.COLON:
                    //пустой символ
                    break;
                case tokenType.COMMA:
                    var_list_right_rec();
                    break;
            }
        }

        private void var_list_right_rec()
        {
            if (lexeme != tokenType.COMMA)
                throw new Exception($"Ожидался , , а встретился {lexeme}");
            Next();
            if (lexeme != tokenType.ID)
                throw new Exception($"Ожидался id, а встретился {lexeme}");
            Next();
            var_listLL();
        }

        private void body()
        {
            if (lexeme != tokenType.BEGIN)
                throw new Exception($"Ожидался begin, а встретился {lexeme}");
            Next();
            oper_list();
            if (lexeme != tokenType.END)
                throw new Exception($"Ожидался end, а встретился {lexeme}");
            Next();
            if (lexeme != tokenType.DOT)
                throw new Exception($"Ожидался . , а встретился {lexeme}");

        }

        private void oper_list()
        {
            oper();
            oper_listLL();
        }

        private void oper_listLL()
        {
            switch (lexeme)
            {
                case tokenType.END:
                    //пустой символ
                    break;
                case tokenType.ID:
                    oper_list_right_rec();
                    break;
                case tokenType.IF:
                    oper_list_right_rec();
                    break;

            }
        }

        private void oper_list_right_rec()
        {
            oper();
            oper_listLL();
        }

        private void oper()
        {
            switch (lexeme)
            {
                case tokenType.ID:
                    if (lexeme != tokenType.ID)
                        throw new Exception($"Ожидался id, а встретился {lexeme}");
                    Next();
                    if (lexeme != tokenType.COLON)
                        throw new Exception($"Ожидался colon, а встретился {lexeme}");
                    Next();
                    if (lexeme != tokenType.EQUAL)
                        throw new Exception($"Ожидался equal, а встретился {lexeme}");
                    Next();
                    operand();
                    operLL();
                    break;
                case tokenType.IF:
                    cond();
                    break;
            }

        }

        private void operLL()
        {
            switch (lexeme)
            {
                case tokenType.SEMICOLON:
                    if (lexeme != tokenType.SEMICOLON)
                        throw new Exception($"Ожидался ; , а встретился {lexeme}");
                    Next();
                    break;
                case tokenType.ID:
                    operand();
                    sign();
                    operand();
                    if (lexeme != tokenType.SEMICOLON)
                        throw new Exception($"Ожидался ; , а встретился {lexeme}");
                    Next();
                    break;
                case tokenType.LIT:
                    operand();
                    sign();
                    operand();
                    if (lexeme != tokenType.SEMICOLON)
                        throw new Exception($"Ожидался ; , а встретился {lexeme}");
                    Next();
                    break;
            }

        }

        private void operand()
        {
            if (lexeme != tokenType.ID &&
                lexeme != tokenType.LIT)
                throw new Exception($"Ожидался id или lit, а встретился {lexeme}");
            Next();
        }

        private void sign()
        {
            if (lexeme != tokenType.PLUS &&
                lexeme != tokenType.MINUS &&
                lexeme != tokenType.MULTIPLY &&
                lexeme != tokenType.SPLIT)
                throw new Exception($"Ожидался знак , а встретился {lexeme}");
            Next();
        }

        private void cond()
        {

            if (lexeme != tokenType.IF)
                throw new Exception($"Ожидался if, а встретился {lexeme}");
            Next();
            if (lexeme != tokenType.LPAR)
                throw new Exception($"Ожидался ( , а встретился {lexeme}");
            Next();
            expr();
            if (lexeme != tokenType.RPAR)
                throw new Exception($"Ожидался ) , а встретился {lexeme}");
            Next();
            if (lexeme != tokenType.THEN)
                throw new Exception($"Ожидался then, а встретился {lexeme}");
            Next();
            oper_block();
            condLL();
        }

        private void condLL()
        {
            switch (lexeme)
            {
                case tokenType.END:
                    //пустой символ
                    break;
                case tokenType.IF:
                    //пустой символ
                    break;
                case tokenType.ELSE:
                    if (lexeme != tokenType.ELSE)
                        throw new Exception($"Ожидался else, а встретился {lexeme}");
                    Next();
                    oper_block();
                    break;
            }
        }

        private void oper_block()
        {
            switch (lexeme)
            {
                case tokenType.ID:
                    oper();
                    //if (lexeme != tokenType.SEMICOLON)
                    //    throw new Exception($"Ожидался ; , а встретился {lexeme}");
                    //Next();
                    break;
                case tokenType.IF:
                    oper();
                    //if (lexeme != tokenType.SEMICOLON)
                    //    throw new Exception($"Ожидался ; , а встретился {lexeme}");
                    //Next();
                    break;
                case tokenType.BEGIN:
                    if (lexeme != tokenType.BEGIN)
                        throw new Exception($"Ожидался begin, а встретился {lexeme}");
                    Next();
                    oper_list();
                    if (lexeme != tokenType.END)
                        throw new Exception($"Ожидался end, а встретился {lexeme}");
                    Next();
                    if (lexeme != tokenType.SEMICOLON)
                        throw new Exception($"Ожидался ;, а встретился {lexeme}");
                    Next();
                    break;
            }

        }

        private void expr()
        {
            //n times Next
            Next();
            //Next();
            //Next();
            //Next();
            //Next();
            //Next();
            //Next();
            //Next();
            //Next();
            //Next();
            //Next();
        }

    }
}
