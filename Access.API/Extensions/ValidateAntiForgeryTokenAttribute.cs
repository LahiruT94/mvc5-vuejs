﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Access.API.Extensions
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
	public sealed class ValidateAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
	{
		public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext,
			CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			try
			{
				//AntiForgery.Validate();
			}
			catch
			{
				actionContext.Response = new HttpResponseMessage
				{
					StatusCode = HttpStatusCode.Forbidden,
					RequestMessage = actionContext.ControllerContext.Request
				};
				return FromResult(actionContext.Response);
			}
			return continuation();
		}

		private Task<HttpResponseMessage> FromResult(HttpResponseMessage result)
		{
			var source = new TaskCompletionSource<HttpResponseMessage>();
			source.SetResult(result);
			return source.Task;
		}
	}
}