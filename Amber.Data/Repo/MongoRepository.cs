using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Amber.Data.Model;
using Amber.Data.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Amber.Data.Repo
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument>
        where TDocument : IMongoDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(IAmberDatabaseSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public virtual IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public virtual IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual TDocument FindById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            return _collection.Find(filter).SingleOrDefault();
        }

        public virtual Task<TDocument> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public TDocument FindByName(string name)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Name, name);
            return _collection.Find(filter).SingleOrDefault();
        }

        public Task<TDocument> FindByNameAsync(string name)
        {
            return Task.Run(() =>
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Name, name);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public virtual IEnumerable<TDocument> FindManyById(IEnumerable<ObjectId> ids)
        {
            //var objectIds = ids.ToList().Select(x=> new ObjectId(x));
            var filter = Builders<TDocument>.Filter.In(doc => doc.Id, ids);
            return _collection.Find(filter).ToEnumerable();
        }

        public virtual Task<IEnumerable<TDocument>> FindManyByIdAsync(IEnumerable<ObjectId> ids)
        {
            return Task.Run(() =>
            {
                //var objectIds = ids.ToList().Select(x => new ObjectId(x));
                var filter = Builders<TDocument>.Filter.In(doc => doc.Id, ids);
                return _collection.Find(filter).ToEnumerable();
            });
        }

        public IEnumerable<TDocument> FindManyByNames(IEnumerable<string> names)
        {
            var filter = Builders<TDocument>.Filter.In(doc => doc.Name, names);
            return _collection.Find(filter).ToEnumerable();
        }

        public Task<IEnumerable<TDocument>> FindManyByNamesAsync(IEnumerable<string> names)
        {
            return Task.Run(() =>
            {
                //var objectIds = ids.ToList().Select(x => new ObjectId(x));
                var filter = Builders<TDocument>.Filter.In(doc => doc.Name, names);
                return _collection.Find(filter).ToEnumerable();
            });
        }

        public virtual void InsertOne(TDocument document)
        {
            _collection.InsertOne(document);
        }

        public virtual Task InsertOneAsync(TDocument document)
        {
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public void InsertMany(ICollection<TDocument> documents)
        {
            _collection.InsertMany(documents);
        }

        public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public void ReplaceOne(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }

        public virtual async Task ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        public void DeleteById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            _collection.FindOneAndDelete(filter);
        }

        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }

        public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }

        public Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }
    }
}
