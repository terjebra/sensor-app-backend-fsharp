module EventPublisher

open EventTypes
type EventPublisher<'Event>() = 
  let event =  new Event<'Event>()
  member this.NewEvent = 
    event.Publish
  member this.Notify(newEvent) =
     event.Trigger (newEvent)

  
