using System;

namespace Access.Data.ViewModels
{
	public class AccessListViewModel
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Phone { get; set; }

		public string Email { get; set; }

		public string Note { get; set; }

		public int ProjectId { get; set; }

		public string ProjectTitle { get; set; }

		public int AccessId { get; set; }

		public string AccessType { get; set; }

		public string Password { get; set; }

		public string Login { get; set; }

		public string Address { get; set; }

		public string AccessNote { get; set; }

		public override string ToString()
		{
			return String.Concat(AccessNote, Title, AccessType, Address, Email, Login, Note, Password, Phone, ProjectTitle);
		}
	}
}