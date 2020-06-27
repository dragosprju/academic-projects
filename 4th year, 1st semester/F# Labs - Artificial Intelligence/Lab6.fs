(**************************************************************************
 *                                                                        *
 *  File:        Program.fs                                               *
 *  Copyright:   (c) 2016, Florin Leon                                    *
 *  E-mail:      florin.leon@tuiasi.ro                                    *
 *  Website:     http://florinleon.byethost24.com/lab_ia.htm              *
 *  Description: Active Patterns  (Artificial Intelligence lab 6)         *
 *                                                                        *
 **************************************************************************)

open System
open System.IO

let facts =
    File.ReadAllLines "viteza.kbf"
    |> List.ofSeq

let getText (toks : string[]) =
    Array.fold (fun acc s -> acc + " " + s) toks.[2] toks.[3..]

let (|Root|Question|Answers|Rule|Conclusion|Other|) (line : string, node) =
    let toks = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
    match toks.[0], node with
    | "root", _ -> Root toks.[1] 
    | "question", n when n = toks.[1] -> Question (getText toks)
    | "answers", n when n = toks.[1] -> Answers toks.[2..]
    | "rule", n when n = toks.[2] -> Rule (toks.[4], toks.[6])
    | "conclusion", n when n = toks.[1] -> Conclusion (getText toks)
    | _ -> Other toks

    // root (de exemplu: root n11)
    // question (de exemplu: question n11 Viteza isi pastreaza directia?)
    // answers (de exemplu: answers n11 da nu nu_stiu)
    // rule (de exemplu: rule when n11 if nu then n21)
    // spre deosebire de celelalte optiuni, Rule are ca argument o tupla de forma (if', then')
    // in exemplul de mai sus: Rule ("nu", "n21")
    // conclusion (de exemplu: conclusion n31 Miscare curbilinie neuniforma.)
    // Other

let getRoot() =
    List.choose (fun e -> 
        match e, "" with 
        | Root x -> Some x 
        | _ -> None) facts
    |> List.head

let getQuestion node = 
    List.choose (fun e ->
        match e, node with
        | Question x -> Some x
        | _ -> None) facts
    |> List.head

let getValidAnswers node =
   List.choose (fun e ->
        match e, node with
        | Answers x -> Some x
        | _ -> None) facts
    |> List.head

let applyRule node answer =
   List.choose (fun e ->
        match e, node with
        | Rule (if', then') when if' = answer -> Some then'
        | _ -> None) facts
    |> List.head

let getConclusion node =
   // numai frunzele au o concluzie, deci rezultatul poate fi o lista vida ([])
   // daca exista o concluzie c, se returneaza Some c, altfel None
   List.choose (fun e ->
        match e, node with
        | Conclusion c -> Some c
        | _ -> None) facts

let rec readAnswer validAnswers =
    // se concateneaza raspunsurile din validAnswers folosind Array.reduce, rezultand de exemplu: "da/nu"
    let allowed = Array.reduce(fun acc el -> acc + "/" + el) validAnswers
    printf "Raspunsul dumneavoastra (%s): " allowed
    let answer = Console.ReadLine()
    // daca answer este un raspuns valid (in validAnswers), atunci intoarce answer
    // altfel, apeleaza recursiv functia de citire
    if List.exists ((=)answer) (Array.toList validAnswers) then
        answer
    else
        readAnswer validAnswers

let rec mainLoop node =
    let conclusion = getConclusion node
    if not conclusion.IsEmpty then
        printfn "%s" (List.head conclusion)
    else
        let question = getQuestion node
        printfn "%s" question
        
        getValidAnswers node |> readAnswer |> applyRule node |> mainLoop

    //match getConclusion node with
    //| [c] -> 
    //    printfn "%s" c
    //| [_] ->
    //    let question = getQuestion node
   //     printfn "%s" question
   //     // se apeleaza getValidAnswers, readAnswer, applyRule, apoi se reia bucla mainLoop
   //     // se poate folosi operatorul pipe-forward |>
   //     getValidAnswers node |> readAnswer |> applyRule node |> mainLoop
   

[<EntryPoint>]
let main _ =
    getRoot() |> mainLoop
    System.Console.Read()
    0
