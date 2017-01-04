qsort 			:: String -> [String]
qsort x			 =
	qsort2 y
	where
		y = words x

qsort2 			:: [String] -> [String]
qsort2 	[]		 = []
qsort2 (x:xs)	 = 
	qsort2 smaller ++ [x] ++ qsort2 larger
	where
		smaller = [a | a <- xs, a <= x]
		larger  = [b | b <- xs, b > x]

