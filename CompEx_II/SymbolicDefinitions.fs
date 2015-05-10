module SymbolicDefinitions

open System.Numerics
open MathNet.Numerics
open MathNet.Symbolics

let rootValues_A = 
    [ "a", Real -0.9
      "c", Real 0.14 ]

module Poisoned = 
    open Operators
    
    let x = symbol "x_i"
    let y = symbol "y(x_i)"
    let h = symbol "h"
    let a = symbol "a"
    let c = symbol "c"
    let f_A = 30 * y * ((x * x + a * x + c))
