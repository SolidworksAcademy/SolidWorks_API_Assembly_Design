using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidWorksApi_Lesson3_Assembly.Helpers;

namespace SolidWorksApi_Lesson3_Assembly.Parts
{
    public class Pipe : Base
    {
        public double OutsideDiameter { get; set; }
        public double InsideDiameter { get; set; }
        public double Lenght { get; set; }


        public string MateOutsideFace { get; set; }
        public string MateBase { get; set; }

        SldWorks swApp;
        ModelDoc2 swModel;
        Feature swFeature;


        public override void CreatePart()
        {
            swApp = SolidWorksSingleton.GetApplication();
            DocumentManager.CreateNewPartDoc();
            swModel = (ModelDoc2)swApp.ActiveDoc;

            swFeature = swModel.FeatureByPositionReverse(3);
            swFeature.Name = "Front";

            swModel.Extension.SelectByID2("Front", "PLANE", 0, 0, 0, false, 0, null, 0);

            swModel.InsertSketch2(true);

            swModel.CreateCircleByRadius2(0, 0, 0, InsideDiameter / 2);
            swModel.CreateCircleByRadius2(0, 0, 0, OutsideDiameter / 2);

            swModel.InsertSketch2(true);

            swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, false,0,0,Lenght,0, false, false, false, false,0,0, false, false, false, false, false, false, false,0,0, false);

            BasicOpertations.ChangeEntityName("FACE", MateOutsideFace, OutsideDiameter / 2, 0, Lenght / 2);

            double dim = (OutsideDiameter-InsideDiameter)/4;
            BasicOpertations.ChangeEntityName("FACE",MateBase,(InsideDiameter/2)+dim,0,0);

            DocumentManager.Save(TargetFolder,FileName,DocumentManager.sw_DocType.part);




        }
    }
}
