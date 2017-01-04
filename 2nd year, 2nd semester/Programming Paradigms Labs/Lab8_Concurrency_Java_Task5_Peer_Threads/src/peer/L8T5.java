package peer;

import java.util.ArrayList;
import java.util.List;

public class L8T5 {

	public static void main(String[] args) {
		List<Worker> muncitori = new ArrayList<Worker>();
		List<Integer> nrDeProcesat = new ArrayList<Integer>();
		int nrMuncitori = 4;
		int nrRezultate = 40;
		Integer[] rezultat = new Integer[nrRezultate];
		
		for (int i=0; i<nrMuncitori; i++) {
			muncitori.add(new Worker("T" + String.valueOf(i+1), nrDeProcesat, rezultat));
		}
		
		for (int i=0; i<nrRezultate; i++) {
			nrDeProcesat.add(i+1);
		}
		
		for (Worker muncitor : muncitori) {
			muncitor.start();
		}
		
		boolean ok = false;
		while (!ok) {
			ok = true;
			for (Worker muncitor : muncitori) {
				if (muncitor.getState() != Thread.State.TERMINATED) {
					ok = false;
				}
			}
		}
		System.out.printf("Rezultate: ");
		for (int i=0; i<nrRezultate; i++) {
			System.out.printf("%d ", rezultat[i]);
		}
		
	}

}
