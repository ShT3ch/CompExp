module Objectives

open Definitions

open MathNet.Symbolics

module Poisoned = 
    open Operators

    let y = symbol "y_i"
    let x = symbol "x_i"
    let α = symbol "α"
    let z = symbol "z"
    
    let y' = z
    let f = y+2*α+2+α*x*(1-x)

    let y0A = 0
    let e = (Expression.Exp Expression.One)
    let y1B N = (Evaluate.evaluate (Map.ofList ["α", αN N |> Real ]) (e-(Expression.One/e)+α)).RealValue


let leftBoundaryA = 
        {
            a = 0.0
            F1 = 0.0;
            C1 = 1.0;
            B1 = 0.0;
        }
let rightBoundaryB N = 
        {
            b = 1.0
            Fn = Poisoned.y1B N;
            An = 0.0;
            Cn = 1.0;
        }

let rightBoundaryE N h = 
        {
            b = 1.0
            Fn = (2.0*e+(αN N)-2.0)*h;
            An = -1.0;
            Cn = (1.0+h);
        }

let rightBoundaryG N h= 
        {
            b = 1.0
            Fn = (e - ((1.0) / e) + (αN N))*h;
            An = -1.0;
            Cn = (1.0);
        }


let originAE N x = -2.0+ e**(-x) + e**x+ (αN N)*(-1.0+x)*x
let originAG N x = (αN N)*x**2.0 - (αN N)*x + e**(-x) + e**x - 2.0


let originAB N x = 
    (1.0/(e**2.0-1.0))
    *(e**(-x))
    *(((αN N) + 2.0)*e**(2.0*x+1.0)+(e**x)*(2.0-(αN N)*(x-1.0)*x) + e**(x+2.0) * ((αN N)*(x-1.0)*x-2.0)
    - e*((αN N)+2.0) - 3.0*e**(2.0*x) + e**(2.0*x + 2.0) + e**2.0 + 1.0)    

let f1 x y z = z
let f2 N x y z = 
    (Evaluate.evaluate (
        Map.ofList [
            "y_i", y |> Real; 
            "x_i", x |> Real; 
            "z",   z |> Real; 
            "α",αN N |> Real]) Poisoned.f).RealValue
