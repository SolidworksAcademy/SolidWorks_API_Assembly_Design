using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidWorksApi_Lesson3_Assembly.Parts;

namespace SolidWorksApi_Lesson3_Assembly.Helpers
{
    public class BasicOpertations
    {

        public static void ChangeEntityName(string Etype, string Ename, double CoorX, double CoorY, double CoorZ)
        {
            SldWorks swApp;
            ModelDoc2 swModel;
            SelectionMgr swSelMgr;
            Face2 swFace;
            PartDoc swPart;

            swApp = SolidWorksSingleton.GetApplication();

            swModel = swApp.ActiveDoc;

            swModel.Extension.SelectByID2("", Etype, CoorX, CoorY, CoorZ, false, 0, null, 0);
            
            swSelMgr = swModel.SelectionManager;
            swFace = swSelMgr.GetSelectedObject6(1,-1);
            swPart = swApp.ActiveDoc;
           
            swPart.SetEntityName(swFace, Ename);


        }

        public static void SimpleCut()
        {
            SldWorks swApp;
            ModelDoc2 swModel;
            Feature swFeature;

            swApp = SolidWorksSingleton.GetApplication();
            swModel = swApp.ActiveDoc;

            swFeature = swModel.FeatureManager.FeatureCut3(true, false, false, (int)swEndConditions_e.swEndCondThroughAll, (int)swEndConditions_e.swEndCondBlind, 0, 0, false, false, false, false, 0, 0, false, false, false, false, false, true, true, false, false, false, (int)swEndConditions_e.swEndCondMidPlane, 0, true);

        }

        public static void SimpleFillet(double radius)
        {
            SldWorks swApp;
            ModelDoc2 swModel;

            swApp = SolidWorksSingleton.GetApplication();
            swModel = swApp.ActiveDoc;

            swModel.FeatureManager.FeatureFillet3((int)swFeatureFilletOptions_e.swFeatureFilletUniformRadius,radius,0,0,0,0,0,null,null,null,null,null,null,null);

        }

        public static void AddComponent(Pipe pipe, AnglePart anglePart)
        {
            SldWorks swApp;
            ModelDoc2 swModel;
            AssemblyDoc swAssy;
            object components;

            object compNames;
            object transformationMatrix;
            object coorSysNames;

            swApp = SolidWorksSingleton.GetApplication();

            swModel = (ModelDoc2)swApp.ActiveDoc;
            swAssy = (AssemblyDoc)swApp.ActiveDoc;

            string[] xcompnames = new string[2];

            xcompnames[0] = pipe.TargetFolder + "\\" + pipe.FileName + ".sldprt";
            xcompnames[1] = anglePart.TargetFolder + "\\" + anglePart.FileName + ".sldprt";

            string[] xcoorsysnames = new string[2];

            xcoorsysnames[0] = "Coordinate System1";
            xcoorsysnames[1] = "Coordinate System1";

            var tMatrix = new double[]
            {
                0,0,1,
                0,1,0,
                -1,0,0,

                pipe.Lenght,anglePart.YLenght,0,
                0,0,0,0,

                1,0,0,
                0,1,0,
                0,0,1,
                0,0,0,
                0,0,0,0,

            };
          

            

            compNames = xcompnames;
            coorSysNames = xcoorsysnames;
            transformationMatrix = tMatrix;

            components = swAssy.AddComponents3(compNames, transformationMatrix, coorSysNames);


            swAssy = (AssemblyDoc)swApp.ActiveDoc;
            string comp = anglePart.FileName + "-1@" + anglePart.AssemblyName;

            swModel.Extension.SelectByID2(comp, "COMPONENT", 0, 0, 0, false, 0, null, 0);
            swAssy.FixComponent();


        }


        public static void AddConcentricMate(string Comp1, string MateFace1, string Comp2, string MateFace2)
        {
            SldWorks swApp;
            ModelDoc2 swModel;
            PartDoc swPart;
            AssemblyDoc swAssy;
            Mate2 mate;
            Component2 swComponent;
            Entity swEntity;
            Entity swFace1;
            Entity swFace2;
            bool bRet;
            int errorCode1;


            swApp = SolidWorksSingleton.GetApplication();

            swAssy = (AssemblyDoc)swApp.ActiveDoc;

            swComponent = swAssy.GetComponentByName(Comp1 + "-1");
            swModel = swComponent.GetModelDoc2();
            swPart = (PartDoc)swModel;
            swEntity = swPart.GetEntityByName(MateFace1,(int)swSelectType_e.swSelFACES);
            swFace1 = swComponent.GetCorrespondingEntity(swEntity);


            swComponent = swAssy.GetComponentByName(Comp2 + "-1");
            swModel = swComponent.GetModelDoc2();
            swPart = (PartDoc)swModel;
            swEntity = swPart.GetEntityByName(MateFace2, (int)swSelectType_e.swSelFACES);
            swFace2 = swComponent.GetCorrespondingEntity(swEntity);

            bRet = swFace1.Select4(false,null);
            bRet = swFace2.Select4(true,null);

            mate = swAssy.AddMate3((int)swMateType_e.swMateCONCENTRIC,(int)swMateAlign_e.swMateAlignALIGNED,false,0,0,0,0,0,0,0,0,false,out errorCode1);

            swModel.ForceRebuild3(false);


        }


        public static void AddDistanceMate(string Comp1, string MateFace1, string Comp2, string MateFace2,double dimension)
        {
            SldWorks swApp;
            ModelDoc2 swModel;
            PartDoc swPart;
            AssemblyDoc swAssy;
            Mate2 mate;
            Component2 swComponent;
            Entity swEntity;
            Entity swFace1;
            Entity swFace2;
            bool bRet;
            int errorCode1;


            swApp = SolidWorksSingleton.GetApplication();

            swAssy = (AssemblyDoc)swApp.ActiveDoc;

            swComponent = swAssy.GetComponentByName(Comp1 + "-1");
            swModel = swComponent.GetModelDoc2();
            swPart = (PartDoc)swModel;
            swEntity = swPart.GetEntityByName(MateFace1, (int)swSelectType_e.swSelFACES);
            swFace1 = swComponent.GetCorrespondingEntity(swEntity);


            swComponent = swAssy.GetComponentByName(Comp2 + "-1");
            swModel = swComponent.GetModelDoc2();
            swPart = (PartDoc)swModel;
            swEntity = swPart.GetEntityByName(MateFace2, (int)swSelectType_e.swSelFACES);
            swFace2 = swComponent.GetCorrespondingEntity(swEntity);

            bRet = swFace1.Select4(false, null);
            bRet = swFace2.Select4(true, null);

            mate = swAssy.AddMate3((int)swMateType_e.swMateDISTANCE, (int)swMateAlign_e.swMateAlignALIGNED, false, dimension, dimension, dimension, 0, 0, 0, 0, 0, false, out errorCode1);

            swModel.ForceRebuild3(false);


        }



    }
}
