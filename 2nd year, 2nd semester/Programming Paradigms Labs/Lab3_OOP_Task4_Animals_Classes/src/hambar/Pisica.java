package hambar;

public class Pisica extends Animal {
	
	public Pisica() {
		super();
	}
	
	public Pisica(String nume) {
		super(nume);
	}
	
	public void Vorbeste() {
		if (nume == null) {
			System.out.println("O pisica miauna!");
		}
		else {
			System.out.println(nume + " miauna!");
		}
	}
}
