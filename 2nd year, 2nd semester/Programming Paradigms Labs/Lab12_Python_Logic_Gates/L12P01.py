#Creat porti
#Monitorizare (monitor)
#Conexiuni (connect)
#Setare valori (set)

from logic import *

a = Not("A")
b = Not("B")
c = Not("C")
si1 = And("SI1")
si2 = And("SI2")
si3 = And("SI3")
si4_1 = And("SI4_1")
si4 = And("SI4")
sau1 = Or("SAU1")
sau2 = Or("SAU2")
sau_total = Or("SAUTOT")

sau_total.C.monitor = 1
sau1.C.monitor = 1
sau2.C.monitor = 1
si1.C.monitor = 1
si2.C.monitor = 1
si3.C.monitor = 1
si4.C.monitor = 1

sau1.C.connect(sau_total.A)
sau2.C.connect(sau_total.B)
si4_1.C.connect(si4.A)

a.B.connect(si2.A)
b.B.connect(si2.B)

b.B.connect(si3.B)

a.B.connect(si4_1.B)

a.B.connect(si4.A)

si1.C.connect(sau1.A)
si2.C.connect(sau1.B)
si3.C.connect(sau2.A)
si4.C.connect(sau2.B)

eval_a = input("A = ")
eval_b = input("B = ")
eval_c = input("C = ")

a.A.set(eval_a)
b.A.set(eval_b)
c.A.set(eval_c)

si1.A.set(eval_b)
si1.B.set(eval_c)

si3.A.set(eval_a)

si4_1.B.set(eval_b)
si4.B.set(eval_c)




