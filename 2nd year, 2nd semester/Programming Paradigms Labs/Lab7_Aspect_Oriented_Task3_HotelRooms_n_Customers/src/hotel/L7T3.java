package hotel;

import comparators.CustomerComparator;
import comparators.RoomComparator;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Scanner;
import java.util.concurrent.TimeUnit;

import randomizer.PopulateCustomers;

public class L7T3 {
	
	private static SimpleDateFormat format = new SimpleDateFormat("dd.MM.yyyy");
	public static List<Integer> reservationPrices = new ArrayList<Integer>();	
	public static Date dateStart = null;
	public static Date dateEnd = null;
	
	private static boolean isValidDate(String input) {
		try {
			format.parse(input);
			return true;
		}
		catch(ParseException e) {
			return false;
		}
	}
	
	public static void main(String[] args) {
		StaffHandler hotel = new StaffHandler();
		List<Customer> customers = new ArrayList<Customer>();
		List<Room> hotelRooms = hotel.getRooms();
		List<Reservation> hotelReservations = null;
		
		Customer selectedCustomer = null;
		RoomCategory selectedCategory = null;
		Reservation selectedReservation = null;
		Room selectedRoom = null;
		
		Scanner keyboard = new Scanner(System.in);
		boolean ok;		
		int opt;
		String stringOpt;
		
		hotelRooms.sort(new RoomComparator());
		PopulateCustomers.doItOn(customers,5);		
		customers.sort(new CustomerComparator());
		
		for (Room room : hotelRooms) {
			room.setAvailability(true);
		}
		
		while (true) {
			System.out.println("Customers: ");
			for (Customer customer : customers) {
				System.out.printf(customer.getId() + " ");
			}
			
			System.out.println("\nRooms: ");
			for (Room room : hotelRooms) {
				System.out.printf(room.getNr() + "(" + room.getCateg() + ") ");
			}
			
			hotelReservations =  hotel.getReservations();
			if (!hotelReservations.isEmpty()) {
				System.out.println("\nReservations: ");
				int i = 0;
				for (Reservation reservation : hotelReservations) {
					System.out.printf(++i + ". Customer: " + reservation.getCustomer().getId() + " -> Room: " + reservation.getRoom().getNr() + "(" + reservation.getRoom().getCateg() + ") -> Price: " + reservationPrices.get(i-1) + " -> Checked in: ");
					if (reservation.getRoom().getCheckedBy() != null) {
						System.out.println("Yes.");
					}
					else {
						System.out.println("No.");
					}
				}
			}
			
			System.out.println("\nOptions:");
			System.out.println("1. Reserve a room");
			System.out.println("2. Check in customer");
			System.out.println("3. Check out customer");
			System.out.printf("Enter option: ");
			
			try {
				opt = keyboard.nextInt();
			}
			catch (Exception e) {
				opt = -1;
			}
			
			switch(opt) {
			case 1:
				ok = false;
				while (!ok) {
					ok = false;
					System.out.printf("\nChoose customer: ");
					try {
						opt = keyboard.nextInt();
					}
					catch (Exception e) {
						System.exit(-1);
					}
					for (Customer customer : customers) {
						if (customer.getId() == opt) {
							ok = true;
							selectedCustomer = customer;
							break;
						}
					}
					if (!ok) {
						System.out.printf("Chosen customer doesn't exist. Choose again.\n");
					}
				}
				
				keyboard.nextLine();
				ok = false;
				while (!ok) {
					System.out.printf("Choose room type: ");
					stringOpt = keyboard.nextLine();
					if (stringOpt.equals("A") || stringOpt.equals("B") || stringOpt.equals("C") || stringOpt.equals("D")) {
						ok = true;
						selectedCategory = RoomCategory.valueOf(stringOpt);
					}
					else {
						System.out.printf("Chosen room type doesn't exist. Choose again.\n");
					}
				}
				
				ok = false;
				while (!ok) {
					System.out.printf("Choose start of period (dd.MM.yyyy): ");
					stringOpt = keyboard.nextLine();
					if (isValidDate(stringOpt)) {
						ok = true;
						try {
							dateStart = format.parse(stringOpt);
						} catch (ParseException e) {
							e.printStackTrace();
						}
					}
					else {
						System.out.printf("\nWrong date format. Retry.");
					}
				}
				
				ok = false;
				while (!ok) {
					System.out.printf("Choose end of period (dd.MM.YYYY): ");
					stringOpt = keyboard.nextLine();
					if (isValidDate(stringOpt)) {
						ok = true;
						try {
							dateEnd = format.parse(stringOpt);
						} catch (ParseException e) {
							e.printStackTrace();
						}
					}
					else {
						System.out.printf("\nWrong date format. Retry.");
					}
				}
				
				long diff = Math.abs(dateEnd.getTime() - dateStart.getTime());
				int days = (int) TimeUnit.DAYS.convert(diff, TimeUnit.MILLISECONDS);
				hotel.makeReservation(selectedCustomer, selectedCategory);
				reservationPrices.add(days * selectedCategory.price());
				break;
			case 2:
				ok = false;
				while (!ok) {
					ok = false;
					System.out.printf("\nChoose customer: ");
					try {
						opt = keyboard.nextInt();
					}
					catch (Exception e) {
						System.exit(-1);
					}
					for (Customer customer : customers) {
						if (customer.getId() == opt) {
							ok = true;
							selectedCustomer = customer;
							break;
						}
					}
					if (!ok) {
						System.out.printf("Chosen customer doesn't exist. Choose again.\n");
					}
				}
				
				for (Reservation reservation : hotelReservations) {
					if (reservation.getCustomer() == selectedCustomer) {
						selectedReservation = reservation;
					}
				}
				
				hotel.makeCheckIn(selectedReservation);
				break;
				
			case 3:
				ok = false;
				while (!ok) {
					ok = false;
					System.out.printf("\nChoose room: ");
					try {
						opt = keyboard.nextInt();
					}
					catch (Exception e) {
						System.exit(-1);
					}
					for (Room room : hotelRooms) {
						if (room.getNr() == opt) {
							ok = true;
							selectedRoom = room;
							break;
						}
					}
					if (!ok) {
						System.out.printf("Chosen customer doesn't exist. Choose again.\n");
					}
				}
				
				
				for (Reservation reservation : hotelReservations) {
					if (reservation.getRoom() == selectedRoom) {
						selectedReservation = reservation;
					}
				}
				hotel.makeCheckOut(selectedRoom);
				int priceToDeleteIndex = hotelReservations.indexOf(selectedReservation);
				hotelReservations.remove(selectedReservation);
				reservationPrices.remove(priceToDeleteIndex);
				break;
			}
			System.out.println();
			System.out.println();
			System.out.println();
			//keyboard.close();
		}
	}

}
