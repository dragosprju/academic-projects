package spatiu;

import java.awt.*;
import java.util.Random;

import javax.swing.JPanel;

@SuppressWarnings("serial")
public class Plansa extends JPanel {
	private static final int NR_OF_STARS = 60;
	private static final int TRIANGLE_SIZE = 15;
	private static final int OFFSET = 10;
	private static final int WAIT_TIME = 30;
	private int playerBombX, playerBombY, playerBombR;
	private int enemyBombX, enemyBombY, enemyBombR;
	
	private int playerBombXDef, playerBombYDef, playerBombRDef;
	private int enemyBombXDef, enemyBombYDef, enemyBombRDef;
	private String playerName = "";
	private String enemyName = "";
	private String playerShipName = "";
	private String enemyShipName = "";
	
	private boolean drawPlayerBomb = false;
	private boolean drawEnemyBomb = false;
	private boolean hidePlayerShip = false;
	private boolean hideEnemyShip = false;
	
	private boolean starsDrawn = false;
	private Point starsDrawnPoints[];

	public Plansa() {
		this.setBackground(Color.BLACK);
		playerBombXDef = playerBombX = (int) (getPreferredSize().getWidth() / 2);
		playerBombYDef = playerBombY = (int) (getPreferredSize().getHeight() / 1.25);
		playerBombRDef = playerBombR = 4;
		
		enemyBombXDef = enemyBombX = (int) (getPreferredSize().getWidth() / 2);
		enemyBombYDef = enemyBombY = (int) (getPreferredSize().getHeight() / 5.75);
		enemyBombRDef = enemyBombR = 4;
		
		//debugBombs();
	}
	
	public void debugBombs() {
		drawPlayerBomb = true;
		drawEnemyBomb = true;
	}

	public Dimension getPreferredSize() {
		return new Dimension(500, 300);
	}

	public void paintComponent(Graphics g) {
		super.paintComponent(g); // Don't leave artifacts
		Graphics2D g2d = (Graphics2D) g;
		g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
		drawStars(g);
		if (!hidePlayerShip) 
			drawPlayerShip(g);
		if (!hideEnemyShip) 
			drawEnemyShip(g);
		if (drawPlayerBomb)	
			drawCircleByCenter(g, playerBombX, playerBombY, playerBombR, Color.ORANGE);
		if (drawEnemyBomb) 
			drawCircleByCenter(g, enemyBombX, enemyBombY, enemyBombR, Color.GREEN);
		if (playerName != "") {
			g.setColor(Color.WHITE);
			g.drawString(playerName,this.getPreferredSize().width / 2 - (OFFSET / 2) + 20,
					this.getPreferredSize().height - (OFFSET * 5) + 10);
		}
		if (enemyName != "") {
			g.setColor(Color.WHITE);
			g.drawString(enemyName, this.getPreferredSize().width / 2 - (OFFSET / 2) + 20,
					(OFFSET * 2) + 10);
		}
		if (playerShipName != "") {
			g.setColor(Color.LIGHT_GRAY);
			g.drawString(playerShipName,this.getPreferredSize().width / 2 - (OFFSET / 2) + 20,
					this.getPreferredSize().height - (OFFSET * 5) + 25);
		}
		if (enemyShipName != "") {
			g.setColor(Color.LIGHT_GRAY);
			g.drawString(enemyShipName, this.getPreferredSize().width / 2 - (OFFSET / 2) + 20,
					(OFFSET * 2) + 25);
		}
	}

	private void drawStars(Graphics g) {
		int nrOfStars = NR_OF_STARS;
		if (starsDrawn == false) {
			Random random = new Random();
			starsDrawnPoints = new Point[nrOfStars];
			for (int i = 0; i < nrOfStars; i++) {
				int randX = random.nextInt(480) + OFFSET;
				int randY = random.nextInt(280) + OFFSET;
				starsDrawnPoints[i] = new Point(randX, randY);
				g.setColor(Color.WHITE);
				g.fillOval(randX, randY, 3, 3);
			}
			starsDrawn = true;
		}
		else {
			for (int i = 0; i < nrOfStars; i++) {
				int currentPointX = (int) starsDrawnPoints[i].getX();
				int currentPointY = (int) starsDrawnPoints[i].getY();
				g.setColor(Color.WHITE);
				g.fillOval(currentPointX, currentPointY, 3, 3);
			}
		}
	}

	private void drawTriangleUp(Graphics g, int x, int y, Color color) {
		int xPoly[] = new int[3];
		int yPoly[] = new int[3];

		xPoly[0] = x;
		xPoly[1] = x + (TRIANGLE_SIZE) / 2;
		xPoly[2] = x + TRIANGLE_SIZE;

		yPoly[0] = y + TRIANGLE_SIZE + (TRIANGLE_SIZE / 2);
		yPoly[1] = y;
		yPoly[2] = y + TRIANGLE_SIZE + (TRIANGLE_SIZE / 2);

		Polygon triangle = new Polygon(xPoly, yPoly, xPoly.length);
		g.setColor(color);
		g.drawPolygon(triangle);
		g.fillPolygon(triangle);
	}

