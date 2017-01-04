package cerc;

import java.awt.BorderLayout;
import java.awt.Frame;
import java.awt.Panel;

@SuppressWarnings("serial")
public class MyFrame extends Frame{
	MyDrawing drawing;
	
	public MyFrame() {
		super("Un cerc");
		Panel p = new Panel(new BorderLayout());
		this.add(p);
		
		drawing = new MyDrawing();
		p.add(drawing);
		this.pack();
		this.setVisible(true);
		
	}
}
