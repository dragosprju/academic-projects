analyze -sv09 {design.v}
elaborate -top {top}
#clock -clear; clock clk;
#reset -expression {rst};
clock -none;
reset -none;

