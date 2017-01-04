package spatiu;

import java.util.Random;

public class Joc {
	Nava nave[];
	Integer nrNave;
	Random randomizer;
	
	public Joc() {
		randomizer = new Random();
		nrNave = randomizer.nextInt(19)+1;
		nave = new Nava[nrNave];
		Integer i;
		for(i=0; i<nrNave; i++)
			nave[i] = new Nava();
		DenumesteNave();
	}
	
	public Joc(Integer nrNave) {
		randomizer = new Random();
		this.nrNave = nrNave;
		nave = new Nava[nrNave];
		Integer i;
		for(i=0; i<nrNave; i++)
			nave[i] = new Nava();
		DenumesteNave();
	}
	
	public void DenumesteNave() {
		Integer i, j;
		boolean numeDejaAlese[] = new boolean[nrNave];
		for (i=0; i<nrNave; i++) {
			boolean ok = false;
			while (ok == false) {
				j = randomizer.nextInt(nrNave);
				if (numeDejaAlese[j] == false) {
					nave[i].setNume(NumeNave.values()[j].toString());
					numeDejaAlese[j] = true;
					ok = true;
				}
			}
		}
	}
	
	public void Joaca() {
		Integer i,j;
		for (i=0; i<nrNave; i++) {
			boolean ok = false;
			while (ok == false) {
				j = randomizer.nextInt(nrNave);
				if (nave[j].distrus != true) {
					nave[i].Distruge(nave[j]);
					ok = true;
				}
			}
		}			
	}
	
	public void JoacaPanaLaMoarte() {
		Integer i,j;
		Integer nrNaveInViata = nrNave;
		while (nrNaveInViata > 1) {
			for (i=0; i<nrNave; i++) {
				if (nave[i].distrus == true) {
					continue;
				}
				if (nrNaveInViata == 1) {					
					break;					
				}

				boolean ok = false;
				while (ok == false) {
					j = randomizer.nextInt(nrNave);
					if (nave[j].distrus != true) {
						boolean ok2 = false;
						if (nave[i].distrus == false) {
							ok2 = true;
						}
						nave[i].Distruge(nave[j]);
						if (nave[j].distrus == true && ok2 == true) {
							nrNaveInViata--;
						}
						if (nave[i].distrus == true && nave[i] != nave[j] && ok2 == true) {
							nrNaveInViata--;
						}
						ok = true;
					}
				}
			}
		}
		
		if (nrNaveInViata == 1) {
			for (i=0; i<nrNave; i++) {
				if (nave[i].distrus == false) {
					System.out.println();
					System.out.println("--- CASTIGATOR: " + nave[i].nume + " ---");
					break;
				}
			}

		}
		
		if (nrNaveInViata == 0) {
			System.out.println();
			System.out.println("--- Este remiza! ---");
		}
	}
	
	public void ListaJucatori() {
		Integer i;
		for (i=0; i<nrNave; i++) {
			System.out.format("%2d.  %-12s", i+1, nave[i].nume);
			switch(nave[i].tip) {
			case 1:
				System.out.format("%-20s", "Armura/Atac");
				break;
			case 2:
				System.out.format("%-20s", "Armura/Viteza");
				break;
			case 3:
				System.out.format("%-20s", "Armura/Manevr");
				break;
			case 4:
				System.out.format("%-20s", "Atac/Viteza");
				break;
			case 5:
				System.out.format("%-20s", "Atac/Manevr");
				break;
			case 6:
				System.out.format("%-20s", "Viteza/Manevr");
				break;
			}
			if (nave[i].distrus == true)
				System.out.print("DISTRUS\n");
			else
				System.out.print("In viata\n");
		}
	}
	
	public String Castigator() {
		Integer i;
		for (i=0; i<nrNave; i++) {
			if (nave[i].distrus == false)
				return nave[i].nume;
		}
		return null;
	}
	
	public boolean ExistaNume(String nume) {
		Integer i;
		for (i=0; i<nrNave; i++) {
			if (nave[i].nume.equals(nume)) {
				return true;
			}
		}
		return false;
	}
}
