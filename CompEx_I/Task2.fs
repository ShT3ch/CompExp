module Task2
open FSharp.Charting
open System.Drawing
open System.Windows.Forms
open System.Windows.Forms.DataVisualization
open ComputationCore

type Problem2 = 
    {
        func:(double->double);
        expectedResult:double;
    }

type Task2 = 
    {
        problem: Problem2
        objective: Objective;
    }

let commonObjective = 
    {
        a=0.;b=1.;h=1.
    }

let myProblem =  
    {
        func = fun x -> 1./(1.+x+x*x);
        expectedResult = System.Math.PI/(3.*sqrt(3.));
    }
    
let task = 
    {
        problem = myProblem;
        objective = commonObjective
    }

let generateObjectives baseObjective = 
    Seq.initInfinite (fun number ->( {
                                          a = baseObjective.a; 
                                          b = baseObjective.b ; 
                                          h = baseObjective.h/(double)number
                                     }, number))

let getChain objective problem fromFineness toFineness (integrateMethod:IntegrateMethod<Segment>) = 
    (integrateMethod.Name, generateObjectives objective
                           |> Seq.skip fromFineness
                           |> Seq.take toFineness
                           |> Seq.map (fun (objectiveStep, stepNumber)-> (doCalc(integrateMethod,objectiveStep) - problem.expectedResult,stepNumber))
                           |> Seq.map (fun (x,y)->(y,x))
    )


let simpsonChain problem fromFineness toFineness = 
    let simpsonIntegrateMethod = new SimpsonIntegrate(problem.func):>IntegrateMethod<Segment>
    getChain commonObjective myProblem fromFineness toFineness simpsonIntegrateMethod

let trapezoidChain problem fromFineness toFineness = 
    let trapezoidIntegrateMethod = new TrapezoidalIntegrate(myProblem.func):>IntegrateMethod<Segment>
    getChain commonObjective myProblem fromFineness toFineness trapezoidIntegrateMethod

let rectangleChain problem fromFineness toFineness = 
    let rectangleIntegrateMethod = new RectangleIntegrate(myProblem.func):>IntegrateMethod<Segment>
    getChain commonObjective myProblem fromFineness toFineness rectangleIntegrateMethod

let chains problem fromFineness toFineness = 
    simpsonChain problem fromFineness toFineness
    ::
    trapezoidChain problem fromFineness toFineness
    ::
    rectangleChain problem fromFineness toFineness
    ::
    []

FSharp.Charting.Chart.Combine 
    (
        chains myProblem 7 100 
        |> List.map (fun (name, chain)-> (FSharp.Charting.Chart.Line (chain,name)).WithLegend())
    )