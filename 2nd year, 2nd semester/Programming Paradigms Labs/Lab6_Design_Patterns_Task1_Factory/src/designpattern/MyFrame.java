package designpattern;

import java.awt.Color;
import java.awt.Container;
import java.awt.Dimension;
import java.awt.Graphics;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;
import java.util.List;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;

@SuppressWarnings("serial")
public class MyFrame extends JFrame {
	private List<Forma> listaForme;
	
	public MyFrame() throws Exception {
		fabrica();
		
		canvas.setBackground(Color.WHITE);
		
		this.add(canvas);	
		this.setPreferredSize(new Dimension(500, 500));
		this.pack();
		this.setResizable(false);
	}

	Container contentPane = this.getContentPane();
	JPanel canvas = new JPanel() {
		@Override
		public void paintComponent(Graphics g) {
			for (Forma f : listaForme) {
				f.deseneaza(g);
			}
		}
	};
	
	private void fabrica() throws Exception {
		FabricaForme ff = new ImplementareFabricaForme();
		Forma cerc = ff.make("cerc");
		Forma patrat = ff.make("patrat");
		
		listaForme = new ArrayList<Forma>();
		listaForme.add(cerc);
		listaForme.add(patrat);
		canvas.repaint();
	}
}
