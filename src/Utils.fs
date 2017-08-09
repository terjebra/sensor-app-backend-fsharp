module Utils 

open Suave
open System.Text
open Suave.Operators

let getBody request =
  request.rawForm |> Encoding.UTF8.GetString

let createJsonMimeType body = 
  body >=> Writers.setMimeType "application/json; charset=utf-8"


let badRequest body=
  body >=> Writers.setStatus HTTP_401