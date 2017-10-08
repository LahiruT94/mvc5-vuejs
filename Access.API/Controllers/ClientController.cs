using System;
using System.Data;
using System.Linq;
using System.Net;
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

        // GET: api/Client
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(new{ items = _clientService.GetAll() });
        }

        // GET: api/Client/id
        [HttpGet]
        [ResponseType(typeof(ClientEntity))]
        public IHttpActionResult GetById(int id)
        {
            if (id <= 0)
                return BadRequest();
            try
            {
                ClientEntity client = _clientService.GetById(id);
                if (client == null)
                {
                    return NotFound();
                }

                return Json(new { client });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Client/5/update
        [HttpPut]
        public IHttpActionResult Update(int id, ClientEntity model)
        {
            if (model == null)
                return BadRequest();

            if (id != model.Id)
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

        // POST: api/Client/create
        [ResponseType(typeof(ClientEntity))]
        [HttpPost]
        public IHttpActionResult Create(ClientEntity model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
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

        // DELETE: api/Client/5/delete
        [ResponseType(typeof(ClientEntity))]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                ClientEntity client = _clientService.GetById(id);
                if (client != null)
                {
                    _clientService.Delete(client);

                    return Ok();
                }
                else { throw new NullReferenceException($"Клиент с идентификатором: {id} не найден."); }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}