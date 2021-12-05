#!/usr/bin/env dotnet fsi

let args = fsi.CommandLineArgs

let lines = System.IO.File.ReadLines(args.[1])

let pivot = (Seq.length lines) / 2

let mutable gamma = 0

// For each of the characters in the bitstring
for i in  0..11 do
    let c0 = 
        lines
        |> Seq.map(fun s -> s.[i]) // Get the nth char
        |> Seq.filter(fun c -> c = '1') // Filter out 0s
        |> Seq.length > pivot // Compare the left over count to the halfway point
        |> System.Convert.ToInt32 // Turn the bool into a int
    gamma <- gamma + (c0 <<< (11-i)) // Shift the bit, 4 bits left for the first, 3 bits for the second, etc

let epsilon = 0b111111111111 ^^^ gamma // XOR against all 1s to get the opposite bits
printfn "Gamma: %i" gamma
printfn "Epsilon: %i" epsilon
printfn "Answer: %i" (epsilon * gamma)

