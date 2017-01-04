package spatiu;

import java.awt.*;

@SuppressWarnings("serial")
public class PlansaBombe extends Canvas {
	private int enemyBombX;
	private int enemyBombY;

	public PlansaBombe() {
		enemyBombX = (int) (getPreferredSize().getWidth() / 2 - 5);
		enemyBombY = (int) (getPreferredSize().getHeight() / 2 - 1);
		this.setBackground(Color.RED);
	}

	public Dimension getPreferredSize() {
		return new Dimension(30, 250);
	}

	public void paint(Graphics g) {
		super.paint(g); // Don't leave artifacts
		Graphics2D g2d = (Graphics2D) g;
		g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
		drawCircleByCenter(g, enemyBombX, enemyBombY, 6, Color.ORANGE);
	}
	
	public void blowUpEnemy() {		
		while (enemyBombY >= 50) {
			enemyBombY = enemyBombY - 10;
			this.repaint();
			try {
				Thread.sleep(50);
			} catch (InterruptedException e) {}
		}
	}
	
	private void drawCircleByCenter(Graphics g, int centerX, int centerY, int r, Color color) {
		g.setColor(color);
		int actualX = centerX - r/2;
		int actualY = centerY - r/2;
		g.drawOval(actualX, actualY, r*2, r*2);
		g.fillOval(actualX, actualY, r*2, r*2);
	
	}

}
