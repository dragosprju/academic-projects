module reg8b(di, do, pl, clk);
  input[7:0] di;
  output[7:0] do;
  
  input clk, pl;

  reg[7:0] do;
  
  always@(posedge clk)
    if (pl == 1'b1)
      do = di;
    else
      do = do;
endmodule
      
module reg3b(di, do, pl, clk);
  input[2:0] di;
  output[2:0] do;
  
  input clk, pl;

  reg[2:0] do;
  
  always@(posedge clk)
    if (pl == 1'b1)
      do = di;
    else
      do = do;
endmodule

module mux8b2c(A, B, do, sel);
  input[7:0] A, B;
  input sel;
  output[7:0] do;
  
  reg[7:0] do;
  
  always@(A or B or sel)
    if(sel == 0)
      do = A;
    else
      do = B;  
endmodule