// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

module Program1 =  

    let list1 = [1;2;3]
    let list2 = [for i in [1..3] do yield i*2]
   // let list3 = [for i in [1..3] do -> i*2]
    let list4 = List.map(fun x->x*2) list1

    let last list =
        let list_rev = List.rev list
        List.head list_rev

    let main =
        let list = [1; 2; 3]
        printfn "Ultimul element al listei %A este %A" list (last list)
        0

module Program2 =

    let palindrom list =
        let mutable result = true
        for i in 0..(List.length list / 2 - 1) do
            let index0 = i
            let index1 = List.length list - i - 1
            let value0 = List.nth list index0
            let value1 = List.nth list index1

            if value0 <> value1 then
                result <- false                
        result

    let main list =
        if palindrom list then
            printfn "Lista %A este palindrom." list
        else
            printfn "Lista %A nu este palindrom." list
        0

module Program3 =
    
    let duplicate list = 
        let mutable newList = []
        for i in 0..List.length list - 1 do
            newList <- newList@[(List.nth list i);(List.nth list i)]
        newList

    let duplicate2 list =
        let list2 = List.map (fun x -> [x;x]) list
        let list3 = List.concat list2
        list3

    let main list =
        printfn "Lista duplicata pt %A este %A" list (duplicate2 list)
        0

module Program4 = 

    let remove3th list =
        let mutable newList = []
        for i in 0..List.length list - 1 do
            if (i+1) % 3 <> 0 then
                newList <- newList@[(List.nth list i)]   
        newList

    let remove3th2 list =
        let list2 = List.mapi (fun i x -> (i+1, x)) list
        let list3 = List.filter (fun (i,x) -> (i % 3 <> 0)) list2
        let list4 = List.map (fun (i,x) -> x) list3
        list4

    let main list =
        printfn "Lista fara elementele index modulo 3 pt lista %A este lista %A" list (remove3th list)
        0

module Program5 =
    open System

    let random = new Random()
    let randomize noOfElements = List.map (fun x -> random.Next()) [1..noOfElements]

    let main noOfElements =
        printfn "Lista aleatoare: %A" (randomize noOfElements)
        0

module Program6 =
    
    open System

    let fibonacci22 list =
        let value1 = List.nth list (List.length list - 1)
        let value2 = List.nth list (List.length list - 2)
        list@[value1 + value2]
        
    let rec fibonacci maxNum =
        let mutable initList = [0;1]
        while (List.head (List.rev initList) < maxNum) do
            initList <- fibonacci22 initList
        initList

    let main value =
        printfn "%A" (fibonacci value)
        0

[<EntryPoint>]
let main argv = 
    Program1.main
    Program2.main [1;2;3;2;1]
    Program3.main [1;2]
    Program4.main [1;2;3;4;5;6;7]
    Program5.main 10
    Program6.main 1000
    0
//    Program6.main
    
