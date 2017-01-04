package threadpool;

import java.util.LinkedList;
import java.util.List;

public class ThreadPool {
	List<Worker> taskQueue = new LinkedList<Worker>();
	List<Thread> threads = new LinkedList<Thread>();

	int capacity;

	public ThreadPool(int numThreads) {
		capacity = numThreads;
	}

	public void addWorker(Worker worker) {
		System.out.println("Added worker of duration " + worker.getDuration() + " seconds. ");
		taskQueue.add(worker);
	}
	
	Thread threadPoolCurator = new Thread() {
		public void run() {
			while (!Thread.interrupted()) {
				for (int i=0; i < capacity && i < threads.size(); i++) {
					if (threads.get(i).getState()==Thread.State.TERMINATED) {
						System.out.printf("Thread " + (i + 1) + " finished. ");
						threads.remove(i);
						if (!taskQueue.isEmpty()) {
							System.out.printf("Starting new thread. Running for " + taskQueue.get(0).getDuration() + " seconds.\n");
							threads.add(taskQueue.get(0));
							taskQueue.remove(0);
							threads.get(threads.size()-1).start();
						}
						else {
							System.out.printf("\n");
						}
					}
				}
				if (threads.isEmpty()) {
					System.out.println("All tasks done. Thread pool ended.");
					this.interrupt();
				}
			}
		}
	};

	public void execute() {
		for (int i = 0; i < capacity && i < taskQueue.size(); i++) {
			threads.add(taskQueue.get(0));
			System.out.println("Starting thread " + (i + 1) + ". Running for "
					+ taskQueue.get(0).getDuration() + " seconds.");
			taskQueue.remove(0);
			threads.get(i).start();
		}
		threadPoolCurator.start();
	}
}
