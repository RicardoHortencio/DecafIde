﻿using System;

namespace DecafIde.LipBased
{
    public class Symbol
    {
        String name;
        public IType type;
        IScope scope;

        internal IScope Scope
        {
            get { return scope; }
            set { scope = value; }
        }
        public Symbol(String name)
        {
            this.name = name;
        }

        public Symbol(String name, IType type)
            : this(name)
        {
            this.type = type;
        }

        public string getName()
        {
            return name;
        }

        public override string ToString()
        {
            string s = "";
            if (scope != null) s = scope.getScopeName() + ".";
            if (type != null) return '<' + s + getName() + ":" + type + '>';
            return s + getName();
        }

        public static String stripBrackets(String s)
        {
            return s.Substring(1, s.Length - 1);
        }
    }
}
