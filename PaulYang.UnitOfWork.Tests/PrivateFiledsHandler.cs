using System;
using System.Reflection;

namespace PaulYang.UnitOfWork.Tests
{
    public static class PrivateFiledsHandler
    {
        public static T GetPrivateFiled<T>(this object instance, string filedName)
            where T:class 
        {
            BindingFlags flag = BindingFlags.Instance |BindingFlags.NonPublic;
            Type t = instance.GetType();
            FieldInfo filedInfo = t.GetField(filedName, flag);
            if(filedInfo == null) throw new NullReferenceException("the filed ["+filedName+"] is null!");
            return (T) filedInfo.GetValue(instance);
        }
    }
}