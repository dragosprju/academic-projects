package masterslave;

public class Slave extends Thread {
	Integer[] rezultat;
	int start;
	int stop;
	
	public Slave(Integer[] rezultat, int lungSubinterval, int indexSubinterval) {
		this.rezultat = rezultat;
		start = lungSubinterval*(indexSubinterval-1);
		stop = start + lungSubinterval;
	}
	
	public void run() {
		for (int i=start; i<stop; i++) {
			int s = 0;
			for (int j=1; j<=(i+1); j++)
				s += j;
			rezultat[i] = s;
		}
	}
}
