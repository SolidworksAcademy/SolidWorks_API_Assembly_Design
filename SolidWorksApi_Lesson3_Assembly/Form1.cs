using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidWorksApi_Lesson3_Assembly.Helpers;
using SolidWorksApi_Lesson3_Assembly.Parts;
using static SolidWorksApi_Lesson3_Assembly.Helpers.DocumentManager;

namespace SolidWorksApi_Lesson3_Assembly
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_CreateAssembly_Click(object sender, EventArgs e)
        {
            try
            {
                double h1 = UnitConverter.ConvertToMeter(txt_h1.Text);
                double w = UnitConverter.ConvertToMeter(txt_w.Text);
                double l1 = UnitConverter.ConvertToMeter(txt_l1.Text);
                double t1 = UnitConverter.ConvertToMeter(txt_t1.Text);
                double r1 = UnitConverter.ConvertToMeter(txt_r1.Text);
                double r2 = UnitConverter.ConvertToMeter(txt_r2.Text);
                double r3 = UnitConverter.ConvertToMeter(txt_r3.Text);
                double x1 = UnitConverter.ConvertToMeter(txt_x1.Text);
                double x2 = UnitConverter.ConvertToMeter(txt_x2.Text);
                double d3 = UnitConverter.ConvertToMeter(txt_d3.Text);
                double d1 = UnitConverter.ConvertToMeter(txt_d1.Text);
                double d2 = UnitConverter.ConvertToMeter(txt_d2.Text);
                double l2 = UnitConverter.ConvertToMeter(txt_l2.Text);
                double h2 = d1;
                double x3 = UnitConverter.ConvertToMeter(txt_x3.Text);


                Rules.DimensionCheck(r1, w / 4, "R1 radüsü çok büyük");
                Rules.DimensionCheck(2 * t1 + x3, l2, "L2 ölçüsü çok kısa, Lütfen parça boyunu uzatınınız...");


                string path = DocumentManager.CreateDir(txt_TargetFolder.Text);



                AnglePart a = new AnglePart();
                a.AssemblyName = txt_AssemblyName.Text;
                a.BoltHoles = d3 / 2;
                a.Rad1 = r1;
                a.Rad2 = r2;
                a.PipeHole = (d1 + 0.001) / 2;
                a.Thickness = t1;
                a.Width = w;
                a.XLenght = l1;
                a.YLenght = h1 + h2;
                a.X1 = x1;
                a.X2 = x2;
                a.MateRef1 = "Ref1";
                a.MateRefHole = "RefHole";
                a.TargetFolder = path;
                a.FileName = txt_Part1Name.Text;
                


                a.CreatePart();


                Pipe p = new Pipe();
                p.InsideDiameter = d2;
                p.OutsideDiameter = d1;
                p.Lenght = l2;
                p.TargetFolder = path;
                p.FileName = txt_Part2Name.Text;
                p.MateBase = "PipeFace";
                p.MateOutsideFace = "PipeOutsideFace";
                p.CreatePart();


                DocumentManager.CreateAssemblyDoc(path, txt_AssemblyName.Text);
                BasicOpertations.AddComponent(p, a);

                BasicOpertations.AddConcentricMate(a.FileName, a.MateRefHole, p.FileName, p.MateOutsideFace);

                BasicOpertations.AddDistanceMate(a.FileName, a.MateRef1, p.FileName, p.MateBase,a.Thickness);

                DocumentManager.Save(path,txt_AssemblyName.Text,sw_DocType.assembly);

                Process.Start("explorer.exe",path.ToString());

            }

            catch (FormatException)
            {
                MessageBox.Show("Sayısal olmayan bir değer girdiniz. Lütfen kontrol ediniz...");
            }


            catch (ArgumentNullException)
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz");
            }


            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu",ex.Message);
            }
        }

        private void btn_Target_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog target = new FolderBrowserDialog();
            target.ShowDialog();
            txt_TargetFolder.Text = target.SelectedPath;

        }
    }
}
