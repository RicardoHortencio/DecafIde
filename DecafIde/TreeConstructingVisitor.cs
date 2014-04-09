using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DecafIde
{
    class TreeConstructingVisitor : DecafBaseVisitor<TreeNode>
    {
        TreeView myTreeView;
        public TreeConstructingVisitor(TreeView theTreeView)
        {
            myTreeView = theTreeView;
        }

        public override TreeNode VisitProgram([NotNull] DecafParser.ProgramContext context)
        {
            TreeNode mainNode = new TreeNode("ProgramVisitor VisitProgram");
            
            foreach (var child in context.children)
            {
                TerminalNodeImpl childTerminal = child as TerminalNodeImpl;
                if (childTerminal != null)
                    write(childTerminal, mainNode);
                else Visit(child, mainNode);
            }


            myTreeView.Nodes.Add(mainNode);

            return mainNode;
        }

        private TreeNode write(TerminalNodeImpl childTerminal, TreeNode mainNode)
        {
            TreeNode tempNode = new TreeNode(childTerminal.symbol.Text);
            mainNode.Nodes.Add(tempNode);
            return tempNode;
        }

        private void Visit(IParseTree node, TreeNode mainNode)
        {
            for (int i = 0; i < node.ChildCount; i++)
            {
                Visit(node.GetChild(i), mainNode);
            }
            TerminalNodeImpl childTerminal = node as TerminalNodeImpl;
            Boolean setFirst = false;
            TreeNode tempNode = new TreeNode();
            if (childTerminal != null)
            {
                if (!setFirst) { 
                tempNode = new TreeNode(childTerminal.symbol.Text);
                setFirst = true;
                }
                write(childTerminal, mainNode);
            }

            myTreeView.Nodes.Add(tempNode);
        }
    }
}
