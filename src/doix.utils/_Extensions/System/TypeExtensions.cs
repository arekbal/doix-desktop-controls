using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
  public static class TypeExtensions
  {  
    public static IEnumerable<Type> GetBaseTypes(this Type that)
    {
      var t = that.BaseType;
      while (t != null)
      {
        yield return t;
        t = t.BaseType;
      }
    }

    public static IEnumerable<Type> GetSelfAndBaseTypes(this Type that)
    {
      yield return that;
      foreach (var baseType in GetBaseTypes(that))
        yield return baseType;
    }
  }
}
