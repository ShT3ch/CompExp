module Symbolic

open System

type Expression =
    | X
    | Const of float
    | Neg of Expression
    | Add of Expression * Expression
    | Sub of Expression * Expression
    | Mul of Expression * Expression
    | Div of Expression * Expression
    | Pow of Expression * Expression
    | Exp of Expression
    | Log of Expression
    | Sin of Expression
    | Cos of Expression

let (|Op|_|) (x : Expression) =
    match x with
    | Add(e1, e2) -> Some(Add, e1, e2)
    | Sub(e1, e2) -> Some(Sub, e1, e2)
    | Mul(e1, e2) -> Some(Mul, e1, e2)
    | Div(e1, e2) -> Some(Div, e1, e2)
    | Pow(e1, e2) -> Some(Pow, e1, e2)
    | _ -> None

let (|Func|_|) (x : Expression) =
    match x with
    | Exp(e) -> Some(Exp, e)
    | Log(e) -> Some(Log, e)
    | Sin(e) -> Some(Sin, e)
    | Cos(e) -> Some(Cos, e)
    | Neg(e) -> Some(Neg, e)
    | _ -> None

let OpName (e: Expression) : string =
    match e with
    | Add(e1, e2) -> "+"
    | Sub(e1, e2) -> "-"
    | Mul(e1, e2) -> "*"
    | Div(e1, e2) -> "/"
    | Pow(e1, e2) -> "^"
    | _ -> failwith(sprintf "Unrecognized operator [%A]" e)

let FuncName (e: Expression) (a : string) : string =
    match e with
    | Exp(x) -> sprintf "e^(%s)" a
    | Log(x) -> sprintf "log(%s)" a
    | Sin(x) -> sprintf "sin(%s)" a
    | Cos(x) -> sprintf "cos(%s)" a
    | _ -> failwith(sprintf "Unrecognized function [%A]" e)

let rec FormatExpression (inner : Expression) : string =
    match inner with
    | X -> "x";
    | Const(n) -> sprintf "%.1f" n
    | Neg x -> sprintf "-%s" (FormatExpression(x))
    | Op(op, e1, e2) -> "(" + FormatExpression(e1) + " " + OpName(inner) + " " + FormatExpression(e2) + ")"
    | Func(f, e) -> FuncName(inner) (FormatExpression(e))

let rec Compute exp x = 
    match exp with
    | X -> x
    | Const(n) -> n
    | Neg(e) -> - (Compute e x)
    | Add(e1, e2) -> (Compute e1 x) + (Compute e2 x )
    | Sub(e1, e2) -> (Compute e1 x) - (Compute e2 x)
    | Mul(e1, e2) -> (Compute e1 x) * (Compute e2 x)
    | Div(e1, e2) -> (Compute e1 x) / (Compute e2 x)
    | Pow(e1, e2) -> Math.Pow((Compute e1 x), (Compute e2 x))
    | Exp(e) -> Math.Exp(Compute e x)
    | Log(e) -> Math.Log(Compute e x)
    | Sin(e) -> Math.Sin(Compute e x)
    | Cos(e) -> Math.Cos(Compute e x)
    | _ -> failwith(sprintf "Unable to match expression [%A]" x)


let rec Derivative x : Expression =
    match x with
    | X -> Const(1.)
    | Const(n) -> Const(0.)
    | Neg(e) -> Neg(Derivative(e))
    | Add(e1, e2) -> Add(Derivative(e1), Derivative(e2))
    | Sub(e1, e2) -> Sub(Derivative(e1), Derivative(e2))
    | Mul(e1, e2) -> Add(Mul(Derivative(e1), e2), Mul(e1, Derivative(e2)))
    | Div(e1, e2) -> Div( Sub(Mul(Derivative(e1), e2), Mul(e1, Derivative(e2))), Pow(e2, Const(2.)))
    | Pow(e, Const(n)) ->
        let simplePowDerivative exp = 
            Mul(Const n, Pow(exp, Const (n - 1.))) 
        match e with
        | X -> simplePowDerivative X
        | _ ->
            let outer = simplePowDerivative
            let inner = Derivative e
            Mul(outer(e), inner)
    | Pow(Const(n), e) -> Mul(Mul(Log(Const(n)), Pow(Const(n), e)), Derivative(e))
    | Exp(X) -> Exp(X)
    | Log(X) -> Div(Const(1.), X)
    | Sin(X) -> Cos(X)
    | Cos(X) -> Neg(Sin(X))
//    | Div(Const(1.), e) -> Div(Neg(Derivative(e)), Pow(e, Const(2.)))
    | Func(g, f) ->
        let dg = Derivative(g(X))
        let df = Derivative(f)
        match dg with
        | Func(dgf, dge) -> Mul(dgf(f), df)
        | Op (op, e1, e2) -> Mul(op(e1, e2), df)
        | _ -> failwith(sprintf "Unable to match compound function [%A]" dg)
    | _ -> failwith(sprintf "Unable to match expression [%A]" x)
