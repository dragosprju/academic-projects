from datetime import datetime, timedelta
from pprint import *
import time
import os

curr_dir = os.path.dirname(os.path.abspath(__file__))
updates_file = curr_dir + "/../common/updates.txt"
calculations_file = curr_dir + "/../common/calculations.txt"
frmt = "%Y-%m-%d %H:%M:%S"
print_calcs = False
print_days_ = False

def get_time_delta(previous_time, current_time):
	previous_time = previous_time.strip()
	current_time = current_time.strip()

	if len(previous_time) < 19:
		previous_time += ":00"

	if len(current_time) < 19:
		current_time += ":00"

	prev_time = datetime.strptime(previous_time, frmt)
	curr_time = datetime.strptime(current_time, frmt)

	tdelta = curr_time - prev_time
	return tdelta

def did_days_change(previous_time, current_time):
	if previous_time[0:10] != current_time[0:10]:
		return True
	else:
		return False

def add_to_day_calculation(calculations, state, time_start, time_end):
	time_start = time_start.strip()
	time_end = time_end.strip()

	if state not in calculations:
		calculations[state] = get_time_delta(time_start, time_end)
	else:
		calculations[state] += get_time_delta(time_start, time_end)

	if print_calcs: print "[" + state + ": " + time_start + " -> " + time_end + " = " + str(calculations[state]) + "]"

def next_day_str(curr_time_str):
	curr_time_str = curr_time_str.strip()

	curr_time = datetime.strptime(curr_time_str, frmt)
	curr_time += timedelta(days=1)
	
	return str(curr_time)

def finish_day(days, calculations, state, prev_time, curr_time = None):
	last_day = False

	if not curr_time:
		last_day = True
		curr_time = next_day_str(prev_time)

	# Taking only the date and putting midnight time
	end_day_time = curr_time[0:10] + " 00:00:00" 
	
	# Ending the day with calculation of previous state up until midnight
	add_to_day_calculation(calculations, state, prev_time, end_day_time) 

	if "CONNECTED" in calculations:
		if "STRAIGHT" in calculations:
			calculations["STRAIGHT"] += calculations["CONNECTED"]
		else:
			calculations["STRAIGHT"] = calculations["CONNECTED"]

		del calculations["CONNECTED"]
	days[prev_time[0:10]] = calculations.copy()

	# Setting up so current state gets calculated starting from midnight for the current day
	calculations.clear()	
	prev_time = end_day_time

	return prev_time

def print_days(days):
	if not print_days_: return
	for day in days:
		print "--"
		print day + ":"
		for calc in days[day]:
			print calc + ": " + str(days[day][calc])

def write_days(days, write_floats_directly = False):
	with open(calculations_file, "w") as myfile:
		myfile.truncate()

		for day in days:
			for state in days[day]:
				to_write = None				
				if write_floats_directly:
					to_write = days[day][state].total_seconds() / timedelta(hours=24).total_seconds()
				else:
					to_write = str(days[day][state])
				myfile.write(day + "," + state.upper() + "," + to_write + "\r\n")


with open(updates_file, "r") as f:
	print "Calculating..."
	prev_time = None
	curr_time = None

	prev_state = ""
	curr_state = ""

	calculations = {}
	days = {}

	for line in f:
		if not line.strip():
			continue
		line_sp = line.split(",")

		prev_time = curr_time
		prev_state = curr_state

		curr_time = line_sp[1]
		curr_state = line_sp[0]
		
		# We are at the start of the file and the device connected in the middle of the day
		# From the start of the day up until that moment we must mark it DISCONNECTED
		if prev_time == None:
			#if curr_state != "CONNECTED":
				#print "Warning: First line of file is not supposed to be anything else than 'CONNECTED'. Found '%s'." % curr_state

			prev_time = curr_time[0:10] + " 00:00:00"
			prev_state = "DISCONNECTED"
	
		if did_days_change(prev_time, curr_time):
			print_days(days)
			prev_time = finish_day(days, calculations, prev_state, prev_time, curr_time)

		add_to_day_calculation(calculations, prev_state, prev_time, curr_time)

	if curr_state != "DISCONNECTED": # The device is currently running
		add_to_day_calculation(calculations, curr_state, curr_time, time.strftime(frmt))
		finish_day(days, calculations, "DISCONNECTED", time.strftime(frmt))
	else:
		#prev_state = curr_state
		#prev_time = curr_time

		#curr_state = "DISCONNECTED"
		#curr_time = time.strftime(frmt)
		#finish_day(days, calculations, curr_state, curr_time)
		#finish_day(days, calculations, curr_state, time.strftime(frmt))

		finish_day(days, calculations, curr_state, curr_time)

	print_days(days)
	write_days(days)
				
	
