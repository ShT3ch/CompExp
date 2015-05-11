module TaylorStaff

open SymbolicDefinitions
open Objectives

open DiffSharp.Symbolic
open System.Numerics
open MathNet.Numerics
open MathNet.Symbolics

module Poisoned = 
    open Operators

    
    let taylorMembers y y' = 
        let swapArgs f x y = f y x
        let df = swapArgs Calculus.differentiate
        let dx = Poisoned.x
        let dy = Poisoned.y
        let firstBrace = (df y' dx + (df y' dy)*(y'))
        let secondStep = (Poisoned.h**2/2)*firstBrace
        let secondSquareBrace =
                (
                    ((df (df y' dx) dx) + (df(df y' dx)dy)*y') +
                    (((df(df y' dy) dx)+ (df (df y' dy) dy)*y')*y') +
                    firstBrace*(df y' dy)
                )
        let thirdStep = 
            (Poisoned.h**3/(2*3))*secondSquareBrace
                
        let fourthStep = 
            (Poisoned.h**4/(2*3*4)) *
                (
                    (df(df(df y' dx) dx) dx)+(df(df(df y' dx) dx) dy)*y' +
                    ((df(df(df y' dx)dy)dx) + (df(df(df y' dx)dy)dy)*y')*y' +
                    (firstBrace)*(df(df y' dx) dy) +
                    (df(df(df y' dy)dx)dx)+(df(df(df y' dy)dx)dy)*y'+
                    (df(df(df y' dy)dy)dx) + (df(df(df y' dy)dy)dy)*y'+
                    firstBrace*(df(df y' dy)dy)+
                    secondSquareBrace*(df y' dy)+
                    (((df(df y' dy) dx)+ (df (df y' dy) dy)*y')*y')
                )
        [
            y;
            Poisoned.h*y';
            secondStep;
            thirdStep;
            fourthStep
        ]

    let taylorAssembler objective order = 
        taylorMembers Poisoned.y objective.y'Symbolic
        |> Seq.take (order+1)
        |> Seq.sum
