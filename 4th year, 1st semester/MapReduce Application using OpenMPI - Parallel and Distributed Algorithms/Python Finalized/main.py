#!/usr/bin/env python
# -*- coding: utf-8 -*-

# ------------------------------------------------------------------ #
#  Biblioteci                                                        #
# ------------------------------------------------------------------ #

# Biblioteci generale
import sys
import os
import time
import string
from os import listdir
from os.path import isfile, join

# Bibliotecă MPI
from mpi4py import MPI

# Biblioteci personale
# - Clase statice sunt cu literă mică la început
# - Clase instanțiabile sunt cu literă mare la început
from mapreduce import constants
from mapreduce import error
from mapreduce import commands
from mapreduce import funcs

# ------------------------------------------------------------------ #
#  Valori globale                                                    #
# ------------------------------------------------------------------ #

comm = MPI.COMM_WORLD
size = comm.Get_size()
rank = comm.Get_rank()

# Alfabetul "abcd...xyz012..89"
alphanumerics_list = list(string.ascii_lowercase) + list(string.digits)
current_directory = os.path.dirname(os.path.realpath(__file__))
start_time = 0


# ------------------------------------------------------------------ #
#  Faza 0: Verificări preeliminare                                   #
# ------------------------------------------------------------------ #

###############################################
# Verificările preeliminare făcute de nodul 0 #
###############################################
if rank == 0:

    # Dorim ca numărul de procese să fie mai mare decât 1. În caz că este doar un proces,
    # numai nodul de rang 0 ne poate anunța acest lucru, deci verificarea este făcută
    # de către acesta.
    if size == 1:
        error.show("The number of processes '-np' argument should be bigger than 1.")
        exit(-1)

    # Verificăm dacă numărul de procese (size) acoperă și rădăcina aleasă arbitrar
    # în fișierul constants.py. Este logic ca acest lucru să fie făcut de procesul
    # cu rangul 0, în caz că -np este 1 și constants.ROOT este >=1.
    if constants.ROOT >= size:
        error.show("The root (value chosen in constants.py) is out of range for the number of" \
                   " processes '-np'. Either choose a smaller rank for root or" \
                   " choose a bigger number for the number of processes '-np'.")
        exit(-1)

################################################################
# Verificările preeliminare făcute la rădăcina aleasă arbitrar #
################################################################
if rank == constants.ROOT: # nu elif pt. că se poate ca rădăcina (constants.ROOT) să fie și rangul 0

    # !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! #
    # Ținem și un timp pentru afișarea duratei între faze/pași de lucru #
    # !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! #
    start_time = time.time()
    if constants.ANNOUNCE_END_PHASE1 == 1 or constants.ANNOUNCE_END_PHASE2 == 1 or constants.ANNOUNCE_END_PHASE3 == 1:
        error.show_info("Started.", rank=rank)

    # Verificăm dacă s-au dat argumentele necesare
    if len(sys.argv) < 3:
        error.show("The program must have 2 arguments. You need to include a directory to read files from" \
                   " and a directory to write resulting files into.")
        exit(-1)

    # Verificăm dacă argumentele necesare sunt directoare existente
    if not os.path.exists(sys.argv[1]):
        error.show("The first given argument is not a directory path or the directory doesn't exist.")
        exit(-1)
    if not os.path.exists(sys.argv[2]):
        error.show("The second given argument is not a directory path or the directory doesn't exist.")
        exit(-1)

#########################################################################
# Verificările preeliminare făcute la nodurile auxiliare / non-rădăcină #
#########################################################################
# Deocamdată nodurile auxiliare așteaptă ca rădăcina să-și facă
# verificările preeliminare, deci nu fac nimic.
elif rank <> constants.ROOT:
    pass

