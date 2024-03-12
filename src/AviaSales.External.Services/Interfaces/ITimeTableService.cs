using AviaSales.External.Services.Models;

namespace AviaSales.External.Services.Interfaces;

/// <summary>
/// 
/// </summary>
public interface ITimeTableService
{
   /// <summary>
   /// 
   /// </summary>
   /// <param name="from"></param>
   /// <param name="to"></param>
   /// <param name="date"></param>
   /// <param name="count"></param>
   /// <returns></returns>
   Task<OTA_AirDetailsRS?> SearchFlights(string from,string to,string date,byte count);
}