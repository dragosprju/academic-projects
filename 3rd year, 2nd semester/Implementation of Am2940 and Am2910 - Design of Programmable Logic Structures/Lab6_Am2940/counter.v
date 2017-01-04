module counter8b(di, do, ci, co, en, res, pl, inc, dec, clk);
  input[7:0] di;
  output[7:0] do;
  
  input ci;
  output co;
  
  input res, en, pl, inc, dec, clk;
  
  reg[7:0] do;
  
  assign co = ci | (~(do === 8'hff));
  
  always@(posedge clk)
    casex({res, pl, en, ci, inc, dec})
      6'b1xxxxx: do = 8'b0000_0000;
      6'b01xxxx: do = di;
      6'b00101x: do = do + 1;
      6'b001001: do = do - 1;
    endcase 
  
  /* Varianta mea
   always@(negedge clk or negedge ci or posedge ci)
    casex({inc, dec, ci, do})
      {3'b100, 8'hff}: co = 1'b0;
      {3'b100, 8'h00}: co = 1'b1;
      {3'bx10, 8'h00}: co = 1'b0;
      {3'bx10, 8'hff}: co = 1'b1;
      default: co = 1'b1;
    endcase 
  */
endmodule