#######################################################################
# Pasul după verificările preeliminare, făcut de către toate nodurile #
#######################################################################
# Notăm directoriile date ca argument ca adrese absolute,
# pentru că s-au confirmat a fi corecte
dirpath_to_read_from = current_directory + "/" + str(sys.argv[1])
dirpath_to_save_into = current_directory + "/" + str(sys.argv[2])


# ------------------------------------------------------------------ #
#  Faza 1: Mapare                                                    #
# ------------------------------------------------------------------ #

#############################
# Maparea la toate nodurile #
#############################
# Rădăcina și nodurile își populează, fiecare în privat, lista cu fișierele inițiale de procesat
files = [join(dirpath_to_read_from,f) for f in listdir(dirpath_to_read_from) if isfile(join(dirpath_to_read_from, f))]

#######################
# Maparea la rădăcină #
#######################
# Rădăcina va împărți numere de ordine ale fișierelor pentru fiecare nod auxiliar,
# care să reprezinte numerele de ordine a fișierelor pe care nodul respectiv le va procesa
if rank == constants.ROOT:

    # Aici se populează o listă cu valori 0..24, când sunt 25 de fișiere inițiale
    workjobs = [i for i in range(len(files))]

    # Aici se împart la cerere fișierele inițiale de procesat de către nodurile auxiliare
    while len(workjobs) > 0:
        status = MPI.Status()
        message = comm.recv(source=MPI.ANY_SOURCE, tag=constants.COMMANDS_TAG, status=status)

        sender = status.source
        if constants.INFO_PRINT_PHASE1_2:
            error.show_info("(Phase 1) Got %s from node #%s" % (str(message), str(sender)), rank=rank)

        if message == commands.WANT_JOB_PHASE1:
            job = workjobs.pop()
            comm.send(job, dest=sender, tag=constants.DATA_TAG)

            if constants.INFO_PRINT_PHASE1_2:
                error.show_info("(Phase 1) Sending job %s to node #%s" % (str(job), str(sender)), rank=rank)
        else:
            comm.send(commands.RETRY_LATER, dest=sender, tag=constants.COMMANDS_TAG)

            if constants.INFO_PRINT_PHASE1_2:
                error.show_info("(Phase 1) Sending RETRY_LATER to node #%s" % str(message), rank=rank)

    # Aici se trimite informația necesară ca nodurile auxiliare să oprească bucla de cerere
    # de muncă pentru faza 1 și să treacă la faza 2
    for nodei in range(size):
        if nodei <> constants.ROOT:
            comm.send(commands.NEXT_PHASE, dest=nodei, tag=constants.COMMANDS_TAG)
                                # ^ Aici valoarea „comandă” trimisă este importantă,
                                # deoarece nodurile auxiliare se uită specific după
                                # valoarea NEXT_PHASE încât să oprească bucla de
                                # primire a fișierelor index

            message = commands.EMPTY_COMMAND
            while message <> commands.ACK_NEXT_PHASE:
                message = comm.recv(source=nodei, tag=constants.COMMANDS_TAG)

    # Cu acest mesaj se anunță că s-a terminat această fază
    if constants.INFO_PRINT_PHASE1_1:
        error.show_info("(Phase 1) Successfully distributed %d jobs as files to be worked on." % len(files), rank=rank)

