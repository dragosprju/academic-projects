package counter;

public class Responder implements IResponder {
	private int count;
	
	public Responder(int startingCount) {
		count = startingCount;
	}
	
	@Override
	public void incrementCounter() {
		count++;
	}
	
	public Integer getCount() {
		return count;
	}
}
