package hotel;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.TimeUnit;
import java.io.PrintStream;

public aspect WaitingList {	
	pointcut makeReservation(Customer customer, RoomCategory categ) : call(void StaffHandler.makeReservation(Customer, RoomCategory)) && args(customer, categ);
	pointcut makeCheckOut(Room room) : call(void StaffHandler.makeCheckOut(Room)) && args(room);
	pointcut beforeOptions(String string) : call(void PrintStream.println(String)) && args(string);
	private List<Customer> waitingListCustomers = new ArrayList<Customer>();
	private List<RoomCategory> waitingListCategs = new ArrayList<RoomCategory>();
	private List<Integer> waitingListPeriods = new ArrayList<Integer>();
	
	before(Customer customer, RoomCategory categ) : makeReservation(customer, categ) {
		StaffHandler hotel = (StaffHandler)thisJoinPoint.getTarget();
		boolean ok = false;
		for (Room room : hotel.getRooms()) {
			if (room.isAvailable() && room.getCateg() == categ) {
				ok = true;
			}
		}
		if (!ok) {
			System.out.println("No room free! Customer was put on waiting list.");
			waitingListCustomers.add(customer);
			waitingListCategs.add(categ);
			long diff = Math.abs(L7T3.dateEnd.getTime() - L7T3.dateStart.getTime());
			int days = (int) TimeUnit.DAYS.convert(diff, TimeUnit.MILLISECONDS);
			waitingListPeriods.add(days);
		}		
	}
	
	after(Room room) : makeCheckOut(room) {
		StaffHandler hotel = (StaffHandler)thisJoinPoint.getTarget();
		for (Customer customer : waitingListCustomers) {
			int index = waitingListCustomers.indexOf(customer);
			if (waitingListCategs.get(index) == room.getCateg()) {
				hotel.makeReservation(customer, room.getCateg());
				L7T3.reservationPrices.add(waitingListPeriods.get(index)*room.getCateg().price());
				waitingListCustomers.remove(index);
				waitingListPeriods.remove(index);
				waitingListCategs.remove(index);
				break;
			}
		}
		
	}
	
	before(String string) : beforeOptions(string) {
		if (string.equals("\nOptions:") && !waitingListCustomers.isEmpty()) {
			System.out.println("\nWaiting List:");
			for (int i=0; i<waitingListCustomers.size(); i++) {				
				System.out.println(i+1 + ". Customer: " + waitingListCustomers.get(i).getId() + " -> Room type: " + waitingListCategs.get(i) + " -> Price: " + waitingListPeriods.get(i)*waitingListCategs.get(i).price());
			}
		}
	}
}
