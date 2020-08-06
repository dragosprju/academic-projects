`define WIDTH 8
`define NADDR 8
`define NADDRB 3

interface interf_fifo;
	wire[`WIDTH-1:0] di;
	wire[`WIDTH-1:0] do;

	wire rst, clk, push, pop
	wire full, empty;

	modport asDUT (
		input di,
		output do,

		input push,
		input pop,
		output full,
		output empty,

		input clk,
		input rst
	);

	//modport asMonitor (
	//	output di,
	//	output do,

	//	output push,
	//	output pop,
	//	output full,
	//	output empty,

	//	output rst,
	//	output clk
	//);

endinterface