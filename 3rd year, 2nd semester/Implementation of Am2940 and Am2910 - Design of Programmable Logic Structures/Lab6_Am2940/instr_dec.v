module instr_dec(instr, cmd, out);
  input[2:0] instr;
  input[2:0] cmd;
  
  // 0-  PLAR
  // 1-  PLWR
  // 2-  PLCR
  // 3-  SELA
  // 4-  SELW
  // 5-  PLAC
  // 6-  ENA
  // 7-  INCA
  // 8-  DECA
  // 9-  RESW
  // 10- PLWC
  // 11- ENW
  // 12- INCW
  // 13- DECW
  // 14- SELDATA[1:0]
  // 15- OEDATA
  output[16:0] out;
  
  reg[16:0] out;
  
  always@(instr or cmd)
  casex({instr, cmd})
    // Format: PLs_SELs_ADDRsgnls_WRDsgnsl_DATA
    6'b000_xxx: out=17'b001_00_0000_00000_000;
    6'b001_xxx: out=17'b000_00_0000_00000_101;
    6'b010_xxx: out=17'b000_00_0000_00000_011;
    6'b011_xxx: out=17'b000_00_0000_00000_001;
    6'b100_xx0,
    6'b100_x11: out=17'b000_11_1000_01000_000;
    6'b100_x01: out=17'b000_10_1000_10000_000;
    6'b101_xxx: out=17'b100_00_1000_00000_000;
    6'b110_xx0,
    6'b110_x11: out=17'b01_000_0000_01000_000;
    6'b110_x01: out=17'b01_000_0000_10000_000;
    6'b111_000: out=17'b00_000_0110_00101_000;
    6'b111_0x1: out=17'b00_000_0110_00110_000;
    6'b111_010: out=17'b00_000_0110_00100_000;
    6'b111_100: out=17'b00_000_0101_00101_000;
    6'b111_1x1: out=17'b00_000_0101_00110_000;
    6'b111_110: out=17'b00_000_0101_00100_000;
  endcase
endmodule


    
  
  
  


