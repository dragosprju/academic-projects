from bluetooth import *
from pprint import *
import time
import signal
import sys

name = "stef-HP-EliteBook-8560p"
port = 1 
mac_address = "20:16:01:26:56:21"
update_time_every = 900
curr_dir = os.path.dirname(os.path.abspath(__file__))
settings_file = curr_dir + "/../common/settings.txt"
updates_file = curr_dir + "/../common/updates.txt"

sock = None

threshold = 0.15
connected = "NO"
bent = "NO"

busy_count = 5000
busy_count_allow = 5000

def exit_handler(signal, frame):
	global sock

	#print ""	
	append_updates_file("disconnected", time.strftime("%Y-%m-%d %H:%M:%S"))
	rewrite_settings_file("connected", "no")
	sock.close()
	#print "Disconnected & shutting down."
	sys.exit(0)
	

def find_target_by_name():
	while True:
		print("Searching for target '%s'..." % name)

		nearby_devices = discover_devices(lookup_names=True)

		for addr, nname in nearby_devices:
			print("- %s (%s)" % (nname, addr))
			if name == nname:
				print "Found!"
				return addr

		if not mac_address:
			print "Not found. Retrying..."

def read_char():
	try:
		data = sock.recv(1)
		if len(data) == 0:
			print "Received empty data!"
			return ""
		else:
			#sys.stdout.write('%s' % data)
			return data
	except IOError:
		#print "Stumbled upon IOError!"
		return ""

def check_time(got_time):
	got_time = got_time[:-3]
	print "[" + got_time + " == " + time.strftime("%Y-%m-%d %H:%M") + "?",
	if got_time != time.strftime("%Y-%m-%d %H:%M"):
		print "N => 's']"
		send_string("s")
		return False
	else:
		print "Y]"
		return True

# This shouldn't exist and better error corrections should happen
# when using flash memory usage to remember updates as to not
# consistently send them
def pick_own_time_if_incorrect(is_time_correct, t):
	if not is_time_correct:
		print "Used my own time!"
		return time.strftime("%Y-%m-%d %H:%M")
	else:
		return t
		

def send_string(string):
	print ">", string
	sock.send(string)	

def is_newline(c):
	if c == "\n":
		return True
	else:
		return False

def rewrite_settings_file(key, value):
	global threshold
	global connected
	global bent

	print "[" + key.upper() + "=" + str(value) + " to '" + settings_file + "']"
	
	if key.lower() == "threshold":
		threshold = value

	if key.lower() == "connected":
		connected = value.upper()

	if key.lower() == "bent":
		bent = value.upper()

	with open(settings_file, "w") as setfile:
		setfile.truncate()
		setfile.write("CONNECTED=%s\r\n" % connected)
		setfile.write("THRESHOLD=%1.2f\r\n" % threshold)
		setfile.write("BENT=%s\r\n" % bent)
	

def append_updates_file(key, time):
	line_to_write = key.upper() + "," + time
	print "[" + line_to_write + " to '" + updates_file + "']"
	
	with open(updates_file, "a") as updfile:
		updfile.write(line_to_write + "\r\n")

def main():
	global mac_address
	global sock

	if not mac_address:
		mac_address = find_target_by_name()
	else:
		pass

	print "Connecting to device via bluetooth..."
	sock=None
	
	connected = False
	do_print = True
	while not connected:
		try:
			sock = BluetoothSocket(RFCOMM)
			sock.connect((mac_address, port))
			connected = True
		except Exception as e:
			if "File descriptor" in str(e):
				do_print = False				

			if do_print:
				print "Connection via bluetooth failed (%s). Retrying..." % str(e)
			sock.close()
			
	sock.settimeout(1)
	print "Connected to device via bluetooth."

	append_updates_file("connected", time.strftime("%Y-%m-%d %H:%M:%S"))
	rewrite_settings_file("connected", "yes")

	state = 0
	line = ""

	time_counter = 0
	first_run_commands = ["s", "h"]
	skip_a_tick = False

	while True:		
		char = read_char()
		
		line += char
		if is_newline(char):
			print line,

			if "Awaiting time update" in line:
				send_string(time.strftime("%Y-%m-%d %H:%M:%S"))
			elif "Threshold value was manually set to" in line:
				rewrite_settings_file("threshold", float(line[-7:-3]))
			elif "Threshold value is" in line:
				rewrite_settings_file("threshold", float(line[-7:-3]))
			elif "Detected bent as of" in line:
				extracted_time = line[-22:-3]
				is_time_correct = check_time(extracted_time)
				time_to_append = pick_own_time_if_incorrect(is_time_correct, extracted_time)
				append_updates_file("bent", time_to_append)
				rewrite_settings_file("bent", "yes")
			elif "Detected straight as of" in line:
				extracted_time = line[-22:-3]
				is_time_correct = check_time(extracted_time)
				time_to_append = pick_own_time_if_incorrect(is_time_correct, extracted_time)
				append_updates_file("straight", time_to_append)
				rewrite_settings_file("bent", "no")
			elif "Time updated to" in line:
				check_time(line[-22:-3])
			line = ""		

		if len(first_run_commands) > 0: #and not skip_a_tick:
			send_string(first_run_commands.pop())
		#elif skip_a_tick:
		#	skip_a_tick = 0

		# Updating time
		if time_counter == update_time_every:
			send_string("s")
			time_counter = 0
		else:
			time_counter += 1

if __name__ == "__main__":
	signal.signal(signal.SIGINT, exit_handler)
	main()





