package test;

import javax.ejb.Remote;

@Remote
public interface HelloBeanRemote {
	public String sayHello();
}
