module am2910(di, do, instr, ccn, rldn, ci, plpc, clk, fulln, emptyn, pln, mapn, vectn);
	
	input[11:0] di;
	input[3:0] instr;

	output[11:0] do;

	input ccn, rldn, ci, clk, plpc;
	output fulln, emptyn, pln, mapn, vectn;

	wire plreg, zeror, respc, plpc,
		push, pop, clear;
	wire[1:0] selmux;
	wire[11:0] D, R, F, uPC;
	wire[11:0] do, mux_uPC, uPC_stack;

	reg12b reg_inst(di, R, clk, plreg, dec, zeror);
	mux12bx4 mux_inst(D, R, F, uPC, selmux, mux_uPC);
	micropc2 micropc_inst(mux_uPC, uPC, uPC_stack, clk, respc, plpc, ci);
	stack2 stack_inst(uPC_stack, F, clk, clear, push, pop, fulln, emptyn);
	instrdec instrdec_inst(instr, ccn, zeror, 
		{plrc, dec, clear, push, pop, respc, selmux[1], selmux[0], pln, mapn, vectn});

	assign plreg = ~rldn | plrc;
	assign do = mux_uPC;
	assign ci = 1;
	assign D = di;
endmodule

