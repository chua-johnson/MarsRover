using MarsRoverService.Models;
using MarsRoverService.Service;
using MarsRoverService.Service.Interface;
using NUnit.Framework;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Tests
{
    public class MarsRoverAPITest
    {
        private string _date = null;
        private INasaService _nasaService = null;


        [SetUp]
        public void Setup()
        {
            _date = "June 2, 2018";
            _nasaService = new NasaService();
        }

        [Test]
        public void GetNASA_AP_IResponseTEST()
        {
            NasaInfo nasaInfo = _nasaService.GetNASAInfo(_date);
            Assert.IsNotNull(nasaInfo);

        }

        [Test]
        public void ValidDateTEST()
        {
            Assert.IsTrue(_nasaService.IsValidDate(_date));
        }

        [Test]
        public void InvalidDateTEST()
        {
            Assert.IsFalse(_nasaService.IsValidDate("2019-13-33"));
        }

        [Test]
        public void ConvertDateToYYYY_MM_DD_TEST()
        {
            string actualDate = _nasaService.ConvertDateToYYYYMMDD("2-22-2019","yyyy-MM-dd");
            string expectedDateFormat = "2019-02-22";
            Assert.AreEqual(expectedDateFormat,actualDate);
        }

        private string replaceBackslash(string date)
        {
            date = date.Replace('/', '-');
            return date;
        }

        [Test]
        public async Task GetNasaInfoAsyncTEST()
        {
            string date = "2/20/18";
            date = replaceBackslash(date);
            NasaInfo nasaInfo = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44374/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = await client.GetAsync($"api/nasa/{date}");
                if (response.IsSuccessStatusCode)
                {
                     nasaInfo = await response.Content.ReadAsAsync<NasaInfo>();
                }
            }
            Assert.IsNotNull(nasaInfo);
        }
    }
}