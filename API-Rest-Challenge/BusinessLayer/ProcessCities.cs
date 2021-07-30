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
        public static Suggestions getCities(string uri, string name, out string error, out bool result,
            double? lat = null, double? lon = null)
        {
            List<Citie> Cities = new List<Citie>();
            List<object> resultado = new List<object>();
            Suggestions s = new Suggestions();
            result = false;
            error = "";
            try
            {
                Read data = new Read();
                if (data.ReadData(uri, out error))
                {
                    if (lat != null && lon != null)
                    {

                        Cities = data.ListCities.Where(s => s.name.Contains(name)).ToList<Citie>();
                        if (Cities.Count > 0)
                        {
                            List<Citie> citiessort = new List<Citie>();
                            foreach (var item in Cities)
                            {
                                int meterofDistance =
                                    ProcessCities.ObtenDistanciaEnMetros(lat, lon, item.latitud, item.longitud);
                                item.distance = meterofDistance;
                            }

                            resultado = (from citie in Cities
                                         orderby citie.distance
                                         select new
                                         {
                                             name = citie.name,
                                             latitud = citie.latitud,
                                             longitud = citie.longitud,
                                             score = 0.0
                                         }).ToList<object>();


                            foreach (var c in citiessort)
                            {
                                Console.WriteLine(c.name + "\t" + c.distance + "\t" + c.latitud + "\t" + c.longitud);
                            }

                            result = true;
                        }
                    }
                    else
                    {
                        Cities = data.ListCities.
                            Where(s => s.name.Contains(name))
                            .ToList<Citie>();

                        resultado = (from citie in Cities
                                     orderby citie.distance
                                     select new
                                     {
                                         name = citie.name,
                                         latitud = citie.latitud,
                                         longitud = citie.longitud,
                                         score = 0.0
                                     }).ToList<object>(); result = true;
                    }
                }
                else
                {
                    error = "No se lograron cargar los datos";
                    result = false;
                }

                s.suggestions = resultado;

                return s;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }


        public static int ObtenDistanciaEnMetros(double? latitud1, double? longitud1, double latitud2, double longitud2)
        {
            double _distancia = 0;

            try
            {
                double _latitud = (double)(latitud2 - latitud1) * (Math.PI / 180);
                double _longitud = (double)(longitud2 - longitud1) * (Math.PI / 180);
                double _a = Math.Sin(_latitud / 2) * Math.Sin(_latitud / 2) + (double)Math.Cos((double)latitud1 * (Math.PI / 180)) * Math.Cos(latitud2 * (Math.PI / 180)) * Math.Sin(_longitud / 2) * Math.Sin(_longitud / 2);
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
