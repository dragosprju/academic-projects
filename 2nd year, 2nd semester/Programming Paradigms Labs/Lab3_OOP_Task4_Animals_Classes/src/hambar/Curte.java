package hambar;

import java.util.Random;

public class Curte {
	Animal curte[];
	Integer nrAnimale;
	Random randomizer;
	
	public Curte() {
		randomizer = new Random();
		nrAnimale = randomizer.nextInt(9) + 1;
		curte = new Animal[nrAnimale];
		Populeaza();
		DenumesteAnimale();
	}
	
	public Curte(int nrAnimale) {
		randomizer = new Random();
		this.nrAnimale = nrAnimale;
		curte = new Animal[nrAnimale];
		Populeaza();
		DenumesteAnimale();
	}
	
	public void Populeaza() {
		Integer i;
		for (i=0; i<nrAnimale; i++) {
			Integer animal = randomizer.nextInt(2);
			switch(animal) {
			case 0:
				curte[i] = new Animal();
				break;
			case 1:
				curte[i] = new Caine();
				break;
			case 2:
				curte[i] = new Pisica();
				break;
			}
		}
	}
	
	public void DenumesteAnimale() {
		Integer i, j;
		boolean numeDejaAlese[] = new boolean[9];
		for (i=0; i<nrAnimale; i++) {
			boolean ok = false;
			while (ok == false) {
				j = randomizer.nextInt(9);
				if (numeDejaAlese[j] == false) {
					curte[i].setNume(NumeAnimale.values()[j].toString());
					numeDejaAlese[j] = true;
					ok = true;
				}
			}
		}
	}
	
	public void Manifesta() {
		Integer i,j;
		for (i=0; i<nrAnimale; i++) {
			j = randomizer.nextInt(2);
			if (j == 0) {
				curte[i].Vorbeste();
			}
			
			if (j == 1) {
				boolean ok = false;
				while (ok == false) {
					j = randomizer.nextInt(nrAnimale);
					if (j == -1) {
						j = 0;
					}
					if (i != j) {
						curte[i].Imprieteneste_te(curte[j]);
						ok = true;
					}
				}
			}
		}
	}
	
	public void Debug() {
		Integer i;
		for (i=0; i<nrAnimale; i++)
			System.out.println(curte[i].getNume() + " -- " + curte[i].getPrieten() + "\t");
	}
}
