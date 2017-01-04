package buffer;

import java.util.Scanner;

public class L8T2 {

	public static void main(String[] args) {
		final Buffer<String> buffer = new Buffer<String>();
		final String banc = new String("La psihiatru:\n" + 
				"- Mda, ce probleme aveti?\n" +
				"- Uit.\n" +
				"- Dati exemple.\n" +
				"- Pai, cand ma bag in pat cu amanta, uit de nevasta.\n" +
				"- Si cand te bagi in pat cu nevasta?...\n" +
				"- Uit de ce m-am bagat...");
		
		Thread sender = new Thread() {
			public void run() {
				Scanner scanner = new Scanner(banc);
				while (scanner.hasNextLine()) {
					String line = scanner.nextLine();
					buffer.add(line);
					/*
					try {
						Thread.sleep(1000);
					} catch (InterruptedException e) {
						e.printStackTrace();
					}*/
				}
			}
		};
		
		Thread receiver = new Thread() {
			public void run() {
				while (!Thread.interrupted()) {
					String message = buffer.remove();
					System.out.println(message);
					try {
						Thread.sleep(1000);
					} catch (InterruptedException e) {
						e.printStackTrace();
					}
				}
			}
		};
		
		sender.start();
		receiver.start();
	}
}
