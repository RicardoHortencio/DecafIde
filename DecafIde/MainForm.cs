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


namespace DecafIde
{
    public partial class MainForm : Form
    {
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
            e.ChangedRange.SetFoldingMarkers("{", "}");
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
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AntlrInputStream inputCharStream = new AntlrInputStream(CodeTB.Text);
            DecafLexer tokenCreator = new DecafLexer(inputCharStream);
            CommonTokenStream inputTokenStream = new CommonTokenStream(tokenCreator);
            DecafParser mainParser = new DecafParser(inputTokenStream);

            errorTB.Text = "";
            mainParser.RemoveErrorListeners();
            mainParser.AddErrorListener(new ParsingErrorDetector(errorTB));

            DecafParser.ProgramContext AbstractSyntaxTree = mainParser.program();

            TreeConstructingVisitor visitor = new TreeConstructingVisitor(TreeVisualizer);
            visitor.Visit(AbstractSyntaxTree);

            ParseTreeWalker walker = new ParseTreeWalker();
            SymbolTableConstructor theConstructor = new SymbolTableConstructor();
            try
            {
                walker.Walk(theConstructor, AbstractSyntaxTree);
            }
            catch (Exception ex)
            {
                errorTB.Text += ex.Message + Environment.NewLine;
                errorTB.Text += "Error found between line " + theConstructor.beginningLine + " and line " + theConstructor.endingLine;
            }
            if (errorTB.Text == string.Empty)
                errorTB.Text = "Congrats! No errors were found!";
        }
    }
}
