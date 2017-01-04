module stack_test2();	

	reg[11:0] di;
	reg clk, clear, push, pop;

	wire[11:0] do;
	wire fulln, emptyn;

	stack stack_inst(di, do, clk, clear, push, pop, fulln, emptyn);

	initial
	begin
		#0 clk = 0;
		forever #5 clk = ~clk;
	end

	initial
	begin
		#0 clear = 1; push = 0; pop = 0;
		#10 clear = 0;
		#10 di = 12'b0110;
		#10 push = 1;
		#10 push = 0;
		#20 pop = 1;
		#10 pop = 0;
		#10 $finish;
	end
endmodule