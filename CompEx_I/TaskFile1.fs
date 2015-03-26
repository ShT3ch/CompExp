module TaskFile1

open ComputationCore
open TaskList
open MathNet.Symbolics

let objectiveCommon = {a = 0.; b = 1.; h = 0.01}

//let funcs = [funcII; funcIII; funcIV; funcV]
//let numbers = [0..20]|> List.map(fun x-> (double)x)

let solve myNumber myFunction myObjective = 
    let myExRectangular = new RectangleIntegrate(myFunction)
    let myExTrapezoidal = new TrapezoidalIntegrate(myFunction)
    let myExSimpson = new SimpsonIntegrate(myFunction)

    let myTableLines = lines myFunction myObjective

    let outputTable = buildStringLines myTableLines

    outputTable

let writeAnswer number func objective =
    let curriedFunc = func number
    printf "Number of member: %i; function: %s\r\n" number (Infix.print curriedFunc)
    printf "%s\r\n" (solve number (doLambda curriedFunc) objective |> String.concat "\r\n")


let myNumber =  16
let myFunction = FuncIV 

let polyNumber = 13
let polyFunc = FuncIII

writeAnswer myNumber myFunction objectiveCommon
writeAnswer polyNumber polyFunc objectiveCommon