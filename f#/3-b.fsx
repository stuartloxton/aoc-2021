#!/usr/bin/env dotnet fsi

let args = fsi.CommandLineArgs

let lines = System.IO.File.ReadLines(args.[1]) |> Seq.filter(fun x -> x <> "") |> Seq.cache

let binToInt binaryString =
     Seq.toList binaryString |> Seq.rev |> Seq.mapi (fun i x -> if x = '1' then pown 2 i else 0) |> Seq.fold (fun acc charge -> acc + charge) 0

let rec filterValues (lines : seq<string>) (acc : string) comp =
    let pivot : float = (float (Seq.length lines)) / 2.0;
    printfn "Pivot: %.2f" pivot
    let (key, rem) =
        lines
        |> Seq.groupBy (fun x -> x.[0])
        |> Seq.filter (fun (k, r) -> comp k r pivot)
        |> Seq.head
    match Seq.length(rem) with
        | 1 -> (acc + (Seq.head rem)) 
        | _ -> filterValues (rem |> Seq.map(fun x -> x.[1..])) (acc + string key) comp

let findMostCommon (lines : seq<string>) (acc : string) =
    filterValues lines "" (fun key rem (pivot : float) ->
        printfn "Key %c, Length: %i" key (Seq.length rem)
        let flen = float (Seq.length rem)
        flen > pivot || (flen = pivot && key = '1')
    )

let findLeastCommon (lines : seq<string>) (acc : string) =
    filterValues lines "" (fun key rem (pivot : float) -> 
        printfn "Key %c, Length: %i" key (Seq.length rem)
        let flen = float (Seq.length rem)
        flen < pivot || (flen = pivot && key = '0')
    )

let oxygen = findMostCommon lines ""
let co2 = findLeastCommon lines ""


printfn "oxygen generator rating: %s %i" oxygen (binToInt oxygen)
printfn "co2 scrubber rating: %s %i" co2  (binToInt co2)
printfn "Answer: %i" ((binToInt co2) * (binToInt oxygen))
