using Datalagring_examination.Contexts;
using Datalagring_examination.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Datalagring_examination.Services;

internal class CommentService
{
	private readonly DataContext _context = new();

	public async Task CreateAsync(CommentEntity comment)
	{
		//If any cases exists matching Id with comment caseId, creates a new comment

		if(await _context.Cases.AnyAsync(x => x.Id == comment.CaseId))
		{
			_context.Add(comment);
			await _context.SaveChangesAsync();
		}
	}

}
