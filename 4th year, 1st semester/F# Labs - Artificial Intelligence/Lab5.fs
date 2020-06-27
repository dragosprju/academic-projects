// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.


module Program1 =

    let fizzBuzz x = 
        match x with
        | x when x % 3 = 0 && x % 5 = 0 -> "FizzBuzz"
        | x when x % 3 = 0 -> "Fizz"
        | x when x % 5 = 0 -> "Buzz"        
        | _ -> string(x);

    let main _ =
        for i in 1..20 do
            printfn "%s" (fizzBuzz i);

module Program2 = 

    let compare a b =
        if a < b then "smaller"
        elif a > b then "bigger"
        else "equal"

    let takeLast list =
        List.rev (List.tail (List.rev list))

    let split list n =
        let rec take n list =
            match compare (List.length list) n with
            | "smaller" -> list
            | "bigger" -> take n (takeLast list)
            | "equal" -> list
            | _ -> [];

        let rec drop n list =
            //printfn "List for drop: %A;\r\nList length: %d; n is: %d; List length minus n: %d" list (List.length list) n ((List.length list)- n)
            //printfn "Result for compare: %s\r\n\r\n" (compare ((List.length list) - n) 0)
            match compare ((List.length list) - n) 0 with            
            | "smaller" -> []
            | "bigger" -> drop (n+1) (List.tail list) 
            | "equal" -> list            
            | _ -> [];
        take n list, drop n list

    let main _ =
        let list1 = List.ofSeq "123456789"
        printfn "%A" (split list1 3)

        let list2 = ['1'; '2']
        printfn "%A" (split list2 3)

        let list3 = []
        printfn "%A" (split list3 3)

module Program3 = 
    open System

    type Shape = 
    | Rectangle of height : int * width : int
    | Circle of radius : int

    let rec readValidInteger() =
        match Console.ReadLine() |> Int32.TryParse with
        | (true, n) when n > 0 -> n
        | (_,_) -> printfn "\nIntroduceti un numar intreg pozitiv!"; readValidInteger()

    let rec validChoice() =
        // permite numai introducerea "D" sau "C"
        match Console.ReadLine() with
        | "D" -> "dreptunghi"
        | "C" -> "cerc"
        | _ -> printfn "\nIntroduceti 'D' pentru dreptunghi sau 'C' pentru cerc. Evitati spatii!"; validChoice()

    let inputRectangle() =
        // se introduc lungimea si latimea si se returneaza obiectul Rectangle corespunzator
        printf "Introduceti lungimea: "
        let h = readValidInteger()
        printf "Introduceti latimea: "
        let w = readValidInteger()
        Rectangle(h, w)

    let inputCircle() =
        // se introduce raza si se returneaza obiectul Circle corespunzator
        printf "Introduceti raza cercului: "
        let r = readValidInteger()
        Circle(r)

    let inputShape x =
        printfn "Figura %d: Introduceti D pentru dreptunghi si C pentru cerc:" x
        // in functie de optiune, se apeleaza inputRectangle sau inputCircle
        let toCheck = validChoice()
        match toCheck with
        | "dreptunghi" -> inputRectangle()
        | "cerc" -> inputCircle()
        | _ -> printf "Ceva grav s-a intamplat!"; Rectangle(0, 0)


    let printShape (s : Shape) = 
        // in functie de tipul obiectului, returneaza un string
        // ("Dreptunghi cu lungimea %d si latimea %d h w) sau
        // ("Cerc cu raza %d" r)
        match s with
        | Rectangle (height, width) -> sprintf "Dreptunghi cu lungimea %d si latimea %d" height width
        | Circle (radius) -> sprintf "Cerc cu raza %d" radius
        | _ -> "Ceva grav s-a intamplat!"
        

    let main _ =
        List.init 3 (fun x -> inputShape (x+1))
        |> List.map printShape
        |> List.iter (printfn "%s")


[<EntryPoint>]
let main argv = 
    //Program1.main();
    //Program2.main();
    Program3.main();
    System.Console.ReadLine();
    0 // return an integer exit code
