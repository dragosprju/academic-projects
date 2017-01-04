package spatiu;

import java.util.Random;

public class Joc {
	Fereastra fereastra;
	Random randomizer;
	int scor = 0;
	private final String[] shipTypes = { "Armura/Atac", "Armura/Viteza", 
			"Armura/Manevr", "Atac/Viteza", "Atac/Manevr", "Viteza/Manevr" };
	Nava navaJucator;
	Nava navaInamica;
	
	public Joc() {
		navaJucator = new Nava();
		navaInamica = new Nava();
		
		fereastra = new Fereastra(this);
		randomizer = new Random();
		fereastra.setVisible(true);
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
		System.out.println("Am setat la inamic: " + navaInamica.tip);
	}
	
	public void attack() {
		if (navaJucator.PoateDistrugePe(navaInamica) == 0) {
			System.out.println("Se distrug amandoua navele." + navaJucator.PoateDistruge(navaInamica) + " " + navaInamica.PoateDistruge(navaJucator));
			fereastra.blowUpBoth(true, true);
			fereastra.stopGame();
			fereastra.popup("Jocul s-a terminat. V-ati distrus reciproc.\nScor final: "
					+ scor);
			System.out.println("Debug: tu-" + navaJucator.tip.toString() + ", el-"
					+ navaInamica.tip.toString());
		}
		else if (navaJucator.PoateDistrugePe(navaInamica) == 1) {
			System.out.println("Se distruge inamicul.");
			scor++;
			fereastra.setScore(scor);
			fereastra.blowUpEnemy(true);
			fereastra.disableAttack();
		}
		else if (navaJucator.PoateDistrugePe(navaInamica) == 2) {
			System.out.println("Nu se distruge inamicul.");
			fereastra.blowUpEnemy(false);
		}
	}
	
	public void attack2() {
		if (navaInamica.PoateDistrugePe(navaJucator) == 1) {
			fereastra.blowUpPlayer(true);
			System.out.println("Se distruge jucatorul.");
			fereastra.stopGame();
			fereastra.popup("Jocul s-a terminat. Inamicul te-a distrus.\nScor final: "
					+ scor);
			System.out.println("Debug: tu-" + navaJucator.tip.toString() + ", el-"
					+ navaJucator.tip.toString());
		}
		else if (navaInamica.PoateDistrugePe(navaJucator) == 2) {
			fereastra.blowUpPlayer(false);
			System.out.println("Nu se distruge jucatorul.");
		}
	}
	
	public void run() {
		fereastra.getPlansa().showEnemy();
		chooseShip();
		fereastra.enableAttack();
		fereastra.enableRun();
	}
}
