module DbTypes

open System

type Temperature = {
  Id: Guid
  Temperature: decimal
  Registered: DateTime
}