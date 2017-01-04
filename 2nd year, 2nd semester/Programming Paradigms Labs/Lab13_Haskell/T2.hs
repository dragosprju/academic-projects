factorial :: [Int] -> Int
factorial x = factorial2 (length x)

factorial2 n = if n > 0 then n * factorial2 (n-1) else 1