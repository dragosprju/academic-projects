// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

module Program5 =

    open System

    let random = new Random()

    let randomize noOfElements = List.map (fun x -> (random.Next()) [1..noOfElements]
    
    let main noOfElements =
        printfn "Lista aleatoare: %A" (randomize noOfElements)
        0

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code
