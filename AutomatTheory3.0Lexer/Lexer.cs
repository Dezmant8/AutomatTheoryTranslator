using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatTheory3._0Lexer
{
    internal class Lexer
    {
        private string code;
        private List<string> lexerResult;
        public List<Token> tokens;
        public List<tokenType> types;

        public Lexer(string _code)
        {
            code = _code.ToLower() + '\n';
            lexerResult = new List<string>();
            types = new List<tokenType>();
            tokens = new List<Token>();
        }

        static Dictionary<string, tokenType> specialWords = new Dictionary<string, tokenType>()
        {
            {"integer", tokenType.INTEGER },
            {"real", tokenType.REAL },
            {"longint", tokenType.LONGINT },
            {"var", tokenType.VAR },
            {"begin", tokenType.BEGIN },
            {"end", tokenType.END},
            {"if", tokenType.IF },
            {"else", tokenType.ELSE },
            {"then", tokenType.THEN },
            {"and", tokenType.AND },
            {"or", tokenType.OR },
            {"expr", tokenType.EXPR }

        };
        static Dictionary<char, tokenType> specialSymbols = new Dictionary<char, tokenType>()
        {
            { '+', tokenType.PLUS },
            { '-', tokenType.MINUS },
            { '*', tokenType.MULTIPLY },
            { '/', tokenType.SPLIT },
            { ',', tokenType.COMMA },
            { '=', tokenType.EQUAL },
            {')', tokenType.RPAR },
            {'(', tokenType.LPAR },
            {':', tokenType.COLON },
            {';', tokenType.SEMICOLON },
            {'.', tokenType.DOT },
            {'>', tokenType.MORE },
            {'<', tokenType.LESS },

        };

        private static bool IsSpecialSymbol(char symbol)
        {
            return specialSymbols.ContainsKey(symbol);
        }
        private static bool IsSpecialWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return false;
            }
            return specialWords.ContainsKey(word);
        }

        private void CreateTokenAndAddResult(tokenType type, string value)
        {
            Token token = new Token(type);
            //token = new Token(type);
            token.Value = value;
            token.Type = type;
            //lexerResult.Add(token.ToString());
            //types.Add(type);
            tokens.Add(token);
            //return token;
        }

        //private void CreateTokenAndAddResult(out Token token, tokenType type, string value)
        //{
        //    //Token token = new Token(type);
        //    token = new Token(type);
        //    token.Value = value;
        //    lexerResult.Add(token.ToString());
        //    types.Add(type);
        //    //return token
        //}

        private char IsDigitOrLeter(char _char)
        {
            if (char.IsDigit(_char))
            {
                return 'd';
            }
            else if (char.IsLetter(_char))
            {
                return 'l';
            }
            //else
            //{
            //    throw new Exception("встречен некорректный символ");
            //}
            return _char;
        }

        private void AddToken(string code)
        {
            Token token;
            tokenType type;

            if(code.Length == 1)
            {
                if(IsSpecialSymbol(code[0]) )
                {
                    specialSymbols.TryGetValue(code[0], out type);
                    CreateTokenAndAddResult(type, code);
                }
                else if (char.IsLetter(code[0]))
                {
                    CreateTokenAndAddResult(tokenType.ID, code);
                }
                else if (!char.IsLetter(code[0]) || (char.IsDigit(code[0])))
                {
                    throw new Exception("встречен некорректный символ");
                }
            }
            else
            {
                if (IsSpecialWord(code))
                {
                    specialWords.TryGetValue(code, out type);
                    CreateTokenAndAddResult(type, code);
                    //перегрузку чтобы либо просто type либо type и code
                    //и сделать возврат null вместо code у слов типа integer
                }
                else
                {
                    CreateTokenAndAddResult(tokenType.ID, code);
                }
            }
        }

        public List<Token> AnalyzeCode()
        {
            int i = -1;
            string buffer = string.Empty;
            char _char;

            while (++i < code.Length)
            {
                _char = IsDigitOrLeter(code[i]);

                switch (_char )
                {
                    case 'd':
                        for(; char.IsLetterOrDigit(code[i]); ++i)
                        {
                            if (char.IsLetter(code[i]))
                            {
                                throw new Exception("Incorecr variable name");
                                //try catch где вызываю analyze code
                            }
                            
                            buffer += code[i];
                        }
                        
                        Token token;
                        //token = CreateTokenAndAddResult( tokenType.LIT, buffer);
                        //tokens.Add(token);
                        CreateTokenAndAddResult(tokenType.LIT, buffer);
                        buffer = string.Empty;
                        --i;
                        continue;
                    case 'l':
                        for(; char.IsLetterOrDigit(code[i]); i++)
                        {
                            buffer += code[i];
                        }
                        AddToken(buffer);
                        buffer = string.Empty;
                        --i;
                        continue;
                    case ' ':
                        continue;
                    case '\n':
                        //AddToken("\n");
                        continue;
                    case '\r':
                        continue;
                    default:
                        AddToken(_char.ToString());
                        continue;
                }
            }
            return tokens;
        }
    }
}
