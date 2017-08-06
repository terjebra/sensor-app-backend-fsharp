module CommandHandler 
open CommandTypes
open DomainTypes
open PersistenceTypes
open System


let registerTemperatureReading (saveTemperature: SaveTemperature) id (data:RegisterTemperatureReading) =
  let reading = {
    Id = (ReadingId id)
    Temperature = (Temperature data.Temperature)
    RegisteredAt  = (RegisteredTime DateTime.Now)
  }

  saveTemperature reading
  reading

let commandhandler 
   (saveTemperature: SaveTemperature)
   (command: TemperatureCommand) 
  : TemperatureReading =

  match command.Action with
    | RegisterTemperatureReading data ->
      registerTemperatureReading saveTemperature command.Id data