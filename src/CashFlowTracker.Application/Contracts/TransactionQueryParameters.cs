namespace CashFlowTracker.Application.Contracts
{
    public class TransactionQueryParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? CategoryId { get; set; }
    }

}
