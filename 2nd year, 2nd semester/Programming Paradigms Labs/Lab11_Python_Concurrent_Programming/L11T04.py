from threading import Thread, Event
from random import shuffle

class FibonacciPool(Thread):
	fibonacciResults = {1:1, 2:1}
	fibonacciWorkers = []

	def __init__(self, noOfThreads):
		Thread.__init__(self)
		self.noOfThreads = noOfThreads

	def printResults(self):
		print "Result: ",
		for key in self.fibonacciResults:
			print str(self.fibonacciResults[key]) + " ",

	def run(self):
		for i in range(self.noOfThreads)[2:]:
			self.fibonacciWorkers.append(FibonacciWorker(self.fibonacciResults, i+1))
		#shuffle(self.fibonacciWorkers)
		for worker in self.fibonacciWorkers:
			worker.start()
		for worker in self.fibonacciWorkers:
			worker.join()
		self.printResults()


class FibonacciWorker(Thread):
	timer = Event()

	def __init__(self, results, index):
		Thread.__init__(self)
		self.index = index
		self.results = results

	def calculate(self):
		if self.index-1 in self.results and self.index-2 in self.results:
			self.calculateFromCache()
		else:
			self.calculateNormally()

	def calculateFromCache(self):
		print str(self.index) + ": Calculated using cache"
		self.results[self.index] = self.results[self.index-1] + self.results[self.index-2]

	def calculateNormally(self):
		print str(self.index) + ": Calculated normally"
		a = 1
		b = 1
		for i in range(self.index-2):
			result = a + b
			a = b
			b = result
		self.results[self.index] = result

	def run(self):
		self.calculate()

def main():
	fibonacciPool = FibonacciPool(10)
	fibonacciPool.start()

if __name__=="__main__":
	main()


