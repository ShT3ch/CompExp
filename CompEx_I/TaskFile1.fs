module TaskFile1
open ComputationCore

let funcII B = fun (x:double)-> sqrt(1.+x+x*x+B)
let funcIII B = fun (x:double)-> sqrt(1.+B*cos(x))
let funcIV B = fun (x:double)-> sqrt(1.+B*sin(x))
let funcV B = fun (x:double)-> sqrt(1.+x+x*x*B)
let objectiveCommon = {a = 0.; b = 1.; h = 0.01}

//let funcs = [funcII; funcIII; funcIV; funcV]
//let numbers = [0..20]|> List.map(fun x-> (double)x)

let solve myNumber myFunction myObjective = 
    let myFunc = myFunction ((double)myNumber)

    let myExRectangular = new RectangleIntegrate(myFunc)
    let myExTrapezoidal = new TrapezoidalIntegrate(myFunc)
    let myExSimpson = new SimpsonIntegrate(myFunc)

    let myTableLines = lines myFunc myObjective

    let outputTable = buildStringLines myTableLines

    outputTable

let writeAnswer number func objective =
    printf "Number of member: %i\r\n" number
    printf "%s\r\n" (solve number func objective |> String.concat "\r\n")


let myNumber =  16
let myFunction = funcV 

let polyNumber = 13
let polyFunc = funcIII

writeAnswer myNumber myFunction objectiveCommon
writeAnswer polyNumber polyFunc objectiveCommon