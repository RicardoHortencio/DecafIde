using Antlr4.StringTemplate;
using DecafIde.ILCode.ClassMembers.MethodMembers;
using DecafIde.ILCode.ClassMembers.MethodMembers.Statements;
using System.Collections.Generic;

namespace DecafIde.ILCode
{
    abstract class Command
    {
        private TemplateGroup stg = new TemplateGroupFile(@"C:\Users\Paulo\SkyDrive\Visual Studio 2013\Projects\DecafIde\DecafIde\CIL.stg");
        protected Template selfTemplate;

        public abstract string getTemplateName();
        public abstract void setGenCode();

        protected Template getSelfTemplate()
        {
            return stg.GetInstanceOf(getTemplateName());
        }
        public string getInheritedTemplateName()
        {
            string templateName = getTemplateName();
            return char.ToLower(templateName[0]) + templateName.Substring(1);
        }
        public string getGenCode()
        {
            setGenCode();
            return selfTemplate.Render();
        }

        public void setCommandList(IEnumerable<Command> theCommandList)
        {
            foreach (Command item in theCommandList)
                setSingleCommand(item);
        }
        public void setSingleCommand(Command theCommand)
        {
            setPlaceholder(theCommand.getInheritedTemplateName(), theCommand.getGenCode());
        }

        public void setCommandList(IEnumerable<LocalVar> theCommandList)
        {
            int i = 0;
            foreach (LocalVar item in theCommandList)
            {
                i++;
                setSingleCommand(item, i);
            }
        }

        public void setSingleCommand(LocalVar theCommand, int index)
        {
            setPlaceholder(theCommand.getInheritedTemplateName(), "[" + index + "] " + theCommand.getGenCode());
        }

        public void setCommandList(string fieldName, IEnumerable<ExpressionBaseClass> theCommandList)
        {
            foreach (ExpressionBaseClass item in theCommandList)
            {
                setSingleCommand(item);
            }
        }

        public void setSingleCommand(string fieldName, ExpressionBaseClass theCommand)
        {
            setPlaceholder(theCommand.getInheritedTemplateName(), theCommand.PushCode);
        }

        public void setPlaceholder(string fieldName, string fieldValue)
        {
            selfTemplate.Add(fieldName, fieldValue);
        }
        public void setPlaceholder(string fieldName, bool fieldValue)
        {
            selfTemplate.Add(fieldName, fieldValue ? "true" : "false");
        }

        public Command()
        {
            selfTemplate = getSelfTemplate();
        }
        public Command(string stgRoute)
            : this()
        {
            TemplateGroup stg = new TemplateGroupFile(stgRoute);
        }
    }
}
