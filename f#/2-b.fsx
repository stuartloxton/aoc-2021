#!/usr/bin/env dotnet fsi

let args = fsi.CommandLineArgs

let applyCommand (horizonal, aim, depth) arr =
    match arr with
    | [|"forward"; dist|] -> (horizonal + (int dist), aim, depth + ((int dist) * aim))
    | [|"down"; dist|] -> (horizonal, aim + (int dist), depth)
    | [|"up"; dist|] -> (horizonal, aim - (int dist), depth)
    | _ -> (horizonal, aim, depth)

let (horizontal, aim, depth) = 
    System.IO.File.ReadLines(args.[1])
    |> Seq.filter(fun x -> x <> "")
    |> Seq.map(fun x -> x.Split " ")
    |> Seq.fold applyCommand (0, 0, 0)

printfn "Answer: %i * %i = %i" horizontal depth (horizontal * depth)
