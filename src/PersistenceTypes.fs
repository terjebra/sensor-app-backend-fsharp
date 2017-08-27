
module PersistenceTypes
open System

open DomainTypes

type GetTemperatures = Option<string> -> TemperatureReading list

type SaveTemperature = TemperatureReading ->  TemperatureReading

type DbConnection = string