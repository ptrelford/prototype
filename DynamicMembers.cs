using System;
using System.Collections.Generic;
using System.Dynamic;

public class DynamicMembers : DynamicObject
{
    readonly Dictionary<string, object> _members =
        new Dictionary<string, object>();
    DynamicMembers _prototype;
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
        if (_prototype != null)
            return _prototype.TryGetMember(binder, out result); 
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
        if (_members.TryGetValue(binder.Name, out member))
        {
            return InvokeDelegate(member, args, out result);
        }
        if (_prototype != null)
        {
            return _prototype.TryInvokeMember(binder, args, out result);
        }
        result = null;
        return false;
    }
    private static bool InvokeDelegate(object member, object[] args, out object result)
    {
        if (member is Delegate)
        {
            var del = member as Delegate;
            result = del.DynamicInvoke(args);
            return true;
        }
        else
        {
            result = null;
            return false;
        }
    }
}