module datamux(a, b, c, sel, out);
  input[7:0] a,b;
  input[2:0] c;
  input[1:0] sel;
  
  output[7:0] out;  
  reg[7:0] out;
  
  always@(a or b or c or sel)
  casex(sel)
    2'b00: out = a;
    2'b01: out = b;
    2'b1x: out = {5'b11111, c};
  endcase
endmodule 

module donegen(cr, addrcnt, wordcnt, wordreg, wcineg, done);
  input[1:0] cr;
  input[7:0] addrcnt;
  input[7:0] wordcnt;
  input[7:0] wordreg;
  
  input wcineg;
  
  output done;
  reg done;
  
  always@(cr or addrcnt or wordcnt or wordreg or wcineg)
    casex({cr, wcineg})
      3'b00_0:
        if (wordcnt === 8'h01)
          done = 1;
        else
          done = 0;
      3'b00_1:
        if (wordcnt === 8'h00)
          done = 1;
        else
          done = 0;
      3'b01_0:
        if ((wordcnt + 1) === wordreg)
          done = 1;
        else
          done = 0;
      3'b01_1:
        if(wordcnt === wordreg)
          done = 1;
        else
          done = 0;
      3'b10_x:
        if(wordcnt === addrcnt)
          done = 1;
        else
          done = 0;
      3'b11_x:
        done = 0;
      default:
        done = 0;
    endcase
endmodule
        
  
  
