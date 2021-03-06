/* Test steps:
 * 1. LOAD ADDRESS
 * 2. READ ADDRESS COUNTER (to check address counter proper functioning)
 * 3. ENABLE COUNTERS (to modify address counter by incrementation, as to reinitialize it with address register and check address register proper functioning)
 * 4. REINITIALIZE COUNTERS
 * 5. READ ADDRESS COUNTER
 */
module am2940_test3();
  
  reg[2:0] instr;
  reg clk;
  
  reg[7:0] data_in;
  reg acineg, wcineg;  
  
  wire[7:0] addr_out, data_out;
  wire aconeg, wconeg, done;
  
  
  am2940 am2940_inst(addr_out, data_in, data_out, acineg, aconeg, wcineg, wconeg, instr, done, clk);
  
  initial
    #100 $finish;
    
  initial
  begin
    #0 clk = 0;
    forever #5 clk = ~clk;
  end
  
  initial
  begin
    #0 instr = 3'b000; acineg = 0; wcineg = 0;
    #10 data_in=8'b0000_0000;
    #10 instr = 3'b101;
    #10 data_in=8'b1010_1010;
    #10 instr = 3'b011;
    #10 instr = 3'b111;
    #20 instr = 3'b100;
    #10 instr = 3'b011;
  end
endmodule

