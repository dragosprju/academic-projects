analyze -sv09 {alu.v}
elaborate -top {top}
#clock -clear; clock clk;
#reset -expression {rst};
clock -none;
reset -none;

