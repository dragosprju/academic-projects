package foc;

import spatiu.Plansa;

// Clasa 'Adaptor'
// Clasa 'Target' e IFoc
// Clasa 'Adaptee' e Plansa
public class FocBila implements IFoc {
	
	private Plansa plansa;
	private boolean ptJucator;
	
	public FocBila(Plansa plansa, boolean eJucator) {
		this.plansa = plansa;
		ptJucator = eJucator;
	}

	@Override
	public void Trage(final boolean distruge) {
		if (!ptJucator) {
			Thread thread = new Thread() {
				
				public void run() {
					plansa.blowUpPlayer(distruge);
				}
			};
			thread.start();
		}
		else if (ptJucator) {
			
			Thread thread = new Thread() {
				
				public void run() {
					plansa.blowUpEnemy(distruge);
				}
			};
			thread.start();
			
		}
	}
	
	
}
