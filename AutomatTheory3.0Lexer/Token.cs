using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatTheory3._0Lexer
{
    public class Token
    {
        public tokenType Type { get; set; }
        public string Value { get; set; }
        //public string DCR;
        public Token(tokenType type)
        {
            Type = type;
        }
        public override string ToString()
        {
            return $"Lexeme: {Value ?? ""}, Type: {Type}";
        }
    }
}
