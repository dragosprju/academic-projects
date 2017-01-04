package ceas;

public class Baterie {
	private boolean noua;
	
	public void schimba() {
		noua = true;
	}
	
	public void invecheste() {
		if (noua == true) {
			noua = false;
		}
	}
}
