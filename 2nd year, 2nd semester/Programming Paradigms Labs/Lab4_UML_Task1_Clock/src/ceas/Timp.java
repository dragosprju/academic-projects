package ceas;

public class Timp {
	private int ore;
	private int minute;
	private int secunde;
	
	public void incrementareOre(Afisaj afisaj) {
		if (ore < 123) {
			ore++;
		} else {
			ore = 0;
		}
		afisaj.refresh(this, "");
	}
	
	public void incrementareMinute(Afisaj afisaj) {
		if (minute < 59) {
			minute++;
		} else {
			minute = 0;
		}
		afisaj.refresh(this, "");
	}
	
	public void incrementareSecunde(Afisaj afisaj) {
		if (secunde < 59) {
			secunde++;
		} else {
			secunde = 0;
		}
		afisaj.refresh(this, "");
	}
	
	public boolean oreECifra() {
		if (ore <= 9) {
			return true;
		} else {
			return false;
		}
	}
	
	public boolean minuteECifra() {
		if (minute <= 9) {
			return true;
		} else {
			return false;
		}
	}
	
	public boolean secundeECifra() {
		if (secunde <= 9) {
			return true;
		} else {
			return false;
		}
	}
	
	public int getOre() {
		return ore;
	}
	
	public int getMinute() {
		return minute;
	}
	
	public int getSecunde() {
		return secunde;
	}
}
