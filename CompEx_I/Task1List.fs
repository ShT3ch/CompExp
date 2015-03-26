module TaskList

open MathNet.Symbolics

open Operators

let t = symbol "t"

let Int (B:int) = Expression.FromInt32(B)

let half = (one/two)

let FuncII B = pow (1+t+t*t+Int(B)) half
let FuncIII B = pow (1+Int(B)*cos(t)) half
let FuncIV B = pow (1+Int(B)*sin(t)) half
let FuncV B = pow (1+t+t*t*Int(B)) half

let funcs = [FuncII;FuncIII;FuncIV;FuncV]

let doLambda func:(double->double) = fun X -> (Evaluate.evaluate (Map.ofSeq ["t", Real X]) func).RealValue

//let calcAll B = funcs |> Seq.map (fun func-> func B) |> Seq.map LaTeX.print |> Seq.iter (printf "%s\r\n")
//
//calcAll 16

//let funcIV B = fun (x:double)-> sqrt(1.+B*sin(x))
//let funcV B = fun (x:double)-> sqrt(1.+x+x*x*B)
