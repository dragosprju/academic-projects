import SocketServer
import string
import msvcrt
from threading import Thread

class myTCPServer(SocketServer.StreamRequestHandler):
	name = ""
	send_line = ""
	receive_line = ""
	thread_started = False

	def handle (self):
		if not self.thread_started:
			self.name = raw_input("Name: ")
			self.sender = Thread(target=self.send_forever)
			self.sender.start()
			self.thread_started = True
		while 1:
			self.receive_line = self.rfile.readline()
			print "%s" % (self.receive_line),

	def send_forever(self):
		print "Started!"
		while 1:
			if msvcrt.kbhit():
				self.send_line += msvcrt.getch()
				if msvcrt.getch() == '\r':
					self.wfile.write("<%s> %s" % (self.name, self.send_line))
					print "<%s> %s" % (self.name, self.send_line)
					self.send_line = ""



#Crearea obiectului SocketServer
serv = SocketServer.TCPServer(("",50008),myTCPServer)

#Activarea serverului pentru a putea gestiona cererile clientilor
serv.serve_forever()