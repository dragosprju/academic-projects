module sprom_test();	

	reg[11:0] di;
	reg[2:0] address;
	reg clk, we;

	wire[11:0] do;

	sprom8x12 sprom_inst(di, do, clk, address, we);

	initial
	begin
		#0 clk = 0;
		forever #5 clk = ~clk;
	end

	initial
	begin
		#0 di = 12'b0000; address = 3'b000; we = 1;
		#10 di = 12'b0001; address = 3'b001;
		#10 di = 12'b0010; address = 3'b010;
		#10 di = 12'b0011; address = 3'b011;
		#10 di = 12'b0100; address = 3'b100;
		#10 di = 12'b0101; address = 3'b101;
		#10 di = 12'b0110; address = 3'b110;
		#10 di = 12'b0111; address = 3'b111;
		#30 we = 0;
		#10 address = 3'b000;
		#10 address = 3'b001;
		#10 address = 3'b010;
		#10 address = 3'b011;
		#10 address = 3'b100;
		#10 address = 3'b101;
		#10 address = 3'b110;
		#10 address = 3'b111;
		#10 $finish;
	end
endmodule