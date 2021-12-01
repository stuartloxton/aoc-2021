#!/usr/bin/env dotnet fsi

(*
    Pass input file as first command line argument
    e.g. dotnet fsi 1.fsx input-1.txt

    Turn lines into a sequence, create a new sequence of adjacent pairs then
    fold by incrementing an accumulator if the second elem is larger than
    the first
*)

let args = fsi.CommandLineArgs

let ans = 
    System.IO.File.ReadLines(args.[1])
    |> Seq.filter(fun x -> x <> "")
    |> Seq.map int
    |> Seq.pairwise
    |> Seq.fold (fun acc (z1, z2) -> if z2 > z1 then acc + 1 else acc) 0

printfn "Answer: %i" ans
