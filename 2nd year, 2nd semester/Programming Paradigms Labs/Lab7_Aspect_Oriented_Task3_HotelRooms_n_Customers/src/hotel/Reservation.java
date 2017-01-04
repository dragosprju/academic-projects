package hotel;

public class Reservation {
	private Room room;
	private Customer customer;
	
	public Room getRoom() {
		return room;
	}
	
	public Customer getCustomer() { 
		return customer;
	}
	
	public Reservation(Room room, Customer customer) {
		this.room = room;
		this.customer = customer;
	}
}
