using MarsRoverService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverService.Service.Interface
{
    public interface INasaService
    {
        bool IsValidDate(string date);
        string ConvertDateToYYYYMMDD(string date,string dateFormat);

        NasaInfo GetNASAInfo(string date);
    }
}
