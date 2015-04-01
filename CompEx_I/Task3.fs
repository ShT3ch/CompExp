module Task3

open System
open Symbolic
open Definitions
open ComputationCore
open Task1
open TaskList

let cutAreaOfIntegrating boundEstimator oldObjective eps = 
    { a = oldObjective.a
      b = boundEstimator eps
      h = 1. }

let rec nthOrderDerivative exp n = 
    match n with
    | 0 -> exp
    | x -> nthOrderDerivative (Derivative exp) (x-1) 

let maxNthOrderDerivative exp n objective accuracy = 
    let derivative = nthOrderDerivative exp n
    split objective.a objective.b accuracy
    |> Seq.map (fun segment-> segment.Right)
    |> Seq.map(Compute derivative)
    |> Seq.max

let estimateStepOfSimpson exp boundEstimator accuracy taskObjective eps = 
    let workingDomain = cutAreaOfIntegrating boundEstimator taskObjective eps
    printf "domain: a = %3.5f; b = %3.5f; h = %3.5f\r\n" workingDomain.a workingDomain.b workingDomain.h
    let m4 = maxNthOrderDerivative exp 4 workingDomain accuracy
    printf "Max 4 derivative: %10.7f\r\n" (m4)
    let hsqsq = ((eps * 2880.) / ((workingDomain.b - workingDomain.a) * m4))
    printf "Squared step: %10.7f\r\n" (hsqsq)
    let estimatedH = Math.Pow(hsqsq, 1. / 4.)
    printf "Estimated step: %10.7f\r\n" (estimatedH)
    {a = workingDomain.a; b = workingDomain.b; h = estimatedH}

let eps = 0.005
let derivativeAccuracy = 0.001
let boundEstimator = IIIEstimatingHigherBoundOfIntegrating
let func =  Poisoned.IIIFunc3

let estimatedStep = (estimateStepOfSimpson func boundEstimator derivativeAccuracy commonObjective3 eps);
writeSolution func estimatedStep