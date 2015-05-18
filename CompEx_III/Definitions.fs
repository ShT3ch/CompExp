module Definitions

type LeftBoundaryCondition  = 
    {
        a:float
        C1:float
        B1:float
        F1:float
    }

type RightBoundaryCondition  = 
    {
        b:float
        An:float
        Cn:float
        Fn:float
    }

let αN N = 2.0+0.1*N 
let e = exp 1.0
