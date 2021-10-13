using MIntaZH.Mappa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace MIntaZH
{
    public partial class Form1 : Form
    {
        List<OlympicResult> results = new List<OlympicResult>();
        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;
        public Form1()
        {
            InitializeComponent();
            Betölt("Summer_olympic_Medals.csv");
            Feltölt();
            Helyezes();
            button1.Text = Resource1.Add;
            

        }

        private void Betölt(string fileName)
        {
            using (var sr = new StreamReader(fileName, Encoding.Default))
            {
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(',');
                    var or = new OlympicResult();

                    var medals = new int[3];

                    medals[0] = int.Parse(line[5]);
                    medals[1] = int.Parse(line[6]);
                    medals[2] = int.Parse(line[7]);

                    or.Year = int.Parse(line[0]);
                    or.Country = line[3];
                    or.Medals = medals;

                    results.Add(or);

                }
            }
        }
        private void Feltölt()
        {
            var years = (from r in results
                         orderby r.Year
                         select r.Year).Distinct();
            comboBox1.DataSource = years.ToList();
        }

        private int Helyezeskalk(OlympicResult or)
        {
            var CurrentPosition = 0;
            

            var FilteredList = (from r in results
                                where r.Year == or.Year && r.Country != or.Country
                                select r
                                );

            foreach (var r in FilteredList)
            {
                if (r.Medals[0] > or.Medals[0])
                    CurrentPosition++;
                if ((r.Medals[0] == or.Medals[0]) && (r.Medals[1] == or.Medals[1]))
                    CurrentPosition++;
                if ((r.Medals[0] == or.Medals[0]) && (r.Medals[1] == or.Medals[1]) && (r.Medals[2] > or.Medals[2]))
                    CurrentPosition++;
            }


            return CurrentPosition + 1;
        }
        private void Helyezes()
        {
            foreach (var r in results)
            {
                r.Position = Helyezeskalk(r);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;

                CreateExcel();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;

            
         
    }
    }
     private void CreateExcel()
        {
            
            var headers = new string[]
            {
                "Helyezés",
                "Ország",
                "Arany",
                "Ezüst",
                "Bronz"
            };
            for (int i = 0; i < headers.Length; i++) { 
                xlSheet.Cells[1, i + 1] = headers[i];
                   }
            var FilteredResults = (from r in results
                                   where r.Year == (int)comboBox1.SelectedItem
                                   orderby r.Position
                                   select r
                                ) ;
            int db = 2;
            foreach(var r in FilteredResults)
            {
                xlSheet.Cells[db, 1] = r.Position;
                xlSheet.Cells[db, 2] = r.Country;
                xlSheet.Cells[db, 3] = r.Medals[0];
                xlSheet.Cells[db, 4] = r.Medals[1];
                xlSheet.Cells[db, 5] = r.Medals[2];
                db++;
            }
        }
    }
}
