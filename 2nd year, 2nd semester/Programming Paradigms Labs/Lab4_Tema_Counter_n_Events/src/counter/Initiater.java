package counter;

import java.util.List;
import java.util.ArrayList;

public class Initiater {
	private List<IResponder> responders = new ArrayList<IResponder>();
	
	public void addResponder(IResponder toAdd) {
		responders.add(toAdd);
	}
	
	public void incrementCounters() {
		for (IResponder responder : responders) {
			responder.incrementCounter();
		}
		
	}
}
