module Objectives

open Symbolic

type Objective = 
    { y' : double -> double -> double
      y0 : double
      y : double -> double
      implicitExp : double -> double -> double -> double 
//      y'Exp: Expression
      }

let Objective_A = 
    { y' = fun x y -> 30.0 * y * (x - 0.2) * (x - 0.7)
      y0 = 0.1
      y = fun x -> 0.1 * exp (x * (10.0 * x ** 2.0 - 13.5 * x + 4.2))
      implicitExp = fun x_i1  y_i h -> y_i / (1.0 - 30.0 * h * (x_i1 - 0.2) * (x_i1 - 0.7))
//      y'Exp = Mul(Const(30.0),
      }
