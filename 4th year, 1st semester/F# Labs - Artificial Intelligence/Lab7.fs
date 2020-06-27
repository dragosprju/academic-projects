// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

let calcPar list = 
    List.sum (List.mapi (fun i x -> if (i % 2 = 0) then x else 0) list)


[<EntryPoint>]
let main argv = 
    let list = [1; 2; 3; 4; 5; 6; 7; 8]
    printf "%A" (calcPar list)
    System.Console.ReadLine()
    0 // return an integer exit code
