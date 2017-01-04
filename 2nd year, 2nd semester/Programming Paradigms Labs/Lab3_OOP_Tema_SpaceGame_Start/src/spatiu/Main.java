package spatiu;

import java.io.IOException;

public class Main {

	public static void main(String[] args) throws IOException {
		JocIntens joc = new JocIntens(10);
		Pariu pariu = new Pariu();
		pariu.CerePariu(joc);
		joc.JoacaPanaLaMoarte();
		pariu.AmCastigat_Print(joc);
	}

}
