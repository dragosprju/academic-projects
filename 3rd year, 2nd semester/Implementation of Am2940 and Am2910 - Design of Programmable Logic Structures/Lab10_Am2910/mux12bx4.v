module mux12bx4(A, B, C, D, sel, out);
	input[11:0] A, B, C, D;
	input[1:0] sel;

	output[11:0] out;
	reg[11:0] out;

	always@(A or B or C or D)
	case(sel)
		2'b00: out = A;
		2'b01: out = B;
		2'b10: out = C;
		2'b11: out = D;
	endcase
endmodule