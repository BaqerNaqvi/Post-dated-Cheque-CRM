namespace DAL.Generic
{
    public class PagingParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public int? Id { get; set; }
        public string? IDstring { get; set; }
        public string? OrderBy { get; set; }
    }

    public class PaymentFilters : PagingParams
    {
        public int? agreementId { get; set; }
        public int? bankId { get; set; }
        public int? paymentMethodId { get; set; }
        public int? companyId { get; set; }
        public int? month { get; set; }
    }

    public class AgreementFilters : PagingParams
    {
        public int? companyId { get; set; }
    }
}
