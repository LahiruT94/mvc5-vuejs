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
		protected readonly IRepository<TEntity> Repository;

		protected BaseService(IRepository<TEntity> repository)
		{
			Repository = repository;
		}

		public virtual IEnumerable<TEntity> GetAll()
		{
			return Repository.Table.AsEnumerable();
		}

		public virtual TEntity GetById(int id)
		{
			return Repository.GetById(id);
		}

		IEnumerable<TEntity> IBaseService<TEntity>.GetByQuery(Expression<Func<TEntity, bool>> query = null)
		{
			var queryResult = Repository.Table.AsQueryable();

			if (query != null)
				queryResult = queryResult.Where(query);

			return queryResult;
		}

		TEntity IBaseService<TEntity>.GetFirst(Expression<Func<TEntity, bool>> predicate)
		{
			return Repository.Table.FirstOrDefault(predicate);
		}

		public virtual int Insert(TEntity entity)
		{
			return Repository.Insert(entity);
		}

		public virtual void Update(TEntity entity)
		{
			Repository.Update(entity);
		}

		public virtual void Update(List<TEntity> entities)
		{
			Repository.Update(entities);
		}

		public virtual void Delete(TEntity entity)
		{
			Repository.Delete(entity);
		}

		public virtual void Delete(int id)
		{
			Delete(GetById(id));
		}

		public virtual void Delete(List<TEntity> entites)
		{
			Repository.Delete(entites);
		}

		protected IEnumerable<TEntity> GetByQuery(Expression<Func<TEntity, bool>> query = null)
		{
			return ((IBaseService<TEntity>)this).GetByQuery(query);
		}

		protected TEntity GetFirst(Expression<Func<TEntity, bool>> predicate)
		{
			return ((IBaseService<TEntity>)this).GetFirst(predicate);
		}
	}
}