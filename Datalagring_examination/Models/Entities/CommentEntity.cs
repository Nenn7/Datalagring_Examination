namespace Datalagring_examination.Models.Entities;

internal class CommentEntity
{
    public int Id { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime Created{ get; set; } = DateTime.Now;
    public int CaseId { get; set; }
    public CaseEntity Case { get; set; } = null!;
}
