using DataLayer;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class ProcessCities
    {
        public const int _RADIO_TIERRA = 6371;
        public static List<Citie> getCities(string filter, out bool result, out string error)
        {
            List<Citie> Cities = new List<Citie>();
            result = false;
            error = "";
            try
            {
                Read data = new Read();
                if (data.ReadData("..\\netcoreapp3.1\\cities_canada-usa.tsv", out error))
                {
                    string[] filtros = filter.Split("&");
                    if (filtros.Length == 3)
                    {
                        string name = filtros[0].Split("=")[1];
                        double lon = double.Parse(filtros[1].Split("=")[1]);
                        double lat = double.Parse(filtros[2].Split("=")[1]);
                        
                        Cities = data.ListCities.Where(s => s.name.Contains(name)).ToList<Citie>();
                        if (Cities.Count > 0)
                        {
                            List<Citie> citiessort = new List<Citie>();
                            foreach (var item in Cities)
                            {
                                int meterofDistance = ProcessCities.ObtenDistanciaEnMetros(lat, lon, item.latitud, item.longitud);
                                item.distance = meterofDistance;
                            }

                            citiessort = (from citie in Cities 
                                          orderby citie.distance 
                                          select citie).ToList<Citie>();



                            foreach (var c in citiessort)
                            {
                                Console.WriteLine(c.name + "\t" + c.distance + "\t" + c.latitud + "\t" + c.longitud);
                            }

                            result = true;
                        }
                    }
                    else if (filtros.Length == 1)
                    {
                        string name = filtros[0].Split("=")[1];
                        Cities = data.ListCities.
                            Where(s => s.name.Contains(name))
                            .ToList<Citie>();
                        result = true;
                    }
                }
                else
                {
                    error = "No se lograron cargar los datos";
                    result = false;
                }

                return Cities;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }


        public static int ObtenDistanciaEnMetros(double latitud1, double longitud1, double latitud2, double longitud2)
        {
            double _distancia = 0;

            try
            {
                double _latitud = (latitud2 - latitud1) * (Math.PI / 180);
                double _longitud = (longitud2 - longitud1) * (Math.PI / 180);
                double _a = Math.Sin(_latitud / 2) * Math.Sin(_latitud / 2) + Math.Cos(latitud1 * (Math.PI / 180)) * Math.Cos(latitud2 * (Math.PI / 180)) * Math.Sin(_longitud / 2) * Math.Sin(_longitud / 2);
                double _c = 2 * Math.Atan2(Math.Sqrt(_a), Math.Sqrt(1 - _a));

                _distancia = (_RADIO_TIERRA * _c) * 1000;
            }
            catch (Exception)
            {
                _distancia = -1;
            }

            return (int)Math.Round(_distancia, MidpointRounding.AwayFromZero);
        }

    }
}
