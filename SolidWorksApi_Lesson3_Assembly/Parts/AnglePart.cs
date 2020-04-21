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
    public class AnglePart : Base
    {
        public double XLenght { get; set; }
        public double YLenght { get; set; }
        public double Width { get; set; }
        public double Thickness { get; set; }
        public double BoltHoles { get; set; }
        public double PipeHole { get; set; }
        public double Rad1 { get; set; }
        public double Rad2 { get; set; }
        public double X1 { get; set; }
        public double X2 { get; set; }

        public string MateRef1 { get; set; }
        public string MateRefHole { get; set; }


        SldWorks swApp;
        ModelDoc2 swModel;
        Feature swFeature;
        PartDoc swPart;
        Entity swEntity;


        public override void CreatePart()
        {
            swApp = SolidWorksSingleton.GetApplication();
            DocumentManager.CreateNewPartDoc();

            swModel = (ModelDoc2)swApp.ActiveDoc;

            swFeature = swModel.FeatureByPositionReverse(3);
            swFeature.Name = "Front";

            swModel.Extension.SelectByID2("Front","PLANE",0,0,0,false,0,null,0);

            swModel.InsertSketch2(true);

            swModel.CreateLine2(0,0,0,XLenght,0,0);
            //swModel.AddDimension2(0,0,0);

            swModel.CreateLine2(0,0,0,0,YLenght,0);
            //swModel.AddDimension2(0, 0, 0);

            //int markHorizontal = 2;
            //int markVertical = 4;

            //swModel.Extension.SelectByID2("Point1@Origin", "EXTSKETCHSEGMENT",0,0,0,false, markHorizontal|markVertical,null,0);

            object datumDisp = "Point1@Origin";

            swModel.SketchManager.FullyDefineSketch(true, true, (int)swSketchFullyDefineRelationType_e.swSketchFullyDefineRelationType_Vertical | (int)swSketchFullyDefineRelationType_e.swSketchFullyDefineRelationType_Horizontal, true, (int)swAutodimScheme_e.swAutodimSchemeBaseline, datumDisp, (int)swAutodimScheme_e.swAutodimSchemeBaseline, datumDisp, (int)swAutodimHorizontalPlacement_e.swAutodimHorizontalPlacementBelow, (int)swAutodimVerticalPlacement_e.swAutodimVerticalPlacementLeft);

            swModel.InsertSketch2(true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Sketch1";

            swModel.Extension.SelectByID2("Sketch1","SKETCH",0,0,0,false,0,null,0);          

            swModel.FeatureManager.InsertSheetMetalBaseFlange2(Thickness, false, Thickness, Width, 0, true, 0, 0, 0, null, false, 0, 0, 0, 0, false, false, false, false);

            BasicOpertations.ChangeEntityName("FACE", MateRef1, Thickness, YLenght / 2, -Width / 2);

            swModel.Extension.SelectByID2("","FACE",XLenght/2,Thickness,-Width/2,false,0,null,0);

            swModel.InsertSketch2(true);

            swModel.CreateCircleByRadius2(XLenght-X1,X2,0,BoltHoles);
            swModel.CreateCircleByRadius2(XLenght-X1,Width-X2,0,BoltHoles);

            swModel.InsertSketch2(true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "BoltHoles";

            swModel.Extension.SelectByID2("BoltHoles","SKETCH",0,0,0,false,0,null,0);

            BasicOpertations.SimpleCut();


            swPart = (PartDoc)swApp.ActiveDoc;

            swEntity = swPart.GetEntityByName(MateRef1, (int)swSelectType_e.swSelFACES);
            swEntity.Select4(false,null);

            swModel.InsertSketch2(true);

            swModel.CreateCircleByRadius2(Width/2,YLenght-(PipeHole*2),0,PipeHole);
            swModel.AddDiameterDimension(0,0,0);

            swModel.InsertSketch2(true);

            BasicOpertations.SimpleCut();

            BasicOpertations.ChangeEntityName("FACE", MateRefHole, Thickness / 2, YLenght - PipeHole, -Width / 2);

            swModel = (ModelDoc2)swApp.ActiveDoc;

            swModel.Extension.SelectByID2("","EDGE",XLenght,Thickness/2,0,false,0,null,0);
            swModel.Extension.SelectByID2("","EDGE",XLenght,Thickness/2,-Width,true,0,null,0);

            BasicOpertations.SimpleFillet(Rad2);

            swModel.Extension.SelectByID2("","EDGE",Thickness/2,YLenght,0,false,0,null,0);
            swModel.Extension.SelectByID2("","EDGE",Thickness/2,YLenght,-Width,true,0,null,0);

            BasicOpertations.SimpleFillet(Rad1);

            swModel.ClearSelection2(true);
            swModel.ViewZoomtofit2();

            DocumentManager.Save(TargetFolder, FileName,DocumentManager.sw_DocType.part);

        }
    }
}