#################################
# Maparea la nodurile auxiliare #
#################################
elif rank <> constants.ROOT:

    dirpath_phase1 = dirpath_to_save_into + "/" + constants.MAP_FOLDERNAME

    # Ca și nod auxiliar de rang X, deschid fișiere goale pentru scriere, denumite conform alfabetului "0..9ab..z",
    # la care nume mai atașez și rangul meu, încât să nu fac concurență cu celelalte noduri auxiliare.
    filename_pattern = constants.REPLACE_DESCRIPTOR + "_node%d.txt" % rank
    to_write_in_files = funcs.create_files_dict_from_pattern(dirpath_phase1, filename_pattern, alphanumerics_list)

    cmd = commands.EMPTY_COMMAND

    # Tot cer de muncă și lucrez, până când rădăcina mă anunță să mă duc la următoarea fază
    while cmd != commands.NEXT_PHASE:
        # Cer
        comm.send(commands.WANT_JOB_PHASE1, dest=constants.ROOT, tag=constants.COMMANDS_TAG)

        status = MPI.Status()
        # Primesc de muncă
        message = comm.recv(source=constants.ROOT, tag=MPI.ANY_TAG, status=status)
        tag = status.tag

        if constants.INFO_PRINT_PHASE1_2:
            error.show_info("(Phase 1) Got %s from root" % str(message), rank=rank)

        # Fac o filtrare a ce primesc, încât pot primi o comandă de a mă muta la faza următoare
        # sau pot primi un index de fișier inițial de lucrat. Aceste două informații le primesc
        # pe TAGuri diferite a spațiului de comunicare.
        if tag == constants.COMMANDS_TAG:
            cmd = message

            if cmd == commands.NEXT_PHASE:
                comm.send(commands.ACK_NEXT_PHASE, dest=constants.ROOT, tag=constants.COMMANDS_TAG)
        elif tag == constants.DATA_TAG:
            filei = message

            # Traduc, ca și nod auxiliar, numerele de ordine primite în adrese absolute
            # la fișierele de lucru asignate mie
            workfile = files[filei]

            # Informez utilizatorul, dacă se dorește, ce fișiere am de procesat în această fază de mapare.
            # Amintim că în mapreduce/constants.py se poate activa/dezactiva afișarea mesajelor.
            if constants.INFO_PRINT_PHASE1_1:
                workfile_name = workfile.split('/')[-1]
                error.show_info("(Phase 1) Working on %s." % workfile_name, rank=rank)

            # Pregătesc a nota aparițiile cuvintelor sub format <nume_fișier.txt, cuvânt, 1>
            # în fișiere numite sub forma a_node##.txt, .. , z_node##.txt, 0_node##.txt, .. , 9_node##.txt,
            # cuvinte apărute DIN directorul de la argumentul 2, aparițiile fiind notate ÎN fișiere create
            # pe directoriul "working_files/map"
            filename_pattern = constants.REPLACE_DESCRIPTOR + "_node%d.txt" % rank

            funcs.map_file(workfile, to_write_in_files)

    for file_key in to_write_in_files:
        to_write_in_files[file_key].close()

    if constants.INFO_PRINT_PHASE1_1:
        error.show_info("Finished!", rank=rank)

#############################
# Maparea la nodul rădăcină #
#############################
# Informăm utilizatorul că am terminat prima fază
if rank == constants.ROOT and constants.ANNOUNCE_END_PHASE1:
    elapsed_time = time.time() - start_time
    error.show_info("Passed phase #1 (mapping).  Time: %.4f" % elapsed_time, rank=rank)


# ------------------------------------------------------------------ #
#  Faza 2: Reducere                                                  #
# ------------------------------------------------------------------ #

