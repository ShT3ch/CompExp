module Task3

open System
open Symbolic
open Definitions
open ComputationCore
open Task1
open TaskList

let myEstimatingHigherBoundOfIntegrating eps = 1./eps

let cutAreaOfIntegrating oldObjective eps = 
    { a = 1.
      b = myEstimatingHigherBoundOfIntegrating eps
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

let estimateStepOfSimpson exp accuracy taskObjective eps = 
    let workingDomain = cutAreaOfIntegrating taskObjective eps
    printf "domain: a = %3.5f; b = %3.5f; h = %3.5f\r\n" workingDomain.a workingDomain.b workingDomain.h
    let m4 = maxNthOrderDerivative exp 4 workingDomain accuracy
    printf "Max 4 derivative: %10.7f\r\n" (m4)
    let hsqsq = ((eps * 2880.) / ((workingDomain.b - workingDomain.a) * m4))
    printf "Squared step: %10.7f\r\n" (hsqsq)
    let estimatedH = Math.Pow(hsqsq, 1. / 4.)
    printf "Estimated step: %10.7f\r\n" (estimatedH)
    {a = workingDomain.a; b = workingDomain.b; h = estimatedH}

let estimatedStep = (estimateStepOfSimpson Poisoned.MyFunc3 1 objective3 0.0001);
writeSolution Poisoned.MyFunc3 estimatedStep

let domain = cutAreaOfIntegrating objective3 0.01
let derivative = nthOrderDerivative Poisoned.MyFunc3 4
split domain.a domain.b 0.1
|> Seq.map (fun segment-> segment.Right)
|> Seq.map (Compute  derivative)
|> Seq.max