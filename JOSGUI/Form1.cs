using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace JOSGUI
{
    public partial class Form1 : Form
    {
        public JOS.Multiplet exp;
        public JOS.Solver solver;
        public Form1()
        {
            InitializeComponent();
            exp = new JOS.Multiplet();
            solver = new JOS.Solver();
        }

        private void OpenFileButton_click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                exp.LoadFromFile(openFileDialog1.FileName);
                textBox1.Text += "Loaded file " + openFileDialog1.FileName + "\r\n";
                textBox1.Text += "n= " + exp.n + " 2J+1= " + exp.TwoJPlusOne + "\r\n";
                for (int i = 0; i < exp.u2.Count; i++) { textBox1.Text += (1.0 / exp.lambda0[i]).ToString("G6") + " " + exp.fexp[i].ToString("g4") + "  " + exp.u2[i] + "  " + exp.u4[i] + "  " + exp.u6[i] + "\r\n"; };
            }
        }

        private void FitButton_click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            solver.FitLM(exp, out string MSG, out string latex);
            Console.WriteLine(sw.ElapsedMilliseconds);
            textBox1.Text += MSG;
            textBox2.Text += latex;
            string uniq = DateTime.Now.ToString("yyyyMMddHHmmss");
            System.IO.File.WriteAllText("abso" + uniq + exp.absofilename + ".log", textBox1.Text);
        }

        private void RatesButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                int numFiles = openFileDialog2.FileNames.Length;
                for (int i = 0; i < numFiles; i++)
                {
                    //string msg = "";
                    //string latex = "";
                    textBox1.Text += "Loading file " + openFileDialog2.FileNames[i] + "\r\n";
                    JOS.Multiplet EmiMult = new JOS.Multiplet();
                    EmiMult.GetOmegas(exp);
                    EmiMult.LoadEmiFromFile(openFileDialog2.FileNames[i]);
                    EmiMult.CalculateRates();
                    EmiMult.DumpEmidata(out string msg);
                    textBox1.Text += msg;
                    EmiMult.ReportRates(out msg, out string latex);
                    textBox1.Text += msg + "\r\n";
                    textBox2.Text += latex + "\r\n";
                    Console.WriteLine(msg);
                }
                string uniq = DateTime.Now.ToString("yyyyMMddHHmmss");
                System.IO.File.WriteAllText("emi" + uniq + exp.absofilename + ".log", textBox1.Text);
            }
        }

        private void OpenFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
