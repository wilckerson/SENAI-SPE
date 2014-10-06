using System;

namespace Common.Security
{
    /// <summary>
    /// Allow Anounymous to access any action.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AllowAnonymousExAttribute : Attribute
    {
    }
}