#########################
# Reducerea la rădăcină #
#########################
# Cum în folderul în care au rezultat fișierele de după faza de mapare sunt în format:
# a_node##.txt, ..., z_node##.txt, 0_node##.txt, ..., 9_node##.txt
# vom asigna lucru la fiecare nod prin a împărți 'alfabetul' menționat la începutul acestui
# fișier cu aceeași metodă cu care am distribuit fișierele inițiale.
#
# Dacă eu ca nod am primit literele 'f', 'g', 'h', voi lua toate fișierele din
# directoriul unde s-a salvat rezultatele mapării (implicit "working_files/map") și voi alege să reduc pe rând
# fișierele care au ca prima literă 'f', apoi 'g', apoi 'h', în fișiere denumite similar
# după cele inițiale, dar la care mai adaug și rangului nodului meu ("1.txt_node##.txt").
#
# Reducere înseamnă să luăm toate liniile din toate fișierele care sunt supuse
# formatului <cuvânt, nume_fișier.txt 1> și să sumăm ultima valoare din tuplu,
# atât timp cât cuvântul împreună cu "nume_fișier.txt" corespund între termenii de sumat,
# rezultând o singură linie de același format <cuvânt, nume_fișier.txt, TOTAL_APARIȚII>.
if rank == constants.ROOT:

    # Funcționalitatea la rădăcină este foarte similară cu cele din celelalte faze.
    # Se răspunde la cereri de muncă până când nu mai este de lucru la această fază.
    workjobs = list(alphanumerics_list)

    while len(workjobs) > 0:
        status = MPI.Status()
        message = comm.recv(source=MPI.ANY_SOURCE, tag=constants.COMMANDS_TAG, status=status)

        sender = status.source
        if constants.INFO_PRINT_PHASE2_2:
            error.show_info("(Phase 2) Got %s from node #%s" % (str(message), str(sender)), rank=rank)

        if message == commands.WANT_JOB_PHASE2:
            job = workjobs.pop()
            comm.send(job, dest=sender, tag=constants.DATA_TAG)

            if constants.INFO_PRINT_PHASE2_2:
                error.show_info("(Phase 2) Sending job %s to node #%s" % (str(job), str(sender)), rank=rank)
        else:
            comm.send(commands.RETRY_LATER, dest=sender, tag=constants.COMMANDS_TAG)

            if constants.INFO_PRINT_PHASE2_2:
                error.show_info("(Phase 2) Sending RETRY_LATER to node #%s" % str(message), rank=rank)

    # Se trimite un semnal de trecere la faza următoare și se așteaptă confirmare.
    for nodei in range(size):
        if nodei <> constants.ROOT:
            comm.send(commands.NEXT_PHASE, dest=nodei, tag=constants.COMMANDS_TAG)

            message = commands.EMPTY_COMMAND
            while message <> commands.ACK_NEXT_PHASE:
                message = comm.recv(source=nodei, tag=constants.COMMANDS_TAG)

    if constants.INFO_PRINT_PHASE2_1:
        error.show_info("(Phase 2) Distributed %d alphanumeric elements to auxiliary nodes as file indexes." % len(alphanumerics_list), rank=rank)

###################################
# Reducerea la nodurile auxiliare #
###################################
# De aici vor rezulta fișiere de format <cuvânt, nume_fișier.txt, TOTAL_APARIȚII> în
# directorul specific acestei faze, implicit setat a fi "working_files/reduce"
if rank <> constants.ROOT:

    dirpath_phase2_read = dirpath_phase1
    dirpath_phase2_write = dirpath_to_save_into + "/" + constants.REDUCE_FOLDERNAME
    reduce_pattern = constants.REPLACE_DESCRIPTOR + "_node%d.txt" % rank

    # Se iau toate fișierele de la faza anterioară și se pun într-o listă.
    # Din această listă, se extrag fișierele ce se cuvin pentru lucru unui nod auxiliar la un moment dat,
    # depinzând de partiția de muncă indicată de rădăcină.
    workfiles_all = [f for f in listdir(dirpath_phase2_read) if isfile(join(dirpath_phase2_read, f))]

    # Cod din nou similar între nodurile auxiliare
    cmd = commands.EMPTY_COMMAND

    while cmd != commands.NEXT_PHASE:
        comm.send(commands.WANT_JOB_PHASE2, dest=constants.ROOT, tag=constants.COMMANDS_TAG)

        status = MPI.Status()
        message = comm.recv(source=constants.ROOT, tag=MPI.ANY_TAG, status=status)
        tag = status.tag

        if constants.INFO_PRINT_PHASE2_2:
            error.show_info("(Phase 2) Got %s from root" % str(message), rank=rank)

        if tag == constants.COMMANDS_TAG:
            cmd = message

            if cmd == commands.NEXT_PHASE:
                comm.send(commands.ACK_NEXT_PHASE, dest=constants.ROOT, tag=constants.COMMANDS_TAG)
        elif tag == constants.DATA_TAG:
            filei = message

            # De aici se particularizează față de celelalte faze: Aici se extrag fișierele
            # cuvenite nodului după ce s-a primit indexul de fișier (partiția de lucru) de la rădăcină.
            workfiles_mine = [dirpath_phase2_read + "/" + w for w in workfiles_all if w[0] in filei]

            if constants.INFO_PRINT_PHASE2_1:
                error.show_info("(Phase 2) Working on files starting with '%s'." % filei[0], rank=rank)

            # Aici are loc reducerea în sine
            funcs.reduce_files_by_docid(workfiles_mine, dirpath_phase2_write, reduce_pattern)

