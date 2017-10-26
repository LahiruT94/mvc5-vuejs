using System;
using System.Web.Http;
using System.Web.Http.Description;
using Access.Data.Models;
using Access.Data.Services;

namespace Access.API
{
	public class AccessController : BaseApiController
	{
		private readonly IAccessService _accessService;

		public AccessController(IAccessService accessService)
		{
			_accessService = accessService;
		}
		// GET: api/Access
		[HttpGet]
		[AcceptVerbs("GET")]
		public IHttpActionResult Get([FromUri] Filter filter)
		{
			var access = _accessService.Get(filter);

			return Json(new
			{
				Items = access,
				Total = access.TotalItemCount
			});
		}

		// GET: api/Access/id
		[HttpGet]
		[AcceptVerbs("GET")]
		public IHttpActionResult GetById(int id)
		{
			if (id <= 0)
				return BadRequest();
			try
			{
				var access = _accessService.GetById(id);
				if (access == null)
					return NotFound();

				return Json(new {access});
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		// PUT: api/Access
		[AcceptVerbs("PUT", "PATCH")]
		public IHttpActionResult Update(AccessEntity model)
		{
			if (model == null)
				return BadRequest();

			try
			{
				_accessService.Update(model);

				return Ok(model);
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		// POST: api/Access
		[HttpPost]
		[AcceptVerbs("POST")]
		public IHttpActionResult Create(AccessEntity model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				_accessService.Insert(model);

				return Ok();
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		// DELETE: api/Access/5/delete
		[HttpDelete]
		[AcceptVerbs("DELETE")]
		public IHttpActionResult Delete(int id)
		{
			try
			{
				var access = _accessService.GetById(id);
				if (access == null)
					throw new NullReferenceException($"Доступ с идентификатором: {id} не найден.");

				_accessService.Delete(access);

				return Ok();
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		[HttpDelete]
		[AcceptVerbs("DELETE")]
		public IHttpActionResult Delete([FromUri] int[] ids)
		{
			try
			{
				if (ids == null || ids.Length == 0)
					return BadRequest();

				_accessService.Delete(ids);

				return Ok();
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}
	}
}