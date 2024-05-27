using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace AutomatTheory3._0Lexer
{
    public partial class Form1 : Form
    {
        public static Form1 form1;
        public Form1()
        {
            InitializeComponent();
            form1 = this;
        }

        public void ExprClear()
        {
            exprTextBox.Clear();
        }
        public void ExprResult(string expr)
        {
            exprTextBox.Text += expr;
        }

        private void ShowResult(List<Token> code)
        {
            string result = string.Empty;

            foreach(Token item in code)
            {
                result += $"{item}\r\n ";
            }
            resultTextBox.Text = result;
        }

        //private void PrintParser(List<string> result)
        //{
        //    errorTextBox.Text = string.Empty;
        //    foreach (string item in result)
        //    {
        //        errorTextBox.Text += $"{item}\r\n ";
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                codeTextBox.Text = File.ReadAllText(dialog.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            errorTextBox.Clear();
            resultTextBox.Clear();
            exprTextBox.Clear();
            string code = codeTextBox.Text;
            Lexer lexer = new Lexer(code);
            try
            {
                List<Token> result = lexer.AnalyzeCode();
                ShowResult(result);
                ParserLR parser2 = new ParserLR(result);
                parser2.Start();
                exprTextBox.Text += "разбор завершён успешно";
            }
            catch (Exception ex)
            {
                errorTextBox.Text += $"{ex.Message}\r\n ";
            }
            //ShowResult(result);
            //Parser parser = new Parser(result);
            //try
            //{
            //    parserTextBox.Text = string.Empty;
            //    parser.prog();
            //}
            //catch (Exception ex)
            //{
            //    parserTextBox.Text += $"{ex}\r\n ";
            //}
            
            //ParserLR parser2 = new ParserLR(result);
            //parser2.Start();
            //try
            //{
            //    parser2.Start();
            //}
            //catch (Exception ex)
            //{
            //    errorTextBox.Text += $"{ex.Message}\r\n ";
            //}
            //PrintParser(parser.result);
        }
    }
}
