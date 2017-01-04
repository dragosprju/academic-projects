module micropc(di, do, do_stack, clk, respc, plpc, ci);

	input[11:0] di;
	input clk, respc, plpc, ci;

	output[11:0] do;
  output[11:0] do_stack;

	reg[11:0] do;

  assign do_stack = ci ? do : do + 1;

	always@(posedge clk)
    casex({respc, plpc})
      2'b1x: do <= 0;
      2'b01: do <= di + ci;
      2'b00: do <= do + ci;
    endcase
endmodule





