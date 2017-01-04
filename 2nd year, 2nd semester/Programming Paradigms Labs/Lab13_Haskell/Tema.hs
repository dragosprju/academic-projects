p :: Parser (Char,Char)
p = do 
	x <- item
	item
	y <- item
	return (x,y)