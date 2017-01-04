library verilog;
use verilog.vl_types.all;
entity donegen is
    port(
        cr              : in     vl_logic_vector(1 downto 0);
        addrcnt         : in     vl_logic_vector(7 downto 0);
        wordcnt         : in     vl_logic_vector(7 downto 0);
        wordreg         : in     vl_logic_vector(7 downto 0);
        wcineg          : in     vl_logic;
        done            : out    vl_logic
    );
end donegen;
