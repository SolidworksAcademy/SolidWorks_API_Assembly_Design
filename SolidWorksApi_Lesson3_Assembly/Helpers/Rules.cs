using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidWorksApi_Lesson3_Assembly.Helpers
{
    public class Rules
    {
        public static void DimensionCheck(double val1, double val2, string message)
        {

            if (val1>val2)
            {
                throw new Exception(message);
            }

        }

    }
}
