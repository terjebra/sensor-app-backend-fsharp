module DbTypes

open System

type Temperature = {
  Id: Guid
  Temperature: float
  Registered: DateTime
}