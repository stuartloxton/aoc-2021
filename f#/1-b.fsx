#!/usr/bin/env dotnet fsi

(*
    Pass input file as first command line argument
    e.g. dotnet fsi 1-b.fsx input-1.txt
    
    Same as previous process but before pairing, create a new windowed sequence
    and then map to it's sum
*)

let args = fsi.CommandLineArgs

let ans = 
    System.IO.File.ReadLines(args.[1])
    |> Seq.filter(fun x -> x <> "")
    |> Seq.map int
    |> Seq.windowed 3
    |> Seq.map Seq.reduce +
    |> Seq.pairwise
    |> Seq.fold (fun acc (z1, z2) -> if z2 > z1 then acc + 1 else acc) 0

printfn "Answer: %i" ans
