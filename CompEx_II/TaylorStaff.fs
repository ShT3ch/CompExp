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
        [
            y;
            Poisoned.h*y';
            (Poisoned.h*Poisoned.h/Expression.Two)*(Calculus.differentiate Poisoned.x y' + (Calculus.differentiate Poisoned.y y')*(y'))
        ]

    let taylorAssembler objective order = 
        taylorMembers Poisoned.y objective.y'Symbolic
        |> Seq.take (order+1)
        |> Seq.sum
