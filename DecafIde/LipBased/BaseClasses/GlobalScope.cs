
namespace DecafIde.LipBased
{
    class GlobalScope : BaseScope
    {
        public GlobalScope()
            : base(null)
        { }
        public GlobalScope(IScope enclosingScope)
            : base(enclosingScope)
        { }

        public string getScopeName()
        {
            return "Program";
        }
    }
}
