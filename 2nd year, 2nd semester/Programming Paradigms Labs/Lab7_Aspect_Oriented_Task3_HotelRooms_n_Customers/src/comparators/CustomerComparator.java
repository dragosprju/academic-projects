package comparators;

import java.util.Comparator;
import hotel.Customer;

public class CustomerComparator implements Comparator<Customer> {
	
	@Override
	public int compare(Customer a, Customer b) {
		int aID = a.getId();
		int bID = b.getId();
		
	    if (aID < bID) {
	        return -1;
	    }
	    else if (aID == bID) {
	        return 0;
	    }
	    else {
	        return 1;
	    }
	  }
	}
