module DesciptingSugar

open Symbolic

module Poisoned =    
    type C = 
        | I of int
        | D of float

    let C value = 
        match value with
        | I integerValue -> Const((double) integerValue)
        | D doubleValue -> Const(doubleValue)

    let (~+) e = Const(e)
    let (~-) e = Neg(e)
    let (+) e1 e2 = Add(e1,e2)
    let (-) e1 e2 = Sub(e1,e2)
    let (*) e1 e2 = Mul(e1,e2)
    let (/) e1 e2 = Div(e1,e2)
    let (^) e1 e2 = Pow(e1,e2)
    let ln e1 = Log(e1)
    let sin e1 = Log(e1)
    let cos e1 = Cos(e1)