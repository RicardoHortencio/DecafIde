using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.StringTemplate;

namespace DecafIde
{
    static class stringtemplateTryout
    {
        public static string tryMyTemplate()
        {
            TemplateGroup stg = new TemplateGroupFile(@"C:\Users\Paulo\SkyDrive\Visual Studio 2013\Projects\DecafIde\DecafIde\CIL.stg");

            Template fileTemplate = stg.GetInstanceOf("file");
            Template structTemplate = stg.GetInstanceOf("struct");

            structTemplate.Add("name", "name1");
            structTemplate.Add("fields", "field1");
            structTemplate.Add("fields", "field2");


            fileTemplate.Add("defs", structTemplate.Render());

            structTemplate = stg.GetInstanceOf("struct");
            structTemplate.Add("name", "name2");
            structTemplate.Add("fields", "field21");
            structTemplate.Add("fields", "field22");

            fileTemplate.Add("defs", structTemplate.Render());

            return fileTemplate.Render();
        }
    }
}
