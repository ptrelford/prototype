using System;

public class Prototype : DynamicMembers
{
    public static Prototype Define(Action<dynamic> cons)
    {
        return new Prototype(cons);
    }
    public static Prototype<T> Define<T>(Action<dynamic,T> cons)
    {
        return new Prototype<T>(cons);
    }
    public static Prototype<T1,T2> Define<T1, T2>(Action<dynamic, T1, T2> cons)
    {
        return new Prototype<T1, T2>(cons);
    }
    readonly Action<dynamic> _cons;
    protected Prototype()
    {
        _prototype = new Instance();
    }
    private Prototype(Action<dynamic> cons) 
        : this()
    {
        _cons = cons;
    }
    public dynamic New()
    {
        var instance = CreateInstance();
        _cons(instance);
        return instance;
    }
    readonly Instance _prototype;
    internal Instance CreateInstance()
    {
        return new Instance(_prototype);
    }
    public dynamic prototype
    {
        get { return _prototype; }
    }
    internal class Instance : DynamicMembers
    {
        internal Instance() : base() { }
        internal Instance(Instance parent) : base(parent) { }
    }
}

public class Prototype<T> : Prototype
{
    readonly Action<dynamic, T> _cons;
    internal Prototype(Action<dynamic, T> cons)
    {
        _cons = cons;
    }
    public dynamic New(T value)
    {
        var instance = CreateInstance();
        _cons(instance, value);
        return instance;
    }
}

public class Prototype<T1,T2> : Prototype
{
    readonly Action<dynamic,T1,T2> _cons;
    internal Prototype(Action<dynamic,T1,T2> cons)
    {
        _cons = cons;
    }
    public dynamic New(T1 value1, T2 value2)
    {
        var instance = CreateInstance();
        _cons(instance, value1, value2);
        return instance;
    }
}
