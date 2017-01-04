from datetime import datetime, timedelta
from pprint import *
import os

curr_dir = os.path.dirname(os.path.abspath(__file__))
calculations_file = curr_dir + "/../common/calculations.txt"
settings_file = curr_dir + "/../common/settings.txt"
how_far_back = 7

threshold_default = 0.15

intervals = (
    ('weeks', 604800),  # 60 * 60 * 24 * 7
    ('days', 86400),    # 60 * 60 * 24
    ('hours', 3600),    # 60 * 60
    ('minutes', 60),
    ('seconds', 1),
    )

def display_time(seconds, granularity=2):
    result = []

    for name, count in intervals:
        value = seconds // count
        if value:
            seconds -= value * count
            if value == 1:
                name = name.rstrip('s')
            result.append("{} {}".format(value, name))
    return ', '.join(result[:granularity])

def get_no_hours(timedelta_str):
	tm = datetime.strptime(timedelta_str, "%H:%M:%S")
	delta = timedelta(hours=tm.hour, minutes=tm.minute, seconds=tm.second)

	calc = delta.total_seconds() / timedelta(hours=1).total_seconds()
	return calc

def get_time_delta(timedelta_str):
	tm = datetime.strptime(timedelta_str, "%H:%M:%S")
	delta = timedelta(hours=tm.hour, minutes=tm.minute, seconds=tm.second)

	return delta

def calc_graph_values():
	day_labels = [(datetime.now() - timedelta(days=x)).strftime("%a, %e %b") for x in reversed(range(-1,how_far_back))]
	values = [
		[0.0 for i in range(how_far_back + 1)], 
		[0.0 for i in range(how_far_back + 1)], 
		[0.0 for i in range(how_far_back + 1)]
	]

	with open(calculations_file,"r") as myfile:

		for line in myfile:
			line = line.strip()
			if line:
				line_sp = line.split(",")
			else:
				continue

			day_label_from_line = datetime.strptime(line_sp[0], "%Y-%m-%d").strftime("%a, %e %b")
			state_from_line = line_sp[1]
			timedelta_from_line = line_sp[2]

			day_label_index = day_labels.index(day_label_from_line)
			if state_from_line == "DISCONNECTED":
				#state_index = 0
				continue
			elif state_from_line == "BENT":
				state_index = 1
			elif state_from_line == "STRAIGHT":
				state_index = 0

			values[state_index][day_label_index]= round(get_no_hours(timedelta_from_line), 3)

	return (day_labels, values)

def get_settings():
	connected = False
	threshold = threshold_default
	bent = False

	with open(settings_file, "r") as myfile:

		for line in myfile:
			line = line.strip()
			line_sp = line.split("=")

			if "CONNECTED" in line:
				if line_sp[1] == "YES":
					connected = True
				else:
					connected = False

			if "THRESHOLD" in line:
				threshold = float(line_sp[1])

			if "BENT" in line:
				if line_sp[1] == "YES":
					bent = True
				else:
					bent = False

	return [connected, threshold, bent]

if __name__ == "__main__":
	values = calc_graph_values()
	get_settings()

	print connected
	print threshold
	pprint(values)
