package counter;

import java.util.ArrayList;
import java.util.List;

public class Listener {
	private List<Responder> watchedResponders = new ArrayList<Responder>();
	private List<Integer> oldCounts = new ArrayList<Integer>();
	
	public void addResponder(Responder responder) {
		watchedResponders.add(responder);
		oldCounts.add(responder.getCount());
	}
	
	public void checkResponders() {
		int i, selectedResponderCount;
		for (i=0; i < watchedResponders.size(); i++) {
			selectedResponderCount = watchedResponders.get(i).getCount();
			if (selectedResponderCount != oldCounts.get(i)) {
				System.out.println("Counter " + i + " changed: " + selectedResponderCount);
				oldCounts.set(i, selectedResponderCount);
			}
		}
	}
	
}
