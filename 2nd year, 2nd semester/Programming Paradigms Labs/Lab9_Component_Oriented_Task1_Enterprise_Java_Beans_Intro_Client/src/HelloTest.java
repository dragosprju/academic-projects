import java.util.Properties;

import javax.naming.Context;
import javax.naming.InitialContext;
import javax.naming.NamingException;

import test.HelloBeanRemote;

public class HelloTest {

	public static void main(String[] args) throws NamingException {	        
		Properties jndiProps = new Properties();
		jndiProps.put("java.naming.factory.initial", "com.sun.enterprise.naming.impl.SerialInitContextFactory");
		jndiProps.put("java.naming.factory.url.pkgs", "com.sun.enterprise.naming");
		jndiProps.put("java.naming.factory.state", "com.sun.corba.ee.impl.presentation.rmi.JNDIStateFactoryImpl");
		jndiProps.setProperty("org.omg.CORBA.ORBInitialHost", "127.0.0.1");
		jndiProps.setProperty("org.omg.CORBA.ORBInitialPort", "3700");
		Context ctx = new InitialContext(jndiProps);
		HelloBeanRemote bean = (HelloBeanRemote) ctx.lookup("java:global/L09P01/HelloBean");
		System.out.println(bean.sayHello());
	}
}
