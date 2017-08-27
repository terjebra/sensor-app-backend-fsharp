module EventTypes

open System
open DomainTypes
open Common

type NewTemperatureEvent ={
  Id : Guid
  Temperature: float
  Timestamp: string
}
   
type TemperatureEvent =
  | NewTemperature of NewTemperatureEvent


let toNewTemperatureEvent (reading: TemperatureReading) : TemperatureEvent  = 
    let (ReadingId id ) = reading.Id
    let (Temperature  temperature) = reading.Temperature
    let (RegisteredTime registered) = reading.RegisteredAt

    NewTemperature {Id = id; Temperature = temperature; Timestamp = (toISO8601 registered)}

