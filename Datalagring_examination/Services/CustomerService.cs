using Datalagring_examination.Contexts;
using Datalagring_examination.Models;
using Datalagring_examination.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Datalagring_examination.Services;

internal class CustomerService
{
	private readonly DataContext _context = new DataContext();

	public async Task<CustomerEntity> CreateAsync(CustomerModel model)
	{
		//Checks if a customer already exists based on email, if not, creates a new customer based on the information provided

		var _customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.Email == model.Email);

		if (_customerEntity == null)
		{
			_customerEntity = new CustomerEntity()
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
			};

			await _context.AddAsync(_customerEntity);
			await _context.SaveChangesAsync();	
		}

		return _customerEntity;
	}

}
