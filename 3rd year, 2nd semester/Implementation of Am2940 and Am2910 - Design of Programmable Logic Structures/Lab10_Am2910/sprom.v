module sprom8x12(di, do, clk, address, we);
  input [11:0] di;
  input [2:0] address;
  input clk, we;
  output [11:0] do;
  reg [11:0] do;
  reg [11:0] mem[0:7];
  
  always@(posedge clk)
  begin
      do <= mem[address];
      if (we)
        mem[address] <= di;
  end
endmodule