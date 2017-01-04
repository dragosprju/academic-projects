package spatiu;

public class JocIntens extends Joc {
	
	public JocIntens() {
		super();
	}
	
	public JocIntens(Integer nrNave) {
		super(nrNave);
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
	}
}
