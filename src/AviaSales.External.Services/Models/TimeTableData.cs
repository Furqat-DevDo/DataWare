using System.Xml.Serialization;

namespace AviaSales.External.Services.Models;

[XmlRoot(ElementName="Airport", Namespace="http://www.opentravel.org/OTA/2003/05")]
public class Airport {
    [XmlAttribute(AttributeName="CodeContext")]
    public string CodeContext { get; set; }

    [XmlAttribute(AttributeName="FLSDayIndicator")]
    public string FLSDayIndicator { get; set; }

    [XmlAttribute(AttributeName="FLSLocationName")]
    public string FLSLocationName { get; set; }

    [XmlAttribute(AttributeName="LocationCode")]
    public string LocationCode { get; set; }

    [XmlAttribute(AttributeName="Terminal")]
    public string Terminal { get; set; }
}

[XmlRoot(ElementName="MarketingAirline", Namespace="http://www.opentravel.org/OTA/2003/05")]
public class MarketingAirline : Airport {
    [XmlAttribute(AttributeName="Code")]
    public string Code { get; set; }

    [XmlAttribute(AttributeName="CompanyShortName")]
    public string CompanyShortName { get; set; }
}

[XmlRoot(ElementName="OperatingAirline", Namespace="http://www.opentravel.org/OTA/2003/05")]
public class OperatingAirline : Airport {
    [XmlAttribute(AttributeName="Code")]
    public string Code { get; set; }

    [XmlAttribute(AttributeName="CompanyShortName")]
    public string CompanyShortName { get; set; }

    [XmlAttribute(AttributeName="FlightNumber")]
    public string FlightNumber { get; set; }
}

[XmlRoot(ElementName="FlightLegDetails", Namespace="http://www.opentravel.org/OTA/2003/05")]
public class FlightLegDetails {
    [XmlElement(ElementName="DepartureAirport", Namespace="http://www.opentravel.org/OTA/2003/05")]
    public Airport DepartureAirport { get; set; }

    [XmlElement(ElementName="ArrivalAirport", Namespace="http://www.opentravel.org/OTA/2003/05")]
    public Airport ArrivalAirport { get; set; }

    [XmlElement(ElementName="MarketingAirline", Namespace="http://www.opentravel.org/OTA/2003/05")]
    public MarketingAirline MarketingAirline { get; set; }

    [XmlAttribute(AttributeName="ArrivalDateTime")]
    public string ArrivalDateTime { get; set; }

    [XmlAttribute(AttributeName="DepartureDateTime")]
    public string DepartureDateTime { get; set; }

    [XmlAttribute(AttributeName="FLSArrivalTimeOffset")]
    public string FLSArrivalTimeOffset { get; set; }

    [XmlAttribute(AttributeName="FLSDepartureTimeOffset")]
    public string FLSDepartureTimeOffset { get; set; }

    [XmlAttribute(AttributeName="FLSUUID")]
    public string FLSUUID { get; set; }

    [XmlAttribute(AttributeName="FlightNumber")]
    public string FlightNumber { get; set; }

    [XmlElement(ElementName="OperatingAirline", Namespace="http://www.opentravel.org/OTA/2003/05")]
    public OperatingAirline OperatingAirline { get; set; }
}

[XmlRoot(ElementName="FlightDetails", Namespace="http://www.opentravel.org/OTA/2003/05")]
public class FlightDetails {
    [XmlElement(ElementName="FlightLegDetails", Namespace="http://www.opentravel.org/OTA/2003/05")]
    public List<FlightLegDetails> FlightLegDetails { get; set; }

    [XmlAttribute(AttributeName="FLSArrivalCode")]
    public string FLSArrivalCode { get; set; }

    [XmlAttribute(AttributeName="FLSArrivalDateTime")]
    public string FLSArrivalDateTime { get; set; }

    [XmlAttribute(AttributeName="FLSArrivalName")]
    public string FLSArrivalName { get; set; }

    [XmlAttribute(AttributeName="FLSArrivalTimeOffset")]
    public string FLSArrivalTimeOffset { get; set; }

    [XmlAttribute(AttributeName="FLSDayIndicator")]
    public string FLSDayIndicator { get; set; }

    [XmlAttribute(AttributeName="FLSDepartureCode")]
    public string FLSDepartureCode { get; set; }

    [XmlAttribute(AttributeName="FLSDepartureDateTime")]
    public string FLSDepartureDateTime { get; set; }

    [XmlAttribute(AttributeName="FLSDepartureName")]
    public string FLSDepartureName { get; set; }

    [XmlAttribute(AttributeName="FLSDepartureTimeOffset")]
    public string FLSDepartureTimeOffset { get; set; }

    [XmlAttribute(AttributeName="FLSFlightDays")]
    public string FLSFlightDays { get; set; }

    [XmlAttribute(AttributeName="FLSFlightLegs")]
    public string FLSFlightLegs { get; set; }

    [XmlAttribute(AttributeName="FLSFlightType")]
    public string FLSFlightType { get; set; }

    [XmlAttribute(AttributeName="TotalFlightTime")]
    public string TotalFlightTime { get; set; }

    [XmlAttribute(AttributeName="TotalMiles")]
    public string TotalMiles { get; set; }

    [XmlAttribute(AttributeName="TotalTripTime")]
    public string TotalTripTime { get; set; }
}

[XmlRoot(ElementName="OTA_AirDetailsRS", Namespace="http://www.opentravel.org/OTA/2003/05")]
public class OTA_AirDetailsRS {
    [XmlElement(ElementName="FlightDetails", Namespace="http://www.opentravel.org/OTA/2003/05")]
    public List<FlightDetails> FlightDetails { get; set; }
}

