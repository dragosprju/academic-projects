package nava;

import java.util.Observable;
import java.util.Random;

public class Nava extends Observable implements Runnable {
	private String nume;
	private int priority;
	private CampLupta campLupta;
	private Random random = new Random();
	private boolean distrus = false;
	private String message;
	private int doIAttack, doIDestroy, whoDoIAttack;
	private Thread thread = new Thread(this);
	private boolean predefinedTarget = false;
	
	public Nava(String nume, int priority, CampLupta campLupta) {
		this.nume = nume;
		this.priority = priority;
		this.campLupta = campLupta;
		this.addObserver(campLupta);
	}
	
	public void start() {
		thread.start();
	}
	
	public void run() {
		if (!predefinedTarget) {
			setTarget();
		}
		attack();
		predefinedTarget = false;
	}
	
	public void attack() {
		boolean IAttack = false, IDestroy = false;
		if (distrus == true) {
			return;
		}
		if (doIAttack > 5) {
			IAttack = true;
		}
		if (doIDestroy > 5) {
			IDestroy = true;
		}
		Nava toAttack = campLupta.getNava(whoDoIAttack);
		if (toAttack == this || toAttack.getDistrus()) {
			IAttack = false;
			IDestroy = false;
		}
		if (IAttack && IDestroy) {
			message = nume + " ataca " + toAttack.nume + " si il distruge.\n";
			notif();
			toAttack.distruge();
		}
		else if (IAttack && !IDestroy) {
			message = nume + " ataca " + toAttack.nume + " si rateaza.\n";
			notif();
			toAttack.setTarget(this);
			toAttack.setPriority(0);		
			toAttack.predefinedTarget = true;
		}
	}
	
	public void distruge() {
		distrus = true;
	}	
	
	public String getNume() {
		return nume;
	}

	public int getPriority() {
		return priority;
	}
	
	public boolean getDistrus() {
		return distrus;
	}
	
	public Thread getThread() {
		return thread;
	}
	
	public void setTarget() {
		doIAttack = random.nextInt(10) + 1;
		doIDestroy = random.nextInt(10) + 1;
		whoDoIAttack = random.nextInt(campLupta.getNrNave());
	}
	
	public void setTarget(Nava nava) {
		doIAttack = random.nextInt(10) + 1;
		doIDestroy = random.nextInt(10) + 1;
		this.whoDoIAttack = campLupta.getIndexNava(nava);
	}
	
	public void setPriority(int priority) {
		this.priority = priority;
	}
	
	public void notif() {
		setChanged();
		notifyObservers(message);
	}
}
