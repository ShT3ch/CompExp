module AproximationStuff

type LeftBoundaryCondition  = 
    {
        C1:float
        B1:float
        a:float
    }

type RightBoundaryCondition  = 
    {
        An:float
        Cn:float
        b:float
    }

let pimenovApproximation pi qi rightBoundary leftBoundary h n = 
    let A i = 
        match i with
        | _ when (i=n) -> rightBoundary.An
        | k -> 1.0
    let B i = 
        match i with 
        | _ when (i=0) -> leftBoundary.B1
        | k -> 1.0
    let C i = 
        match i with
        | _ when (i=0) -> leftBoundary.C1
        | _ when (i=n) -> rightBoundary.Cn
        | k -> -(2.0+(pi k)*h**2.0)
    let F i =
        match i with
        | _ when (i=0) -> leftBoundary.a
        | _ when (i=n) -> rightBoundary.b
        | k -> (qi k)*h**2.0
    (A,B,C,F)
        