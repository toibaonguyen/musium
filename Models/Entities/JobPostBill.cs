namespace JobNet.Models.Entities;

public class JobPostBill : Entity
{
    public int Id { get; set; }
    public int JobPostId { get; set; }
    public JobPost JobPost { get; set; } = null!;
    public required decimal Amount { get; set; }
    public DateTime BillDate { get; set; }
    public bool IsPayed { get; set; }
    public DateTime? PaymentDate { get; set; }
}