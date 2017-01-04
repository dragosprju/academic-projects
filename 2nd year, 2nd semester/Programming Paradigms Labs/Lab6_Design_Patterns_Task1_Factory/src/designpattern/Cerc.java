package designpattern;

import java.awt.Graphics;
import java.util.Random;

public class Cerc implements Forma {
	private int x,y,raza;
	
	public Cerc() {
		Random rnd = new Random();
		x = rnd.nextInt(500);
		y = rnd.nextInt(500);
		raza = rnd.nextInt(200);
	}
	
	public void deseneaza(Graphics g) {
		g.drawOval(x, y, raza, raza);
	}
}
