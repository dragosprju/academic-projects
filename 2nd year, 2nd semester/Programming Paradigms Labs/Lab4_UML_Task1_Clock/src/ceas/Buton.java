package ceas;

public class Buton {
	private int nrButon;
	
	public Buton(int nrButon) {
		this.nrButon = nrButon;
	}
	
	public void apasa(Afisaj afisaj, Timp timp) {
		if (nrButon == 1) {
			if (afisaj.getClipOre() == false && afisaj.getClipMinute() == false && afisaj.getClipSecunde() == false) {
				afisaj.setClipOre(true);
			} else if (afisaj.getClipOre() == true && afisaj.getClipMinute() == false && afisaj.getClipSecunde() == false) {
				afisaj.setClipOre(false);
				afisaj.setClipMinute(true);
			} else if (afisaj.getClipMinute() == true && afisaj.getClipOre() == false && afisaj.getClipSecunde() == false) {
				afisaj.setClipMinute(false);
				afisaj.setClipSecunde(true);
			} else if (afisaj.getClipSecunde() == true && afisaj.getClipOre() == false && afisaj.getClipMinute() == false) {
				afisaj.setClipSecunde(false);
				afisaj.setClipOre(true);
			}
		} else if (nrButon == 2) {
			if (afisaj.getClipOre() == true) {
				timp.incrementareOre(afisaj);
			} else if (afisaj.getClipMinute() == true) {
				timp.incrementareMinute(afisaj);
			} else if (afisaj.getClipSecunde() == true) {
				timp.incrementareSecunde(afisaj);
			}
		}
	}
}
