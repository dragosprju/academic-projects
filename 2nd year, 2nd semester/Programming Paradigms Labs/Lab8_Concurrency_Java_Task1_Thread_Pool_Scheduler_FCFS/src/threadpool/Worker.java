package threadpool;

public class Worker extends Thread {
	private int seconds;
	
	public Worker(int seconds) {
		this.seconds = seconds;
	}
	
	public int getDuration() {
		return seconds;
	}
	
	@Override
	public void run() {
		try {
			Thread.sleep(1000*seconds);
		} catch(Exception e) {};
	}
}
