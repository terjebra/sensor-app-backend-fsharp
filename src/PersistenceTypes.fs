
module PersistenceTypes

open DomainTypes

type GetTemperature = ReadingId -> TemperatureReading

type SaveTemperature = TemperatureReading ->  TemperatureReading

type DbConnection = string