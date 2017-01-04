package hotel;

public aspect CheckInCustomer {
	private Customer Room.checkedBy = null;
	
	public Customer Room.getCheckedBy() {
		return checkedBy;
	}
	
	public void Room.setCheckedBy(Customer value) {
		checkedBy = value;
	}
	
	public void Room.uncheck() {
		setCheckedBy(null);
	}
	
	public void StaffHandler.makeCheckIn(Reservation res) {
		res.getRoom().setCheckedBy(res.getCustomer());
		//consume reservation getReservations().remove(res);
	}
}
