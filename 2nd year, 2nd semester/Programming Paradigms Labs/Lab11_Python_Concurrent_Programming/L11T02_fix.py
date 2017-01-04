from threading import Thread, RLock, Event
from Queue import PriorityQueue

class ThreadPool(Thread):
	_started = False
	lock = RLock()
	threads = PriorityQueue()
	_stop = Event()
	timer = Event()

	def __init__(self, secondsSharing, credit, nextThreadPool):
		Thread.__init__(self)
		self.secondsSharing = secondsSharing
		self.credit = credit
		self.nextThreadPool = nextThreadPool

	def addTask(self, thread):
		self.threads.put(thread)
		thread.setCredit(self.credit)
		thread.setLock(self.lock)
		thread.setStop(self._stop)

	def readdTask(self, thread):
		newThread = Task(thread.getID(), thread.getState())
		self.threads.put(newThread)
		newThread.setCredit(thread.getCredit())
		newThread.setLock(self.lock)
		newThread.setStop(self._stop)

	@staticmethod
	def addTaskToThreadPool(threadPool, thread):
		print "Confirm adding to thread pool " + str(threadPool.secondsSharing)
		newThread = Task(thread.getID(), thread.getState())
		threadPool.threads.put(newThread)
		print threadPool.threads
		newThread.setCredit(threadPool.credit)
		newThread.setLock(threadPool.lock)
		newThread.setStop(threadPool._stop)	

	def stopTask(self, thread):
		self._stop.set()

	def startTask(self, thread):
		self._stop.clear()
		thread.start()

	def started(self):
		return self._started

	def stop(self):
		self._started = False
		print "\nStopping thread pool " + str(self.secondsSharing) + "\n"
		if not self.nextThreadPool == None:
			print "\nStarting next thread pool " + str(self.nextThreadPool.secondsSharing)
			self.nextThreadPool.start()

	def run(self):
		self._started = True
		while self._started:
			if self.threads.empty():
				self.stop()
			else:
				runningThread = self.threads.get()
				self.timer.wait(1)
				print "Thread Pool " + str(self.secondsSharing) + " pulled thread " + str(runningThread.getID()) + " with credit " + str(runningThread.getCredit()) + "\n"
				if runningThread.getCredit() == -1 or (self.nextThreadPool == None and runningThread.getCredit() == 0):
					self.stop()
				elif not self.nextThreadPool == None and runningThread.getCredit() == 0:
					print "Added to next thread pool " + str(self.nextThreadPool.secondsSharing)
					ThreadPool.addTaskToThreadPool(self.nextThreadPool, runningThread)
				else:
					self.startTask(runningThread)
					self.timer.wait(self.secondsSharing - 0.5)
					self.stopTask(runningThread)
					if runningThread.getCredit() > 0:
						runningThread.decrementCredit()
						self.readdTask(runningThread)
					
class Task(Thread):
	credit = -1
	_stop = Event()

	def __init__(self, ID, counter):
		Thread.__init__(self)
		self.ID = ID
		self.state = counter

	def __cmp__(self, other):
		return cmp(other.credit, self.credit)

	def run(self):
		try:
			self.lock.acquire()
			while not self.stopped() and self.state > 0:
				if self.state == 1:
					self.credit = -1
				print "Thread " + str(self.ID) + ": Now counting " + str(self.state) + "."
				self.state -= 1
				self._stop.wait(1)
		finally:
			self.lock.release()
			print "Thread " + str(self.ID) + ": Finished. State: " + str(self.state) + ". Credit: " + str(self.credit) + "\n"

	def stopped(self):
		return self._stop.isSet()

	def getCredit(self):
		return self.credit

	def getID(self):
		return self.ID

	def getState(self):
		return self.state

	def setCredit(self, credit):
		self.credit = credit

	def decrementCredit(self):
		self.credit -= 1

	def setLock(self, lock):
		self.lock = lock

	def setStop(self, stop):
		self._stop = stop

def main():
	threadPool2 = ThreadPool(3, 2, None)

	threadPool = ThreadPool(5, 2, None)				#TODO: De discutat de ce nu merge Thread Pool 3
	counters = (5, 20)
	for i in range(2):
		threadPool.addTask(Task(i+1, counters[i]))
	threadPool.start()


main()