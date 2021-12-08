#!/usr/bin/env dotnet fsi

let args = fsi.CommandLineArgs

let lines =
    System.IO.File.ReadLines(args.[1])
    |> Seq.filter(fun x -> x <> "")

let map = Array2D.create 1000 1000  0

let splitLine (line : string) =
    match line.Split " -> " with
    | [|c1; c2|] -> (c1, c2)
    | _ -> ("0,0", "0,0")

let splitCoord (coord : string) =
    match coord.Split "," with
    | [|x; y|] -> (int x, int y)
    | _ -> (0, 0)

let drawLine (x1, y1) (x2, y2) =
    for x in x1..x2 do
        for y in y1..y2 do
            map.[x, y] <- map.[x, y] + 1

let coords = lines |> Seq.map (fun line ->
    let (c1, c2) = splitLine line
    let (x1, y1) = splitCoord c1
    let (x2, y2) = splitCoord c2

    match ((x1, y1), (x2, y2)) with
    | ((x1, y1), (x2, y2)) when y1 = y2 -> if x1 < x2 then drawLine (x1, y1) (x2, y2) else drawLine (x2, y2) (x1, y1)
    | ((x1, y1), (x2, y2)) when x1 = x2 -> if y1 < y2 then drawLine (x1, y1) (x2, y2) else drawLine (x2, y2) (x1, y1)
    | _ -> drawLine (1, 1) (0, 0)

    (splitCoord c1, splitCoord c2)
)

Seq.reduce (fun x y -> x) coords

let mutable t = 0

for y in 0 .. 999 do
    for x in 0 .. 999 do
        if map.[x, y] > 1 then
            t <- t + 1
        printf "%i" map.[x, y]
    printf "\n"

printfn "Answer: %i" t
