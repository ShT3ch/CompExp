module TridiagonalStuff

let rec C' A B C i:float = 
    match i with
    | 0 -> C 1
    | n -> (C n) - ((A n) * (B(n - 1))) / (C' A B C (n - 1))

let rec F' A B C F i:float = 
    match i with
    | 0 -> F 1
    | n -> (F n) - ((A n) * (F' A B C F (n - 1))) / (C' A B C (n - 1))

let ThomasAlgo A B C F n = 
    let f' = F' A B C F
    let c' = C' A B C
    [n-1 .. -1 .. 0]
    |> List.fold (fun (x_i1::xs) i -> 
        (((f' i) - (B i) * (x_i1)) / (c' i)) :: x_i1 :: xs) 
        [((f' n)/(c' n))]

let MatrixOutput A B C F n = 
    [0 .. n]
    |> List.map (
        fun j ->
            [0 .. n+1]
            |> List.map (
                fun i ->
                    printfn "j - %i" j
                    match (i, j) with
                    | _ when (i = j) -> C j
                    | _ when ((i - 1 = j)&&(i<=n))->B j
                    | _ when (i + 1 = j)->A j
                    | _ when (i = n+1) -> F j
                    | _ -> 0.0
                    )
                )
    |> List.map(fun frow-> frow |> List.map (sprintf "%.9f"))
    |> List.map(fun srow -> srow |> List.mapi(fun i s -> if (i = n+1) then "|"+s else s))
    |> List.map(fun srow -> String.concat " " srow)
    |> String.concat "\r\n"
