using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Access.Data.Models;

namespace Access.Data.Services
{
	/// <summary>
	/// Базовый интерфейс сервисов.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IBaseService<TEntity>
		where TEntity : BaseEntity
	{
		/// <summary>
		/// Вернет запись по идентификатору.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		TEntity GetById(int id);

		/// <summary>
		/// Вернет первую запись по условию.
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		TEntity GetFirst(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Вернет записи по условию.
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		IEnumerable<TEntity> GetByQuery(Expression<Func<TEntity, bool>> query = null);

		/// <summary>
		/// Вернет все записи.
		/// </summary>
		/// <returns></returns>
		IEnumerable<TEntity> GetAll();

		/// <summary>
		/// Добавление записи.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		int Insert(TEntity entity);

		/// <summary>
		/// Обновление записи.
		/// </summary>
		/// <param name="entity"></param>
		void Update(TEntity entity);

		/// <summary>
		/// Обновление записей.
		/// </summary>
		/// <param name="entities"></param>
		void Update(List<TEntity> entities);

		/// <summary>
		/// Удаление по идентификатору.
		/// </summary>
		/// <param name="id"></param>
		void Delete(int id);

		/// <summary>
		/// Удаление записи.
		/// </summary>
		/// <param name="entity"></param>
		void Delete(TEntity entity);

		/// <summary>
		/// Удаление записей.
		/// </summary>
		/// <param name="entities"></param>
		void Delete(List<TEntity> entities);

	}
}
