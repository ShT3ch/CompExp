module Utilities

open Symbolic
open FSharp.Charting

let domainSplitter leftBound rightBound fineness = 
    if (leftBound > rightBound) then failwith "leftBound > rightBound"
    let step = (rightBound - leftBound) / (fineness |> float)
    (step, seq { leftBound..step..rightBound })

let drawLineEuler name (yBC_seq, xBC_seq)  = 
    Chart.Line(Seq.zip xBC_seq yBC_seq, name)

let rec getNthMemberOfTaylor order exp h = 
    match order with
    | 0 -> exp
    | n -> 
        Add(
            exp,
            Mul( 
                Div(
                    Const(h), 
                    Const(order |> float)
                    ), 
               getNthMemberOfTaylor (order-1) (Derivative exp) h)
           )
let addConst y_i exp = 
    Add(Const(y_i), exp)
    