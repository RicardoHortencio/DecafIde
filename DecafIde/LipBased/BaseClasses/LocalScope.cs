
namespace DecafIde.LipBased
{
    class LocalScope : BaseScope
    {
        public LocalScope(IScope parent) : base(parent) { }

        public string getScopeName() { return "_local"; }
    }
}
