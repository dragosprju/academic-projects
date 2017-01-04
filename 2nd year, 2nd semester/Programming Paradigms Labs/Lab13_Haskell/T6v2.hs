data Tree a = Leaf
            | Node Integer (Tree a) a (Tree a)
            deriving (Show, Eq)

foldTree :: [a] -> Tree a
foldTree = foldr (\x tree -> insertInTree x tree) Leaf

insertInTree :: a -> Tree a -> Tree a
insertInTree x Leaf = Node 0 (Leaf) x (Leaf)
insertInTree x (Node n t1 val t2) = if h1 < h2 
                                    then Node (h2+1) (insertInTree x t1) val t2 
                                    else Node (h1+1) t1 val (insertInTree x t2) 
  where h1 = heightTree t1
        h2 = heightTree t2

heightTree :: Tree a -> Integer
heightTree Leaf = 0
heightTree (Node n t1 val t2) = n


main = do
	putStrLn "Insereaza text: "
	text <- getLine
	let tree = foldTree (words text)
	return()