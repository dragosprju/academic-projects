library verilog;
use verilog.vl_types.all;
entity stack is
    port(
        di              : in     vl_logic_vector(11 downto 0);
        do              : out    vl_logic_vector(11 downto 0);
        clk             : in     vl_logic;
        clear           : in     vl_logic;
        push            : in     vl_logic;
        pop             : in     vl_logic;
        fulln           : out    vl_logic;
        emptyn          : out    vl_logic
    );
end stack;
