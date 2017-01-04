module stack2(di, do, clk, clear, push, pop, fulln, emptyn);
  input[11:0] di;
  output [11:0] do;
  
  input clk, clear, push, pop;
  output fulln, emptyn;
  
  reg[3:0] ptr_pop, ptr_push;

  wire[2:0] ptr_mux;
  sprom8x12 sprom_inst(di, do, clk, ptr_mux, we);

  assign emptyn = ~ptr_pop[3];
  assign fulln = ptr_push[3];
  assign ptr_mux = ((push & fulln) | (pop & ~emptyn)) ? ptr_push[2:0] : ptr_pop[2:0];
  assign we = push;
  
  always@(posedge clk)
  casex({clear, push, pop, emptyn, fulln})
    5'b1xx_xx:
    begin      
      ptr_push <= 4'b1000;
      ptr_pop <= 4'b1111;
    end

    5'b01x_x1:
    begin
      ptr_push <= ptr_push + 1;
      ptr_pop <= ptr_pop + 1;      
    end

    5'b001_1x:
    begin
      ptr_push <= ptr_push - 1;
      ptr_pop <= ptr_pop - 1;      
    end    
  endcase
endmodule
      
