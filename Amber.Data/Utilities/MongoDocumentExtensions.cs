using System.Collections.Generic;
using System.Linq;
using Amber.Data.Model;

namespace Amber.Data.Utilities
{

    public static class MongoDocumentExtensions
    {
        public static void WithChildren<TDocument>(this IEnumerable<TDocument> documents) where TDocument : IMongoDocument
        {
            var listElementType = documents.GetType().GetGenericArguments().First();
            var childProperties = listElementType.GetProperties()
                .Where(x => x.GetCustomAttributes(typeof(BsonReferenceAttribute), true).Length == 1);
            var sourceDestinationProperties = childProperties
                .Select(x => new
                    {
                        arrayProp = x,
                        destination = listElementType.GetProperty((x.GetCustomAttributes(typeof(BsonReferenceAttribute), true)[0] as BsonReferenceAttribute).Property)
                    });
            
        }
    }
}
