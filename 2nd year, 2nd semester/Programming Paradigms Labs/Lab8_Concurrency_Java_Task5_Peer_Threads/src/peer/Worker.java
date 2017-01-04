package peer;

import java.util.List;

public class Worker extends Thread {
	List<Integer> nrDeProcesat;
	Integer[] rezultat;
	String nume;
	
	public Worker(String nume, List<Integer> nrDeProcesat, Integer[] rezultat) {
		this.nrDeProcesat = nrDeProcesat;
		this.rezultat = rezultat;
		this.nume = nume;
	}
	
	public void run() {
		while (!nrDeProcesat.isEmpty()) {
			int deProcesat = nrDeProcesat.remove(0);
			int s = 0;
			for (int i=1; i<=deProcesat; i++) {
				s += i;
			}
			rezultat[deProcesat-1] = s;
			System.out.printf("[%s][%d] Rezultat: %d\n", nume, deProcesat, s);
		}
	}
}
