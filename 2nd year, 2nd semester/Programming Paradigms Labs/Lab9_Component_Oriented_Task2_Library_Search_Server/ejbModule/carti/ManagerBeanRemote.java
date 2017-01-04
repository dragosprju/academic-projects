package carti;

import java.util.List;

import javax.ejb.Remote;

@Remote
public interface ManagerBeanRemote {
	public void adaugaCarte(String titlu, String autor, int anAparitie,
			String isbn);
	public List<CarteBeanRemote> listaCarti();
	public String getAfisare();
	public void curataBiblioteca();
	public String cautaISBN(String isbn);
	public String cautaAutor(String autor);
	public String cautaAn(int an);
	public String imprumuta(int index);
	public String returneaza(int index);
	void scoateCarte(int index);


}