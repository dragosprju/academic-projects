package masterslave;

import java.util.ArrayList;
import java.util.List;

public class Master extends Thread {
	List<Slave> slaves = new ArrayList<Slave>();
	Integer[] rezultat;
	int nr;
	
	public Master(int nr, int div) {
		this.nr = nr;
		rezultat = new Integer[nr];
		for (int i=0; i<div; i++) {
			slaves.add(new Slave(rezultat, nr/div, i+1));
		}
	}
	
	@Override
	public void run() {
		boolean ok = false;
		for (Slave slave : slaves) {
			slave.start();
		}
		while (!ok) {
			ok = true;
			for (Slave slave : slaves) {
				if (slave.getState() != Thread.State.TERMINATED) {
					ok = false;
				}
			}
		}
		System.out.printf("Rezultat: ");
		for (int i=0; i<nr; i++) {
			System.out.printf("%d ", rezultat[i]);
		}		
	}
}
