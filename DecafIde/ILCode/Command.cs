using Antlr4.StringTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public void setPlaceholder(string fieldName, string fieldValue)
        {
            selfTemplate.Add(fieldName, fieldValue);
        }
        public void setPlaceholder(string fieldName, bool fieldValue)
        {
            selfTemplate.Add(fieldName, fieldValue?"true":"false");
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
