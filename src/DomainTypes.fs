module DomainTypes

open System

type ReadingId = ReadingId of Guid
type Temperature  =  Temperature of float
type RegisteredTime = RegisteredTime of DateTime

type TemperatureReading = {
  Id: ReadingId
  RegisteredAt: RegisteredTime
  Temperature: Temperature
}

