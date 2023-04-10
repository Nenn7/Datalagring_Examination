using Datalagring_examination.Contexts;
using Datalagring_examination.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Datalagring_examination.Services;

internal class StatusService
{
	private readonly DataContext _context = new();

	public async Task InitializeAsync()
	{
		//Creates three different statuses onto the status table upon calling the method

		if (!await _context.Statuses.AnyAsync())
		{
			List<StatusEntity> list = new()
			{
				new StatusEntity() { Status = "Ej påbörjad" },
				new StatusEntity() { Status = "Pågående" },
				new StatusEntity() { Status = "Avslutad" },
			};

			_context.AddRange(list);
			await _context.SaveChangesAsync();
		}
}

}
