module Socket

open Suave  
open Suave.WebSocket
open Suave.Sockets
open Suave.Sockets.Control
open System.Text
open System
open EventPublisher
open Common
open EventTypes

let webSocket (eventPublisher:EventPublisher<TemperatureEvent>)  (webSocket : WebSocket) (context: HttpContext) =
    
    let cb ev =
        let data = 
            match ev with
            | NewTemperature data ->
                data
                |> serialize
                |> Encoding.ASCII.GetBytes
                |> ByteSegment
            | _ ->
               ""
                |> Encoding.ASCII.GetBytes
                |> ByteSegment
        webSocket.send Opcode.Text data true |> Async.Ignore |> Async.Start

    let subscription = eventPublisher.NewEvent.Subscribe(cb)

    socket {
      let mutable loop = true
      while loop do
          let! msg = webSocket.read()
          match msg with
          | (Text, data, true) ->
                let str =  Encoding.Unicode.GetString(data, 0, data.Length)
                let response = sprintf "response to %s" str
                let byteResponse =
                    response
                    |> System.Text.Encoding.ASCII.GetBytes
                    |> ByteSegment
                do! webSocket.send Text byteResponse true
          | (Close, _, _) ->
              do! webSocket.send Close ([||] |> ByteSegment) true
              loop <- false
              subscription.Dispose()
          | _ -> ()
      }