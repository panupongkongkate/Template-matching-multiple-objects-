using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using System.Diagnostics;

namespace testmatch
{
    public partial class Form1 : Form
    {
        OpenFileDialog op1 = new OpenFileDialog();
        Image<Bgr, byte> source = null;
        Image<Bgr, byte> template = null;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (op1.ShowDialog() == DialogResult.OK)
            {
                source = new Image<Bgr, byte>(op1.FileName);
                imageBox1.Image = source;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (op1.ShowDialog() == DialogResult.OK)
            {
                template = new Image<Bgr, byte>(op1.FileName);
                imageBox2.Image = template;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((source != null) && (template != null))
            {

                List<Rectangle> rects = new List<Rectangle>();

                Image<Bgr, byte> imageToShow = source.Copy();

             


                using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
                {
                    //แปลงภาพเทา
                    // Image<Gray, byte> result = source.Convert<Gray, byte>();

                    //ทำการ MatchTemplate 

                    //CvInvoke.MatchTemplate(source, template, result, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);


                    //กำหนดค่า min max ของภาพ
                    double[] minValues, maxValues;

                    Point[] minLocations, maxLocations;

                    //หาค่าmin max ของภาพ 1 
                    result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);





                    //ทำการวาดภาพ

                    //// You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.

                    float[,,] match00 = result.Data;
                    for (int x = 0; x < match00.GetLength(1); x++)
                    {
                        for (int y = 0; y < match00.GetLength(0); y++)
                        {
                            double matchsouce = match00[y, x, 0];
                            if (matchsouce > 0.9)
                            {
                                Rectangle bect = new Rectangle(new Point(x, y), template.Size);
                                imageToShow.Draw(bect, new Bgr(Color.Red), 5);

                                rects.Add(bect);

                            }

                        }


                        /*
                         if (maxValues[0] > threshold)
                         {

                             Rectangle match = new Rectangle(maxLocations[0] , template.Size);
                             CvInvoke.Rectangle(imageToShow, match, new MCvScalar(0,0,255), 5);
                             imageBox3.Image = imageToShow;

                         }
                         */


                    }
                    imageBox3.Image = imageToShow;


















                }

            }
        }

        private void imageBox3_Click(object sender, EventArgs e)
        {

        }
    }
}

