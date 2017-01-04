#!/bin/bash
server_pid=99999
calculator_pid=99999
driver_pid=99999

file_towatch="common/updates.txt"
update_every="5s"

function server() {
	. server/venv/bin/activate
	export FLASK_DEBUG=1
	export FLASK_APP=server/server.py
	flask run
}

function calculator() {
	while true; do
		python calculator/calculate.py
		sleep $update_every
	done

	#inotifywait -e close_write,moved_to,create $directory |
	#while read -r directory events filename; do
	#  if [ "$filename" = "$filewatch" ]; then
	#    python calculator/calculate.py
	#  fi
	#done
}

function driver() {
	python driver/driver.py &
}

function exit_handler() {
	kill -SIGKILL $calculator_pid
	kill $server_pid
	kill $driver_pid

	flask_pid=$(pgrep -f "flask")
	kill $flask_pid
	
	driver_py_pid=$(pgrep -f "driver.py")
	kill -SIGINT $driver_py_pid

	sleep_pid=$(pgrep -f "sleep "$update_every)
	kill $sleep_pid

	echo -e "\r\nExitting gracefully..."
	trap - SIGINT
}

echo -e "\r\n"
calculator &
calculator_pid=$!
server &
server_pid=$!
driver &
driver_pid=$!

trap exit_handler SIGINT
