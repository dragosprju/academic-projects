from logic import *

and1 = And("AND1")
and2 = And("AND2")
and3 = And("AND3")
or1 = Or("OR")
xor1 = Xor("XOR")
not1 = Not("NOT")
not_or = Not("NOT-OR")

xor1.C.monitor = 1

eval_a = input("A = ")
eval_b = input("B = ")
eval_c = input("C = ")

or1.C.connect(not_or.A)
not_or.B.connect(not1.A)
not_or.B.connect(and3.A)
and1.C.connect(and2.A)
not1.B.connect(and2.B)

and2.C.connect(xor1.A)
and3.B.connect(xor1.B)

and1.A.set(eval_a)
and1.B.set(eval_b)
or1.A.set(eval_c)
or1.B.set(eval_a)
and3.B.set(eval_b)





