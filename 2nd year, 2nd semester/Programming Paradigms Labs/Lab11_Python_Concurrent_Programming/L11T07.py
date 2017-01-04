from threading import Thread
from graphics import *

class Pipeline1(Thread):

	def __init__(self, results, alpha):
		Thread.__init__(self)
		self.results = results

	def run(self):
		self.results = [nr * 3 for nr in self.results]
		print "Pipeline 1 finished."

class Pipeline2(Thread):

	def __init__(self, results):
		Thread.__init__(self)
		self.results = results

	def run(self):
		self.results.sort()
		print "Pipeline 2 finished."

class Pipeline3(Thread):

	def __init__(self, results):
		Thread.__init__(self)
		self.results = results

	def run(self):
		win = GraphWin("Graph")
		win.setCoords(0.0, 0.0, 10.0, 10.0)
		for number in self.results:
			p = Point(number, number)
			p.setFill('red')
			p.draw(win)
		try:
			win.getKey()
			win.close()
		except:
			print "Pipeline 3 finished."

def main():
	results = [1, 3, 3.5, 3.2, 3]
	pipeline1, pipeline2, pipeline3 = Pipeline1(results, 2), Pipeline2(results), Pipeline3(results)

	pipeline1.start()
	pipeline1.join()
	pipeline2.start()
	pipeline2.join()
	pipeline3.start()
	pipeline3.join()
	print "Main ended."

if __name__=='__main__':
	main()