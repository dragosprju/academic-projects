`define WIDTH 8
`define NADDR 8
`define NADDRB 3

module dpram_1w1r(clk, we, addra, di, addrb, do);
	input clk, we;
	input[`NADDRB-1:0] addra, addrb;
	input[`WIDTH-1:0] di;

	output [`WIDTH-1:0] do;

	reg[`WIDTH-1:0] do;
	reg[`WIDTH-1:0] mem[0:`NADDR-1];

	always@(posedge clk)
	begin
		do <= mem[addrb];
		if (we)
			mem[addra] <= di;
	end
endmodule