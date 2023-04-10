using Datalagring_examination.Contexts;
using Datalagring_examination.Models;
using Datalagring_examination.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System.Linq.Expressions;

namespace Datalagring_examination.Services;

internal class CaseService
{
	private readonly DataContext _context = new();

	public async Task<CaseEntity> CreateAsync(CaseModel model)
	{
		//Creates a new case based on description and customerId tied to the case

		CaseEntity caseEntity = new CaseEntity()
		{
			CaseDescription = model.CaseDescription,
			CustomerId = model.CustomerId,
		};

		await _context.AddAsync(caseEntity);
		await _context.SaveChangesAsync();

		return caseEntity;
	}

	public async Task<IEnumerable<CaseEntity>> GetAllAsync()
	{
		//Returns all cases with customer, status, and comments tied to the cases

		return await _context.Cases
			.Include(x => x.Customer)
			.Include(x => x.Status)
			.Include(x => x.Comments)
			.ToListAsync();
	}

	public async Task<CaseEntity> GetAsync(Expression<Func<CaseEntity, bool>> predicate)
	{
		//Returns a specific case based on for example an id

		var _caseEntity = await _context.Cases
			.Include(x => x.Customer)
			.Include(x => x.Status)
			.Include(x => x.Comments)
			.FirstOrDefaultAsync(predicate);

		return _caseEntity!;
	}

	public async Task<CaseEntity> UpdateCaseStatusAsync(Expression<Func<CaseEntity, bool>> predicate, int input)
	{
		//Finds a specific case and updates the status for that case according to the input sent via the parameters

		var _caseEntity = await _context.Cases.FirstOrDefaultAsync(predicate);
		if(_caseEntity != null)
		{
			switch (input)
			{
				case 1:
					_caseEntity.StatusId = 1;
					_caseEntity.Modified = DateTime.Now;
					break;
				case 2:
					_caseEntity.StatusId = 2;
					_caseEntity.Modified = DateTime.Now;
					break;
				case 3:
					_caseEntity.StatusId = 3;
					_caseEntity.Modified = DateTime.Now;
					break;
				default:
					Console.WriteLine("Ogiltigt värde");
					break;
			}

			_context.Update(_caseEntity);
			await _context.SaveChangesAsync();
		}

		return _caseEntity!;
	}
}
