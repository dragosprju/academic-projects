/* Test of ENABLE COUNTERS in mode 2. Checking of proper incrementation of ADDRESS REGISTER (this time) and DONE signal generation. */
module am2940_test7();
  
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
    #0 instr = 3'b000; data_in = 8'b0000_0010; acineg = 0; wcineg = 0;
    #10 instr = 3'b101;
    #10 data_in = 8'b0000_0001;    
    #10 instr = 3'b110;
    #10 data_in = 8'b0000_0100;
    #10 instr = 3'b111;
  end
endmodule




