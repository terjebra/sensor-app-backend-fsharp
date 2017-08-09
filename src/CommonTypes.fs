module CommonTypes
open System

type Error =  {
  Type: string
  Message: string
}

type ValidationError = string
type InvalidDateError = string

type TemperatureError = 
  | ValidationError of ValidationError
  | InvalidDateError of InvalidDateError