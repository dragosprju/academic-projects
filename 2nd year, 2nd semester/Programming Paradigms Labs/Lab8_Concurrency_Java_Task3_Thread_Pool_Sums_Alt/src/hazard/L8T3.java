package hazard;

import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.BlockingQueue;

public class L8T3 {

	public static void main(String[] args) {
		final int numFibonacci = 50;
		BlockingQueue<Integer> numereDeProcesat = new ArrayBlockingQueue<Integer>(numFibonacci);
		final Long[] rezultate = new Long[numFibonacci];		
		FibonacciThreadPool threadPool = new FibonacciThreadPool(3, numereDeProcesat, rezultate);
		
		for (int i=1; i<=50; i++) {
			numereDeProcesat.add(i);
		}
		
		threadPool.incepe();
	}

}
