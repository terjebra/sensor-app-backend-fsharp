module Common
open Result
open Newtonsoft.Json

let deserialize<'a> string =
  JsonConvert.DeserializeObject<'a> string
 
let serialize obj =
  JsonConvert.SerializeObject obj
