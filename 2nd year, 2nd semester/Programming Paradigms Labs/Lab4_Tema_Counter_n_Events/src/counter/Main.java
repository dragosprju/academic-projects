package counter;

public class Main {

	public static void main(String[] args) {
		Initiater initiater = new Initiater();
		Responder responder1 = new Responder(0);
		Listener listener = new Listener();
		
		initiater.addResponder(responder1);	
		listener.addResponder(responder1);
		
		Thread thread1 = new Thread() {
			public void run() {
				while (!Thread.interrupted()) {
					initiater.incrementCounters();
					try {
						Thread.sleep(1000);
					} catch (InterruptedException ex) {
						Thread.currentThread().interrupt();
					}
				}
			}
		};
		
		Thread thread2 = new Thread() {
			public void run() {
				while (true) {
					listener.checkResponders();
				}
			}
		};
		
		Thread thread3 = new Thread() {
			public void run() {
				try {
					Thread.sleep(4999);
				} catch (InterruptedException ex) {
					Thread.currentThread().interrupt();
				}
				System.out.println("Stopping thread 1...");
				thread1.interrupt();
			}
		};
		
		thread1.start();
		thread2.start();
		thread3.start();
	}

}
