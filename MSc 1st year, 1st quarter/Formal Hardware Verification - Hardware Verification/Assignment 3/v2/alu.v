
/**************/
/* Exercise 1 */
/**************/

module half_adder(a, b, s, c);

	input a, b;
	output s, c;

	assign s = a ^ b;
	assign c = a & b;

endmodule


module full_adder(a, b, c_in, s, c_out);

	input a, b, c_in;
	output s, c_out;

	wire ha1c, ha1s, ha2c, c_out;

	half_adder ha1(a, b, ha1s, ha1c);
	half_adder ha2(c_in, ha1s, s, ha2c);

	assign c_out = ha1c | ha2c;

endmodule


/**************/
/* Exercise 2 */
/**************/

module rca8(a, b, c0, s);

	input[7:0] a, b;
	output[8:0] s;
	input c0;

	wire[7:0] a, b;
	wire[6:0] c;
	wire[8:0] s;

	full_adder fa1(a[0], b[0], c0, s[0], c[0]);
	full_adder fa2(a[1], b[1], c[0], s[1], c[1]);
	full_adder fa3(a[2], b[2], c[1], s[2], c[2]);
	full_adder fa4(a[3], b[3], c[2], s[3], c[3]);
	full_adder fa5(a[4], b[4], c[3], s[4], c[4]);
	full_adder fa6(a[5], b[5], c[4], s[5], c[5]);
	full_adder fa7(a[6], b[6], c[5], s[6], c[6]);
	full_adder fa8(a[7], b[7], c[6], s[7], s[8]);

endmodule

// Some testing helps when you make simple mistakes
/*
module test_fa;
	
	reg a, b, c_in;
	wire s, c_out;

	full_adder fa1(a, b, c_in, s, c_out);

	initial
	begin
	    $dumpfile("test.vcd");
	    $dumpvars(0, test_fa);
	    
	end

	initial begin
		#0 a=1; b=1; c_in=0;
		#100 $finish;
	end

endmodule


module test_rca8;
	
	reg[7:0] a, b;
	reg c0;
	wire[8:0] s;

	rca8 rca8_inst(a, b, c0, s);

	initial
	begin
	    $dumpfile("test.vcd");
	    $dumpvars(0, test_rca8);
	end

	initial begin
		#0 a=8'h01; b=8'h01; c0=0;
		#100 $finish;
	end

endmodule
*/

/*
// This test 'top' is in a more complete version below
module top;
	wire[7:0] a, b;
	wire c0;
	wire[8:0] s;

	rca8 rca8_inst(a, b, c0, s);

	rca8_assert: assert property(a + b + c0 == s);

endmodule
*/


/**************/
/* Exercise 3 */
/**************/

module mux2(in0, in1, sel, out);

	input in0, in1, sel;
	output out;

	assign out = sel ? in1 : in0;

endmodule

module gatemux(k, i, j, aX_in, bX_in, s_in, bX_out, rX_out);

	input k, i, j;
	input aX_in, bX_in, s_in;
	output bX_out, rX_out;

	wire mux2_1_out, mux2_2_out;

	assign bX_out = !j ^ !(i & bX_in);
	assign mux2_1_in0 = aX_in & bX_in;
	assign mux2_1_in1 = aX_in | bX_in;

	assign mux2_2_in1 = aX_in ^ bX_in;

	mux2 mux2_1(mux2_1_in0, mux2_1_in1, i, mux2_1_out);
	mux2 mux2_2(mux2_1_out, mux2_2_in1, j, mux2_2_out);
	mux2 mux2_3(mux2_2_out, s_in, k, rX_out);

endmodule

module alu(a, b, r, op, C, Z, N, V);

	input[3:0] op;
	input[7:0] a, b;
	output[7:0] r;

	output C, Z, N, V;

	wire[7:0] a, b, b_for_rca, r;
	wire[8:0] s;
	wire k, i, j, c_in, C, Z, N, V;

	assign k = op[3];
	assign i = op[2];
	assign j = op[1];
	assign c_in = op[0];

	rca8 rca8_inst(a, b_for_rca, c_in, s);

	gatemux gatemux0(k, i, j, a[0], b[0], s[0], b_for_rca[0], r[0]);
	gatemux gatemux1(k, i, j, a[1], b[1], s[1], b_for_rca[1], r[1]);
	gatemux gatemux2(k, i, j, a[2], b[2], s[2], b_for_rca[2], r[2]);
	gatemux gatemux3(k, i, j, a[3], b[3], s[3], b_for_rca[3], r[3]);
	gatemux gatemux4(k, i, j, a[4], b[4], s[4], b_for_rca[4], r[4]);
	gatemux gatemux5(k, i, j, a[5], b[5], s[5], b_for_rca[5], r[5]);
	gatemux gatemux6(k, i, j, a[6], b[6], s[6], b_for_rca[6], r[6]);
	gatemux gatemux7(k, i, j, a[7], b[7], s[7], b_for_rca[7], r[7]);

	assign C = s[8];
	assign Z = (s == 0);
	assign V = (a[7] & b_for_rca[7] & !s[7]) + (!a[7] & !(b_for_rca[7]) & s[7]);
	assign N = s[7];

endmodule

