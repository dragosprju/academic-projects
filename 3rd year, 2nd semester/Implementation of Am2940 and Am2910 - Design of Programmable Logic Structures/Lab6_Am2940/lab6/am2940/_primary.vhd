library verilog;
use verilog.vl_types.all;
entity am2940 is
    port(
        addr_out        : out    vl_logic_vector(7 downto 0);
        data_in         : in     vl_logic_vector(7 downto 0);
        data_out        : out    vl_logic_vector(7 downto 0);
        acineg          : in     vl_logic;
        aconeg          : in     vl_logic;
        wcineg          : in     vl_logic;
        wconeg          : in     vl_logic;
        instr           : in     vl_logic_vector(2 downto 0);
        done            : out    vl_logic;
        clk             : in     vl_logic
    );
end am2940;
