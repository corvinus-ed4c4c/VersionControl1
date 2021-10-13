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
           // Feltölt();
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

        
    }
}
