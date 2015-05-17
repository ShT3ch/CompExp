module TridiagonalStuff

let rec C' A B C i:float = 
    match i with
    | 0 -> C 1
    | n -> (C n) - ((A n) * (B(n - 1))) / (C' A B C (n - 1))

let rec F' A F C i:float = 
    match i with
    | 0 -> F 1
    | n -> (F n) - ((A n) * (F' A F C (n - 1))) / (C' A F C (n - 1))

let ThomasAlgo A B C F n = 
    let f' = F' A F C
    let c' = C' A B C
    [n-1 .. -1 .. 0]
    |> List.fold (fun (x_i1::xs) i -> 
        (((f' i) - (B i) * (x_i1)) / (c' i)) :: x_i1 :: xs) 
        [((f' n)/(c' n))]
