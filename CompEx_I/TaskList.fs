module TaskList

open Symbolic
open DesciptingSugar

module Poisoned = 
    open Poisoned
    let func = 
        C(I 1)+cos(X)^(X+ C(I 1)/C(I 2))

FormatExpression Poisoned.func
