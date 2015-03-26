module TaskList

open Symbolic
open DesciptingSugar
open Definitions

module Poisoned = 
    open Poisoned
    let Int c = (C(I(c)))
    let one = Int 1
    let two = Int 2
    let half = one/two    
    
    let FuncII B = (one+X+X*X+Int(B))^half
    let FuncIII B =  (one+Int(B)*cos(X))^ half
    let FuncIV B =   (one+Int(B)*sin(X))^ half
    let FuncV B =    (one+X+X*X*Int(B)) ^half

    let MyFunc2 = C(D 1.)/(C(D 1.)+X+X*X);


let commonObjective1 = {a = 0.; b = 1.; h = 0.01}

let commonObjective2 = 
    {
        a=0.;b=1.;h=1.
    }

let myProblem2 =  
        {
            func = Poisoned.MyFunc2
            expectedResult = System.Math.PI/(3.*sqrt(3.));
        }
    
let task2 = 
    {
        problem = myProblem2;
        objective = commonObjective2
    }




FormatExpression (Poisoned.FuncII 16)
