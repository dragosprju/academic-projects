package ceas;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class Ceas {
	private Buton buton1;
	private Buton buton2;
	private Afisaj afisaj;
	private Baterie baterie1;
	private Baterie baterie2;
	private Timp timp;
	private String mesaj;
	
	public Ceas() {
		buton1 = new Buton(1);
		buton2 = new Buton(2);
		afisaj = new Afisaj();
		baterie1 = new Baterie();
		baterie2 = new Baterie();
		timp = new Timp();
		mesaj = new String();
		mesaj = "";
	}
	
	public void porneste() {
		afisaj.refresh(timp, mesaj);
		mesaj = "";
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		String line = new String();
		try {
			line = br.readLine();
		} catch (IOException e) {
		}
		switch(line) {
		case "1":
			apasButon1();
			break;
		case "2":
			apasButon2();
			break;
		case "3":
			apasButon1si2();
			break;
		case "4":
			schimbaBateria1();
			mesaj = "Bateria 1 este noua.";
			break;
		case "5":
			schimbaBateria2();
			mesaj = "Bateria 2 este noua.";
			break;
		}
		baterie1.invecheste();
		baterie2.invecheste();
		porneste();
	}

	public void apasButon1() {
		buton1.apasa(afisaj, timp);
	}
	
	public void apasButon2() {
		buton2.apasa(afisaj, timp);
	}
	
	public void apasButon1si2() {
		afisaj.stopClipire();
	}
	
	public void schimbaBateria1() {
		baterie1.schimba();
		afisaj.refresh(timp, "Bateria 1 este noua.");
	}
	
	public void schimbaBateria2() {
		baterie2.schimba();
		afisaj.refresh(timp, "Bateria 2 este noua.");
	}
	
	public Buton getButon1() {
		return buton1;
	}
	
	public Buton getButon2() {
		return buton2;
	}
}
