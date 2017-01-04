module Main where

data Tree a = Nil | Branch a (Tree a) (Tree a)
	deriving (Show, Eq)

main = do
	putStrLn "Insereaza text: "
	text <- getLine
	let textWords = (words text)
	tree <- Nil
	insert tree "Hello"

insert :: (Ord a) => Tree a -> a -> Tree a
insert Nil a = Branch a Nil Nil
insert (Branch a left right) b
	| b == a = Branch a left right
	| b < a = Branch a (insert left b) right
	| b > a = Branch a left (insert right b)
