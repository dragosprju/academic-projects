`define WIDTH 8
`define NADDR 8
`define NADDRB 3

class Scoreboard;
	fifo fifo1;
	mailbox fifo2;
	integer size, isFull, isEmpty;

	logic[`WIDTH-1:data] last_data;

	function new(input fifo fifo1);
		this.fifo1 = fifo1
		fifo2 = new()
		size = 0
	endfunction

	task step(Transaction tr);
		if (tr.rst) begin
			fifo2 = new()
			size = 0
		end
		else if (tr.push && isFull == 0) begin
			fifo2.put(tr.data)
			size++;
		end
		else if (tr.pop && isEmpty == 0) begin
			fifo2.get(this.last_data)
			size--;
		end

		if (size == 0) begin
			isEmpty = 1
		end
		else begin
			isEmpty = 0
		end

		if (size == (`WIDTH - 1)) begin
			isFull = 1
		end
		else begin
			isFull = 0
		end

		check(tr)
		 
	endtask

	task check(Transaction tr);
		error = 0

		if (isFull == 1 && fifo1.full == 0) begin
			$display("%dns: REAL FIFO did not detect full! Current size occupied in FAKE FIFO: %d\n", $time, size)
			error = 1
		end

		if (isEmpty == 1 && fifo1.empty == 0) begin
			$display("%dns: REAL FIFO did not detect empty! Current size occupied in FAKE FIFO: %d\n", $time, size)
			error = 1
		end

		if (isFull == 0 && fifo1.full == 1) begin
			$display("%dns: REAL FIFO considers queue full! Current size occupied in FAKE FIFO: %d\n", $time, size)
			error = 1
		end

		if (isEmpty == 0 && fifo1.empty == 1) begin
			$display("%dns: REAL FIFO considers queue empty! Current size occupied in FAKE FIFO: %d\n", $time, size)
			error = 1
		end

		if (isEmpty == 0 && tr.pop == 1 && this.last_data != fifo1.do) begin
			$display("%dns: REAL FIFO pop'd data different from FAKE FIFO: %d\n", $time, size)
			error = 1
		end

		if (error == 0) begin
			$display("%dns: REAL FIFO and FAKE FIFO are a match. Everything working correctly!\n", $time)
			$display("        Size: %d. Push: %d. Pop: %d. Full: %d. Empty: %d. Data pushing: %x. Data popped: %x." )
		end
	endtask
endclass