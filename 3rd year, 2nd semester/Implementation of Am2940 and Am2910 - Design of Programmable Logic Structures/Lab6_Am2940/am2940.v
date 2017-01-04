module am2940(addr_out, data_in, data_out, acineg, aconeg, wcineg, wconeg, instr, done, clk);
  input[7:0] data_in;
  input[2:0] instr;
  input acineg, aconeg, wcineg, wconeg;
  input clk;
  
  output[7:0] data_out;
  
  output[7:0] addr_out;
  
  output done;
  
  wire[7:0] areg_out;
  wire[7:0] amux_out;
  wire[7:0] wreg_out;
  wire[7:0] wmux_out;
  wire[2:0] creg_out;
  
  wire[7:0] acnt_out;
  wire[7:0] wcnt_out; 
  
  assign res_gnd = 0;
  wire aconeg, wconeg;
  wire[1:0] seldata;

  assign addr_out = acnt_out;
  
  instr_dec instrdec_inst(instr, creg_out, {plar, plwr, plcr, sela, selw, plac, ena, inca, deca, resw, plwc, enw, incw, decw, seldata[1], seldata[0], oedata});
  
  reg8b addrreg(data_in, areg_out, plar, clk);
  reg8b wordreg(data_in, wreg_out, plwr, clk);
  reg3b ctrlreg(data_in[2:0], creg_out, plcr, clk);
  
  mux8b2c addrmux(data_in, areg_out, amux_out, sela);
  mux8b2c wordmux(data_in, wreg_out, wmux_out, selw);
  
  counter8b addrcnt(amux_out, acnt_out, acineg, aconeg, ena, res_gnd, plac, inca, deca, clk);
  counter8b wordcnt(wmux_out, wcnt_out, wcineg, wconeg, enw, resw, plwc, incw, decw, clk);

  datamux datamux_inst(acnt_out, wcnt_out, creg_out, seldata, data_out);
  donegen donegen_inst(creg_out[1:0], acnt_out, wcnt_out, wreg_out, wcineg, done);
  
endmodule
  
  

    
  
  
  
