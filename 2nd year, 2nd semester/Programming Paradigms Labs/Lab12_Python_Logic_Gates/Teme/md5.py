import hashlib
from shutil import *

filename = raw_input("Please input file name: ")
fo = open(filename, "r")
m = hashlib.md5()
m.update(fo.name)
result = m.hexdigest()
if filename.split(".")[-1] != filename:
	result += "." + filename.split(".")[-1]
copyfile(filename, result)
print "Created: %s" % (result)