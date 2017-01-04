package carti;

import java.io.Serializable;
import javax.ejb.Remote;

@Remote
public interface CarteBeanRemote extends Serializable {
	public String getTitlu();

	public String getAutor();

	public int getAnAparitie();

	public String getIsbn();

	public String detalii();
}
