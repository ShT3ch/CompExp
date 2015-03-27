module Tests

open FSharp.Charting
open System.Drawing
open System.Windows.Forms
open System.Windows.Forms.DataVisualization
open Symbolic
open TaskList

let chart = 
    [30 .. 100]
    |> Seq.map (fun n->(double)n/100.)
    |> Seq.map (fun x-> (x, Compute (Poisoned.MyFunc3|>Derivative|>Derivative|>Derivative|>Derivative) x))
    |> Chart.Line

Compute (Poisoned.MyFunc3|>Derivative|>Derivative|>Derivative|>Derivative) 0.02