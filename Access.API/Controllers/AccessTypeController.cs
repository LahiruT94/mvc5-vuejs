using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(new { accessTypeList = _accessTypeService.GetAll() });
        }


        // GET: api/AccessType/5
        [ResponseType(typeof(AccessTypeEntity))]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            if (id <= 0)
                return BadRequest();
            try
            {
                AccessTypeEntity accessType = _accessTypeService.GetById(id);
                if (accessType == null)
                {
                    return NotFound();
                }

                return Json(new { accessType });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        // Put: api/AccessType/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult Update(int id, AccessTypeEntity model)
        {
            if (model == null)
                return BadRequest();

            if (id != model.Id)
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
        public IHttpActionResult Create(AccessTypeEntity model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
        public IHttpActionResult Delete(int id)
        {
            try
            {
                AccessTypeEntity access = _accessTypeService.GetById(id);
                if (access == null)
                {
                    throw new NullReferenceException($"Тип доступа с идентификатором: {id} не найден.");
                }

                _accessTypeService.Delete(access);

                return Ok();

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}