package threadpool;

public class L8T1 {	
	public static void main(String[] args) {
		ThreadPool threadPool = new ThreadPool(5);

		
		for (int i=0; i<10; i++) {
			threadPool.addWorker(new Worker(i+3));
		}
		threadPool.execute();
	}
}
