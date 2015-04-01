module TaskList

open Symbolic
open DesciptingSugar
open Definitions

module Poisoned = 
    open Poisoned

    let Int c = (C(I(c)))
    let one = Int 1
    let two = Int 2
    let half = C (D 0.5)
    
    let FuncII B = (one + X + X * X + Int(B))^half
    let FuncIII B = (one + Int(B) * cos (X))^ half
    let FuncIV B = (one + Int(B) * sin (X))^ half
    let FuncV B = (one + X + X * X * Int(B)) ^half

    
    let IIIFunc2 =  (one/(one-X+(X^two)))
    let IVFunc2 = C(D 1.)/(C(D 1.) + X + X * X);
    

    let IIIFunc3 = (one/(one+(X^(Int 5))))
    let IVFunc3 = (one/(X*((one+(X^two))^half)))
    
    

let commonObjective1 = 
    { a = 0.
      b = 1.
      h = 0.01 }

let commonObjective2 = 
    { a = 0.
      b = 1.
      h = 1. }

let shtechObjective3 = 
    { a = 1.
      b = System.Double.PositiveInfinity
      h = 1. }

let commonObjective3 = 
    { a = 0.
      b = System.Double.PositiveInfinity
      h = 1. }
    
let IVProblem2 =  
        { func = Poisoned.IVFunc2
          expectedResult = System.Math.PI / (3. * sqrt (3.)) }

        
let IIIProblem2 =  
        { func = Poisoned.IIIFunc2
          expectedResult = 2. * (System.Math.PI / (3. * sqrt (3.))) }

let IIIEstimatingHigherBoundOfIntegrating eps = sqrt(sqrt(1./(4.*eps))) |> ceil 
let IVEstimatingHigherBoundOfIntegrating eps = 1./eps
