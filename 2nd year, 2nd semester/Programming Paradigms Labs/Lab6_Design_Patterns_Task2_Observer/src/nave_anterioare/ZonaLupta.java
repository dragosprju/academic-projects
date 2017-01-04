package nave_anterioare;

import java.util.ArrayList;
import java.util.List;
import java.util.Observable;

class ZonaLupta extends Observable {
	private List<Nava> listaNave = new ArrayList<Nava>();
	
	public void afiseazaInfoNava(Nava navaSursa, int i) {
		Nava nava = listaNave.get(i);
		System.out.printf("Tip nava: " + nava.getNumeClasa() + "\n" +
				"Tip foc: " + nava.getNumeFoc() + "\n" +
				"Viata: " + nava.getViata() + "\n" +
				"Putere: " + nava.getPutere() + "\n" +
				"Ne tinteste? ");
		if (nava.teTintesc(navaSursa)) {
			System.out.printf("DA!\n\n");
		} else {
			System.out.printf("Nu. Nu te panica, man!\n\n");
		}
	}
	
	public void adaugaNava(Nava nava) {
		listaNave.add(nava);
		setChanged();
		notifyObservers(nava);
		this.addObserver(nava);
	}
	
	public int getNavaIndex(Nava nava) {
		return listaNave.indexOf(nava);
	}
}
