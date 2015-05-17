// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.


open Shoots
open Objectives
open AproximationStuff
open TridiagonalStuff
//#load "C:\Users\sht3ch\Documents\LearnSpace\CompExp\packages\FSharp.Charting.0.90.10\FSharp.Charting.fsx"

open FSharp.Charting
open System.Windows.Forms
open System.Windows.Forms.DataVisualization
open System.Drawing

let solveAsMan N y0 y1 cMethod = 
    let a = 0.0
    let b = 1.0
    let n = 4
    let ε = 0.00001
    let μ0 = 2.0
    let μ1 = 1.0
    let f2 = f2 N

    let (xLast :: xs, y1Last :: y1Tail, y2Last :: y2Tail) = 
        computeCauchy (a::[]) b n (y0::[]) (μ0::[]) f1 f2 cMethod
    shoot a b n ε ((μ0, y1Last), μ1) eulerMethod f1 f2 y0 y1 []

let countedBy cMethod methodName N = 
    let solution= solveAsMan N 0.0 (Poisoned.y1 N) cMethod
    solution
    |>List.rev
    |>List.mapi (fun i (xs,y1s,y2s) -> Chart.Line ((Seq.zip xs y1s),sprintf "counted %i on %s" i methodName ))
    |>List.map (Chart.WithLegend true)

let originLine N = 
    let (xs,y1s,y2s)::solution= solveAsMan N 0.0 (Poisoned.y1 N) eulerMethod
    Chart.Line (xs |>Seq.map (fun x -> (x,(originAB N x)+0.01)),"origin")

let pic N = 
    Chart.Combine(List.concat [ //[Chart.Point [1,(Poisoned.y1 N)]]
                                [ (originLine N) ]
                                (countedBy eulerMethod "simple euler" N)
                                (countedBy eulerMethodBackward "euler backward" N) 
                                (countedBy runge_kutta4thMethod "runge-kutta 4" N) 
                                                                                             ])
pic 16.0

let a = 0.0
let b =1.0
let n = 50
let h = (b - a) / (n |> float)
let N = 16.0
let xi i = a + (h*(i|>float))

let (A,B,C,F) = 
    
    let pi i = 1.0
    let qi i = 2.0*(αN N)+2.0+(αN N)*(xi i)*(1.0-((xi i)))
    let leftBound = 
        {
            a = 0.0;
            C1 = 1.0;
            B1 = 0.0;
        }
    let rightBound = 
        {
            b = Poisoned.y1 N;
            An = 0.0;
            Cn = 1.0;
        }
    pimenovApproximation pi qi rightBound leftBound h n

let solution h = 
    ThomasAlgo A B C F n
    |>List.mapi ( fun i y -> (xi i, y))

[
    solution h |> Chart.Line;
    (originLine N)] |> Chart.Combine
