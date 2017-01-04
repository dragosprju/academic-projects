package nava;

import java.util.ArrayList;
import java.util.List;
import java.util.Observable;
import java.util.Observer;
import java.util.concurrent.PriorityBlockingQueue;

public class CampLupta implements Runnable, Observer {
	NavaComparator navaComparator = new NavaComparator();
	PriorityBlockingQueue<Nava> naveQueue;
	List<Nava> naveList = new ArrayList<Nava>();
	List<Nava> naveInstantiate = new ArrayList<Nava>();	
	Nava navaCastigatoare = null;
	int threadCap;
	
	public CampLupta(int nrNave, int threadCap) {
		naveQueue = new PriorityBlockingQueue<Nava>(nrNave, navaComparator);
		this.threadCap = threadCap;
		for (int i=0; i<nrNave; i++) {
			Nava toAdd = new Nava("N" + i, i+1, this);
			naveQueue.add(toAdd);
			naveList.add(toAdd);
			//System.out.println("Am adaugat " + naveList.get(i).getNume() + ".");
		}
	}
	
	public void start() {
		Thread thread = new Thread(this);
		for (int i=0; i<threadCap; i++) {
			naveInstantiate.add(naveQueue.poll());
		}
		thread.start();

	}
	
	public void run() {
		while (!verificaCastig()) {
			for (int i=0; i<naveInstantiate.size(); i++) {
				Nava nava = naveInstantiate.remove(0);
				if (nava.getThread().getState() == Thread.State.NEW) {
					nava.getThread().run();
					//System.out.printf("[%s][%d] ", nava.getNume(), nava.getPriority());
					naveInstantiate.remove(nava);
					nava.setPriority(nava.getPriority()+1);
					naveQueue.add(nava);
				}
			}
			if (naveInstantiate.size() < threadCap) {
				for (int i=0; i<(threadCap-naveInstantiate.size()); i++) {
					Nava deInstantiat = naveQueue.poll();
					if (deInstantiat != null) {
						if (!deInstantiat.getDistrus()) {
							naveInstantiate.add(deInstantiat);
						}
						else {
							deInstantiat.setPriority(0);
							i--;
						}
					}
					else {
						//System.out.println(naveInstantiate.size());
					}					
				}
			}
		}
		System.out.println("Nava castigatoare: " + navaCastigatoare.getNume() + ".");
	}
	
	public List<Nava> getNave() {
		return naveList;
	}
	
	public int getNrNave() {
		return naveList.size();
	}

	public Nava getNava(int whoDoIAttack) {
		return naveList.get(whoDoIAttack);
	}
	
	public int getIndexNava(Nava nava) {
		return naveList.indexOf(nava);
	}
	
	public boolean verificaCastig() {
		boolean ok1 = true;
		boolean ok2 = true;
		for (Nava nava : naveList) {
			if (ok1 && !nava.getDistrus()) {
				ok1 = false;
				navaCastigatoare = nava;
			}
			else if (ok2 && !nava.getDistrus()) {
				ok2 = false;
			}
		}
		if (!ok1 && ok2) {
			return true;
		}
		else {
			return false;
		}
	}

	@Override
	public void update(Observable o, Object arg) {
		System.out.printf((String)arg);		
	}
}
