//************//
// Exercise 6 //
//************//

// It works without |-> because the covers are trying to find the pattern 
// of the property throughout the whole time and there isn't any need to match
// any pre-condition before a following condition after the supposed arrow is asserted

// newgen?0

property latency_1clk_;
	@(posedge clk)
	(i1$SRC$req.o0$data$type == 0) && (i3$SNK$snk.i0$data$type != 0) ##1 (i3$SNK$snk.i0$data$type == 0);
endproperty

property latency_2clk_;
	@(posedge clk)
	(i1$SRC$req.o0$data$type == 0) && (i3$SNK$snk.i0$data$type != 0) ##1 (i3$SNK$snk.i0$data$type != 0) ##1 (i3$SNK$snk.i0$data$type == 0);
endproperty

property latency_3clkOrMore_fail_;
	@(posedge clk)
	(i1$SRC$req.o0$data$type == 0) && (i3$SNK$snk.i0$data$type != 0) ##1 (i3$SNK$snk.i0$data$type != 0) [*2:$] ##1 (i3$SNK$snk.i0$data$type == 0);
endproperty

property latency_1clk_alt_;
	@(posedge clk)
	(i0$SRC$reqresp.o0$data$type == 1) && (i3$SNK$snk.i0$data$type != 1) ##1 (i3$SNK$snk.i0$data$type != 1) ##1 (i3$SNK$snk.i0$data$type == 1);
endproperty

latency_1clk: cover property (latency_1clk_);
latency_2clk: cover property (latency_2clk_);
latency_3clkOrMore_fail:  cover property (latency_3clkOrMore_fail_);
latency_1clk_alt: cover property (latency_1clk_alt_);
