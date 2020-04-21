using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidWorksApi_Lesson3_Assembly.Helpers
{
    public class UnitConverter
    {
        public static double ConvertToMeter(string val)
        {
            if (!String.IsNullOrEmpty(val))
            {
                if (val.Contains("."))
                {
                    val = val.Replace(".", ",");
                }

                double val1 = Convert.ToDouble(val);
                return val1 / 1000;


            }
            else
            {
                throw new ArgumentNullException();
            }

        }


    }
}
