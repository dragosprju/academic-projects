package spatiu;

import java.awt.*;
import java.awt.event.*;
import java.util.Random;

import javax.swing.*;

@SuppressWarnings("serial")
public class Fereastra extends JFrame implements ActionListener {
	private JPanel northPanel, southPanel, southPanel2;
	private JLabel playerLabel, scoreLabel;
	private JTextField playerName;
	private JButton startGameButton, attackButton, runButton;
	private JComboBox<String> playerShipType;
	private Plansa plansa;
	private final String[] shipTypes = { "Armura/Atac", "Armura/Viteza", 
			"Armura/Manevr", "Atac/Viteza", "Atac/Manevr", "Viteza/Manevr" };
	private Random randomizer;
	private Joc joc;
	
	public Fereastra(Joc joc) {
		this.joc = joc;
		northPanel = new JPanel();
		southPanel = new JPanel();
		southPanel2 = new JPanel();
		playerLabel = new JLabel("Nume jucator: ");
		scoreLabel = new JLabel("Scor: - ");
		playerName = new JTextField(10);
		startGameButton = new JButton("Începe jocul");
		attackButton = new JButton("Ataca");
		runButton = new JButton("Fugi");
		playerShipType = new JComboBox<String>(shipTypes);
		plansa = new Plansa();
		randomizer = new Random();
		
		// Setam setarile initiale pt. fereastra propriu-zisa
		this.setSize(500, 450);
		this.setTitle("Spaceship Game, by Dragos v0.1");
		this.setResizable(false);
		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.getContentPane().setLayout(new BorderLayout());
		northPanel.setBackground(Color.BLACK);
		southPanel.setLayout(new FlowLayout(FlowLayout.LEFT));
		southPanel2.setLayout(new FlowLayout(FlowLayout.LEFT));
		southPanel.setPreferredSize(new Dimension(500, 30));
		southPanel2.setPreferredSize(new Dimension(400, 80));
		
		int selectedShipType = randomizer.nextInt(5) + 1;
		playerShipType.setSelectedIndex(selectedShipType);
		startGameButton.addActionListener(this);
		playerShipType.addActionListener(this);
		attackButton.addActionListener(this);
		runButton.addActionListener(this);
		
		attackButton.setEnabled(false);
		runButton.setEnabled(false);
		
		// Adaugam componentele
		northPanel.add(plansa);
		southPanel.add(playerLabel);
		southPanel.add(playerName);
		southPanel.add(playerShipType);
		southPanel.add(startGameButton);
		southPanel2.add(attackButton);
		southPanel2.add(runButton);
		southPanel2.add(scoreLabel);
		
		this.getContentPane().add(northPanel, BorderLayout.NORTH);
		this.getContentPane().add(southPanel, BorderLayout.CENTER);
		this.getContentPane().add(southPanel2, BorderLayout.SOUTH);
	}
	
	public Plansa getPlansa() {
		return plansa;
	}
	
	public void blowUpBoth(boolean killPlayer, boolean killEnemy) {
		Thread thread = new Thread() {
			
			public void run() {
				disableAttack();
				disableRun();
				plansa.blowUpBoth(true, true);
			}
		};
		thread.start();
	}
	
	public void blowUpPlayer(final boolean killPlayer) {
		Thread thread = new Thread() {
			
			public void run() {
				disableAttack();
				disableRun();
				plansa.blowUpPlayer(killPlayer);
			}
		};
		thread.start();
		
	}
	
	public void blowUpEnemy(final boolean killEnemy) {
		Thread thread = new Thread() {			
			public void run() {
				disableAttack();
				disableRun();
				plansa.blowUpEnemy(killEnemy);
				if (killEnemy == false) {
					joc.attack2();
				}
				enableRun();
			}
		};
		thread.start();
	}
	
	public void startGame() {
		playerName.setEditable(false);
		playerShipType.setEnabled(false);
		attackButton.setEnabled(true);
		runButton.setEnabled(true);
		startGameButton.setEnabled(false);
		setScore(0);
	}
	
	public void stopGame() {
		attackButton.setEnabled(false);
		runButton.setEnabled(false);
	}
	
	public void setScore(int score) {
		scoreLabel.setText("Scor: " + score);
	}
	
	public void disableAttack() {
		attackButton.setEnabled(false);
	}
	
	public void enableAttack() {
		attackButton.setEnabled(true);
	}
	
	public void disableRun() {
		runButton.setEnabled(false);
	}
	
	public void enableRun() {
		runButton.setEnabled(true);
	}
	
	public void popup(String message) {
		JOptionPane.showMessageDialog(null, message);
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getSource() == startGameButton) {
			plansa.setPlayerName(playerName.getText());
			plansa.setPlayerShipName(playerShipType.getItemAt(playerShipType.getSelectedIndex()));
			joc.start();
		}
		else if (e.getSource() == playerShipType) {
			plansa.setPlayerShipName(playerShipType.getItemAt(playerShipType.getSelectedIndex()));
		}
		else if (e.getSource() == attackButton) {
			Random random = new Random();
			int randInt = random.nextInt(10);
			if (randInt <= 6) 
				joc.attack();
			else
				joc.attack2();
		}
		else if (e.getSource() == runButton) {
			joc.run();
		}
	}


		
}
