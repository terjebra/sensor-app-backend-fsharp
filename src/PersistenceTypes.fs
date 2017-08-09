
module PersistenceTypes
open System

open DomainTypes

type GetTemperatures = Option<DateTime> -> TemperatureReading list

type SaveTemperature = TemperatureReading ->  TemperatureReading

type DbConnection = string