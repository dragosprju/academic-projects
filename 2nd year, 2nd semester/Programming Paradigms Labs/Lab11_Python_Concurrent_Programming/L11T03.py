from threading import Thread, Lock, Event
from Queue import PriorityQueue

class IDLock():
	lock = Lock()
	ID = -1
	task = None

	def acquire(self, task, opt = False):
		acquired = self.lock.acquire(opt)
		#print "Task " + str(task.getID()) + " acquired?: " + str(acquired)
		if acquired:
			self.ID = task.getID()
			self.task = task
		return acquired

	def lockedByWho(self):
		return self.ID

	def getTask(self):
		return self.task

	def release(self, task):
		if self.task == task:
			self.ID = -1
			self.task = None
			return self.lock.release()
		else:
			return False

class ThreadPool(Thread):
	lock = IDLock()
	tasks = []
	timer = Event()

	def __init__(self):
		Thread.__init__(self)

	def addTask(self, task):
		self.tasks.append(task)
		task.setLock(self.lock)

	def stop(self):
		self.started = False
		print "\nStopping thread pool."

	def printTaskList(self):
		print "Tasks: ",
		for task in self.tasks:
			print str(task.getID()) + " ",
		print ""

	def run(self):
		threads = []
		self.tasks.sort()	
		#self.printTaskList()

		while len(self.tasks) > 0:

			for task in self.tasks:
				threads.append(Thread(target=task.acquireLock))
			for thread in threads:
				thread.start()
			threads = []

			highestLocker = self.lock.getTask()
			print "Lock taken by task " + str(self.lock.lockedByWho()) + " with initial priority " + str(self.lock.getTask().getPriority()) + "."			

			highestLocker.setPriority(0)
			self.tasks.sort()
			#self.printTaskList()

			t = Thread(target=self.tasks[0].do)
			self.tasks.remove(self.tasks[0])
			t.start()
			t.join()

class Task():
	timer = Event()
	done = False

	def __init__(self, ID, priority, seconds):
		self.ID = ID
		self.priority = priority
		self.seconds = seconds

	def __cmp__(self, other):
		return cmp(self.priority, other.priority)

	def getPriority(self):
		return self.priority

	def getID(self):
		return self.ID

	def done(self):
		return self.done

	def setLock(self, lock):
		self.lock = lock

	def setPriority(self, priority):
		self.priority = priority

	def acquireLock(self):
		self.lock.acquire(self)

	def do(self):
		try:
			if self.lock.lockedByWho() == self.ID:
				print "Task " + str(self.ID) + ": waiting " + str(self.seconds) + " seconds..."
				self.timer.wait(self.seconds-0.1)
			else:
				print "Error. Task " + str(self.ID) + " is being executed and doesn't own the lock."
		finally:
			self.lock.release(self)

def main():
	threadPool = ThreadPool()
	for i in range(3):
		threadPool.addTask(Task(i+1, i+1, 2))
	threadPool.start()

main()