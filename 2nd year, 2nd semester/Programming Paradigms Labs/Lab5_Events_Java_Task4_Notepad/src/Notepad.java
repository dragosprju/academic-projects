import javax.swing.*;

import java.awt.BorderLayout;
import java.awt.Font;
import java.awt.event.*;
import java.util.Scanner;
import java.io.*;

@SuppressWarnings("serial")
// Facem clasa Notepad in sine fereastra container
public class Notepad extends JFrame implements ActionListener {

	private JTextArea textArea;
	private JScrollPane textAreaScrollPane;
	private JMenuBar menuBar;
	private JMenu fileMenu, helpMenu;
	private JMenuItem openFile, saveFile, exit, about;
	
	public Notepad() {
		// Initializam componente
		textArea = new JTextArea();
		textAreaScrollPane = new JScrollPane(textArea, JScrollPane.VERTICAL_SCROLLBAR_ALWAYS, JScrollPane.HORIZONTAL_SCROLLBAR_NEVER);
		
		menuBar = new JMenuBar();
		
		fileMenu = new JMenu();
		helpMenu = new JMenu();
		
		openFile = new JMenuItem();
		saveFile = new JMenuItem();
		exit = new JMenuItem();
		about = new JMenuItem();
		
		// Setam setari initiale containerului (Notepad extends JFrame)
		this.setSize(500, 300);
		this.setTitle("Dragos' Notepad v0.2");
		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.getContentPane().setLayout(new BorderLayout());
		
		// Setam setari initiale Text Area-ului
		textArea.setFont(new Font("Lucida Console", Font.PLAIN, 13));
		textArea.setLineWrap(true);
		
		// Setam setari initiale Menu Bar-ului
		
		// FILE
		fileMenu.setText("File");
		fileMenu.setMnemonic(KeyEvent.VK_F);
		menuBar.add(fileMenu);
		
		// OPEN
		openFile.setText("Open");
		openFile.setMnemonic(KeyEvent.VK_O);
		openFile.addActionListener(this);
		fileMenu.add(openFile);		
		
		// SAVE
		saveFile.setText("Save");
		saveFile.setMnemonic(KeyEvent.VK_S);
		saveFile.addActionListener(this);
		fileMenu.add(saveFile);
		
		// EXIT
		exit.setText("Exit");
		exit.setMnemonic(KeyEvent.VK_X);
		exit.addActionListener(this);
		fileMenu.add(exit);
		
		// HELP
		helpMenu.setText("Help");
		helpMenu.setMnemonic(KeyEvent.VK_H);
		menuBar.add(helpMenu);
		
		// ABOUT
		about.setText("About");
		about.setMnemonic(KeyEvent.VK_A);
		about.addActionListener(this);
		helpMenu.add(about);
				
		
		// Adaugam componente in container
		this.setJMenuBar(menuBar);
		this.getContentPane().add(textAreaScrollPane);
	}
	
	// Metoda aceasta se pune in Main ca sa porneasca Notepadul
	public void open() {
		this.setVisible(true);
	}

	@Override
	public void actionPerformed(ActionEvent e) {
		// TODO Auto-generated method stub
		
		if (e.getSource() == this.openFile) {
			JFileChooser open = new JFileChooser();
			int option = open.showOpenDialog(this); // optiuni posibile selectate: approve sau cancel
			
			if (option == JFileChooser.APPROVE_OPTION) {
				this.textArea.setText("");
				try {
					Scanner scan = new Scanner(new FileReader(open.getSelectedFile().getPath()));
					while (scan.hasNext()) {
						this.textArea.append(scan.nextLine() + "\n");
					}
					scan.close();
				} catch (Exception ex) {
					System.out.println(ex.getMessage());
				}
			}
		}
		
		else if (e.getSource() == this.saveFile) {
			JFileChooser save = new JFileChooser();
			int option = save.showSaveDialog(this); // atentie, e showSAVEdialog, nu ca sus, cu OPEN
			
			if (option == JFileChooser.APPROVE_OPTION) {
				try {
					BufferedWriter out = new BufferedWriter(new FileWriter(save.getSelectedFile().getPath()));
					out.write(this.textArea.getText());
					out.close();
				} catch (Exception ex) {
					System.out.println(ex.getMessage());
				}
			}
		}
		
		else if (e.getSource() == this.exit) {
			System.exit(0);
		}
		
		else if (e.getSource() == this.about) {
			JOptionPane.showMessageDialog(this, "Dragos' Notepad v0.2\nPerju Dragoș-Ștefan");
		}
	}
	
}