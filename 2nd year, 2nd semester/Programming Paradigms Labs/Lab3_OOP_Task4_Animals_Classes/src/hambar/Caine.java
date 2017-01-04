package hambar;

public class Caine extends Animal {
	public Caine() {
		super();
	}
	
	public Caine(String nume) {
		super(nume);
	}
	
	public void Vorbeste() {
		if (nume == null) {
			System.out.println("Un caine latra.");
		}
		else {
			System.out.println(nume + " latra.");
		}
	}
}
