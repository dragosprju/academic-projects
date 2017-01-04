student(ion).
student(maria).
student(ana).
baiat(ion).
fata(maria).
fata(ana).
studenta(georgiana).
studenta(X) :- student(X), fata(X).
nota(ana, 10).
nota(maria, 4).
nota(ion, 7).
promovat(X) :- student(X), nota(X, Y), Y >= 5, \+ studenta(X).
