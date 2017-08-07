module Common

open Result
open Newtonsoft.Json
open System

let deserialize<'a> string =
  JsonConvert.DeserializeObject<'a> string
 
let serialize obj =
  JsonConvert.SerializeObject obj

let createId  = System.Guid.NewGuid()

let currentTime = DateTime.Now
