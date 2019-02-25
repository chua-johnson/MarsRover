using MarsRoverService.Models;
using MarsRoverService.Service.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MarsRoverService.Service
{
    public class NasaService : INasaService
    {
        public string ConvertDateToYYYYMMDD(string date,string dateFormat)
        {
            
            //CultureInfo MyCultureInfo = new CultureInfo("en-US");
            //date = "Friday, April 10, 2009";
            //date = "June 2, 2018";
            //date = "Jul-13-2016";
            //date = "April 31, 2018";
            //date = "02/25/2017";
            
            //DateTime MyDateTime = DateTime.Parse(date, CultureInfo.CreateSpecificCulture("fr-FR"));
            //return MyDateTime.ToString("yyyy-MM-dd");

            string format = "MM-dd-yyyy";
            var formatInfo = new DateTimeFormatInfo()
            {
                ShortDatePattern = format
                //YearMonthPattern = format
            };

            DateTime convertedDate = Convert.ToDateTime(date,formatInfo);
            return convertedDate.ToString(dateFormat);
        }

        public NasaInfo GetNASAInfo(string date)
        {
            NasaInfo nasaInfo = null;
            string jsonString = null;
            if(IsValidDate(date))
            {
                date = ConvertDateToYYYYMMDD(date, "yyyy-MM-dd");
                jsonString = connectToNASAApi(date);
                nasaInfo = JsonConvert.DeserializeObject<NasaInfo>(jsonString);
            }

            return nasaInfo;
        }

        private string connectToNASAApi(string date)
        {
            string url = $"https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY&date={date}";
            WebRequest requestGet = WebRequest.Create(url);
            requestGet.Method = "GET";
            HttpWebResponse responseGet = (HttpWebResponse)requestGet.GetResponse();

            string jsonString = null;
            using (Stream stream = responseGet.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
                reader.Close();
            }
            return jsonString;
        }

        public bool IsValidDate(string date)
        {
            //DateTime tempDate;

            //if (DateTime.TryParse(date, out tempDate) == true)
            //    return true;
            //else
            //    return false;
            bool isValidDate = false;
            try
            {
                string format = "MM-dd-yyyy";
                var formatInfo = new DateTimeFormatInfo()
                {
                    ShortDatePattern = format
                    //YearMonthPattern = format
                };

                DateTime convertedDate = Convert.ToDateTime(date, formatInfo);

                if(!Object.ReferenceEquals(convertedDate,null))
                {
                    isValidDate = true;
                }
                return isValidDate;

            }
            catch
            {
                isValidDate = false;
                return isValidDate;
            }
        }
    }
}
