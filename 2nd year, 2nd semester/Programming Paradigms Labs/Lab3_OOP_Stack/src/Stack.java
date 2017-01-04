public class Stack<T> {
	private T[] buffer;
	private int first = -1;
	private int capacity;

	@SuppressWarnings("unchecked")
	public Stack(int cap) {
		buffer = (T[]) (new Object[cap]);
		this.capacity = cap;
	}

	public boolean push(T x) {
		if (first < capacity) {
			buffer[++first] = x;
			return true;
		} else {
			return false;
		}
	}

	public T pop() {
		if (!isEmpty()) {
			first--;
			return buffer[first+1];
		}
		else
			return null;
	}

	public T top() {
		if (!isEmpty()) {
			return buffer[first];
		}
		else
			return null;
	}

	public boolean isEmpty() {
		if (first == -1)
			return true;
		else
			return false;
	}
}
