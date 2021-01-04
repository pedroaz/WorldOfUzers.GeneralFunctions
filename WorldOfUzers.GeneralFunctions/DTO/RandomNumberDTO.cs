using System;
using System.Collections.Generic;
using System.Text;

namespace WorldOfUzers.GeneralFunctions.DTO
{
    public class RandomNumberDTO
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public int RandomValue { get; set; }

        public RandomNumberDTO(int value)
        {
            PartitionKey = "RandomValue";
            RandomValue = value;
            RowKey = value.ToString();
        }
    }
}
