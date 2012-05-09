using System;
using System.Collections.Generic;
using System.Dynamic;

public class DynamicMembers : DynamicObject
{
    readonly Dictionary<string, object> _members =
        new Dictionary<string, object>();
    public override bool TryGetMember(
        GetMemberBinder binder, out object result)
    {
        string name = binder.Name;
        return _members.TryGetValue(name, out result);
    }
    public override bool TrySetMember(
        SetMemberBinder binder, object value)
    {
        _members[binder.Name] = value;
        return true;
    }
    public int Count
    {
        get { return _members.Count; }
    }
    public override bool TryInvokeMember(
        InvokeMemberBinder binder, object[] args, out object result)
    {
        object member;
        if (_members.TryGetValue(binder.Name, out member))
        {
            if (member is Delegate)
            {
                var del = member as Delegate;
                result = del.DynamicInvoke(args);
                return true;
            }
        }
        throw new MissingMethodException();
    }
    public object this[string name]
    {
        get { return _members[name]; }
        set { _members[name] = value; }
    }
}