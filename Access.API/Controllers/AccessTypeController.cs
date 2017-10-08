﻿using System;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Http.Description;
using Access.Data.Models;
using Access.Data.Services;

namespace Access.API
{
	public class AccessTypeController : BaseApiController
	{
		private readonly IAccessTypeService _accessTypeService;

		public AccessTypeController(IAccessTypeService accessTypeService)
		{
			_accessTypeService = accessTypeService;
		}

		// GET: api/AccessType
		[AcceptVerbs("GET")]
		public IHttpActionResult Get([FromUri] Filter filter)
		{
			return Json(new {Model = _accessTypeService.Get(filter)});
		}

		// GET: api/AccessType/5
		[ResponseType(typeof(AccessTypeEntity))]
		[AcceptVerbs("GET")]
		public IHttpActionResult GetById(int id)
		{
			if (id <= 0)
				return BadRequest();
			try
			{
				var accessType = _accessTypeService.GetById(id);
				if (accessType == null)
					return NotFound();

				return Json(new {accessType});
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}


		// Put: api/AccessType/5
		[ResponseType(typeof(void))]
		[AcceptVerbs("PUT", "PATCH")]
		public IHttpActionResult Update(AccessTypeEntity model)
		{
			if (model == null)
				return BadRequest();

			try
			{
				_accessTypeService.Update(model);

				return Ok();
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		// POST: api/AccessType
		[ResponseType(typeof(AccessTypeEntity))]
		[HttpPost]
		[AcceptVerbs("POST")]
		public IHttpActionResult Create(AccessTypeEntity model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				_accessTypeService.Insert(model);

				return Ok();
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		// DELETE: api/AccessType/5
		[ResponseType(typeof(AccessTypeEntity))]
		[HttpDelete]
		[AcceptVerbs("DELETE")]
		public IHttpActionResult Delete(int id)
		{
			try
			{
				var access = _accessTypeService.GetById(id);
				if (access == null)
					throw new NullReferenceException($"Тип доступа с идентификатором: {id} не найден.");

				_accessTypeService.Delete(access);

				return Ok();
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		[ResponseType(typeof(AccessTypeEntity))]
		[HttpDelete]
		[AcceptVerbs("DELETE")]
		public IHttpActionResult Delete([FromUri] int[] ids)
		{
			try
			{
				if (ids == null || ids.Length == 0)
					return BadRequest();

				_accessTypeService.Delete(ids);

				return Ok();
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}
	}
}