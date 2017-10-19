using System;
using System.Web.Http;
using System.Web.Http.Description;
using Access.Data.Models;
using Access.Data.Services;

namespace Access.API
{
	public class ClientController : BaseApiController
	{
		private readonly IClientService _clientService;

		public ClientController(IClientService clientService)
		{
			_clientService = clientService;
		}

		// GET: api/Client/
		[HttpGet]
		[AcceptVerbs("GET")]
		public IHttpActionResult Get([FromUri] Filter filter)
		{
			var clients = _clientService.Get(filter);

			return Json(new {
				Items = clients,
				Total = clients.TotalItemCount
			});
		}

		// GET: api/Client/{id}
		[HttpGet]
		[AcceptVerbs("GET")]
		public IHttpActionResult GetById(int id)
		{
			if (id <= 0)
				return BadRequest();
			try
			{
				var client = _clientService.GetById(id);
				if (client == null)
					return NotFound();

				return Json(new {client});
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		// PUT: api/Client/
		[HttpPut, HttpPatch]
		[AcceptVerbs("PUT", "PATCH")]
		public IHttpActionResult Update(ClientEntity model)
		{
			if (model == null)
				return BadRequest();

			try
			{
				_clientService.Update(model);

				return Ok(model);
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		// POST: api/Client/
		[HttpPost]
		[AcceptVerbs("POST")]
		public IHttpActionResult Create(ClientEntity model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				_clientService.Insert(model);

				return Ok();
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		// DELETE: api/Client/{id}
		[HttpDelete]
		[AcceptVerbs("DELETE")]
		public IHttpActionResult Delete(int id)
		{
			try
			{
				var client = _clientService.GetById(id);
				if (client != null)
				{
					_clientService.Delete(client);

					return Ok();
				}
				throw new NullReferenceException($"Клиент с идентификатором: {id} не найден.");
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

				_clientService.Delete(ids);

				return Ok();
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}
	}
}