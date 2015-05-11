module Program

open Objectives
open Utilities
open Methods

//#load @"C:\Users\sht3ch\Documents\LearnSpace\CompExp\packages\FSharp.Charting.0.90.10\FSharp.Charting.fsx";;

open FSharp.Charting
open System.Windows.Forms
open System.Windows.Forms.DataVisualization
open System.Drawing

let (step, x_Seq) = domainSplitter 0.0 1.0 5

let eulerLine taskObjective step x_Seq  = 
    commonWay x_Seq (eulerForwardBody step) taskObjective
    |> drawLineEuler "euler"

let eulerBackwardLine taskObjective step x_Seq = 
    commonWay x_Seq (eulerBackwardBody step) taskObjective
    |> drawLineEuler "euler backward"

let implicitEulerLine taskObjective step x_Seq = 
    commonWay x_Seq (implicitEulerBody step) taskObjective
    |> drawLineEuler "implicit euler"

let cauchyLine taskObjective step x_Seq = 
    commonWay x_Seq (cauchyBody step) taskObjective
    |> drawLineEuler "cauchy"
    
let runge_kutta4Line taskObjective step x_Seq = 
    commonWay x_Seq (runge_kutta4Body step) taskObjective
    |> drawLineEuler "runge-kutta 4"

let taylor2Line taskObjective step x_Seq = 
    commonWay x_Seq (tailorNthBody 2 step) taskObjective
    |> drawLineEuler "taylor 2"
    
let taylor3Line taskObjective step x_Seq = 
    commonWay x_Seq (tailorNthBody 3 step) taskObjective
    |> drawLineEuler "taylor 3"
    
let taylor4Line taskObjective step x_Seq = 
    commonWay x_Seq (tailorNthBody 4 step) taskObjective
    |> drawLineEuler "taylor 4"

let twoStepAdamsLine taskObjective step x_Seq = 
    multyStepWay x_Seq twoStepAdams runge_kutta4Body step taskObjective
    |> drawLineEuler "two step Adams accelerated by runge-kutta 4"
    
let threeStepAdamsLine taskObjective step x_Seq = 
    multyStepWay x_Seq threeStepAdams runge_kutta4Body step taskObjective
    |> drawLineEuler "three step Adams accelerated by runge-kutta 4"

let fourStepAdamsLine taskObjective step x_Seq = 
    multyStepWay x_Seq fourStepAdams runge_kutta4Body step taskObjective
    |> drawLineEuler "four step Adams accelerated by runge-kutta 4"

[
//    eulerLine Objective_A step x_Seq; 
//    eulerBackwardLine Objective_A step x_Seq;
//    cauchyLine Objective_A step x_Seq;
//    implicitEulerLine Objective_A step x_Seq;
//    runge_kutta4Line Objective_A step x_Seq;
//    twoStepAdamsLine Objective_A step x_Seq;
//    threeStepAdamsLine Objective_A step x_Seq;
    fourStepAdamsLine Objective_A step x_Seq;
    taylor2Line Objective_A step x_Seq;
    taylor3Line Objective_A step x_Seq;
    taylor4Line Objective_A step x_Seq;
    Chart.Line(x_Seq |> Seq.map(fun x -> (x,Objective_A.y x)), "origin");
]
|> List.map (Chart.WithLegend true)
|> Chart.Combine
