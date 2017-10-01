using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Access.Data.DAL;
using Access.Data.Models;

namespace Access.Data.Services
{
	public abstract class BaseService<TEntity> : IBaseService<TEntity>
		where TEntity : BaseEntity
	{
		protected readonly IRepository<TEntity> _repository;

		public BaseService(IRepository<TEntity> repository)
		{
			_repository = repository;
		}

		public virtual IEnumerable<TEntity> GetAll()
		{
			return _repository.Table.AsEnumerable();
		}

		public virtual TEntity GetById(int id)
		{
			return _repository.GetById(id);
		}

		IEnumerable<TEntity> IBaseService<TEntity>.GetByQuery(Expression<Func<TEntity, bool>> query = null)
		{
			var queryResult = _repository.Table.AsQueryable();

			if (query != null)
				queryResult = queryResult.Where(query);

			return queryResult;
		}

		TEntity IBaseService<TEntity>.GetFirst(Expression<Func<TEntity, bool>> predicate)
		{
			return _repository.Table.FirstOrDefault(predicate);
		}

		public virtual int Insert(TEntity entity)
		{
			return _repository.Insert(entity);
		}

		public virtual void Update(TEntity entity)
		{
			_repository.Update(entity);
		}

		public virtual void Delete(TEntity entity)
		{
			_repository.Delete(entity);
		}

		public virtual void Delete(int id)
		{
			Delete(GetById(id));
		}

		protected IEnumerable<TEntity> GetByQuery(Expression<Func<TEntity, bool>> query = null)
		{
			return ((IBaseService<TEntity>) this).GetByQuery(query);
		}

		protected TEntity GetFirst(Expression<Func<TEntity, bool>> predicate)
		{
			return ((IBaseService<TEntity>) this).GetFirst(predicate);
		}
	}
}