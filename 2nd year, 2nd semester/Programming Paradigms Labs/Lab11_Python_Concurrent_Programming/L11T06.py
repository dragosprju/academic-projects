from threading import Thread
from Queue import Queue, Empty

class Peer(Thread):

	def __init__(self, ID, queue, results):
		Thread.__init__(self)
		self.ID = ID
		self.queue = queue
		self.results = results

	def calculate(self, index):
		if index == 1 or index == 2:
			self.results[index] = 1
			return 1
		else:
			a = 1
			b = 1
			result = a + b
			for i in range(index-2):
				a = b
				b = result
				result = a + b
			self.results[index] = result
			return result

	def run(self):
		while not self.queue.empty():			
			try:
				got = self.queue.get(False)
			except Empty:
				break
			result = self.calculate(got)
			print "Peer " + str(self.ID) + ": Calculating " + str(result) + "\n"
		print  "Peer " + str(self.ID) + ": Finished." + "\n"

def main():
	peers = []
	queue = Queue()
	results = {}

	for i in range(20):
		queue.put(i+1)
	for i in range(4):
		peers.append(Peer(i+1, queue, results))
	for peer in peers:
		peer.start()
	for peer in peers:
		peer.join()
	print "Results: "
	for key in results:
		print str(results[key]) + " ",

if __name__=='__main__':
	main()

