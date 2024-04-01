namespace AuctionsAPI.Contracts.Responses
{
    public class AuctionsResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Metadata { get; set; }
        public SaleResponce SaleResponce { get; set; }
    }
}
