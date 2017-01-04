package carti;

import javax.ejb.Stateless;

/**
 * Session Bean implementation class CarteBean
 */
@Stateless
public class CarteBean implements CarteBeanRemote {

	private String titlu;
	private String autor;
	private int anAparitie;
	private String isbn;
	
    /**
     * Default constructor. 
     */
	public CarteBean() {
		this.titlu = "";
		this.autor = "";
		this.anAparitie=0;
		this.isbn = "";
	}
	
    public CarteBean(String titlu, String autor, int anAparitie, String isbn) {
    	this.titlu = titlu;
    	this.autor = autor;
    	this.anAparitie = anAparitie;
    	this.isbn = isbn;
    }

	@Override
	public String getTitlu() {
		return titlu;
	}

	@Override
	public String getAutor() {
		return autor;
	}

	@Override
	public int getAnAparitie() {
		return anAparitie;
	}

	@Override
	public String getIsbn() {
		return isbn;
	}

	@Override
	public String detalii() {
		return titlu + ", " + autor + " [" + anAparitie + ", " + isbn + "]";
	}

}
