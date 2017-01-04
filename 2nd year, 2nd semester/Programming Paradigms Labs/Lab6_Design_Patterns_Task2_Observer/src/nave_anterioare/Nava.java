package nave_anterioare;

import java.util.Observable;
import java.util.Observer;

public class Nava implements Observer {
	private int viata;
	private int putere;
	private eFoc foc;
	private eClasa clasa;
	private Nava tintaNava;
	
	public Nava(int viata, int putere, eFoc foc, eClasa clasa, Nava tintaNava) {
		this.viata = viata;
		this.putere = putere;
		this.foc = foc;
		this.clasa = clasa;
		if (this != tintaNava) {
			this.tintaNava = tintaNava;
		}
	}
	
	@Override
	public void update(Observable o, Object arg) {
		ZonaLupta zonaLupta = (ZonaLupta)o;
		int indexNavaAparuta = zonaLupta.getNavaIndex((Nava)arg);
		System.out.println("O nava noua a aparut pe radarul navei " + zonaLupta.getNavaIndex(this) + " (" + this.getNumeClasa() + ").");
		zonaLupta.afiseazaInfoNava(this, indexNavaAparuta);
	}
	
	public int getViata() {
		return viata;
	}
	
	public int getPutere() {
		return putere;
	}
	
	public String getNumeFoc() {
		return foc.toString();
	}
	
	public String getNumeClasa() {
		return clasa.toString();
	}
	
	public boolean teTintesc(Nava navaPresupusaTinta) {
		if (navaPresupusaTinta != null && tintaNava == navaPresupusaTinta) {
			return true;
		} else {
			return false;
		}
	}
	
}
