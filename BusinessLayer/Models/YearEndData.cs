using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class YearEndData
    {
        public string? Category { get; set; }
        
        public string? YOY2022 { get => CalculateYOY2022(); }

        public string? YOY2021 { get => CalculateYOY2021(); }

        public string? YOY2020 { get => CalculateYOY2020(); }

        public string? YOY2019 => "N/A";

        public List<string> QuarterData { get; set; } = new List<string>();

        private string CalculateYOY2022()
        {
            decimal q12022 = Convert.ToDecimal(QuarterData[3]);

            decimal q12021 = Convert.ToDecimal(QuarterData[7]);

            if(q12021 != 0)
            {
                decimal yoy2022 = q12022 / q12021 * 100;
                yoy2022 = Math.Round(yoy2022, 2);

                return yoy2022 + " %";

            }
            else
            {
                return "N/A";
            }
                      
        }

        private string CalculateYOY2021()
        {
            decimal y2021 = Convert.ToDecimal(QuarterData[4]) + Convert.ToDecimal(QuarterData[5]) + Convert.ToDecimal(QuarterData[6]) + Convert.ToDecimal(QuarterData[7]);
            decimal y2020 = Convert.ToDecimal(QuarterData[8]) + Convert.ToDecimal(QuarterData[9]) + Convert.ToDecimal(QuarterData[10]) + Convert.ToDecimal(QuarterData[11]);

            if(y2020 != 0)
            {
                decimal yoy2021 = y2021 / y2020 * 100;
                yoy2021 = Math.Round(yoy2021, 2);

                return yoy2021 + " %";
            }
            else
            {
                return "N/A";
            }

        }

        private string CalculateYOY2020()
        {
            decimal y2020 = Convert.ToDecimal(QuarterData[8]) + Convert.ToDecimal(QuarterData[9]) + Convert.ToDecimal(QuarterData[10]) + Convert.ToDecimal(QuarterData[11]);
            decimal y2019 = Convert.ToDecimal(QuarterData[12]) + Convert.ToDecimal(QuarterData[13]) + Convert.ToDecimal(QuarterData[14]) + Convert.ToDecimal(QuarterData[15]);

            if(y2019 != 0)
            {
                decimal yoy2020 = y2020 / y2019 * 100;
                yoy2020 = Math.Round(yoy2020, 2);
                return yoy2020 + " %";
            }
            else
            {
                return "N/A";
            }

        }
    }
}
