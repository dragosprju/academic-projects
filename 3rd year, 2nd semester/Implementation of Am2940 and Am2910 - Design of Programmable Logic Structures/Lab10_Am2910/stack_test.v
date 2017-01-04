module stack_test();	

	reg[11:0] di;
	reg clk, clear, clear2, push, pop;

	wire[11:0] do, do2;
	wire fulln, emptyn;

	stack stack_inst(di, do, clk, clear, push, pop, fulln, emptyn);
	stack2 stack2_inst(di, do2, clk, clear, push, pop, fulln2, emptyn2);

	initial
	begin
		#0 clk = 0;
		forever #5 clk = ~clk;
	end

	initial
	begin
		// 5
		#0 clear = 1; push = 0; pop = 0; di = 12'b0000; // 000

		// 15
		#10 clear = 0; push = 1; // 000

		// 25
		#10 di = 12'b0001; // 001

		// 35
		#10 di = 12'b0010; // 010

		// 45
		#10 di = 12'b0011; // 011

		// 55
		#10 di = 12'b0100; // 100

		// 65
		#10 di = 12'b0101; // 101

		// 75
		#10 di = 12'b0110; // 110

		// 85
		#10 di = 12'b0111; // 111

		// 95
		#10 di = 12'b1000; // 111 x2

		// 115
		#20 push = 0;

		// 135
		#20 pop = 1;
		#100 $finish;
	end
endmodule