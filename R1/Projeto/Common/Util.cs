using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public sealed class Util
    {
        /// <summary>
        /// Converte uma String de Formato DD/MM/AAAA para Datetime MM/DD/AAAA
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ConvertStringToDatetime(String date)
        {
            if (String.IsNullOrWhiteSpace(date))
            {
                throw new InvalidCastException("String Nula ou não tem dados");
            }

            DateTime Date = new DateTime();

            var temp = date.Split('/');
            Date = DateTime.Parse(temp[2] + "/" + temp[1] + "/" + temp[0]);
            return Date;
        }

        //FORMAT MM/DD/YYYY
        public static DateTime ConvertStringToDatetime2(String date)
        {
            if (String.IsNullOrWhiteSpace(date))
            {
                throw new InvalidCastException("String Nula ou não tem dados");
            }

            DateTime Date = new DateTime();

            var temp = date.Split('/');

            Date = new DateTime(int.Parse(temp[2]), int.Parse(temp[1]), int.Parse(temp[0]));
            return Date;
        }


        /// <summary>
        /// Retorna a lista de dias da semana, baseado na data inicial e o intervalo total de dias que será levado em consideração
        /// Ex.: 10/01/2012 e totalDays = 5, a rotina trabalhará sobre os dias? 10,11,12,13,14 do mês 01 do ano de 2012
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="totalDays"></param>
        /// <returns></returns>
        public static List<DayOfWeek> GetWeekDaysFromDate(DateTime startDate, int totalDays)
        {
            var weekends = new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
            List<DayOfWeek> daysOfWeek = new List<DayOfWeek>();
            for (int i = 0; i < totalDays; i++)
            {
                DateTime newDate = startDate.AddDays(i);

                switch (newDate.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                        break;
                    case DayOfWeek.Sunday:
                        break;
                    default:
                        daysOfWeek.Add(newDate.DayOfWeek);
                        break;
                }
            }

            return daysOfWeek;
        }

        /// <summary>
        /// Retorna o trecho HTML referênte ao Header de uma Table, que vai conter os dias da semana com as respectivas datas.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="totalDays"></param>
        /// <returns></returns>
        public static String GetTableHeaderWeekDaysFromDate(DateTime startDate, int totalDays)
        {
            var weekends = new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
            List<DayOfWeek> daysOfWeek = new List<DayOfWeek>();

            StringBuilder htmlTableHeader = new StringBuilder();

            String htmlTagHeader = "<th>{0}<br />{1}</th>";

            for (int i = 0; i < totalDays; i++)
            {
                DateTime newDate = startDate.AddDays(i);

                switch (newDate.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                       
                        break;
                    case DayOfWeek.Sunday:
                                                
                       
                        break;
                    case DayOfWeek.Monday:
                                                
                        htmlTableHeader.Append(String.Format(htmlTagHeader, "Segunda", newDate.ToString("dd/MM/yyyy")));
                        htmlTableHeader.Append(Environment.NewLine);
                        break;
                    case DayOfWeek.Thursday:
                                                
                        htmlTableHeader.Append(String.Format(htmlTagHeader, "Quinta", newDate.ToString("dd/MM/yyyy")));
                        htmlTableHeader.Append(Environment.NewLine);
                        break;
                    case DayOfWeek.Tuesday:
                                             
                        htmlTableHeader.Append(String.Format(htmlTagHeader, "Terça", newDate.ToString("dd/MM/yyyy")));
                        htmlTableHeader.Append(Environment.NewLine);
                        break;
                    case DayOfWeek.Wednesday:
                                            
                        htmlTableHeader.Append(String.Format(htmlTagHeader, "Quarta", newDate.ToString("dd/MM/yyyy")));
                        htmlTableHeader.Append(Environment.NewLine);
                        break;
                    case DayOfWeek.Friday:
                                               
                        htmlTableHeader.Append(String.Format(htmlTagHeader, "Sexta", newDate.ToString("dd/MM/yyyy")));
                        htmlTableHeader.Append(Environment.NewLine);
                        break;
                    default:
                        daysOfWeek.Add(newDate.DayOfWeek);
                        htmlTableHeader.Append(String.Format(htmlTagHeader, newDate.DayOfWeek.ToString(), newDate.ToString("dd/MM/yyyy")));
                        htmlTableHeader.Append(Environment.NewLine);

                        break;
                }
            }

            return htmlTableHeader.ToString();
        }

    }
}
