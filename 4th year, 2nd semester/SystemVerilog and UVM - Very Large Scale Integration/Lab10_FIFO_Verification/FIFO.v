`define WIDTH 8
`define NADDR 8
`define NADDRB 3

module fifo(di, do, push, pop, full, empty, rst, clk);
	input clk, push, pop, rst;
	input[`WIDTH-1:0] di;

	output[`WIDTH-1:0] do;
	output full, empty;

	wire[`WIDTH-1:0] di, do;
	wire[`NADDRB-1:0] addra, addrb;
	wire full, empty, we_mem;

	reg[`NADDRB-1:0] rptr, wptr;
	reg do_input; //, first_run;

	assign addra = wptr;
	assign addrb = rptr;

	assign we_mem = (do_input);

	assign empty = (rptr === wptr); // | ((rptr + 1'b1 === wptr) & first_run));
	assign full = (wptr + 1'b1) === rptr;

	dpram_1w1r dpram_inst(clk, we_mem, addra, di, addrb, do);

	always@(posedge clk)
	begin
		// RESET
		if (rst) begin
			rptr = `NADDRB'b0;
			wptr = `NADDRB'b0;
			do_input = 1'b0;
			//first_run = 1'b1;
		end
		else begin
			if (push & !full) begin
				wptr = wptr + 1'b1;
				do_input = 1'b1;
				//first_run = 1'b0;
			end
			else begin
				do_input = 1'b0;
			end

			if (pop & !empty) begin
				rptr = rptr + 1'b1;
			end
		end
	end
endmodule