package designpattern;

import java.awt.Graphics;
import java.util.Random;

public class Patrat implements Forma {
	private int x, y, latime;
	
	public Patrat() {
		Random rnd = new Random();
		
		x = rnd.nextInt(500);
		y = rnd.nextInt(500);
		latime = rnd.nextInt(200);
		
	}
	
	public void deseneaza(Graphics g) {
		g.drawRect(x, y, latime, latime);
	}
}
