
using Datalagring_examination.Models.Entities;

namespace Datalagring_examination.Models;

internal class CaseModel
{ 
	public string CaseDescription { get; set; } = null!;
	public DateTime Created { get; set; } = DateTime.Now;
	public DateTime Modified { get; set; } = DateTime.Now;
	public int StatusId { get; set; } = 1;
	public int CustomerId { get; set; }
	public CustomerEntity Customer { get; set; } = null!;
	public ICollection<CommentEntity> Comments { get; set; } = new HashSet<CommentEntity>();
}
