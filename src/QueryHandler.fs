module QueryHandler

open PersistenceTypes
open QueryTypes
open CommonTypes
open Common
open System
let handleGetTemperatures temperatures = 
  Temperatures temperatures

let queryhandler
   (getTemperatures: GetTemperatures)
   (query: TemperatureQuery) 
  : QueryResult =  
    match query with
      | GetTemperaturesQuery parameter ->  
        handleGetTemperatures (getTemperatures (parameter.Date))