using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

public class DynamicMembers : DynamicObject
{
    readonly Dictionary<string, object> _members =
        new Dictionary<string, object>();
    DynamicMembers _prototype;
    public dynamic Prototype
    {
        get { return _prototype; }
        set { _prototype = value; }
    }
    public DynamicMembers(DynamicMembers prototype)
    {
        _prototype = prototype;
    }
    public DynamicMembers() : this(null) { }
    public dynamic __proto__
    {
        get { return _prototype; }
        set { _prototype = value; }
    }
    public object this[string name]
    {
        get { return _members[name]; }
        set { _members[name] = value; }
    }
    public int Count
    {
        get { return _members.Count; }
    }
    public override bool TryGetMember(
        GetMemberBinder binder, out object result)
    {
        string name = binder.Name;
        if( _members.TryGetValue(name, out result) )       
            return true;
        var prototype = _prototype;
        while (_prototype != null)
        {
            if (_prototype.TryGetMember(binder, out result))
                return true;
            prototype = prototype.Prototype;
        }
        return false;
    }
    public override bool TrySetMember(
        SetMemberBinder binder, object value)
    {
        _members[binder.Name] = value;
        return true;
    }
    public override bool TryInvokeMember(
        InvokeMemberBinder binder, object[] args, out object result)
    {
        object member;
        if (this._members.TryGetValue(binder.Name, out member))
        {
            return InvokeDelegate(this, member, args, out result);
        }
        DynamicMembers prototype = Prototype;
        while( prototype !=null )
        {
            if (prototype._members.TryGetValue(binder.Name, out member))
            {
                return InvokeDelegate(this, member, args, out result);
            }
            prototype = prototype.Prototype;
        }
        result = null;
        return false;
    }
    private static bool InvokeDelegate(dynamic instance, object member, object[] args, out object result)
    {
        if (member is Delegate)
        {
            var del = member as Delegate;
            var ps = del.Method.GetParameters();
            if (ps.Length == args.Length)
            {
                result = del.DynamicInvoke(args);
                return true;
            }
            if (ps.Length == args.Length + 1)
            {    
                var args2 = (new[] {instance}).Concat(args).ToArray();
                result = del.DynamicInvoke(args2);
                return true;
            }
        }      
        result = null;
        return false;
    }
}