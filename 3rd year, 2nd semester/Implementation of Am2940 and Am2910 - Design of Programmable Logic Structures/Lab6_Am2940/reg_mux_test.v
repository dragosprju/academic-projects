module reg3b_test();
  reg[2:0] di;
  reg pl, clk;
  
  wire[2:0] do;  
  
  reg3b reg3b_inst(di, do, pl, clk);
  
  initial
    #100 $finish;
    
  initial
  begin
    #0 clk = 0;
    forever #5 clk = ~clk;
  end
  
  initial
  begin
    #0 di = 3'b000; pl = 1;
    #10 pl = 0;
    #20 di = 3'b101;
    #10 pl = 1;
    #10 pl = 0;
  end
endmodule

module reg8b_test();
  reg[7:0] di;
  reg pl, clk;
  
  wire[7:0] do;  
  
  reg8b reg8b_inst(di, do, pl, clk);
  
  initial
    #100 $finish;
    
  initial
  begin
    #0 clk = 0;
    forever #5 clk = ~clk;
  end
  
  initial
  begin
    #0 di = 8'h00; pl = 1;
    #10 pl = 0;
    #20 di = 8'haa;
    #10 pl = 1;
    #10 pl = 0;
  end
endmodule

module mux8b2c_test();
  reg[7:0] A, B;
  reg sel;
  wire[7:0] do;
  
  mux8b2c mux8b2c_inst(A, B, do, sel);
  
  initial
    #100 $finish;
  
  initial
  begin
    #0 A = 8'h0a; B = 8'haa;
    #10 sel = 0;
    #10 sel = 1;
  end
endmodule

module counter8b_test();
  reg[7:0] di1, di2, di3;
  reg ci, en, res, pl, inc, dec;
  
  reg clk;
  
  wire[7:0] do1, do2, do3;
  wire co12, co23, co3;
  
  counter8b counter1(di1, do1, ci, co12, en, res, pl, inc, dec, clk);
  counter8b counter2(di2, do2, co12, co23, en, res, pl, inc, dec, clk);
  counter8b counter3(di3, do3, co23, co3, en, res, pl, inc, dec, clk);
  
  initial
    #100 $finish;
    
  initial
  begin
    #0 clk = 0;
    forever #5 clk = ~clk;
  end
  
  initial
  begin
    #0 res = 1;
    #10 res = 0; pl = 1; di1=8'hfe; di2=8'hff; di3=8'hfe;
    #10 pl = 0; en = 1; ci = 0; inc = 1;
    #30 en = 0; ci = 1; inc = 0; res = 1;
    #10 res = 0; pl = 1; di1=8'hfe; di2=8'hff; di3=8'hff;
    #10 pl = 0; en = 1; ci = 0; inc = 1;
  end
endmodule
