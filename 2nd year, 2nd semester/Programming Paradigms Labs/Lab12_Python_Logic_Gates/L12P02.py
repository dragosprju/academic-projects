from logic import *

P = Xor("P")
E = Xor("E")
G_fin = Or("Gfin")
G = And("G")
a = And("AND")

G_fin.C.monitor = 1

P.C.connect(E.A)
P.C.connect(a.A)
a.C.connect(G_fin.A)
G.C.connect(G_fin.B)

eval_a = input("A = ")
eval_b = input("B = ")
eval_c = input("C = ")

P.A.set(eval_a)
P.B.set(eval_b)
E.B.set(eval_c)
a.B.set(eval_c)
G.A.set(eval_a)
G.B.set(eval_b)




