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

let rec drawLine (x1 : int, y1 : int) (x2 : int, y2 : int) =
    map.[x1, y1] <- map.[x1, y1] + 1
    if x1 <> x2 || y1 <> y2 then
        let nextX =
            if x2 > x1 then x1 + 1
            elif x2 < x1 then x1 - 1
            else x1
        let nextY =
            if y2 > y1 then y1 + 1
            elif y2 < y1 then y1 - 1
            else y1
        drawLine (nextX, nextY) (x2, y2)

let coords = lines |> Seq.map (fun line ->
    let (c1, c2) = splitLine line
    let (x1, y1) = splitCoord c1
    let (x2, y2) = splitCoord c2

    drawLine (x1, y1) (x2, y2)
    0
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
