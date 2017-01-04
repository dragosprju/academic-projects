from statemachine import StateMachine

def start(cargo):
	return cargo[1][0], cargo

def nickel(cargo): 
	if cargo[0] < 20:
		cargo[0] += 5
	else:
		cargo[0] = 15
	print "Deposit: %d" % (cargo[0])
	del cargo[1][0]
	if len(cargo[1]) == 0:
		return end, cargo[0]
	else:
		return cargo[1][0], cargo

def dime(cargo):
	if cargo[0] < 20:
		cargo[0] += 10
	else:
		cargo[0] = 10
	print "Deposit: %d" % (cargo[0])
	del cargo[1][0]
	if len(cargo[1]) == 0:
		return end, cargo[0]
	else:
		return cargo[1][0], cargo

def end(cargo):
	print "Final deposit: %d" % (cargo)
	return None, None

if __name__=='__main__':
    m = StateMachine()
    m.add_state(start)
    m.add_state(nickel)
    m.add_state(dime)
    m.add_state(end, end_state=1)
    m.set_start(start)
    m.run([0, [nickel, dime, nickel, dime]])