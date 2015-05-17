module Shoots

let eulerMethod (h : float) (x_i :: xs, y1_i :: y1Tail, y2_i :: y2Tail) f1 f2 = 
    let y2_i1 = y2_i + h * (f2 x_i y1_i y2_i)
    let y1_i1 = y1_i + h * (f1 x_i y1_i y2_i)
    (x_i + h :: x_i :: xs, y1_i1 :: y1_i :: y1Tail, y2_i1 :: y2_i :: y2Tail)

let eulerMethodBackward (h : float) (x_i :: xs, y1_i :: y1Tail, y2_i :: y2Tail) f1 f2 = 
    let y2_i1''' = y2_i + h * (f2 x_i y1_i y2_i)
    let y1_i1''' = y1_i + h * (f1 x_i y1_i y2_i)
    let y2_i1 = y2_i + (h/2.0) * ((f2 x_i y1_i y2_i)+(f2 (x_i+h) (y1_i1''') (y2_i1''')))
    let y1_i1 = y1_i + (h/2.0) * ((f1 x_i y1_i y2_i)+(f1 (x_i+h) (y1_i1''') (y2_i1''')))
    (x_i + h :: x_i :: xs, y1_i1 :: y1_i :: y1Tail, y2_i1 :: y2_i :: y2Tail)

let runge_kutta4thMethod (h : float) (x_i :: xs, y1_i :: y1Tail, y2_i :: y2Tail) f1 f2 = 
    let k1 = h * (f1 x_i y1_i y2_i)
    let l1 = h * (f2 x_i y1_i y2_i)
    
    let k2 = h * (f1 (x_i + h / 2.0) (y1_i + k1 / 2.0) (y2_i + l1 / 2.0))
    let l2 = h * (f2 (x_i + h / 2.0) (y1_i + k1 / 2.0) (y2_i + l1 / 2.0))
    
    let k3 = h * (f1 (x_i + h / 2.0) (y1_i + k2 / 2.0) (y2_i + l2 / 2.0))
    let l3 = h * (f2 (x_i + h / 2.0) (y1_i + k2 / 2.0) (y2_i + l2 / 2.0))
    
    let k4 = h * (f1 (x_i + h) (y1_i + k3) (y2_i + l3))
    let l4 = h * (f2 (x_i + h) (y1_i + k3) (y2_i + l3))
    
    let dy1 = (1.0 / 6.0) * (k1 + 2.0 * k2 + 2.0 * k3 + k4)
    let dy2 = (1.0 / 6.0) * (l1 + 2.0 * l2 + 2.0 * l3 + l4)

    let y2_i1 = y2_i + dy2
    let y1_i1 = y1_i + dy1
    (x_i + h :: x_i :: xs, y1_i1 :: y1_i :: y1Tail, y2_i1 :: y2_i :: y2Tail)

let rec computeCauchy (a :: xs) b n y0 y'0 f1 f2 cauchyMethod = 
    match n with
    | 0 -> (a :: xs, y0, y'0)
    | k -> 
        let h = (b - a) / (n |> float)
        printf "a: %f; b: %f; n: %i; h: %f;" a b n h
        let (xs, y1s, y2s) = cauchyMethod h (a :: xs, y0, y'0) f1 f2
        computeCauchy xs b (n - 1) y1s y2s f1 f2 cauchyMethod

let rec shoot a b n ε ((μPrevious, y1Previous), μCurrent) cauchyMethod f1 f2 (y0:float) y1 accumulator = 
    let (xLast :: xs, y1Last :: y1Tail, y2Last :: y2Tail) = 
        computeCauchy (a::[]) b n (y0::[]) (μCurrent::[]) f1 f2 cauchyMethod

    let answer = (xLast :: xs, y1Last :: y1Tail, y2Last :: y2Tail)::accumulator
    printfn "shoot: μCurrent: %f; x: %f; y1: %f; y2 origin: %f; diff: %f; ε: %f;" μCurrent xLast y1Last y1 (y1Last - y1) ε
    if (abs (y1Last - y1) < ε) 
        then 
            answer
        else 
            let μNext = μCurrent - (y1Last - y1) * (μCurrent - μPrevious) / (y1Last - y1Previous)
            shoot a b n ε ((μCurrent, y1Last), μNext) cauchyMethod f1 f2 y0 y1 answer

