using Microsoft.WindowsAzure.Storage.Table;

namespace Rephase.Models
{
    class AzureTableEntity : TableEntity
    {
     
        public AzureTableEntity(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
          
        }
        public AzureTableEntity() { }

        public string Value { get; set; }

    }
}