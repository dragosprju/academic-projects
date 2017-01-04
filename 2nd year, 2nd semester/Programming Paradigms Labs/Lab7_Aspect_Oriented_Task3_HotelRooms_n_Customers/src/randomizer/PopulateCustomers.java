package randomizer;

import java.util.List;
import java.util.Random;

import hotel.Customer;

public class PopulateCustomers {
	private static Random randomizer = new Random();
	
	public static void doItOn(List<Customer> customers, int numberOfCustomers) {
		int randCustomerID;
		Customer customerToAdd;
		boolean ok;
		for (int i=0; i<numberOfCustomers; i++) {
			randCustomerID = randomizer.nextInt(100) + 1;
			customerToAdd = new Customer(randCustomerID);
			ok = true;
			for (Customer customer : customers) {
				if (customer.getId() == randCustomerID) {
					ok = false;
				}
			}
			if (!ok) {
				i--;
				continue;
			}
			customers.add(customerToAdd);
		}
	}
}
