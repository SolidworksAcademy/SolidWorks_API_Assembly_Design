using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidWorksApi_Lesson3_Assembly
{
    internal class SolidWorksSingleton
    {
        private static SldWorks swApp;

        private SolidWorksSingleton()
        {

        }

        internal static SldWorks GetApplication()
        {
            if (swApp==null)
            {
                swApp = Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")) as SldWorks;
                swApp.Visible = true;

                return swApp;

            }

            return swApp;

        }

    }
}
