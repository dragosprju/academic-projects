package nave_anterioare;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

public class L6T2 {

	public static void main(String[] args) {
		final ZonaLupta zonaLupta = new ZonaLupta();
		final List<Nava> listaNave = new ArrayList<Nava>();
		final Random randomizer = new Random();
		int viata, putere;
		eFoc foc;
		eClasa clasa;
		Nava nava;
		
		for (int i=0; i<5; i++) {
			viata = randomizer.nextInt(100) + 1;
			putere = randomizer.nextInt(10) + 1;
			foc = eFoc.values()[randomizer.nextInt(3)];
			clasa = eClasa.values()[randomizer.nextInt(13)];
			if (listaNave.size() > 0) {
				nava = listaNave.get(randomizer.nextInt(listaNave.size()));
			} else {
				nava = null;
			}
			Nava navaDeAdaugat = new Nava(viata, putere, foc, clasa, nava);
			listaNave.add(navaDeAdaugat);
			//zonaLupta.addObserver(navaDeAdaugat);
			zonaLupta.adaugaNava(navaDeAdaugat);
			try {
				Thread.sleep(4000);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
		
	}

}
