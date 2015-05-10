module Methods

open Utilities
open Objectives
open TaylorStaff  
open SymbolicDefinitions

open MathNet.Symbolics

type MultyStepMethod = 
    {
        order: int
        coeffs: double seq
        divisor: double
    }
    
let twoStepAdams = 
    {
        order = 2
        coeffs = [3.0; -1.0]
        divisor = 2.0
    }

let threeStepAdams = 
    {
        order = 3
        coeffs = [23.0; -16.0; 5.0]
        divisor = 12.0
    }

let fourStepAdams = 
    {
        order = 4
        coeffs = [55.0; -59.0; 37.0; -9.0]
        divisor = 24.0
    }

let eulerForwardMethodStep y_i x_i h (y' : double -> double -> double) = y_i + h * y' x_i y_i
let eulerBackwardMethodStep y_i x_i y_i1 x_i1 h (y' : double -> double -> double) = 
    y_i + (h / 2.0) * ((y' x_i y_i) + (y' x_i1 y_i1))
let implicitEulerMethod y_i x_i1 h (implicitExp : double -> double -> double -> double) = 
    implicitExp x_i1  y_i h

let cauchyMethodStep y_i x_i h (y' : double -> double -> double) = 
    let y_i12 = y_i + (h / 2.0) * (y' x_i y_i)
    y_i + h * (y' (x_i + h / 2.0) (y_i12))

let eulerForwardBody step taskObjective (y_i :: y_tail, x_i :: x_evaluatedSeq) x_i1 = 
    let y_i1 = eulerForwardMethodStep y_i x_i step taskObjective.y'
    (y_i1 :: y_i :: y_tail, x_i1 :: x_i :: x_evaluatedSeq)

let implicitEulerBody step taskObjective (y_i :: y_tail, x_i :: x_evaluatedSeq) x_i1 = 
    let y_i1 = implicitEulerMethod y_i x_i1 step taskObjective.implicitExp
    (y_i1 :: y_i :: y_tail, x_i1 :: x_i :: x_evaluatedSeq)

let eulerBackwardBody step taskObjective (y_i :: y_tail, x_i :: x_evaluatedSeq) x_i1 = 
    let y_i1' = eulerForwardMethodStep y_i x_i step taskObjective.y'
    let y_i1 = eulerBackwardMethodStep y_i x_i y_i1' x_i1 step taskObjective.y'
    (y_i1 :: y_i :: y_tail, x_i1 :: x_i :: x_evaluatedSeq)

let cauchyBody step taskObjective (y_i :: y_tail, x_i :: x_evaluatedSeq) x_i1 = 
    let y_i1 = cauchyMethodStep y_i x_i step taskObjective.y'
    (y_i1 :: y_i :: y_tail, x_i1 :: x_i :: x_evaluatedSeq)

let runge_kutta4Step y_i x_i h (y' : double -> double -> double) = 
    let k1 = h * (y' x_i y_i)
    let k2 = h * (y' (x_i + h / 2.0) (y_i + k1 / 2.0))
    let k3 = h * (y' (x_i + h / 2.0) (y_i + k2 / 2.0))
    let k4 = h * (y' (x_i + h) (y_i + k3))
    y_i + (1.0 / 6.0) * (k1 + 2.0 * k2 + 2.0 * k3 + k4)

let runge_kutta4Body step taskObjective (y_i :: y_tail, x_i :: x_evaluatedSeq) x_i1 = 
    let y_i1 = runge_kutta4Step y_i x_i step taskObjective.y'
    (y_i1 :: y_i :: y_tail, x_i1 :: x_i :: x_evaluatedSeq)

let tailorNthBody nth step taskObjective (y_i :: y_tail, x_i :: x_evaluatedSeq) x_i1 = 
    let assembledTaylor = Poisoned.taylorAssembler taskObjective nth
    printfn "%s" (Infix.print assembledTaylor)
    let symbolics = Map.ofList (
        List.concat [
            rootValues_A; 
            [
                "h", step |> Real; 
                "y(x_i)", y_i |> Real; 
                "x_i", x_i |> Real;
            ]
        ])  
    (symbolics
         |> Map.toSeq
         |> Seq.map (fun (symbol, value) -> sprintf "%s: %f" symbol  value.RealValue)
         |> Seq.iter (fun sym -> (printf "%s; " sym) |> ignore))
    let y_i1 = Evaluate.evaluate symbolics assembledTaylor
    (y_i1.RealValue :: y_i :: y_tail, x_i1 :: x_i :: x_evaluatedSeq)

let commonWay x_Seq body taskObjective = 
    x_Seq
    |> Seq.skip 1
    |> Seq.fold (body taskObjective) (taskObjective.y0 :: [], 0.0 :: [])

let multyStepWay x_Seq method accelerationMethod step taskObjective = 
    let accelerationData = 
        commonWay (x_Seq |> Seq.take method.order) (accelerationMethod step) taskObjective
    x_Seq
    |> Seq.skip method.order
    |> Seq.fold (fun (y_Cur, x_Cur) x_i1 ->
            let y_i1 = 
                method.coeffs
                |> Seq.zip3 y_Cur x_Cur
                |> Seq.sumBy (fun (y, x, coef) -> coef * (taskObjective.y' x y))
                |> (*) (step/method.divisor)
                |> (+) <| Seq.head y_Cur
            (y_i1 :: y_Cur, x_i1 :: x_Cur)
        ) accelerationData
