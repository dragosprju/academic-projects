package test;

import java.util.HashMap;
import java.util.Map;

public aspect OptimizeFactorialAspect {
	pointcut factorialOperation(int n) : call (long *.factorial(int)) && args(n);
	pointcut topLevelFactorialOperation(int n) : factorialOperation(n) && !cflowbelow(factorialOperation(int));
	
	private Map<Integer, Long> factorialCache = new HashMap<Integer, Long>();
	
	before (int n) : topLevelFactorialOperation(n) {
		System.out.println("Seeking factorial for " + n);
	}
	
	long around(int n) : factorialOperation(n) {
		Object cachedValue = factorialCache.get(n);
		if (cachedValue != null) {
			System.out.println("Found cached value for " + n + ": " + cachedValue);
			return ((Long)cachedValue).longValue();
		}
		return proceed(n);
	}
	
	after(int n) returning(long result) : topLevelFactorialOperation(n) {
		factorialCache.put(n, result);
	}
}
