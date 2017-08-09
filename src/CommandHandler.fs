module CommandHandler 

open System

open CommandTypes
open DomainTypes
open CommonTypes
open PersistenceTypes
open Common


let saveTemperatureReading (saveTemperature: SaveTemperature) (result: Result<TemperatureReading, TemperatureError list>) =  
  saveTemperature <!> result

let commandhandler 
   (saveTemperature: SaveTemperature)
   (command: TemperatureCommand) 
  : Result<TemperatureReading, TemperatureError list> =

  match command.Action with
    | RegisterTemperatureReading data ->
      saveTemperatureReading saveTemperature (DtoTypes.TemperatureDto.toDomain data)