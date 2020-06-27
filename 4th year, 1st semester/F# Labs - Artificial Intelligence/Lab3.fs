// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

module Program1 =

    let contFraction (vect : int[]) =
        let mutable result = double(0)
        for i = (vect.Length - 1) downto 1 do
            result <- 1.0/(result + double(vect.[i]))
        result <- result + double(vect.[0])
        result    
    
    let main _ = 
        let coefs = [|3; 7; 15|]
        printfn "Valoarea fractiei este:\t%A" (contFraction coefs)
        printfn "Valoarea lui pi este:\t%A" System.Math.PI

        let coefs = [|3; 7; 15; 1; 292; 1|]
        printfn "\nValoarea fractiei este:\t%A" (contFraction coefs)
        printfn "Valoarea lui pi este:\t%A" System.Math.PI
        0 // return an integer exit code

module Program2 = 
    
    let bubbleSort (vect: double[]) = 
        let mutable n = Array.length vect
        let mutable swapped = true
        while swapped do
            swapped <- false
            for i = 0 to n-2 do
                if vect.[i] > vect.[i+1] then
                    let t = vect.[i]
                    vect.[i] <- vect.[i+1]
                    vect.[i+1] <- t
                    swapped <- true
            n <- n-1
        vect

    let main _ =
        let rand = System.Random()
        let vect = Array.init 100 (fun x -> rand.NextDouble() * 100.0)
        printfn "Vectorul sortat: " 
        Array.iter (printf "%.2f\t") (bubbleSort vect)

module Program3 =

    let multiply (A:int[,])(B:int[,]) =
        let n = A.GetLength(0)
        let m = A.GetLength(1)
        let o = B.GetLength(0)
        let p = B.GetLength(1)
        
        if m <> o then
            Array2D.init 1 1 (fun x y -> 0)
        else
            let C = Array2D.init n p (fun x y -> 0)
            for i = 0 to n-1 do
                for j = 0 to p-1 do
                    for k = 0 to m-1 do
                        C.[i,j] <- C.[i,j] + (A.[i,k] * B.[k,j])
            C        
    
    let main _ =

        let rand = System.Random()
        //let a = Array2D.init 2 3 (fun x y -> rand.Next(10))
        //let b = Array2D.init 3 4 (fun x y -> rand.Next(10))     
        
        let a = array2D [[8;4;4];[7;9;5]]
        let b = array2D [[6;4;5;1];[4;9;0;9];[4;2;6;0]]   

        printf "A = \n%A" a
        printf "\n\n"
        printf "B = \n%A" b
        printf "\n\n"

        let c = multiply a b
        printf "A * B = \n%A" c
        
        let d = multiply b a
        printf "\nB * A = \n %A" d
[<EntryPoint>]
let main _ =
    Program1.main()
    Program2.main()
    Program3.main()
    System.Console.ReadLine()
    0