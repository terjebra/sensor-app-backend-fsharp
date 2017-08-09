module QueryTypes
open DomainTypes

type GetTemperaturesQuery = {
  Date: Option<string>
}

type TemperatureQuery = 
 | GetTemperaturesQuery of GetTemperaturesQuery

type QueryResult =
 | Temperature of TemperatureReading
 | Temperatures of TemperatureReading list