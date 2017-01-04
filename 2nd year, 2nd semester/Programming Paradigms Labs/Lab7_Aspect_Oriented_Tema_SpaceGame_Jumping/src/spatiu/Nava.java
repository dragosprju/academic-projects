package spatiu;

import java.util.Random;

public class Nava {
	// [0000] = [Armura|Atac|Viteza|Manevrabilitate]
	// [1100] = 12 = Armura/Atac             -> 1
	// [1010] = 10 = Armura/Viteza           -> 2
	// [1001] = 9  = Armura/Manevrabilitate  -> 3
	// [0110] = 6  = Atac/Viteza             -> 4
	// [0101] = 5  = Atac/Manevrabilitate    -> 5
	// [0011] = 3  = Viteza/Manevrabilitate  -> 6
	Integer tip;
	Random randomizer;
	private final String[] shipTypes = { "Armura/Atac", "Armura/Viteza", 
			"Armura/Manevr", "Atac/Viteza", "Atac/Manevr", "Viteza/Manevr" };
	
	public Nava() {
		randomizer = new Random();
		tip = randomizer.nextInt(5) + 1;
	}
	
	public Nava(Integer tip) {
		this.tip = tip;
	}
	
	public void extrageTipDinSir(String tipInString) {
		System.out.println("Nume nava de extras: " + tipInString);
		if (tipInString.equals(shipTypes[0])) {
			tip = 1;
		}
		else if (tipInString.equals(shipTypes[1])) {
			tip = 2;
		}
		else if (tipInString.equals(shipTypes[2])) {
			tip = 3;
		}
		else if (tipInString.equals(shipTypes[3])) {
			tip = 4;
		}
		else if (tipInString.equals(shipTypes[4])) {
			tip = 5;
		}
		else if (tipInString.equals(shipTypes[5])) {
			tip = 6;
		}
		System.out.println("Am extras: " + tip);
	}
	
	/*
	 * Armura/Atac -> Atac/Viteza + Atac/Manevrabilitate						= 1 -> 4 + 5
	 * Armura/Viteza -> Atac/Viteza + Viteza/Manevrabilitate					= 2 -> 4 + 6
	 * Armura/Manevrabilitate -> Atac/Manevrabilitate + Viteza/Manevrabilitate  = 3 -> 5 + 6
	 * Atac/Viteza -> Armura/Atac + Armura/Viteza								= 4 -> 1 + 2
	 * Atac/Manevrabilitate -> Armura/Atac + Armura/Manevrabiitate				= 5 -> 1 + 3
	 * Viteza/Manevrabilitate -> Armura/Viteza + Armura/Manevrabilitate			= 6 -> 2 + 3
	 */
	public boolean PoateDistruge(Nava navaInamica) {
		if (this.tip == 1 && (navaInamica.tip == 4 || navaInamica.tip == 5)) {
			return true;
		}
		if (this.tip == 2 && (navaInamica.tip == 4 || navaInamica.tip == 6)) {
			return true;
		}
		if (this.tip == 3 && (navaInamica.tip == 5 || navaInamica.tip == 6)) {
			return true;
		}
		if (this.tip == 4 && (navaInamica.tip == 1 || navaInamica.tip == 3)) {
			return true;
		}
		if (this.tip == 5 && (navaInamica.tip == 1 || navaInamica.tip == 3)) {
			return true;
		}
		if (this.tip == 6 && (navaInamica.tip == 2 || navaInamica.tip == 3)) {
			return true;
		}
		return false;
	}
	
	public int PoateDistrugePe(Nava navaInamica) {
		if (this == navaInamica) {
			//System.out.println(nume + " a apasat un buton aleator pe bord si s-a autodetonat.");
			return -1;
		}
		if (tip == navaInamica.tip) {
			//System.out.println(nume + " si " + navaInamica.nume + " s-au atacat si s-au distrus intre ei.");
			return 0;
		}
		if (PoateDistruge(navaInamica)) {
			// A distrus;
			return 1;
		} else {
			//System.out.println(nume + " a incercat sa-l distruga pe " + navaInamica.nume + ", dar n-a reusit.");
			return 2;

		}
	}
}
