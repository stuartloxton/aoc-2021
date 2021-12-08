#!/usr/bin/env dotnet fsi

let args = fsi.CommandLineArgs

let mutable lanternfish : int64 [] = Array.zeroCreate 9
let input = System.IO.File.ReadLines(args.[1]) |> Seq.head
let start = input.Split(",") |> Seq.map int
for x in start do
    lanternfish.[x] <- lanternfish.[x] + 1L

let simulateday (state : int64 []) =
    let birthsToday = state.[0]
    let nextday = Array.append (Array.skip 1 state) [|0L|]
    nextday.[6] <- nextday.[6] + birthsToday
    nextday.[8] <- nextday.[8] + birthsToday
    nextday

for x in 1 .. 256 do
    lanternfish <- simulateday lanternfish
    let count = Seq.reduce (+) lanternfish
    printfn "Day %d count: %d" x count

