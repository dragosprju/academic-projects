
//  Test of WRITE CONTROL REGISTER and READ CONTROL REGISTER commands (checking of control register proper functioning)
module am2940_test_all();
  
  reg[2:0] instr;
  reg clk;
  
  reg[7:0] data_in;
  reg acineg, wcineg;  
  
  wire[7:0] addr_out, data_out;
  wire aconeg, wconeg, done;
  
  
  am2940 am2940_inst(addr_out, data_in, data_out, acineg, aconeg, wcineg, wconeg, instr, done, clk);
  
  initial
    #1000 $finish;
    
  initial
  begin
    #0 clk = 0;
    forever #5 clk = ~clk;
  end
  
  initial
  begin
    // 1  Test of WRITE CONTROL REGISTER and READ CONTROL REGISTER 
    #0 instr = 3'b000; data_in=8'b01010101; acineg = 0; wcineg = 0;
    #10 instr = 3'b001;

    // 2 Test of LOAD WORD COUNT and READ WORD COUNTER commands 
    #10 instr = 3'b000; data_in=8'b0000_0000;
    #10 instr = 3'b110; data_in=8'b0101_0101;
    #10 instr = 3'b010;

    // 3 
    #10 instr = 3'b000; acineg = 0; wcineg = 0;
    #10 data_in=8'b0000_0000;
    #10 instr = 3'b101;
    #10 data_in=8'b1010_1010;
    #10 instr = 3'b011;
    #10 instr = 3'b111;
    #20 instr = 3'b100;
    #10 instr = 3'b011;

    // 4
    #10 instr = 3'b000; acineg = 0; wcineg = 0;
    #10 data_in = 8'b0000_0001;
    #10 instr = 3'b110;
    #10 data_in = 8'b0110_0110;
    #10 instr = 3'b010;
    #10 instr = 3'b000;
    #10 data_in = 8'b0000_0000;
    #10 instr = 3'b100;
    #10 instr = 3'b010;

    // 5 Test of ENABLE COUNTERS in mode 0. Checking of proper decrementation (in this case) and DONE signal generation.
    #10 instr = 3'b000; data_in = 8'b0000_0000;
    #10 instr = 3'b110;
    #10 data_in = 8'b0000_0011;    
    #10 instr = 3'b111;

    // 6 Test of ENABLE COUNTERS in mode 1. Checking of proper incrementation (in this case) and DONE signal generation. 
    #10 instr = 3'b000; data_in = 8'b0000_0001;
    #10 instr = 3'b110;
    #10 data_in = 8'b0000_0011;    
    #10 instr = 3'b111;

    // 7 Test of ENABLE COUNTERS in mode 2. Checking of proper incrementation of ADDRESS REGISTER (this time) and DONE signal generation. 
    #10 instr = 3'b000; data_in = 8'b0000_0010;
    #10 instr = 3'b101;
    #10 data_in = 8'b0000_0001;    
    #10 instr = 3'b110;
    #10 data_in = 8'b0000_0100;
    #10 instr = 3'b111;

    // 8 Test of ENABLE COUNTERS in mode 3. Checking of proper incrementation.
    #10 instr = 3'b000; data_in = 8'b0000_0011;
    #10 instr = 3'b110;
    #10 data_in = 8'b0000_0000;
    #10 instr = 3'b111;
  end
  
endmodule