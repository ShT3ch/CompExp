module Task3List

open MathNet.Symbolics

open Operators

let eps = 

let t = symbol "t"

let Func = 1/(t*((pow (1+ t*t) (one/two) )))

let firstDerivative = Calculus.differentiate t Func
let second = Calculus.differentiate t firstDerivative
let third = Calculus.differentiate t second
let fourth = Calculus.differentiate t third


printf "%s\r\n" (Infix.print Func)
printf "%s\r\n" (LaTeX.print firstDerivative)
printf "%s\r\n" (LaTeX.print second)
printf "%s\r\n" (LaTeX.print third)
printf "%s\r\n" (LaTeX.print fourth)