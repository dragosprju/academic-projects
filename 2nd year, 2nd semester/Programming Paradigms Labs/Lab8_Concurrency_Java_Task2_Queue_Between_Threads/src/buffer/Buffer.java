package buffer;

import java.util.ArrayList;
import java.util.List;

public class Buffer<T> {
	private List<T> myObjects = new ArrayList<T>();
	
	public synchronized void add(T x) {
		myObjects.add(x);
		this.notifyAll();
	}
	
	public synchronized T remove() { 
		while (myObjects.isEmpty()) {
			try	{ 
				this.wait();
			}
			catch (InterruptedException e) {}
		}
		return myObjects.remove(0);
	}
}
