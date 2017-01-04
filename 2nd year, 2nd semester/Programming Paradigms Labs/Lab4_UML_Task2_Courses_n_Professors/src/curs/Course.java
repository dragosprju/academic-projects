package curs;

public class Course {
	private boolean closed;
	private State previousState;
	private State state;
	private int numStudents;
	private int capacity;
	
	public Course(int capacity) {
		this.capacity = capacity;
		state = State.unassigned;
	}
	
	public void addProfessor() {
		if (closed) {
			return;
		}
		changeState(State.assigned);
	}
	
	public void removeProfessor() {
		if (closed) {
			return;
		}
		changeState(State.unassigned);
	}
	
	public void addStudent() {
		if (state == State.full || closed == true) {
			return;
		}
		numStudents++;
		if (numStudents == capacity) {
			changeState(State.full);
			closed = true;
		}
	}
	
	public void removeStudent() {
		if (numStudents == 0 || closed == true) {
			return;
		}
		numStudents--;
		if (state == State.full) {
			revertState();
		}
	}
	
	public void closeRegistration() {
		if (numStudents < 3 || state == State.unassigned) {
			closed = true;
			changeState(State.cancelled);
		} else if (numStudents >= 3 && state == State.assigned) {
			closed = true;
			changeState(State.committed);
		} else if (state == State.full && previousState == State.assigned) {
			changeState(State.committed);
		} else if (state == State.full && previousState == State.unassigned) {
			changeState(State.cancelled);
		}
	}
	
	private void changeState(State state) {
		previousState = this.state;
		this.state = state;
	}
	
	private void revertState() {
		State aux;
		aux = previousState;
		previousState = state;
		state = aux;
		
	}
	
	public String getState() {
		return state.toString();
	}
	
	public int getNumStudents() {
		return numStudents;
	}
	
	public int getCapacity() {
		return capacity;
	}

	public String getRegistrationStatus() {
		if (closed == true) {
			return "closed";
		} else {
			return "open";
		}
	}
}
