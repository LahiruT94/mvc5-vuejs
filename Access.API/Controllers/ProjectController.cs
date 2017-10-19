using System;
using System.Web.Http;
using System.Web.Http.Description;
using Access.Data.Models;
using Access.Data.Services;

namespace Access.API
{
	public class ProjectController : BaseApiController
	{
		private readonly IProjectService _projectService;

		public ProjectController(IProjectService projectService)
		{
			_projectService = projectService;
		}

		// GET: api/Project
		[HttpGet]
		[AcceptVerbs("GET")]
		public IHttpActionResult Get([FromUri] Filter filter)
		{
			var projects = _projectService.Get(filter);

			return Json(new {
				Items = projects,
				Total = projects.TotalItemCount
			});
		}

		// GET: api/Project/id
		[HttpGet]
		[AcceptVerbs("GET")]
		public IHttpActionResult GetById(int id)
		{
			if (id <= 0)
				return BadRequest();
			try
			{
				var project = _projectService.GetById(id);
				if (project == null)
					return NotFound();

				return Json(new {project});
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		// PUT: api/Project/5/update
		[HttpPut, HttpPatch]
		[AcceptVerbs("PUT", "PATCH")]
		public IHttpActionResult Update(ProjectEntity model)
		{
			if (model == null)
				return BadRequest();

			try
			{
				_projectService.Update(model);

				return Ok(model);
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		// POST: api/Project/create
		[HttpPost]
		[AcceptVerbs("POST")]
		public IHttpActionResult Create(ProjectEntity model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				_projectService.Insert(model);

				return Ok();
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		// DELETE: api/Project/{id}
		[HttpDelete]
		[AcceptVerbs("DELETE")]
		public IHttpActionResult Delete(int id)
		{
			try
			{
				var project = _projectService.GetById(id);
				if (project == null)
					throw new NullReferenceException($"Проект с идентификатором: {id} не найден.");

				_projectService.Delete(project);

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

				_projectService.Delete(ids);

				return Ok();
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}
	}
}