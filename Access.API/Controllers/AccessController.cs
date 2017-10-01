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
    public class AccessController : BaseApiController
    {
        private readonly IAccessService _accessService;
        public AccessController(IAccessService accessService)
        {
            _accessService = accessService;
        }

        // GET: api/Access
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(new { accessList = _accessService.GetAll() });
        }

        // GET: api/Access/id
        [HttpGet]
        [ResponseType(typeof(AccessEntity))]
        public IHttpActionResult GetById(int id)
        {
            if (id <= 0)
                return BadRequest();
            try
            {
                AccessEntity access = _accessService.GetById(id);
                if (access == null)
                {
                    return NotFound();
                }

                return Json(new { access });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Access/5/update
        [HttpPut]
        public IHttpActionResult Update(int id, AccessEntity model)
        {
            if (model == null)
                return BadRequest();

            if (id != model.Id)
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

        // POST: api/Access/create
        [ResponseType(typeof(AccessEntity))]
        [HttpPost]
        public IHttpActionResult Create(AccessEntity model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
        [ResponseType(typeof(AccessEntity))]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var access = _accessService.GetById(id);
                if (access == null)
                {
                    throw new NullReferenceException($"Доступ с идентификатором: {id} не найден.");
                }
                _accessService.Delete(access);

                return Ok();

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}