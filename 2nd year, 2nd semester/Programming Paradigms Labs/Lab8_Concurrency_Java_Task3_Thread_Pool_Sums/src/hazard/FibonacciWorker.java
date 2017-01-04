package hazard;

import java.util.concurrent.BlockingQueue;

public class FibonacciWorker extends Thread {
	BlockingQueue<Integer> numereDeProcesat;
	int[] rezultate;
	String nume;
	
	public FibonacciWorker(String nume, BlockingQueue<Integer> numereDeProcesat, int[] rezultate) {
		this.nume = nume;
		this.numereDeProcesat = numereDeProcesat;
		this.rezultate = rezultate;
	}
	
	public void run() {
		while (!numereDeProcesat.isEmpty()) {
			int deProcesat = numereDeProcesat.remove();
			if (deProcesat == 1 || deProcesat == 2) {
				rezultate[deProcesat-1] = 1;
				System.out.printf("[%s][%02d] Inserat simplu.\n", nume, deProcesat);
			}
			else if (deProcesat > 1 && rezultate[deProcesat-2] != 0 && rezultate[deProcesat-3] != 0) {
				rezultate[deProcesat-1] = rezultate[deProcesat-2] + rezultate[deProcesat-3];
				System.out.printf("[%s][%02d] Calculat cu cache. Rezultat: %d\n", nume, deProcesat, rezultate[deProcesat-1]);
			}
			else {
				int ultimul = 1;
				int penultimul = 1;
				int prezentul = 0;
				for (int i=2; i<deProcesat; i++) {
					prezentul = ultimul + penultimul;
					penultimul = ultimul;
					ultimul = prezentul;
				}
				rezultate[deProcesat-1] = prezentul;
				System.out.printf("[%s][%02d] Calculat de la zero. Rezultat: %d\n", nume, deProcesat, prezentul);
			}
		}
		System.out.printf("[%s] Am terminat.\n", nume);
	}

}
