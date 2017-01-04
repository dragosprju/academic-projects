package grafic;

import java.awt.BorderLayout;
import java.awt.Button;
import java.awt.Frame;
import java.awt.Panel;
import java.awt.TextField;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;
import java.util.List;

@SuppressWarnings("serial")
public class MyFrame extends Frame implements ActionListener {
	private TextField tf;
	private List<Button> btns = new ArrayList<Button>();
	
	public MyFrame() {
		super("Fereastra mea");
		Panel p = new Panel(new BorderLayout());
		Panel p2 = new Panel (/* new FlowLayout() */);
		this.add(p);
		p.add(p2, BorderLayout.NORTH);
		for (int i=0; i<3; ++i) {
			// adaugam string gol cu un int ca sa fie string
			Button b = new Button("" + (i+1));
			btns.add(b);
			b.setActionCommand("smantana" + (i+1));
		}
		
		for (Button b : btns) {
			p2.add(b);
			b.addActionListener(this);			
		}
		
		tf = new TextField();
		p.add(tf, BorderLayout.SOUTH);
		this.pack();
		this.setVisible(true);
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		String cmd = e.getActionCommand();
		tf.setText(cmd);
	}	
}
