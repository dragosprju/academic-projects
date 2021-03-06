//  Test of WRITE CONTROL REGISTER and READ CONTROL REGISTER commands (checking of control register proper functioning)
module am2940_test1();
  
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
    #0 instr = 3'b000; data_in=8'b01010101; acineg = 0; wcineg = 0;
    #10 instr = 3'b001; data_in=8'b10101010;
  end
  
endmodule