###############################
# Reducerea la nodul rădăcină #
###############################
# Informăm utilizatorul că am terminat a doua fază
if rank == constants.ROOT and constants.ANNOUNCE_END_PHASE2:
    elapsed_time = time.time() - start_time
    error.show_info("Passed phase #2 (reducing). Time: %.4f" % elapsed_time, rank=rank)

# ------------------------------------------------------------------ #
#  Faza 3: Reducere #2                                               #
# ------------------------------------------------------------------ #
# În faza aceasta, nodurilor auxiliare le vor fi împărțite fișierele din directorul "working_files/reduce"
# rezultate după faza 2, în care avem nume de fișiere de forma "NUME_FISIER_INITIAL_node##.txt". Acestea vor fi
# distribuite in grupări pe baza NUME_FISIER_INITIAL, astfel încât două noduri să nu lucreze la un grup în același
# timp, deoarece din fiecare grup va rezulta un singur fișier NUME-FISIER-INITIAL cu cheile/indecșii finale/finali
# și dacă s-ar întâmpla cum nu vrem - două noduri ar scrie în același fișier.
#
# Ce se întâmplă în această fază este pur și simplu o concatenare a fișierelor de la faza 2, unde s-a obținut
# cheile/indexurile dorite doar că s-au salvat în fișiere diferite, pe baza alfabetului. Este avut grijă a se sorta
# totuși cuvintele între ele și a se realiza INVERSAREA INDEXULUI.

############################
# Reducerea #2 la rădăcină #
############################
if rank == constants.ROOT:

    # Folosim aceeași metodă ca în prima fază (cea de a trimite indecși a fișierelor sub formă de numere de ordine).
    workjobs = [i for i in range(len(files))]

    # Cod similar între rădăcini
    while len(workjobs) > 0:
        status = MPI.Status()
        message = comm.recv(source=MPI.ANY_SOURCE, tag=constants.COMMANDS_TAG, status=status)

        sender = status.source
        if constants.INFO_PRINT_PHASE3_2:
            error.show_info("(Phase 3) Got %s from node #%s" % (str(message), str(sender)), rank=rank)

        if message == commands.WANT_JOB_PHASE3:
            job = workjobs.pop()
            comm.send(job, dest=sender, tag=constants.DATA_TAG)

            if constants.INFO_PRINT_PHASE3_2:
                error.show_info("(Phase 3) Sending job %s to node #%s" % (str(job), str(sender)), rank=rank)
        else:
            comm.send(commands.RETRY_LATER, dest=sender, tag=constants.COMMANDS_TAG)

            if constants.INFO_PRINT_PHASE3_2:
                error.show_info("(Phase 3) Sending RETRY_LATER to node #%s" % str(message), rank=rank)

    for nodei in range(size):
        if nodei <> constants.ROOT:
            comm.send(commands.NEXT_PHASE, dest=nodei, tag=constants.COMMANDS_TAG)

            message = commands.EMPTY_COMMAND
            while message <> commands.ACK_NEXT_PHASE:
                message = comm.recv(source=nodei, tag=constants.COMMANDS_TAG)

    if rank == constants.ROOT and constants.INFO_PRINT_PHASE3_1:
        error.show_info("Distributed %s file names to auxiliary nodes as file indexes " % len(files) + os.linesep \
                        + "(they should access files to work on, based on the initial files' names).", rank=rank, args=[len(files)])

