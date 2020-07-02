#! /usr/bin/env python
import sys
import time
import subprocess
import select
import os

filename = sys.argv[1]
folder = os.path.splitext(filename)[0]

f = subprocess.Popen(['tail','-F',filename], \
        stdout=subprocess.PIPE,stderr=subprocess.PIPE)
p = select.poll()
p.register(f.stdout)
p.register(f.stderr)

loading = ["\\", "|", "/", "-"]
loading_i = 0

while True:
    if p.poll(1):
    	line = f.stdout.readline()

        if "FINISHED" in line:
            sys.stdout.write(" Finished!\n")
            sys.exit(0)
        # elif (("Core" in line) and ("done" in line)):
        #     sys.stdout.write("\r")
        #     sys.stdout.write(line + "      ")
    sys.stdout.write("\r")

    if len(sys.argv) > 2:
        sys.stdout.write("\033[1;32m" + sys.argv[2] + "\033[0m: Running..." + loading[loading_i])
    else:
        sys.stdout.write("Running..." + loading[loading_i])
    loading_i = (loading_i + 1) % 4
    time.sleep(0.1)
