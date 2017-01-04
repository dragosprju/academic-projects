package foc;

import java.awt.BasicStroke;
import java.awt.Color;
import java.awt.Graphics2D;
import java.awt.RenderingHints;

import spatiu.Plansa;

public class FocLinie implements IFoc {
	public static final int WAIT_TIME = 50;
	public static final int WAIT_TIME2 = 500;
	private static final int circleRStart = 2;
	private static final int circleRStop = 30;
	private static final int lineX = 250;
	
	private static final int lineYPlayerStart = 240;
	private static final int lineYPlayerStop = 60;	
	
	private Plansa plansa;
	private boolean ptJucator;

	public FocLinie(Plansa plansa, boolean eJucator) {
		this.plansa = plansa;
		ptJucator = eJucator;
	}

	@Override
	public void Trage(final boolean distruge) {
		if (ptJucator) {
			Thread thread = new Thread() {

				public void run() {
					Graphics2D g2d = (Graphics2D) plansa.getGraphics();
					g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING,
							RenderingHints.VALUE_ANTIALIAS_ON);
					g2d.setColor(Color.YELLOW);
					g2d.setStroke(new BasicStroke(4));
					for (int i = lineYPlayerStart; i >= lineYPlayerStop; i -= 10) {
						g2d.drawLine(lineX, i, lineX, i - 10);
						try {
							Thread.sleep(WAIT_TIME);
						} catch (InterruptedException e) {
							e.printStackTrace();
						}
					}
					if (distruge) {
						for (int i = circleRStart; i < circleRStop; i += 5) {
							plansa.drawCircleByCenter(plansa.getGraphics(),
									lineX, lineYPlayerStop - 30, i,
									Color.YELLOW);
							try {
								Thread.sleep(WAIT_TIME);
							} catch (InterruptedException e) {
								e.printStackTrace();
							}
						}
					} else {
						try {
							Thread.sleep(WAIT_TIME2);
						} catch (InterruptedException e) {
							e.printStackTrace();
						}
						plansa.repaint();
					}
				}
			};
			thread.start();
		}
		else if (!ptJucator) {
			Thread thread = new Thread() {

				public void run() {
					Graphics2D g2d = (Graphics2D) plansa.getGraphics();
					g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING,
							RenderingHints.VALUE_ANTIALIAS_ON);
					g2d.setColor(Color.CYAN);
					g2d.setStroke(new BasicStroke(4));
					for (int i = lineYPlayerStop; i <= lineYPlayerStart; i += 10) {
						g2d.drawLine(lineX, i, lineX, i - 10);
						try {
							Thread.sleep(WAIT_TIME);
						} catch (InterruptedException e) {
							e.printStackTrace();
						}
					}
					if (distruge) {
						for (int i = circleRStart; i < circleRStop; i += 5) {
							plansa.drawCircleByCenter(plansa.getGraphics(),
									lineX, lineYPlayerStart + 30, i,
									Color.CYAN);
							try {
								Thread.sleep(WAIT_TIME);
							} catch (InterruptedException e) {
								e.printStackTrace();
							}
						}
					} else {
						try {
							Thread.sleep(WAIT_TIME2);
						} catch (InterruptedException e) {
							e.printStackTrace();
						}
						plansa.repaint();
					}
				}
			};
			thread.start();
		}
	}
}
