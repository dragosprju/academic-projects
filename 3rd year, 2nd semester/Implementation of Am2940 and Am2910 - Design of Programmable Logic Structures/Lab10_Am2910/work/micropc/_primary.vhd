library verilog;
use verilog.vl_types.all;
entity micropc is
    port(
        di              : in     vl_logic_vector(11 downto 0);
        do              : out    vl_logic_vector(11 downto 0);
        do_stack        : out    vl_logic_vector(11 downto 0);
        clk             : in     vl_logic;
        respc           : in     vl_logic;
        plpc            : in     vl_logic;
        ci              : in     vl_logic
    );
end micropc;
