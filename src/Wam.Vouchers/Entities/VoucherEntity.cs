using Azure;
using Azure.Data.Tables;

namespace Wam.Vouchers.Entities;

public class VoucherEntity : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public Guid? PlayerId { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}