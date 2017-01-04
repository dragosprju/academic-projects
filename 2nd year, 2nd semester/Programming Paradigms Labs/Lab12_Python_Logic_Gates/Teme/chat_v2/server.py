import SocketServer
import string
import msvcrt
from threading import Thread

class myTCPServer(SocketServer.StreamRequestHandler):
	name = ""

	def handle (self):
		if self.name == "":
			self.name = raw_input("Name: ")
		while 1:
			self.receive_line = self.rfile.readline()
			print "%s" % (self.receive_line),
			send_line = raw_input("<%s> " % (self.name))
			self.wfile.write("<" + self.name + "> " + send_line + "\n")

#Crearea obiectului SocketServer
serv = SocketServer.TCPServer(("",50008),myTCPServer)

#Activarea serverului pentru a putea gestiona cererile clientilor
serv.serve_forever()