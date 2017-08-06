module CommandTypes
open System

type RegisterTemperatureReading = {
  Temperature: float
  TimeStamp: string
}

type TemperatureCommandAction = 
  | RegisterTemperatureReading of RegisterTemperatureReading

type TemperatureCommand  = {
  Id : Guid
  Action: TemperatureCommandAction
  TimeStamp: DateTime
}
