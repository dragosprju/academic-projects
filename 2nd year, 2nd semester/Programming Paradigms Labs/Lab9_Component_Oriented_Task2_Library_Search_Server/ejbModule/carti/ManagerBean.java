package carti;

import java.util.ArrayList;
import java.util.List;

import javax.ejb.Stateless;

/**
 * Session Bean implementation class ManagerBean
 */
@Stateless
public class ManagerBean implements ManagerBeanRemote {

	List<CarteBeanRemote> listaCarti = new ArrayList<CarteBeanRemote>();
	public List<Boolean> imprumutat = new ArrayList<Boolean>();
    /**
     * Default constructor. 
     */
    public ManagerBean() {

    }

	@Override
	public void adaugaCarte(String titlu, String autor, int anAparitie,
			String isbn) {
		listaCarti.add(new CarteBean(titlu, autor, anAparitie, isbn));	
		imprumutat.add(new Boolean(Boolean.FALSE));
	}

	@Override
	public List<CarteBeanRemote> listaCarti() {
		return listaCarti;
	}
	
	@Override
	public String getAfisare() {
		String afisare = "";
		int i = 1;
		for (CarteBeanRemote carte : listaCarti) {
			afisare += (i++) + ". " + carte.detalii() + "\n";
		}
		return afisare;
	}
	
	@Override
	public void curataBiblioteca() {
		listaCarti.clear();
	}
	
	@Override
	public String cautaISBN(String isbn) {
		String afisare = "";
		for (CarteBeanRemote carte : listaCarti) {
			int i = 1;
			if (carte.getIsbn().contains(isbn)) {
				afisare += (i++) + ". " + carte.detalii() + "\n";
			}
		}
		return afisare;
	}
	
	@Override
	public String cautaAutor(String autor) {
		String afisare = "";
		for (CarteBeanRemote carte : listaCarti) {
			int i = 1;
			if (carte.getAutor().contains(autor)) {
				afisare += (i++) + ". " + carte.detalii() + "\n";
			}
		}
		return afisare;
	}
	
	@Override
	public String cautaAn(int an) {
		String afisare = "";
		for (CarteBeanRemote carte : listaCarti) {
			int i = 1;
			if (carte.getAnAparitie() == an) {
				afisare += (i++) + ". " + carte.detalii() + "\n";
			}
		}
		return afisare;
	}
	
	@Override
	public String imprumuta(int index) {
		String afisare = "";
		if (imprumutat.get(index-1) == Boolean.TRUE) {
			afisare = "[EROARE] Carte deja imprumutata.";
		}
		else {
			imprumutat.set(index-1, Boolean.TRUE);
			afisare = "[INFO] Carte imprumutata cu succes.";
		}
		return afisare;
	}
	
	@Override
	public String returneaza(int index) {
		String afisare = "";
		if (imprumutat.get(index-1) == Boolean.FALSE) {
			afisare = "[EROARE] Carte deja returnata.";
		}
		else {
			imprumutat.set(index-1, Boolean.FALSE);
			afisare = "[INFO] Carte returnata cu succes.";
		}
		return afisare;
	}
	
	@Override
	public void scoateCarte(int index) {
		listaCarti.remove(index-1);
		imprumutat.remove(index-1);
	}

}
