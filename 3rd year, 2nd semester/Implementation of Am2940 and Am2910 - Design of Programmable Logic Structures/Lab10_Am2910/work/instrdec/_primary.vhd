library verilog;
use verilog.vl_types.all;
entity instrdec is
    port(
        instr           : in     vl_logic_vector(3 downto 0);
        ccn             : in     vl_logic;
        zeror           : in     vl_logic;
        \out\           : out    vl_logic_vector(10 downto 0)
    );
end instrdec;
