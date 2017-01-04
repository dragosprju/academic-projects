library verilog;
use verilog.vl_types.all;
entity sprom8x12 is
    port(
        di              : in     vl_logic_vector(11 downto 0);
        do              : out    vl_logic_vector(11 downto 0);
        clk             : in     vl_logic;
        address         : in     vl_logic_vector(2 downto 0);
        we              : in     vl_logic
    );
end sprom8x12;
