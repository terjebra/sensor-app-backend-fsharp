
module PersistenceTypes

open DomainTypes

type GetTemperature = ReadingId -> TemperatureReading
type SaveTemperature = TemperatureReading -> unit

type DbConnection = string