package cuanta;

import java.awt.Color;
import java.awt.Graphics;
import java.awt.Polygon;

import spatiu.Plansa;

import javax.swing.JPanel;

public aspect Cuanta {
	pointcut blowUpEnemy() : call(void Plansa.blowUpEnemy(boolean));
	pointcut blowUpPlayer() : call(void Plansa.blowUpPlayer(boolean));
	pointcut blowUpBoth() : call(void Plansa.blowUpBoth(boolean, boolean));
	
	private static final int OFFSET = 10;
	private static final int WAIT_TIME = 100;
		
	before() : blowUpEnemy() {
		Plansa plansa = (Plansa)thisJoinPoint.getTarget();
		Graphics g = plansa.getGraphics();
		int bothX = plansa.getPreferredSize().width / 2 - (OFFSET / 2) - 2;
		int downY = plansa.getPreferredSize().height - (OFFSET * 5);
		//int upY = OFFSET * 2;
		
		for (int i=0; i<15; i++) {
			Color colorToDraw;
			if (i%2 == 0) {
				colorToDraw = Color.ORANGE;
			}
			else {
				colorToDraw = Color.CYAN;
			}
			plansa.drawTriangleUp(g, bothX, downY, colorToDraw);
			try {
				Thread.sleep(WAIT_TIME);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}		
	
	before() : blowUpPlayer() {
		Plansa plansa = (Plansa)thisJoinPoint.getTarget();
		Graphics g = plansa.getGraphics();
		int bothX = plansa.getPreferredSize().width / 2 - (OFFSET / 2) - 2;
		//int downY = plansa.getPreferredSize().height - (OFFSET * 5);
		int upY = OFFSET * 2;
		
		for (int i=0; i<15; i++) {
			Color colorToDraw;
			if (i%2 == 0) {
				colorToDraw = Color.ORANGE;
			}
			else {
				colorToDraw = Color.CYAN;
			}
			plansa.drawTriangleDown(g, bothX, upY, colorToDraw);
			try {
				Thread.sleep(WAIT_TIME);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}
	
	before() : blowUpBoth() {
		Plansa plansa = (Plansa)thisJoinPoint.getTarget();
		Graphics g = plansa.getGraphics();
		int bothX = plansa.getPreferredSize().width / 2 - (OFFSET / 2) - 2;
		int downY = plansa.getPreferredSize().height - (OFFSET * 5);
		int upY = OFFSET * 2;
		
		for (int i=0; i<15; i++) {
			Color colorToDraw1, colorToDraw2;
			if (i%2 == 0) {
				colorToDraw1 = Color.ORANGE;
				colorToDraw2 = Color.CYAN;
			}
			else {
				colorToDraw1 = Color.CYAN;
				colorToDraw2 = Color.ORANGE;
			}
			plansa.drawTriangleUp(g, bothX, downY, colorToDraw1);
			plansa.drawTriangleDown(g, bothX, upY, colorToDraw2);
			try {
				Thread.sleep(WAIT_TIME);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}
}
