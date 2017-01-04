package comparators;

import hotel.Room;

import java.util.Comparator;

public class RoomComparator implements Comparator<Room> {
	@Override
	public int compare(Room a, Room b) {
		int aNr = a.getNr();
		int bNr = b.getNr();
		if (aNr < bNr) {
			return -1;
		}
		else if (aNr == bNr) {
			return 0;
		}
		else 
		{
			return 1;
		}
	}
}