######################################
# Reducerea #2 la nodurile auxiliare #
######################################
elif rank <> constants.ROOT:

    dirpath_phase3_read = dirpath_phase2_write
    dirpath_phase3_write = dirpath_to_save_into + "/" + constants.REDUCE2_FOLDERNAME

    cmd = commands.EMPTY_COMMAND

    # Cod similar între nodurile auxiliare
    while cmd != commands.NEXT_PHASE:
        comm.send(commands.WANT_JOB_PHASE3, dest=constants.ROOT, tag=constants.COMMANDS_TAG)

        status = MPI.Status()
        message = comm.recv(source=constants.ROOT, tag=MPI.ANY_TAG, status=status)
        tag = status.tag

        if constants.INFO_PRINT_PHASE3_2:
            error.show_info("(Phase 3) Got %s from root" % str(message), rank=rank)

        if tag == constants.COMMANDS_TAG:
            cmd = message

            if cmd == commands.NEXT_PHASE:
                # Nodurile auxiliare semnalează rădăcinei că au terminat. Menționăm posibilitatea unor
                # noduri auxiliare ce pot da eșec, astfel încât dacă nu se mai ajunge la execuția liniei de cod
                # de mai jos, un eventual sistem de reasignare a „porției” de lucru la alt nod auxiliar poate fi creat
                # în acest sens.
                comm.send(commands.ACK_NEXT_PHASE, dest=constants.ROOT, tag=constants.COMMANDS_TAG)
        elif tag == constants.DATA_TAG:
            filei = message

            # Nodurile auxiliare primesc deci numele fișierelor inițiale, pe baza cărora se iau fișierele obținute
            # din faza 2 pentru a lucra peste ele și practic a le concatena în mod sortat.
            filename = files[filei].split("/")[-1]

            # Aici se realizează căutarea fișerelor denumite în forma
            # "NUME-FISIER-INITIAL_node##.txt" pe baza numelor de fișiere inițiale primite
            workfiles = []
            auxiliary_nodes = [node for node in range(size) if node <> constants.ROOT]
            for nodei in auxiliary_nodes:
                workfiles.append(filename + "_node%d.txt" % nodei)
            workfiles = [w for w in workfiles if isfile(dirpath_phase3_read + "/" + w)]

            # Informarea listei de fișiere în care se va scrie, înainte de a atașa
            # și adresa absolută la ele
            if constants.INFO_PRINT_PHASE3_1 and rank == constants.WHO_PRINTS_AT_PHASE3:
                error.show_info("(Phase 3) I am working on %s." % str(workfiles), rank = rank)

            # Atașăm adresa absolută la fișiere și pregătim un parametru pentru funcția ce se va apela
            reduce2_pattern = constants.REPLACE_DESCRIPTOR
            workfiles = [dirpath_phase3_read + "/" + w for w in workfiles]

            # Se folosește aceeași funcție ca în faza 2, doar că dorim inversare
            funcs.reduce_files_by_docid(workfiles, dirpath_phase3_write, reduce2_pattern, inverse_write=True)


##################################
# Reducerea #2 la nodul rădăcină #
##################################
# Se afișează ultima informare a utilizatorului și se oprește timpul de executat.
# Amintim că acest timp este urmărit de rădăcina, dar este corect deoarece se tot așteaptă
# ca toate nodurile să termine la fiecare fază.
if rank == constants.ROOT:
    elapsed_time = time.time() - start_time
    error.show_info("Passed phase #3 (reducing #2). Time: %.4f" % elapsed_time, rank=rank)
    error.show_info("Finished.", rank=rank)










