﻿namespace Datalagring_examination.Models.Entities;

internal class CustomerEntity
{
	public int Id { get; set; }
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public int PhoneNumber { get; set; }

	public ICollection<CaseEntity> Cases { get; set; } = new HashSet<CaseEntity>();
}
