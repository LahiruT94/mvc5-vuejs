﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Linq;
using Access.Data.Mappings;
using Access.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Access.Data.DAL
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDbContext
	{
		public ApplicationDbContext()
			: base("AccessConnection", false)
		{
			Database.Log = s => Debug.WriteLine(s);
		}

		/// <summary>
		///     Gets or sets a value indicating whether proxy creation setting is enabled (used in EF)
		/// </summary>
		public virtual bool ProxyCreationEnabled
		{
			get => Configuration.ProxyCreationEnabled;
			set => Configuration.ProxyCreationEnabled = value;
		}

		/// <summary>
		///     Gets or sets a value indicating whether auto detect changes setting is enabled (used in EF)
		/// </summary>
		public virtual bool AutoDetectChangesEnabled
		{
			get => Configuration.AutoDetectChangesEnabled;
			set => Configuration.AutoDetectChangesEnabled = value;
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new ClientEntityMap());
            modelBuilder.Configurations.Add(new ProjectEntityMap());
            modelBuilder.Configurations.Add(new AccessTypeEntityMap());
            modelBuilder.Configurations.Add(new AccessEntityMap());

            base.OnModelCreating(modelBuilder);
        }

		/// <summary>
		///     Attach an entity to the context or return an already attached entity (if it was already attached)
		/// </summary>
		/// <typeparam name="TEntity">TEntity</typeparam>
		/// <param name="entity">Entity</param>
		/// <returns>Attached entity</returns>
		public virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
		{
			//little hack here until Entity Framework really supports stored procedures
			//otherwise, navigation properties of loaded entities are not loaded until an entity is attached to the context
			var alreadyAttached = Set<TEntity>().Local.FirstOrDefault(x => x.Id == entity.Id);
			if (alreadyAttached == null)
			{
				//attach new entity
				Set<TEntity>().Attach(entity);
				return entity;
			}

			//entity is already loaded
			return alreadyAttached;
		}

		#region Methods

		/// <summary>
		///     Get DbSet
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <returns>DbSet</returns>
		public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
		{
			return base.Set<TEntity>();
		}

		/// <summary>
		///     Execute stores procedure and load a list of entities at the end
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="commandText">Command text</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>Entities</returns>
		public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
			where TEntity : BaseEntity, new()
		{
			//add parameters to command
			if (parameters != null && parameters.Length > 0)
				for (var i = 0; i <= parameters.Length - 1; i++)
				{
					var p = parameters[i] as DbParameter;
					if (p == null)
						throw new Exception("Not support parameter type");

					commandText += i == 0 ? " " : ", ";

					commandText += "@" + p.ParameterName;
					if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
						commandText += " output";
				}

			var result = Database.SqlQuery<TEntity>(commandText, parameters).ToList();

			//performance hack applied as described here - http://www.nopcommerce.com/boards/t/25483/fix-very-important-speed-improvement.aspx
			var acd = Configuration.AutoDetectChangesEnabled;
			try
			{
				Configuration.AutoDetectChangesEnabled = false;

				for (var i = 0; i < result.Count; i++)
					result[i] = AttachEntityToContext(result[i]);
			}
			finally
			{
				Configuration.AutoDetectChangesEnabled = acd;
			}

			return result;
		}

		/// <summary>
		///     Creates a raw SQL query that will return elements of the given generic type.
		///     The type can be any type that has properties that match the names of the columns returned from the query, or can be
		///     a simple primitive type.
		///     The type does not have to be an entity type. The results of this query are never tracked by the context even if the
		///     type of object returned is an entity type.
		/// </summary>
		/// <typeparam name="TElement">The type of object returned by the query.</typeparam>
		/// <param name="sql">The SQL query string.</param>
		/// <param name="parameters">The parameters to apply to the SQL query string.</param>
		/// <returns>Result</returns>
		public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
		{
			return Database.SqlQuery<TElement>(sql, parameters);
		}

		/// <summary>
		///     Executes the given DDL/DML command against the database.
		/// </summary>
		/// <param name="sql">The command string</param>
		/// <param name="doNotEnsureTransaction">
		///     false - the transaction creation is not ensured; true - the transaction creation
		///     is ensured.
		/// </param>
		/// <param name="timeout">
		///     Timeout value, in seconds. A null value indicates that the default value of the underlying
		///     provider will be used
		/// </param>
		/// <param name="parameters">The parameters to apply to the command string.</param>
		/// <returns>The result returned by the database after executing the command.</returns>
		public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null,
			params object[] parameters)
		{
			int? previousTimeout = null;
			if (timeout.HasValue)
			{
				//store previous timeout
				previousTimeout = ((IObjectContextAdapter) this).ObjectContext.CommandTimeout;
				((IObjectContextAdapter) this).ObjectContext.CommandTimeout = timeout;
			}

			var transactionalBehavior = doNotEnsureTransaction
				? TransactionalBehavior.DoNotEnsureTransaction
				: TransactionalBehavior.EnsureTransaction;
			var result = Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

			if (timeout.HasValue)
				((IObjectContextAdapter) this).ObjectContext.CommandTimeout = previousTimeout;

			//return result
			return result;
		}

		/// <summary>
		///     Detach an entity
		/// </summary>
		/// <param name="entity">Entity</param>
		public void Detach(object entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			((IObjectContextAdapter) this).ObjectContext.Detach(entity);
		}

		#endregion
	}
}