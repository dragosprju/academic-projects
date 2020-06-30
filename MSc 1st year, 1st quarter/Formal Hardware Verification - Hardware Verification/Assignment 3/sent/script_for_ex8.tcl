analyze -sv09 {core8.v}
elaborate -top {ex8}
clock -clear; clock clk;
reset -expression {rst};
#clock -none;
#reset -none;

