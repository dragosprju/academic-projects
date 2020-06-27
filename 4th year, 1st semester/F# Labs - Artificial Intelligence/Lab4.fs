module Program1 =

    let lastButTwo1 list =  
        // cu |>
        List.rev list |> List.tail |> List.tail |> List.head

    let lastButTwo2 = 
        // cu >>
        List.rev >> List.tail >> List.tail >> List.head

    let mainP1() = 
        let list =  [ 1..10 ] 
        printfn "Antepenultimul element: %A" (lastButTwo1 list)
        printfn "Antepenultimul element: %A" (lastButTwo2 list)

// ==========================================================

module Program2 =

    let discount i =
        if i = 0 then
            1.0
        elif i = 1 then
            0.9
        elif i = 2 then
            0.8
        else
            1.0
    
    let discountedSum (list : float list) =
        if List.length list = 3 then
            List.sort list
            |> List.rev
            |> List.mapi (fun i x -> (x * discount i))
            |> List.sum
        else
            List.sum list
            

    let mainP2() =
        let list = [ 20.0; 10.0; 30.0]
        printfn "Preturile articolelor fara reduceri: %A" list
        printfn "Suma fara reduceri: %.2f" (List.sum list)
        printfn "Suma finala: %.2f\n" (discountedSum list)

        let list = [ 10.0; 20.0]
        printfn "Preturile articolelor fara reduceri: %A" list
        printfn "Suma fara reduceri: %.2f" (List.sum list)
        printfn "Suma finala: %.2f\n" (discountedSum list)

// ==========================================================

module Program3 =

    open System
    open System.IO

    let list =
        let lineToTuple (line : string) =
            let toks = line.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            (toks.[0], toks.[1], toks.[2])
        File.ReadAllLines "kinship.csv"
        |> List.ofSeq
        |> List.map lineToTuple

    let findParents1 =
        let mothers = List.filter (fun (a, _, _) -> a = "mother") list
        let fathers = List.filter (fun (a, _, _) -> a = "father") list
        [ 
            for (_, m, c1) in mothers do
                for (_, f, c2) in fathers do
                    if c1 = c2 then 
                        yield (m, f, c1)
        ]

    let getParent relationship child =
        let (_, b, _) = 
            List.find(fun (a,_,c) -> a = relationship && c = child) list
        b

    let findParents2 =
       list
       |> List.filter (fun (a,_,_) -> a = "mother" || a = "father")
       |> List.map (fun (_,_,c) -> ((getParent "father" c), (getParent "mother" c), c))
       |> List.map (fun (a,b,c) -> (string(a), string(b), string(c)) )
       |> Seq.distinct
       |> List.ofSeq

    let printParents = 
        List.iteri (fun i (m, f, c) -> printfn "%d. mama %s / tata %s / copilul %s" (i + 1) m f c)

    let mainP3() = 
        printfn "Metoda imperativa\n"
        findParents1 |> printParents

        printfn "\nMetoda functionala\n"
        findParents2 |> printParents

// ====================================================================

module Program4 = 

    type Person = { Name : string; Children : string list }

    let parents =
        let vlad = { Name = "Vlad"; Children = ["Oana"; "Mircea"] }
        let maria = { Name = "Maria"; Children = ["Dan"] }
        let nelu = { Name = "Nelu"; Children = ["Paul"; "Radu"; "Sorin"] }
        let liviu = { Name = "Liviu"; Children = ["Vlad"; "Maria"; "Nelu"] }
        [ liviu; vlad; maria; nelu ]

    let persons =
        parents 
        |> List.fold (fun acc parent -> List.concat [parent.Name :: acc; parent.Children]) []
        |> Seq.distinct |> List.ofSeq 

    let isSibling (personA : Person) (personB : Person) =
        parents
        |> List.map (fun parent -> parent.Children)
        |> List.map (fun list -> List.exists ((=) personA.Name) list && List.exists ((=) personB.Name) list)
        |> List.reduce (fun acc elem -> acc && elem)
        
    
    let findCousins q =
        persons 
        //|> List.map(fun name -> isCousin name q)
         
    let mainP4() =
        let query1 = "Sorin"
        let query2 = "Paul"
        printf "%s si %s sunt frati?: %b" (isSibling query1 query2)
        //let query = "Sorin"
        //printf "Verii lui %s sunt: " query
        //let result = findCousins query
        //printfn "%A" result
        //List.iter (fun s -> printfn "%s" s) persons

// ====================================================================

[<EntryPoint>]
let main argv = 
    Program1.mainP1()
    Program2.mainP2()
    Program3.mainP3()
    Program4.mainP4()
    0
