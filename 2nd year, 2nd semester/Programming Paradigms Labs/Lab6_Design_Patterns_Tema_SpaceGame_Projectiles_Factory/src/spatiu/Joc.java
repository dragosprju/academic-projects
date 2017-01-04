package spatiu;

import java.util.Random;

public class Joc {
	private Fereastra fereastra;
	private Random randomizer;
	int scor = 0;
	private final String[] shipTypes = { "Armura/Atac", "Armura/Viteza", 
			"Armura/Manevr", "Atac/Viteza", "Atac/Manevr", "Viteza/Manevr" };
	private static final int WAIT_TIME = 2000;
	private Nava navaJucator;
	private Nava navaInamica;
	
	public Joc() {		
		fereastra = new Fereastra(this);
		randomizer = new Random();
		fereastra.setVisible(true);
		navaJucator = new Nava(fereastra.getPlansa(), true);
		navaInamica = new Nava(fereastra.getPlansa(), false);
	}
	
	public void start() {
		chooseShip();
		
		navaJucator.extrageTipDinSir(fereastra.getPlansa().getPlayerShipName());
		//navaInamica.extrageTipDinSir(fereastra.getPlansa().getEnemyShipName());
		
		fereastra.startGame();
	}
	
	public void chooseShip() {
		fereastra.getPlansa().setRedrawStars();
		
		int randomInt = randomizer.nextInt(20);
		String chosenEnemyName = NumeNave.values()[randomInt].toString();		
		fereastra.getPlansa().setEnemyName(chosenEnemyName);
		
		randomInt = randomizer.nextInt(6);
		fereastra.getPlansa().setEnemyShipName(shipTypes[randomInt]);
		navaInamica.tip = randomInt + 1;
		if (randomInt <= 2)
			navaInamica.schimbaFoc();
		//System.out.println("Am setat la inamic: " + navaInamica.tip);
	}
	
	public void attack() {
		/*
		if (navaJucator.PoateDistrugePe(navaInamica) == 0) {
			System.out.println("Se distrug amandoua navele." + navaJucator.PoateDistruge(navaInamica) + " " + navaInamica.PoateDistruge(navaJucator));
			navaJucator.getFoc().Trage(true, true, true, true);
			fereastra.stopGame();
			fereastra.popup("Jocul s-a terminat. V-ati distrus reciproc.\nScor final: "
					+ scor);
			System.out.println("Debug: tu-" + navaJucator.tip.toString() + ", el-"
					+ navaInamica.tip.toString());
		}*/
		if (navaJucator.PoateDistrugePe(navaInamica) == 0) {
			int odds = randomizer.nextInt(100) + 1;
			//System.out.println("Randomizer: " + odds);
			if (odds <= 50) {
				//System.out.println("Jucatorul --> Inamicul");
				fereastra.disableAttack();
				navaJucator.getFoc().Trage(true);
				setScor(++scor);
				Thread thread = new Thread() {

					public void run() {
						try {
							Thread.sleep(WAIT_TIME);
						} catch (InterruptedException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}
						fereastra.enableAttack();					
					}
				};
				thread.start();

			}
			else {
				//System.out.println("Inamicul --> Jucatorul");
				fereastra.disableAttack();
				fereastra.disableRun();
				navaInamica.getFoc().Trage(true);
				Thread thread = new Thread() {

					public void run() {
						try {
							Thread.sleep(WAIT_TIME);
						} catch (InterruptedException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}
						fereastra.stopGame();
						fereastra.popup("Jocul s-a terminat. Inamicul te-a distrus.\nScor final: "
								+ scor);				
					}
				};
				thread.start();

			}
		}
		else if (navaJucator.PoateDistrugePe(navaInamica) == 1) {
			//System.out.println("Se distruge inamicul.");
			setScor(++scor);
			fereastra.disableAttack();
			navaJucator.getFoc().Trage(true);
		}
		else if (navaJucator.PoateDistrugePe(navaInamica) == 2) {
			//System.out.println("Nu se distruge inamicul.");
			fereastra.disableAttack();
			navaJucator.getFoc().Trage(false);
			Thread thread = new Thread() {

				public void run() {
					try {
						Thread.sleep(WAIT_TIME);
					} catch (InterruptedException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
					attack2();			
				}
			};
			thread.start();

		}
	}
	
	public void attack2() {
		if (navaInamica.PoateDistrugePe(navaJucator) == 0) {
			attack();
		}
		else if (navaInamica.PoateDistrugePe(navaJucator) == 1) {
			navaInamica.getFoc().Trage(true);
			//System.out.println("Se distruge jucatorul.");
			fereastra.stopGame();
			fereastra.popup("Jocul s-a terminat. Inamicul te-a distrus.\nScor final: "
					+ scor);
			//System.out.println("Debug: tu-" + navaJucator.tip.toString() + ", el-"
			//		+ navaJucator.tip.toString());
		}
		else if (navaInamica.PoateDistrugePe(navaJucator) == 2) {
			fereastra.disableAttack();
			navaInamica.getFoc().Trage(false);
			Thread thread = new Thread() {

				public void run() {
					try {
						Thread.sleep(WAIT_TIME);
					} catch (InterruptedException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
					fereastra.enableAttack();				
				}
			};
			thread.start();
			//System.out.println("Nu se distruge jucatorul.");
		}
	}
	
	private void setScor(int scor) {
		fereastra.setScore(scor);
		if (scor == 6) {
			navaJucator.schimbaFoc();
		}
	}
	
	public void run() {
		fereastra.getPlansa().showEnemy();
		chooseShip();
		fereastra.enableAttack();
		fereastra.enableRun();
	}
}
