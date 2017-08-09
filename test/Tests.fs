module Tests

open System
open Xunit
open DtoTypes
open CommonTypes
[<Fact>]
let ``Successfully create domain object from DTO`` () =
    
    let dto: RegisterTemperatureDto = {Temperature =  23.4; TimeStamp = "2017-07-08 12:00:00"}

    let result = DtoTypes.TemperatureDto.toDomain dto

    match result with
    | Ok domain-> Assert.True(true)
    | Error list -> Assert.False(true)

[<Fact>]
let ``Failing to create domain object from DTO`` () =
    
    let dto: RegisterTemperatureDto = {Temperature =  23.4; TimeStamp = ""}

    let result = DtoTypes.TemperatureDto.toDomain dto
    
    match result with
        | Error list ->    match  (List.first list)  with
                                | Some error ->  
                                    match error with 
                                    | InvalidDateError msg -> Assert.True(true)
                                    | _ -> Assert.True(false)
                                | None -> Assert.True(false)
        | _ ->  Assert.True(false) 

  

        