package carte;

import java.util.Properties;
import java.util.Scanner;

import javax.naming.Context;
import javax.naming.InitialContext;
import javax.naming.NamingException;

import carti.ManagerBeanRemote;

public class CarteClient {

	public static void main(String[] args) throws NamingException {
		Properties jndiProps = new Properties();
		jndiProps.put("java.naming.factory.initial", "com.sun.enterprise.naming.impl.SerialInitContextFactory");
		jndiProps.put("java.naming.factory.url.pkgs", "com.sun.enterprise.naming");
		jndiProps.put("java.naming.factory.state", "com.sun.corba.ee.impl.presentation.rmi.JNDIStateFactoryImpl");
		jndiProps.setProperty("org.omg.CORBA.ORBInitialHost", "127.0.0.1");
		jndiProps.setProperty("org.omg.CORBA.ORBInitialPort", "3700");
		Context ctx = new InitialContext(jndiProps);
		ManagerBeanRemote managerBean = (ManagerBeanRemote) ctx.lookup("java:global/L09P02/ManagerBean");
		
		managerBean.curataBiblioteca();
		managerBean.adaugaCarte("Test1", "Autor1", 1, "AB12");
		managerBean.adaugaCarte("Test2", "Autor2", 2, "CD34");

		System.out.println("Carti: ");
		System.out.println(managerBean.getAfisare());
		
		while (true) {
			int opt;

			System.out.println("Meniu:");
			System.out.println("1. Cauta dupa ISBN");
			System.out.println("2. Cauta dupa autor");
			System.out.println("3. Cauta dupa an");
			System.out.println("4. Imprumutarea unei carti");
			System.out.println("5. Returnarea unei carti");
			System.out.println("6. Scoaterea unei carti");
			System.out.println("Optiune: ");
			Scanner keyboard = new Scanner(System.in);
			opt = keyboard.nextInt();
			switch(opt) {
			case 1:
				System.out.println("Introduceti ISBN: ");
				keyboard.nextLine();
				String isbn = keyboard.nextLine();
				System.out.println("Carti gasite: ");
				System.out.println(managerBean.cautaISBN(isbn));
				break;
			case 2:
				System.out.println("Introduceti autor: ");
				keyboard.nextLine();
				String autor = keyboard.nextLine();
				System.out.println("Carti gasite: ");
				System.out.println(managerBean.cautaAutor(autor));
				break;
			case 3:
				System.out.println("Introduceti autor: ");
				keyboard.nextLine();
				int an = keyboard.nextInt();
				System.out.println("Carti gasite: ");
				System.out.println(managerBean.cautaAn(an));
				break;
			case 4:
				System.out.println();
				System.out.println("Carti: ");
				System.out.printf(managerBean.getAfisare());
				System.out.println("Alege carte: ");
				keyboard.nextLine();
				int index1 = keyboard.nextInt();
				System.out.println(managerBean.imprumuta(index1));
				break;
			case 5:
				System.out.println();
				System.out.println("Carti: ");
				System.out.println(managerBean.getAfisare());
				System.out.println("Alege carte: ");
				keyboard.nextLine();
				int index2 = keyboard.nextInt();
				System.out.println(managerBean.returneaza(index2));
				break;
			case 6:
				System.out.println();
				System.out.println("Carti: ");
				System.out.println(managerBean.getAfisare());
				System.out.println("Alege carte: ");
				keyboard.nextLine();
				int index3 = keyboard.nextInt();
				managerBean.scoateCarte(index3);
				System.out.println("Carti: ");
				System.out.println(managerBean.getAfisare());
				break;
			}
		}
	}

}
