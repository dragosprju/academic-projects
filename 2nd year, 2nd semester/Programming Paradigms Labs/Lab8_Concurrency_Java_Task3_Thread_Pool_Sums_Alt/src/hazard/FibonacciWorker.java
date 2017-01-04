package hazard;

import java.util.concurrent.BlockingQueue;

public class FibonacciWorker extends Thread {
	BlockingQueue<Integer> numereDeProcesat;
	Long[] rezultate;
	String nume;
	
	public FibonacciWorker(String nume, BlockingQueue<Integer> numereDeProcesat, Long[] rezultate) {
		this.nume = nume;
		this.numereDeProcesat = numereDeProcesat;
		this.rezultate = rezultate;
	}
	
	public void run() {
		while (!numereDeProcesat.isEmpty()) {
			int deProcesat = numereDeProcesat.remove();
			if (deProcesat == 1 || deProcesat == 2) {
				rezultate[deProcesat-1] = (long) 1;
				System.out.printf("[%s][%02d] Inserat simplu.\n", nume, deProcesat);
			}
			else if (deProcesat > 2 && rezultate[deProcesat-2] != null && rezultate[deProcesat-3] != null) {
				rezultate[deProcesat-1] = rezultate[deProcesat-2] + rezultate[deProcesat-3];
				System.out.printf("[%s][%02d] Calculat cu cache. Rezultat: %d\n", nume, deProcesat, rezultate[deProcesat-1]);
			}
			else {
				long ultimul = 1;
				long penultimul = 1;
				long prezentul = 0;
				for (int i=2; i<deProcesat; i++) {
					prezentul = ultimul + penultimul; /*
					if (rezultate[i] == null)
						rezultate[i] = prezentul;*/
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
