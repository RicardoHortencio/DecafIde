
namespace DecafIde.LipBased
{
    public enum SymbolType
    {
        Tuser,
        Tboolean,
        Tchar,
        Tint,
        Tvoid,
        Tinvalid
    }
    class SymbolTable
    {
        public static BuiltInTypeSymbol _boolean = new BuiltInTypeSymbol("boolean", SymbolType.Tboolean);
        public static BuiltInTypeSymbol _char = new BuiltInTypeSymbol("char", SymbolType.Tchar);
        public static BuiltInTypeSymbol _int = new BuiltInTypeSymbol("int", SymbolType.Tint);
        public static BuiltInTypeSymbol _void = new BuiltInTypeSymbol("void", SymbolType.Tvoid);

        GlobalScope globals = new GlobalScope();

        protected void initTypeSystem()
        {
            globals.define(_boolean);
            globals.define(_char);
            globals.define(_int);
            globals.define(_void);
        }

        public SymbolTable()
        {
            initTypeSystem();
        }

        public override string ToString()
        {
            return globals.ToString();
        }
    }
}
