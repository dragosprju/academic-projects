module reg12b(di, do, clk, pl, dec, zero);
	input[11:0] di;
	output[11:0] do;

	input clk, pl, dec;
	output zero;
	
	reg[11:0] do;

	assign zero = (do == 12'b0) ? 1 : 0;

	always@(posedge clk)
	begin
		if (pl)
			do = di;
		else if (dec)
			do = do - 1;
	end
endmodule
