package hazard;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.BlockingQueue;

public class FibonacciThreadPool {
	BlockingQueue<Integer> numereDeProcesat;
	int[] rezultate;
	List<FibonacciWorker> muncitori = new ArrayList<FibonacciWorker>();
	
	public FibonacciThreadPool(int nrMuncitori, BlockingQueue<Integer> numereDeProcesat,
			int[] rezultate) {
		for (int i=0; i<nrMuncitori; i++) {
			muncitori.add(new FibonacciWorker("T" + String.valueOf(i+1),numereDeProcesat, rezultate));
		}
		this.numereDeProcesat = numereDeProcesat;
		this.rezultate = rezultate;
	}
	
	public void incepe() {
		for (FibonacciWorker muncitor : muncitori) {
			muncitor.start();
		}
		Thread thread = new Thread() {
			public void run() {
				boolean ok = false;
				while (!ok) {
					ok = true;
					for (FibonacciWorker muncitor : muncitori) {
						if (muncitor.getState() != Thread.State.TERMINATED) {
							ok = false;
						}
					}
				}
				System.out.println("Rezultat: ");
				for (int i=0; i<(numereDeProcesat.size() + numereDeProcesat.remainingCapacity()); i++) {
					System.out.printf("%d ", rezultate[i]);
				}
			}
		};
		thread.start();
	}

}
