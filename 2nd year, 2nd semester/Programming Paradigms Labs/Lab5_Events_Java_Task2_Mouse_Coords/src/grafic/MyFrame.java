package grafic;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.Frame;
import java.awt.Label;
import java.awt.Panel;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;

@SuppressWarnings("serial")
public class MyFrame extends Frame {
	private Label label;

	public MyFrame() {
		//setLayout(new FlowLayout());
		super("Mouse Clicker v0.3");
		Panel p = new Panel(new BorderLayout());
		this.add(p);
		label = new Label();
		label.setText("Bun venit!");
		p.add(label, BorderLayout.NORTH);

		p.addMouseListener(new MouseAdapter() {

			public void mouseClicked(MouseEvent e) {
				label.setText("(" + e.getX() + ", " + e.getY() + ")");
			}

		});
		
		this.addWindowListener(new WindowAdapter() {
			
			public void windowClosing(WindowEvent e) {
				System.exit(0);
			}
		});
		this.setPreferredSize(new Dimension(500, 500));
		this.pack(); // Pack se uita la Preferred Size
		this.setVisible(true);

	}

}
