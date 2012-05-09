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
    protected Prototype(){ }
    private Prototype(Action<dynamic> cons) 
        : this()
    {
        _cons = cons;
    }
    public dynamic New()
    {
        var instance = new Instance();
        _cons(instance);
        return instance;
    }
    internal class Instance : DynamicMembers
    {
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
        var instance = new Prototype.Instance();
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
        var instance = new Prototype.Instance();
        _cons(instance, value1, value2);
        return instance;
    }
}
