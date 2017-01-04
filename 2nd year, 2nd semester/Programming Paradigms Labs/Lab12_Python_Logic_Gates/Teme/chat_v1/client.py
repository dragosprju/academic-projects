import sys
from socket import *

def receive_forever(self):
	while 1:
		data = sSock.recv(1024)
		print data,

serverHost = 'localhost'
serverPort = 50008

#Crearea socket
sSock = socket(AF_INET, SOCK_STREAM)

#Conectarea la server
sSock.connect((serverHost, serverPort))

name = raw_input("Name: ")
#Trimitere date catre server.
line = ""
while line != 'bye':
	line = raw_input("<%s> " % (name))
	sSock.send("<" + name + "> " + line + '\n')
	#data = sSock.recv(1024)
	
sSock.shutdown(0)
sSock.close()