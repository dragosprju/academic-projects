library verilog;
use verilog.vl_types.all;
entity reg12b is
    port(
        di              : in     vl_logic_vector(11 downto 0);
        do              : out    vl_logic_vector(11 downto 0);
        clk             : in     vl_logic;
        pl              : in     vl_logic;
        dec             : in     vl_logic;
        zero            : out    vl_logic
    );
end reg12b;
