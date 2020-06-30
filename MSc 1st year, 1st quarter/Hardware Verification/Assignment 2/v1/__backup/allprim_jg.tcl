analyze -sv09 {allprim.v}
elaborate -top {top}
clock -clear; clock clk;
reset -expression {rst};

