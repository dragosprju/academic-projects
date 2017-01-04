library verilog;
use verilog.vl_types.all;
entity am2910 is
    port(
        di              : in     vl_logic_vector(11 downto 0);
        do              : out    vl_logic_vector(11 downto 0);
        instr           : in     vl_logic_vector(3 downto 0);
        ccn             : in     vl_logic;
        rldn            : in     vl_logic;
        ci              : in     vl_logic;
        plpc            : in     vl_logic;
        clk             : in     vl_logic;
        fulln           : out    vl_logic;
        emptyn          : out    vl_logic;
        pln             : out    vl_logic;
        mapn            : out    vl_logic;
        vectn           : out    vl_logic
    );
end am2910;