	private void drawTriangleDown(Graphics g, int x, int y, Color color) {
		int xPoly[] = new int[3];
		int yPoly[] = new int[3];

		xPoly[0] = x;
		xPoly[1] = x + (TRIANGLE_SIZE) / 2;
		xPoly[2] = x + TRIANGLE_SIZE;

		yPoly[0] = y;
		yPoly[1] = y + TRIANGLE_SIZE + (TRIANGLE_SIZE / 2);
		yPoly[2] = y;

		Polygon triangle = new Polygon(xPoly, yPoly, xPoly.length);
		g.setColor(color);
		g.drawPolygon(triangle);
		g.fillPolygon(triangle);
	}
	
	private void drawPlayerShip(Graphics g) {
		drawTriangleUp(g, this.getPreferredSize().width / 2 - (OFFSET / 2) - 2,
				this.getPreferredSize().height - (OFFSET * 5), Color.BLUE);
	}

	private void drawEnemyShip(Graphics g) {
		drawTriangleDown(g, this.getPreferredSize().width / 2 - (OFFSET / 2) - 2,
				(OFFSET * 2), Color.RED);
	}
	
	public void blowUpEnemy(boolean killEnemy) {
		drawPlayerBomb = true;
		while (playerBombY >= 31) {
			playerBombY = playerBombY - 10;
			this.repaint();
			try {
				Thread.sleep(WAIT_TIME);
			} catch (InterruptedException e) {}
		}
		for (int i=0; i<18; i++) {
			playerBombR += 1;
			this.repaint();
			try {
				Thread.sleep(WAIT_TIME);
			} catch (InterruptedException e) {}
		}
		if (killEnemy) hideEnemyShip = true;
		for (int i=0; i<23; i++) {
			playerBombR -= 1;
			this.repaint();
			try {
				Thread.sleep(WAIT_TIME);
			} catch (InterruptedException e) {}
		}
		drawPlayerBomb = false;
		playerBombX = playerBombXDef;
		playerBombY = playerBombYDef;
		playerBombR = playerBombRDef;
	}
	
	public void blowUpPlayer(boolean killPlayer) {
		drawEnemyBomb = true;
		while (enemyBombY <= getPreferredSize().height - 42) {
			enemyBombY = enemyBombY + 10;
			this.repaint();
			try {
				Thread.sleep(WAIT_TIME);
			} catch (InterruptedException e) {}
		}
		for (int i=0; i<18; i++) {
			enemyBombR += 1;
			this.repaint();
			try {
				Thread.sleep(WAIT_TIME);
			} catch (InterruptedException e) {}
		}
		if (killPlayer) hidePlayerShip = true;
		for (int i=0; i<23; i++) {
			enemyBombR -= 1;
			this.repaint();
			try {
				Thread.sleep(WAIT_TIME);
			} catch (InterruptedException e) {}
		}
		drawEnemyBomb = false;
		enemyBombX = enemyBombXDef;
		enemyBombY = enemyBombYDef;
		enemyBombR = enemyBombRDef;
	}
	
	/*
	public void blowUpBoth(boolean killPlayer, boolean killEnemy) {
		drawPlayerBomb = true;
		drawEnemyBomb = true;
		
		while (playerBombY >= 31) {
			playerBombY = playerBombY - 10;
			enemyBombY = enemyBombY + 10;
			this.repaint();
			try {
				Thread.sleep(WAIT_TIME);
			} catch (InterruptedException e) {}
		}
		for (int i=0; i<18; i++) {
			playerBombR += 1;
			enemyBombR += 1;
			this.repaint();
			try {
				Thread.sleep(WAIT_TIME);
			} catch (InterruptedException e) {}
		}
		if (killEnemy) hideEnemyShip = true;
		if (killPlayer) hidePlayerShip = true;
		for (int i=0; i<23; i++) {
			playerBombR -= 1;
			enemyBombR -= 1;
			this.repaint();
			try {
				Thread.sleep(WAIT_TIME);
			} catch (InterruptedException e) {}
		}
		drawPlayerBomb = false;
		drawPlayerBomb = false;
	}
	*/
	
	public void drawCircleByCenter(Graphics g, int centerX, int centerY, int r, Color color) {
		g.setColor(color);
		int actualX = centerX - r;
		int actualY = centerY - r;
		g.drawOval(actualX, actualY, r*2, r*2);
		g.fillOval(actualX, actualY, r*2, r*2);
	
	}
	
	public void hidePlayer() {
		hidePlayerShip = true;
		repaint();
	}
	
	public void showPlayer() {
		hidePlayerShip = false;
		repaint();
	}
	
	public void hideEnemy() {
		hideEnemyShip = true;
		repaint();
	}
	
	public void showEnemy() {
		hideEnemyShip = false;
		repaint();
	}
	
	public void setPlayerName(String name) {
		playerName = name;
		repaint();
	}
	
	public void setEnemyName(String name) {
		enemyName = name;
		repaint();
	}
	
	public void setPlayerShipName(String name) {
		playerShipName = name;
		repaint();
	}
	
	public void setEnemyShipName(String name) {
		enemyShipName = name;
		repaint();
	}
	
	public String getPlayerName() {
		return playerName;
	}
	
	public String getPlayerShipName() {
		return playerShipName;
	}
	
	public String getEnemyName() {
		return enemyName;
	}
	
	public String getEnemyShipName() {
		return enemyShipName;
	}
	
	public void setRedrawStars() {
		starsDrawn = false;
	}
}
