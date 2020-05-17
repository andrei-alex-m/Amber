using System;
namespace Amber.Data.Utilities
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class BsonReferenceAttribute: Attribute
    {
        public string Property { get; }
        public BsonReferenceAttribute(string property)
        {
            Property = property;
        }
    }
}
