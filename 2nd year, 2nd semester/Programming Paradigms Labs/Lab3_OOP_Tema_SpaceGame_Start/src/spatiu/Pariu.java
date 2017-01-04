package spatiu;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class Pariu {
	String nume_pariat;
	
	public Pariu() {}
	
	public void CerePariu(Joc joc) throws IOException {
		boolean ok = false;
		joc.ListaJucatori();
		BufferedReader bufferRead = new BufferedReader(new InputStreamReader(System.in));
		
		while (ok == false) {
			System.out.print("\nScrie numele jucatoriului pe care pariezi: ");
			String s = bufferRead.readLine();
			if (Pariaza(joc, s) == true) {
				ok = true;
			} else {
				System.out.println("Numele nu exista. Incearca din nou.");
			}
		}
		System.out.println("Pariul a fost pus.\n");
	}
	
	public boolean Pariaza(Joc joc, String nume) {
		if (joc.ExistaNume(nume)) {
			nume_pariat = nume;
			return true;
		} else {
			return false;
		}
	}
	
	public int AmCastigat(Joc joc) {
		if (joc.Castigator().equals(nume_pariat)) {
			return 1;
		} else if (joc.Castigator() == null) {
			return 0;
		} else {
			return -1;
		}
	}
	
	public void AmCastigat_Print(Joc joc) {
		switch (AmCastigat(joc)) {
		case 1:
			System.out.println("\nBravo! Ai castigat pariul.");
			break;
		case 0:
			System.out.println("\nDin pacate a fost remiza.");
			break;
		case -1:
			System.out.println("\nAi pierdut pariul.");
			break;
		}
	}
}
