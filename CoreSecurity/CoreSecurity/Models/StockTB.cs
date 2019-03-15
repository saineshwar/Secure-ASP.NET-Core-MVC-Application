using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSecurity.Models
{
    [Table("StockTb")]
    public class StockTb
    {
        [Key]
        public int Stockid { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public decimal? Marketcap { get; set; }
        public int Employees { get; set; }
        public DateTime CurrentDate { get; set; }
        public decimal? Value { get; set; }

    }

    [NotMapped]
    public class StockTbInput
    {
        public int Stockid { get; set; }
        public string Name { get; set; }
    }
}
