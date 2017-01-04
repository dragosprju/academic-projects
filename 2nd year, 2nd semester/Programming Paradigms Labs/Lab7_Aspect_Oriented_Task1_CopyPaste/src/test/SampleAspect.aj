package test;

public aspect SampleAspect {
	pointcut messageSending() : call(* Sample.sendMessage(..));
	
	before() : messageSending() {
		System.out.println("A message is about to be send");
	}
}
