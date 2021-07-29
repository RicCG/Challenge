using DataModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataLayer
{
    public class Read
    {
        public List<Citie> ListCities { get; set; }
        public bool ReadData(string uri, out string error)
        {
            try
            {
                error = "";
                ListCities = new List<Citie>();
                string[] records = File.ReadAllLines(uri);
                for (int i = 1; i < records.Length; i++)
                {
                    Citie citie = new Citie();
                    string[] row = records[i].Split("\t");
                    citie.id = int.Parse(row[0]);
                    citie.name = row[1].ToString();
                    citie.ascii = row[2].ToString();
                    citie.alt_name = row[3].ToString();
                    citie.latitud = double.Parse(row[4]);
                    citie.longitud = double.Parse(row[5]);
                    citie.feat_class = row[6].ToString();
                    citie.population = int.Parse(row[14]);

                    ListCities.Add(citie);
                    //Console.WriteLine(records[i]);
                }

                return true;

            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
