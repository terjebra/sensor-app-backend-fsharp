module CommandTypes

open DtoTypes
open System

type TemperatureCommandAction = 
  | RegisterTemperatureReading of RegisterTemperatureDto

type TemperatureCommand  = {
  Id : Guid
  Action: TemperatureCommandAction
  TimeStamp: DateTime
}
