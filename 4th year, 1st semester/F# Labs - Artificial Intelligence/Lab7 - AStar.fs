namespace AStarFs


type Node = {
    State : string; 
    Path : string list; 
    F : float; 
    G : float; 
    H : float 
}


type AStar(problem : SearchProblems.IProblem) = 

    let getBest list =
        let nodeWithMinF = List.minBy (fun el -> el.F) list
        List.find (fun e -> e.F = nodeWithMinF.F) list


    let getSolution node =
        let path = List.fold (fun acc e -> e + " " + acc) node.State node.Path
        System.String.Format("Calea: {0}\r\nCostul : {1}", path, node.F)


    let isEqualToParent currentNode successorNode =
        // returneaza true daca succesorul exista deja in lista Path a nodului curent
        List.exists((=) successorNode) currentNode.Path


    let isBetterInList list successorNode g =
        // returneaza true daca in lista (openList sau ClosedList) exista deja un nod 
        // cu aceeasi stare cu a succesorului, dar cu g mai mic
         List.exists (fun x -> (x.State = successorNode && x.G <= g)) list


    let getValidSuccessors currentNode openList closedList =
        Array.fold(fun acc successor ->
            let g = currentNode.G + problem.GetEdgeCost(currentNode.State, successor)
            let h = problem.GetHeuristic successor
            let f = max currentNode.F (g+h)
            if isEqualToParent currentNode successor || isBetterInList openList successor g || isBetterInList closedList successor g then
                acc
            else
                let successorNode = { 
                    State = successor;
                    Path = currentNode.State :: currentNode.Path;
                    F = f; 
                    G = g;
                    H = h 
                } successorNode :: acc
            ) openList (problem.GetSuccessors currentNode.State)
            // adauga la openList doar succesorii care indeplinesc cele trei conditii


    let rec astar openList closedList =
        // daca openList este vida, returneaza: "Nu exista solutie"
        // altfel, preia primul nod din openList
        //	daca e solutie, returneaza: getSolution currentNode
        //	altfel, expandeaza nodul si actualizeaza cele doua liste: openList', closedList' 
        //	apel recursiv:
        if List.isEmpty openList then
            "Nu exista solutie"
        else
            let currentNode = getBest openList
            let openList' = List.filter (fun e-> e <> currentNode) openList

            if problem.IsGoal currentNode.State then
                getSolution currentNode
            else
                let openList' = getValidSuccessors currentNode openList' closedList
                let closedList' = currentNode::List.filter(fun e -> e.State <> currentNode.State) closedList
                astar openList' closedList'


    // metoda publica
    member this.Compute() = 
        let rootState = problem.GetRoot()
        let rootH = problem.GetHeuristic rootState
        let root = { State = rootState; Path = []; F = rootH; G = 0.0; H = rootH }
        astar [root] []