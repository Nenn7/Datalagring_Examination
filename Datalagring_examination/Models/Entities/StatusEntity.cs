namespace Datalagring_examination.Models.Entities;

internal class StatusEntity
{
    public int Id { get; set; } 
    public string Status { get; set; } = null!;
    public ICollection<CaseEntity> Cases { get; set; } = new HashSet<CaseEntity>();
}
