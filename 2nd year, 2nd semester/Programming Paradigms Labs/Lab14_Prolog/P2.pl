fact(R,0) :- R is 1.
fact(R,N) :- X is N-1, fact(Q, X), R is N*Q.
