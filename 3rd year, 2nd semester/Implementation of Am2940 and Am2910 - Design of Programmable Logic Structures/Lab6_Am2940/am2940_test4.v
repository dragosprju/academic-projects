/* Test steps (in mode 1)
 * 1. LOAD WORD COUNTER (as to load word register and reset word counter)
 * 2. READ WORD COUNTER (to check if word counter is 0)
 * 3. Switch to mode 0 (to load word counter with word register value)
 * 4. REINITIALIZE COUNTERS
 * 5. READ WORD COUNTER (to check word register proper functioning)
 */
module am2940_test4();
  
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
    #10 data_in = 8'b0000_0001;
    #10 instr = 3'b110;
    #10 data_in = 8'b0110_0110;
    #10 instr = 3'b010;
    #10 instr = 3'b000;
    #10 data_in = 8'b0000_0000;
    #10 instr = 3'b100;
    #10 instr = 3'b010;
  end
endmodule


