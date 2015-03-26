module Task1

open ComputationCore
open TaskList
open Symbolic
open Definitions
    
let solve myNumber myFunction myObjective = 
    let myExRectangular = new RectangleIntegrate(myFunction)
    let myExTrapezoidal = new TrapezoidalIntegrate(myFunction)
    let myExSimpson = new SimpsonIntegrate(myFunction)

    let myTableLines = lines myFunction myObjective

    let outputTable = buildStringLines myTableLines

    outputTable

let writeAnswer number func objective =
    let curriedFunc = func number
    printf "Number of member: %i; function: %s\r\n" number (FormatExpression curriedFunc)
    printf "%s\r\n" (solve number curriedFunc objective |> String.concat "\r\n")


let myNumber =  16
let myFunction = Poisoned.FuncV 
//
//let polyNumber = 13
//let polyFunc = FuncIII

writeAnswer myNumber myFunction commonObjective1
//writeAnswer polyNumber polyFunc objectiveCommon