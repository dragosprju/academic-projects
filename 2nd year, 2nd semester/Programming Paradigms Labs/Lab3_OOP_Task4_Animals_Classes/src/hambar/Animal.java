package hambar;

public class Animal {
	protected String nume;
	Animal prieten;
	
	public Animal() {
		nume = null;
		prieten = null;
	};
	
	public Animal(String nume) {
		this.nume = nume;
		prieten = null;
	}
	
	public String getNume() {
		return nume;
	}
	
	public String getPrieten() {
		if (prieten == null)
			return "Nimeni";
		else
			return prieten.nume;
	}
	
	public void setNume(String nume) {
		this.nume = nume;
	}
	
	public void Vorbeste() {
		if (nume == null) {
			System.out.println("Un animal face galagie.");
		}
		else {
			System.out.println(nume + " face galagie.");
		}
	}
	
	public void Imprieteneste_te(Animal prieten) {
		if (prieten != null) {
			String pnume, tnume;
			if (nume == null) {
				tnume = "Un animal";
			} else {
				tnume = nume;
			}
			
			if (prieten.nume == null) {
				pnume = "un alt animal";
			} else {
				pnume = prieten.nume;
			}
			
			if (this.prieten == null && prieten.prieten == null) {
				System.out.println(tnume + " s-a imprietenit cu " + pnume + ". :)");
				this.prieten = prieten;
				prieten.prieten = this;
			} else if (this == prieten.prieten) {
				System.out.println(tnume + " si " + pnume + " sunt deja prieteni. :D");
			} else {
				System.out.println(tnume + " a incercat sa se imprieteneasca cu " + pnume + ", dar acesta avea deja un prieten. :(");
			}
		}
	}
}
