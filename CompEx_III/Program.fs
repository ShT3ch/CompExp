
open Definitions
open Shoots
open Objectives
open AproximationStuff
open TridiagonalStuff
//#load "C:\Users\sht3ch\Documents\LearnSpace\CompExp\packages\FSharp.Charting.0.90.10\FSharp.Charting.fsx"

open FSharp.Charting
open System.Windows.Forms
open System.Windows.Forms.DataVisualization
open System.Drawing

let N = 16.0
let n = 100

let leftC = leftBoundaryA
let rightC = rightBoundaryG N ((1.0 - leftC.a) / (n |> float))

let h = (rightC.b - leftC.a) / (n |> float)
let xi i = leftC.F1 + (h*(i|>float))

let solveAsMan N  cMethod = 
    let a = leftC.a
    let b = rightC.b
    let y0 = leftC.F1
//    let y1 y'1 = rightC.Fn/h - y'1
    let z1 = rightC.Fn/h
    let ε = 0.00001
    let μ0 = 5.0
    let μ1 = 2.0
    let f2 = f2 N

    let (xLast :: xs, y1Last :: y1Tail, y2Last :: y2Tail) = 
        computeCauchy (a::[]) b n (y0::[]) (μ0::[]) f1 f2 cMethod
    shoot a b n ε ((μ0, y1Last), μ1) eulerMethod f1 f2 y0 z1 []

let countedBy cMethod methodName N = 
    let solution= solveAsMan N cMethod
    solution
    |>List.rev
    |>List.mapi (fun i (xs,y1s,y2s) -> Chart.Line ((Seq.zip xs y1s),sprintf "counted %i on %s" i methodName ))
    |>List.map (Chart.WithLegend true)


let originLine N originFunc = 
    let (xs,y1s,y2s)::solution= solveAsMan N eulerMethod
    Chart.Line (xs |>Seq.map (fun x -> (x,(originFunc N x)+0.01)),"origin")


let pic N = 
    Chart.Combine(List.concat [ //[Chart.Point [1,(Poisoned.y1 N)]]
                                [ (originLine N originAG) ]
                                (countedBy eulerMethod "simple euler" N)
                                (countedBy eulerMethodBackward "euler backward" N) 
                                (countedBy runge_kutta4thMethod "runge-kutta 4" N) 
                                                                                             ])
pic 16.0


let (A,B,C,F) = 
    let pi i = 1.0
    let qi i = 2.0*(αN N)+2.0+(αN N)*(xi i)*(1.0-((xi i)))

    let leftBound = leftBoundaryA
    let rightBound = rightBoundaryG N h
    pimenovApproximation pi qi rightBound leftBound h n

let solution = 
    ThomasAlgo A B C F n
    |>List.mapi ( fun i y -> (xi i, y))


//MatrixOutput A B C F n
//ThomasAlgo A B C F n
//|>Seq.iter (printfn "%.4f")

[
    Chart.Line(solution, "lu-decomposition");
    (originLine N originAG)
] 
|> Chart.Combine |> Chart.WithLegend true
