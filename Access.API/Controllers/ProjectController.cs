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
		public IHttpActionResult Get()
		{
			return Json(new {projectList = _projectService.GetAll()});
		}

		// GET: api/Project/id
		[HttpGet]
		[ResponseType(typeof(ProjectEntity))]
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
		[HttpPut]
		public IHttpActionResult Update(int id, ProjectEntity model)
		{
			if (model == null)
				return BadRequest();

			if (id != model.Id)
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

		// DELETE: api/Project/5/delete
		[HttpDelete]
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
	}
}