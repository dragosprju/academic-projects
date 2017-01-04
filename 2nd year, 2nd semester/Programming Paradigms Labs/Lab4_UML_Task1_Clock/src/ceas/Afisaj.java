package ceas;

public class Afisaj {
	private boolean clipescOrele;
	private boolean clipescMinutele;
	private boolean clipescSecundele;
	
	public void refresh(Timp timp, String mesaj) {
		System.out.println("");
		if (timp.oreECifra() == true) {
			System.out.format("0");
		}
		System.out.format("%d:", timp.getOre());
		if (timp.minuteECifra() == true) {
			System.out.format("0");
		}
		System.out.format("%d:", timp.getMinute());
		if (timp.secundeECifra() == true) {
			System.out.format("0");
		}
		System.out.format("%d", timp.getSecunde());
		if (clipescOrele == true && clipescMinutele == false && clipescSecundele == false) {
			System.out.format("\nClipesc orele.");
		}
		if (clipescMinutele == true && clipescOrele == false && clipescSecundele == false) {
			System.out.format("\nClipesc minutele.");
		}
		if (clipescSecundele == true && clipescOrele == false && clipescMinutele == false) {
			System.out.format("\nClipesc secundele.");
		}
		if (mesaj != "") {
			System.out.format("\n" + mesaj);
		}
		System.out.println("");
		System.out.println("1. Apasa butonul 1;");
		System.out.println("2. Apasa butonul 2;");
		System.out.println("3. Apasa ambele butoane;");
		System.out.println("4. Schimba bateria 1;");
		System.out.println("5. Schimba bateria 2;");
		//System.out.format("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
	}
	
	public void stopClipire() {
		clipescOrele = false;
		clipescMinutele = false;
		clipescSecundele = false;
	}
	
	public boolean getClipOre() {
		return clipescOrele;
	}
	
	public boolean getClipMinute() {
		return clipescMinutele;
	}
	
	public boolean getClipSecunde() {
		return clipescSecundele;
	}
	
	public void setClipOre(boolean stare) {
		clipescOrele = stare;
	}
	
	public void setClipMinute(boolean stare) {
		clipescMinutele = stare;
	}
	
	public void setClipSecunde(boolean stare) {
		clipescSecundele = stare;
	}
}
