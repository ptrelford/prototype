using System;

public class Prototype : DynamicMembers
{
    public static Prototype Constructor(Action<dynamic> cons)
    {
        return new Prototype(cons);
    }
    public static Prototype<T> Constructor<T>(Action<dynamic,T> cons)
    {
        return new Prototype<T>(cons);
    }
    public static Prototype<T1,T2> Constructor<T1, T2>(Action<dynamic, T1, T2> cons)
    {
        return new Prototype<T1, T2>(cons);
    }
    readonly Action<dynamic> _cons = _ => { };
    protected Prototype()
    {
        this.Prototype = new DynamicMembers();
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
    internal DynamicMembers CreateInstance()
    {
        return new DynamicMembers(Prototype);
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
