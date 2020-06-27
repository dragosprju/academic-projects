// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open System

module Lab1P1 =
    
    let rec gcd a b =
      if (b <> 0) then
        let t = b
        let b = a % b
        let a = t
        gcd a b
      else
        a

    let main _ = 
        let a, b = 12, 18
        printfn "cmmdc (%A, %A) = %A" a b (gcd a b)

// ====================================================================

module Lab1P2 = 

    let rec guess to_guess =
        let entered_value = int(System.Console.ReadLine());
        if to_guess = entered_value then
            printfn "Raspuns corect!"
        else 
            if to_guess < entered_value then
                printfn "Valoarea este mai mica"
                guess to_guess
            else
                printfn "Valoarea este mai mare"
                guess to_guess

    let main _ = // _ inseamna ca nu sunt argumente
        printfn "Introdu o valoare pt. a ghici numarul: "
        guess ((new Random()).Next(100))
        let c = Console.ReadLine();
        0 // return an integer exit code

// ====================================================================

module Lab1P3 = 

    let are_positive a b c = a > 0 && b > 0 && c > 0
    let are_unequal a b c = (a <> (b + c)) && (b <> (a + c)) && (c <> (a + b))
    let validate a b c = (are_positive a b c) && (are_unequal a b c)

    let read x =
        printf "Introduceti latura %A: " x
        int (Console.ReadLine())

    let main _ = 
        let a, b, c = read 1, read 2, read 3
        if validate a b c then
            printfn "Reprezinta un triunghi valid"
        else
            printfn "Nu reprezinta un triunghi valid"
        let d = Console.ReadLine()
        0 // return an integer exit code

// ====================================================================

module Lab1P4 = 

    let resolve_equation (ai, bi, ci) =
        if ai = 0 then
            (0.0, 0.0, 0)
        else
            let a, b, c = float ai, float bi, float ci
            let delta = (pown b 2) - 4.0 * a * c
            if delta > 0.0 then
                let sol1 = (-b * (sqrt delta)) / (2.0 * a)
                let sol2 = (-b - (sqrt delta)) / (2.0 * a)
                (sol1, sol2, 2)
            elif delta = 0.0 then
                let sol = (-b / (2.0 * a))
                (sol, sol, 1)
            else
                let rsol = -b / (2.0 * a)
                let isol = (sqrt (-delta)) / (2.0 * a)
                (rsol, isol, -2)

    let read x =
        printf "Introduceti coeficientul %A: " x
        int(Console.ReadLine())

    let main _ = 
        let a, b, c = read "a", read "b", read "c"
        let (x, y, t) = resolve_equation (a, b, c)
        if t = 2 then
            printf "Solutiile sunt: %A, %A" x y
        elif t = 1 then
            printf "Solutia este: %A" x
        elif t = -2 then
            printf "Solutiile sunt %A +- i * %A" x y
        else
            printf "Nu este o ecuatie de gradul 2 valida"
        let d = Console.ReadLine()
        0 // return an integer exit code


[<EntryPoint>]
let main argv = 
    Lab1P1.main()
    Lab1P2.main()
    Lab1P3.main()
    Lab1P4.main()
    let ended = Console.ReadLine()
    0 // return an integer exit code
