module Utilities

open FSharp.Charting

let domainSplitter leftBound rightBound fineness = 
    if (leftBound > rightBound) then failwith "leftBound > rightBound"
    let step = (rightBound - leftBound) / (fineness |> float)
    (step, seq { leftBound..step..rightBound })

let drawLineEuler name (yBC_seq, xBC_seq)  = 
    Chart.Line(Seq.zip xBC_seq yBC_seq, name)
    