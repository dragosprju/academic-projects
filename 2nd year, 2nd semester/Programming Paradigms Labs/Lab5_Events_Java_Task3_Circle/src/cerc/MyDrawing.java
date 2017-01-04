package cerc;

import java.awt.Canvas;
import java.awt.Dimension;
import java.awt.Graphics;

@SuppressWarnings("serial")
public class MyDrawing extends Canvas{
	public MyDrawing() {
		this.setPreferredSize(new Dimension(25, 25));
	}
	public void paint(Graphics g) {
		g.drawOval(0, 0, 20, 20);
	}
}
