
public class Main {

	public static void main(String[] args) {
		Stack<Complex> s = new Stack<Complex>(2);
		s.push(new Complex(3,3));
		s.push(new Complex(2,4));
		System.out.println("a = " + s.pop().ToString());
		System.out.println("b = " + s.pop().ToString());
	}
}
