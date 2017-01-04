package hotel;

public aspect ReserveRoomAspect {
	private boolean Room.available;
	
	public void Room.setAvailability(boolean value) {
		available = value;
	}
	
	public boolean Room.isAvailable() {
		return available;
	}
	
	private Room StaffHandler.getAvailableRoom(RoomCategory categ) {
		for (Room room : getRooms()) {
			if (room.isAvailable() && room.getCateg() == categ) {
				return room;
			}
		}		
		return null;
	}
	
	public void StaffHandler.makeReservation(Customer customer, RoomCategory categ) {
		Room foundRoom = getAvailableRoom(categ);
		if (foundRoom != null) {
			Reservation res = new Reservation(foundRoom, customer);
			getReservations().add(res);
			foundRoom.setAvailability(false);
		}
	}
}
