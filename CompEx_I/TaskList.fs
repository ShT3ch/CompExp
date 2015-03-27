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

    let MyFunc2 = C(D 1.)/(C(D 1.) + X + X * X);

    let MyFunc3 = (one/(X*((one+(X^two))^half)))
    let Test = ((X^two)+one)^half


let commonObjective1 = 
    { a = 0.
      b = 1.
      h = 0.01 }

let commonObjective2 = 
    { a = 0.
      b = 1.
      h = 1. }

let objective3 = 
    { a = 0.
      b = System.Double.PositiveInfinity
      h=1.
    }

let myProblem2 =  
        {
            func = Poisoned.MyFunc2
            expectedResult = System.Math.PI/(3. * sqrt (3.));
        }
//    
//let task2 problemOfTask = 
//    {
//        problem = problemOfTask;
//        objective = commonObjective2
//    }

FormatExpression (Poisoned.FuncII 16)
Poisoned.MyFunc3 |> Derivative |> FormatExpression
Poisoned.MyFunc3 |> FormatExpression