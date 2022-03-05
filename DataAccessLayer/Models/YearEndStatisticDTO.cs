using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class YearEndStatisticDTO
    {
        public string? Year { get; set; }

        public string? Quarter { get; set; }

        public string? TotalOrdersPerQuarter { get; set; }

        public string? AvgArticleQtySumPerOrderPerQuarter { get; set; }
            
        public string? AvgSumSalesPerCustomerPerQuarter { get; set; }
        
        public string? SalesTotalPerQuarter { get; set; } 
        
        public string? TotalArticlesInTheSystem { get; set; }
    }
}
