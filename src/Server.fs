module Server

open Suave 
open Suave.WebSocket
open Suave.Sockets
open Suave.Filters 
open Suave.CORS
open Suave.Operators

open Api
open Socket

open EventPublisher
open EventTypes
let app eventPublisher =
  choose
      [ 
        path "/websocket" >=> handShake (webSocket eventPublisher)
        path "/temperatures" >=> GET >=> cors defaultCORSConfig >=> request(getTemperatures)
        path "/temperatures" >=> POST >=> cors defaultCORSConfig >=> request( (registerTemperature eventPublisher))
      ]   
[<EntryPoint>]
let main argv =
    let eventPublisher = new EventPublisher<TemperatureEvent>()
    startWebServer defaultConfig (app eventPublisher)
    0