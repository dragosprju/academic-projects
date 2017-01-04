contains(_, []) :- false.
contains(V, [V|_]) :- true.
contains(V, [_|T]) :- contains(V,T).
