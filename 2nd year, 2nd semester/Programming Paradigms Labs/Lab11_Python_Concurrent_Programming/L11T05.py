from threading import Thread

class Master(Thread):
	results = {}
	slaves = []

	def __init__(self, noOfResults):
		if not noOfResults % 4 == 0:
			print "Number of results is not dividable by 4"
			self = None
		else:
			Thread.__init__(self)
			for i in range(noOfResults):
				self.results[i] = 0

	def printResults(self):
		print "Result: ",
		for key in self.results:
			print str(self.results[key]) + " ",

	def run(self):
		for i in range(4):
			self.slaves.append(Slave(self.results, i+1))
		for slave in self.slaves:
			slave.start()
		for slave in self.slaves:
			slave.join()
		self.printResults()


class Slave(Thread):

	def __init__(self, results, part):
		Thread.__init__(self)
		self.results = results
		self.part = part

	def run(self):
		for i in range(len(self.results))[len(self.results)/4*(self.part-1):len(self.results)/4*self.part]:
			self.calculate(i)

	def calculate(self, index):
		if index == 0 or index == 1:
			self.results[index] = 1
		else:
			a = 1
			b = 1
			result = a + b
			for i in range(index-2):			
				a = b
				b = result
				result = a + b
			self.results[index] = result

def main():
	master = Master(20)
	master.start()

if __name__=='__main__':
	main()