#!/usr/bin/env dotnet fsi

let args = fsi.CommandLineArgs

let applyCommand (x, z) arr =
    match arr with
    | [|"forward"; dist|] -> (x + (int dist), z)
    | [|"down"; dist|] -> (x, z + (int dist))
    | [|"up"; dist|] -> (x, z - (int dist))
    | _ -> (x, z)

let (x, z) = 
    System.IO.File.ReadLines(args.[1])
    |> Seq.filter(fun x -> x <> "")
    |> Seq.map(fun x -> x.Split " ")
    |> Seq.fold applyCommand (0, 0)

printfn "Answer: %i * %i = %i" x z (x * z)
