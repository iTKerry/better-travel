module Tours.API.Domain.Queries

type Query =
    | GetDirections
    | GetResorts of directionId : int