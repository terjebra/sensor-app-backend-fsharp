module CommonTypes

type Result<'Success, 'Failure> =
  | OK of 'Success
  | Error of 'Failure