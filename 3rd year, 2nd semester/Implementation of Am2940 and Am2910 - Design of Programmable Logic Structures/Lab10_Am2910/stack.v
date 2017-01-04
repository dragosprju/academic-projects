module stack(di, do, clk, clear, push, pop, fulln, emptyn);
  input[11:0] di;
  output [11:0] do;
  
  input clk, clear, push, pop;
  output fulln, emptyn;
  
  reg[3:0] ptr_pop, ptr_push;
  reg we;

  wire[2:0] ptr_mux;
  sprom8x12 sprom_inst(di, do, clk, ptr_mux, we);
  
  assign fulln = |ptr_push;
  assign emptyn = ~&ptr_pop;
  assign ptr_mux = ((push & fulln) | (pop & ~emptyn)) ? ptr_push[2:0] : ptr_pop[2:0];
  
  always@(posedge clk)
  casex({clear, push, pop, |ptr_push, &ptr_pop})
    5'b1xx_xx:
    begin      
      ptr_push <= 4'b1000;
      ptr_pop <= 4'b1111;
      we = 0;
    end
    5'b01x_1x:
    begin
      if (we == 0)
        we = 1;
      else
      begin
        ptr_push <= ptr_push + 1;
        ptr_pop <= ptr_pop + 1;
      end
    end
    5'b001_x0:
    begin
      ptr_push <= ptr_push - 1;
      ptr_pop <= ptr_pop - 1;
      we = 0;
    end
    5'b000_xx:
      we = 0;
  endcase
endmodule
      
