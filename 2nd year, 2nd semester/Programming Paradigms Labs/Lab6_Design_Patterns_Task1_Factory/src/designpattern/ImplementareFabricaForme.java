package designpattern;

public class ImplementareFabricaForme implements FabricaForme {
	public Forma make (String FormaName) throws Exception {
		if (FormaName.equals("cerc"))
				return new Cerc();
		else if (FormaName.equals("patrat"))
				return new Patrat();
		else throw new Exception("FabricaForme cannot create " + FormaName);
	}

}