module top;

	wire[7:0] a, b, r;
	wire[8:0] s;
	wire[3:0] op;
	wire c0, C, Z, N, V;

	rca8 rca8_inst(a, b, c0, s);
	alu alu_inst(a, b, r, op, C, Z, N, V);

	wire[8:0] A, B, Bneg; // for verification purposes only;
	assign A = {1'b0, a};
	assign B = {1'b0, b};
	assign Bneg = {1'b0, ~b};

	rca8_general: assert property(A + B + c0 == s);

	alu_op_1000: assert property((op == 4'b1000) -> ({C, r} == A));
	alu_op_1001: assert property((op == 4'b1001) -> ({C, r} == A + 9'h001));
	alu_op_1010_a_not_00: assert property(((op == 4'b1010) & (a != 8'h00)) -> ((r == (a - 8'h01)) & (C == 1'b1))); // a - 1 means actually a + FF which triggers carry
	alu_op_1010_a_00: 	  assert property(((op == 4'b1010) & (a == 8'h00)) -> ((r == (a - 8'h01)) & (C == 1'b0))); // a + FF doesn't trigger carry only when a = 0
	alu_op_1100: assert property((op == 4'b1100) -> ({C, r} == (A + B)));
	alu_op_1101: assert property((op == 4'b1101) -> ({C, r} == (A + B + 9'h001)));
	alu_op_1110: assert property((op == 4'b1110) -> ({C, r} == (A + Bneg)));
	alu_op_1111: assert property((op == 4'b1111) -> ({C, r} == (A + (Bneg + 9'h001)))); // 2's complement; -B is actually neg(B) + 1
	alu_op_0000: assert property((op == 4'b0000) -> (r == (a & b))); // we don't care about C here, 
																	 // it gets toggled depending on the sumation of 'a' and 'b',
																	 // which we don't care about when doing a logic operation
	alu_op_0010: assert property((op == 4'b0010) -> (r == (a ^ b))); // we don't care here as well
	alu_op_0100: assert property((op == 4'b0100) -> (r == (a | b))); // we don't care here as well

	// The only time 'Z' changes value is when 's == 0'. 's' is exposed to primary
	// output only when 'k = op[3] = 1' and through carry 'C'. Any unverified
	// defect except through this property would be a masked one, 
	// which is not of interest (as in for other operations, etc.).
	alu_Z: assert property((op[3] == 1'b1) -> (Z == ((r == 8'b0) & (C == 1'b0))));

	wire abBig, abSmall, op_is_valid, Vmirror, Nmirror;
	wire[8:0] abSum, b_after_op, abSum_without_C;
	assign b_after_op = (op == 4'b1000) ? 0 :
						(op == 4'b1001)	? 0 :    // + 1 will be added by op[0] below
						(op == 4'b1010)	? 9'h0FF :
						(op == 4'b1101) ? B :	 // + 1 will be added by op[0] below
						(op == 4'b1110) ? Bneg :
						(op == 4'b1111) ? Bneg : // 2's complement; -B is actually neg(B) + 1;
												 // +1 will be added by op[0] below
										  B; 
	assign abSmall = (A <= 9'h07F) & (b_after_op <= 9'h07F);
	assign abBig =   (A > 9'h07F) & (b_after_op > 9'h07F);
	assign abSum =   (A + b_after_op + op[0]); // op[0] is carry in
	assign abSum_without_C = abSum - {C, 8'h00};
	assign Vmirror = (	(abSmall & ((abSum_without_C  > 9'h07F))) |
						(abBig   & ((abSum_without_C <= 9'h07F)))
					 );

	assign op_is_add_or_subt = 
						 (op == 4'b1000) | (op == 4'b1001) | (op == 4'b1010) | 
						 (op == 4'b1100) | (op == 4'b1101) | (op == 4'b1110) |
						 (op == 4'b1111);

	// The abSum has to be conceived 1:1 with 'r', not 's', so that the property below is verified correctly.
	// Be very wary as 's' is for the ripple sum component that receives an unfiltered 'b' (that is not negated, etc.)
	// while 'r' works with a modified 'b' from the original.
	alu_r: assert property(op_is_add_or_subt -> ({C, r} == abSum)); // we're checking that the abSum made is correctly made

	alu_V: assert property(op_is_add_or_subt -> (V == Vmirror));

	assign Nmirror = (abSum_without_C > 9'h07F);
	alu_N: assert property(op_is_add_or_subt -> (N == Nmirror));

endmodule

// module test_op_1010_C;
	
// 	reg[7:0] a, b;
// 	reg[3:0] op;
// 	wire C, Z, N, V;
// 	wire[7:0] r;

// 	alu alu_inst(a, b, r, op, C, Z, N, V);

// 	initial
// 	begin
// 	    $dumpfile("test.vcd");
// 	    $dumpvars(0, test_op_1010_C);
// 	end

// 	initial begin
// 		#0 a=8'h00; b=8'h00; op=4'b0000;
// 		#50 a=8'h02; op=4'b1010; // We notice through this test how A - 1 is actually A + FF, which makes carry = 1. 
//                               // The carry toggling this way is therefore normal behaviour.
// 		#100 $finish;
// 	end

// endmodule
