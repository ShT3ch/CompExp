module Definitions

open Symbolic

type Objective = 
    {
    a:double;
    b:double;
    h:double
    }

type Problem2 = 
    {
        func:Expression;
        expectedResult:double;
    }

type Task2 = 
    {
        problem: Problem2
        objective: Objective;
    }