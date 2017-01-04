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
	String nume;
	boolean distrus;
	
	public Nava() {
		randomizer = new Random();
		tip = randomizer.nextInt(5) + 1;
	}
	
	public Nava(Integer tip) {
		this.tip = tip;
	}
	
	public void setNume(String nume) {
		this.nume = nume;
	}
	
	/*
	 * Armura/Atac -> Atac/Viteza + Atac/Manevrabilitate						= 1 -> 4 + 5
	 * Armura/Viteza -> Atac/Viteza + Viteza/Manevrabilitate					= 2 -> 4 + 6
	 * Armura/Manevrabilitate -> Atac/Manevrabilitate + Viteza/Manevrabilitate  = 3 -> 5 + 6
	 * Atac/Viteza -> Armura/Atac + Armura/Viteza								= 4 -> 1 + 2
	 * Atac/Manevrabilitate -> Armura/Atac + Armura/Manevrabiitate				= 5 -> 1 + 3
	 * Viteza/Manevrabiitate -> Armura/Viteza + Armura/Manevrabilitate			= 6 -> 2 + 3
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
	
	public void Distruge(Nava navaInamica) {
		if (this == navaInamica) {
			System.out.println(nume + " a apasat un buton aleator pe bord si s-a autodetonat.");
			distrus = true;
			return;
		}
		if (distrus == true) {
			System.out.println(nume + " e deja distrus. nu poate face nimic.");
			return;
		}
		if (tip == navaInamica.tip) {
			System.out.println(nume + " si " + navaInamica.nume + " s-au atacat si s-au distrus intre ei.");
			distrus = true;
			navaInamica.distrus = true;
			return;
		}
		if (PoateDistruge(navaInamica)) {
			switch(tip) {
			case 1:
				System.out.println(nume + " l-a nimicit pe " + navaInamica.nume + " cu rachete mari si lasere puternice.");
				navaInamica.distrus = true;
				break;
			case 2:
				System.out.println(nume + " l-a izbit pe " + navaInamica.nume + " cu viteza si armura lui puternica.");
				navaInamica.distrus = true;
				break;
			case 3:
				System.out.println(nume + " l-a distrus pe " + navaInamica.nume + " fiind la fel de manevrabil, dar totusi mai protejat!");
				navaInamica.distrus = true;
				break;
			case 4:
				System.out.println(nume + " l-a atacat pe " + navaInamica.nume + " si l-a distrus cu viteza si ferocitate.");
				navaInamica.distrus = true;
				break;
			case 5:
				System.out.println(nume + " l-a ametit pe " + navaInamica.nume + " si l-a bubuit ca bonus!");
				navaInamica.distrus = true;
				break;
			case 6:
				System.out.println(nume + " a fost prea rapid " + navaInamica.nume + " si folosind tactici de super-viteza, l-a invins.");
				navaInamica.distrus = true;
				break;
			}
		} else {
			System.out.println(nume + " a incercat sa-l distruga pe " + navaInamica.nume + ", dar n-a reusit.");
		}
	}
}
