`include "DPRAM.v"
`include "FIFO.v"

`include "Transaction.sv"
`include "Generator.sv"
`include "Interfaces.sv"
`include "Scoreboard.sv"
`include "Driver.sv"
`include "Environment.sv"

program automatic test;
	Environment env;
	initial begin
		env = new();
		env.build();
		env.run();
		env.wrap_up;
	end
endprogram