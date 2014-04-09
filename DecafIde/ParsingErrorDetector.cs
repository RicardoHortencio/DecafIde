using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DecafIde
{
    class ParsingErrorDetector : BaseErrorListener
    {
        TextBox myErrorTB;
        bool foundErrors = false;
        public ParsingErrorDetector(TextBox theTB)
        {
            myErrorTB = theTB;            
        }

        public override void SyntaxError(IRecognizer recognizer,
                IToken offendingSymbol,
                int line, int charPositionInLine,
                string msg,
                RecognitionException e)
        {
            List<String> stack = new List<string>(((Parser)recognizer).GetRuleInvocationStack());

            stack.Reverse();

            if (!foundErrors) { 
                myErrorTB.Text = "Parsing Errors:" + Environment.NewLine;
                foundErrors = true;
            }
            string theError = "";

            theError += "rule stack: " + stack.ToString() + Environment.NewLine;
            theError += "line: " + line + ":" + charPositionInLine + " at " + offendingSymbol + ": " + msg + Environment.NewLine;
            if (theError.Length > 0)
                myErrorTB.Text += Environment.NewLine + theError;
        }
    }
}
