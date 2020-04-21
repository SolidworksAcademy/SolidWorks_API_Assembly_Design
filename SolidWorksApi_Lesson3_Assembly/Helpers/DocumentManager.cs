using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksApi_Lesson3_Assembly.Helpers
{
    public class DocumentManager
    {
     
        public enum sw_DocType
        {
            part,
            assembly,
            drawing
        }
       
        public static void CreateAssemblyDoc(string filePath, string FileName)
        {
            SldWorks swApp;
            ModelDoc2 swModel;

            string defaultAssemblyTemplate;
            swApp = SolidWorksSingleton.GetApplication();

            defaultAssemblyTemplate = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplateAssembly);
            swApp.NewDocument(defaultAssemblyTemplate, 0, 0, 0);

            swModel = (ModelDoc2)swApp.ActiveDoc;

            swModel.SaveAs3(filePath + "\\" + FileName + ".sldasm", (int)swSaveAsVersion_e.swSaveAsCurrentVersion, (int)swSaveAsOptions_e.swSaveAsOptions_CopyAndOpen);

        }


        public static void CreateNewPartDoc()
        {
            SldWorks swApp;
            ModelDoc2 swModel;

            string defaultPartTemplate;

            swApp = SolidWorksSingleton.GetApplication();

            defaultPartTemplate = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplatePart);
            swApp.NewDocument(defaultPartTemplate,0,0,0);
        }

        public static void Save(string targetFolder, string fileName, sw_DocType docType )
        {
            SldWorks swApp;
            ModelDoc2 swModel;
            string doc;

            swApp = SolidWorksSingleton.GetApplication();

            swModel = (ModelDoc2)swApp.ActiveDoc;

            if (docType == sw_DocType.assembly)
            {
                doc = ".sldasm";
            }
            else if (docType == sw_DocType.part)
            {
                doc = ".sldprt";
            }
            else if (docType == sw_DocType.drawing)
            {
                doc = ".slddrw";
            }
            else
            {
                throw new Exception("Kayıt yapılamadı");
            }



                swModel.SaveAs3(targetFolder + "\\" + fileName + doc, (int)swSaveAsVersion_e.swSaveAsCurrentVersion, (int)swSaveAsOptions_e.swSaveAsOptions_CopyAndOpen);

            swApp.CloseDoc(fileName + doc);

        }

        public static string CreateDir(string path)
        {
            string guid = Guid.NewGuid().ToString();
            string root = path + "\\" + guid;

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            else
            {
                MessageBox.Show("Aynı isimde bir dosya mevcut.");
            }
            return root;

        }


    }
}
