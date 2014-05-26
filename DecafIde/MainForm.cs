using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;
using System.IO;
using Irony.Parsing;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using DecafIde.Semantic_Analysis;


namespace DecafIde
{
    public partial class MainForm : Form
    {
        string theFile;
        public MainForm()
        {
            InitializeComponent();
        }

        Style BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Italic);
        public Regex keyWords = new Regex("class|Program|struct|int|char|boolean|struct|void|if|else|while|return|true|false");
        Style NumberStyle = new TextStyle(Brushes.Fuchsia, null, FontStyle.Regular);
        public Regex NumbersRegex = new Regex("0|1|2|3|4|5|6|7|8|9");
        //Style BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        //public Regex operators = new Regex("[+*/[%-.,;|&=]()");
        private void CodeTB_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(BlueStyle);
            e.ChangedRange.SetStyle(BlueStyle, keyWords);
            e.ChangedRange.ClearStyle(NumberStyle);
            e.ChangedRange.SetStyle(NumberStyle, NumbersRegex);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog.OpenFile()) != null)
                    {
                        CodeTB.Text = File.ReadAllText(openFileDialog.FileName);
                        theFile = File.ReadAllText(openFileDialog.FileName);
                    }
                    CodeTB.SelectAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AntlrInputStream inputCharStream2 = new AntlrInputStream(CodeTB.Text);
            AntlrInputStream inputCharStream = new AntlrInputStream(CodeTB.Text);

            NewDecafLexer tokenCreator2 = new NewDecafLexer(inputCharStream2);
            DecafLexer tokenCreator = new DecafLexer(inputCharStream);

            CommonTokenStream inputTokenStream2 = new CommonTokenStream(tokenCreator2);
            CommonTokenStream inputTokenStream = new CommonTokenStream(tokenCreator);


            NewDecafParser mainParser2 = new NewDecafParser(inputTokenStream2);
            DecafParser mainParser = new DecafParser(inputTokenStream);

            errorTB.Text = "";
            mainParser.RemoveErrorListeners();
            mainParser.AddErrorListener(new ParsingErrorDetector(errorTB));

            NewDecafParser.ProgramContext AbstractSyntaxTree2 = mainParser2.program();
            DecafParser.ProgramContext AbstractSyntaxTree = mainParser.program();

            
            
            //TreeConstructingVisitor visitor = new TreeConstructingVisitor(TreeVisualizer);
            //visitor.Visit(AbstractSyntaxTree);
            
            ParseTreeWalker walker2 = new ParseTreeWalker();
            ParseTreeWalker walker = new ParseTreeWalker();
            
            SymbolTableConstructor theConstructor = new SymbolTableConstructor();
            SymbolTable finalTable = theConstructor.finalTable; 

            string generated = "";
            ILGenerator theGenerator = new ILGenerator(generated);
            //try
            //{
                walker2.Walk(theGenerator, AbstractSyntaxTree2);
                walker.Walk(theConstructor, AbstractSyntaxTree);
            //}
            //catch (Exception ex)
            //{
            //    errorTB.Text += ex.Message + Environment.NewLine;
            //    if (theConstructor.beginningLine == theConstructor.endingLine)
            //        errorTB.Text += "Error found in line " + theConstructor.beginningLine;
            //    else
            //    errorTB.Text += "Error found between line " + theConstructor.beginningLine + " and line " + theConstructor.endingLine;
            //}
            if (errorTB.Text == string.Empty)
                errorTB.Text = "Congrats! No errors were found!";

            MessageBox.Show(theGenerator.finalResult);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeTB.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeTB.Paste();
        }

        private void indentLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void tryTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show( stringtemplateTryout.tryMyTemplate());
        }
    }
}
