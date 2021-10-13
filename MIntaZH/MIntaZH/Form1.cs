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

namespace MIntaZH
{
    public partial class Form1 : Form
    {
        List<OlympicResult> results = new List<OlympicResult>();
        public Form1()
        {
            InitializeComponent();
            Betölt("Summer_olympic_Medals.csv");
           Feltölt();
            Helyezes();
            
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
            int counter = 0;

            var FilteredList = (from r in results
                                where r.Year == or.Year && r.Country != or.Country
                                select r
                                );

            foreach(var r in FilteredList)
            {
                if (r.Medals[0] > or.Medals[0])
                    counter++;
                if ((r.Medals[0] == or.Medals[0]) && (r.Medals[1] == or.Medals[1]))
                    counter++;
                if((r.Medals[0] == or.Medals[0]) && (r.Medals[1] == or.Medals[1]) && (r.Medals[2] > or.Medals[2]))
                    counter++;
            }


            return CurrentPosition +1;
        }
        private void Helyezes()
        {
            foreach(var r in results)
            {
                r.Position = Helyezeskalk(r);
            }
        }

    }
}
