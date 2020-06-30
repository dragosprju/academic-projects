//************//
// Exercise 4 //
//************//

// newgen?1

// For merge 1:
// sel = 0 selects the data coming from fork
// sel = 1 selects the data coming from req

// For merge 2:
// sel = 0 selects the data coming from join
// sel = 1 selects the data coming from switch


property fairness_for_sel1(sel);
	@(posedge clk)
	// GFp with p being each value possible
	(sel==0) |-> always s_eventually (sel==1);
endproperty

// We don't need a |-> since we are trying to find a pattern.
// Here we check that sel=1 it takes a clock for sel to become 1, right? This
// assertion is however also true if two clocks are with sel
// not being 1, then sel being 1.
// That's why we verify with other 2 assertions and if they
// fail, this one proves that there's at least one example that
// after 1 clock, sel becomes 1.
property tight_bound_for_sel1_1clk(sel);
	@(posedge clk)
	(sel!=1) ##1 (sel==1);
endproperty

// Here we verify that in the lifetime of the design
// there are two clocks with sel=0, then 
property tight_bound_for_sel1_2clk(sel);
	@(posedge clk)
	(sel!=1) [*2] ##1 (sel==1);
endproperty

property tight_bound_for_sel1_3clkOrMore_fail(sel);
	@(posedge clk)
	(sel!=1) [*3:$] ##1 (sel==1);
endproperty


property fairness_for_sel0(sel);
	@(posedge clk)
	// The other p
	(sel==1) |-> always s_eventually (sel==0);
endproperty

property tight_bound_for_sel0_1clk(sel);
	@(posedge clk)
	(sel!=0) ##1 (sel==0);
endproperty

property tight_bound_for_sel0_2clk(sel);
	@(posedge clk)
	(sel!=0) [*2] ##1 (sel==0);
endproperty

property tight_bound_for_sel0_3clkOrMore_fail(sel);
	@(posedge clk)
	(sel!=0) [*3:$] ##1 (sel==0);
endproperty




// Assert is used since we always want fairness to happen
fairness_merge1_for_sel1: assert property (
	fairness_for_sel1(i10$MRG$merge1.arb0$sel));

fairness_merge2_for_sel1: assert property (
	fairness_for_sel1(i5$MRG$merge2.arb0$sel));

// Cover is used since we want to find just one example for a quick change to sel=1
tight_bound_merge1_for_sel1_1clk: cover property(
	tight_bound_for_sel1_1clk(i10$MRG$merge1.arb0$sel));

tight_bound_merge1_for_sel1_2clk: cover property(
	tight_bound_for_sel1_2clk(i10$MRG$merge1.arb0$sel));

tight_bound_merge1_for_sel1_3clkOrMore_fail: cover property(
	tight_bound_for_sel1_3clkOrMore_fail(i10$MRG$merge1.arb0$sel));


tight_bound_merge2_for_sel1_1clk: cover property(
	tight_bound_for_sel1_1clk(i5$MRG$merge2.arb0$sel));

tight_bound_merge2_for_sel1_2clk: cover property(
	tight_bound_for_sel1_2clk(i5$MRG$merge2.arb0$sel));

tight_bound_merge2_for_sel1_3clkOrMore_fail: cover property(
	tight_bound_for_sel1_3clkOrMore_fail(i5$MRG$merge2.arb0$sel));




// Assert is used since we always want fairness to happen
fairness_merge1_for_sel0: assert property (
	fairness_for_sel0(i10$MRG$merge1.arb0$sel));

fairness_merge2_for_sel0: assert property (
	fairness_for_sel0(i5$MRG$merge2.arb0$sel));

// Cover is used since we want to find just one example for a quick change to sel=0
tight_bound_merge1_for_sel0_1clk: cover property(
	tight_bound_for_sel0_1clk(i10$MRG$merge1.arb0$sel));

tight_bound_merge1_for_sel0_2clk: cover property(
	tight_bound_for_sel0_2clk(i10$MRG$merge1.arb0$sel));

tight_bound_merge1_for_sel0_3clkOrMore_fail: cover property(
	tight_bound_for_sel0_3clkOrMore_fail(i10$MRG$merge1.arb0$sel));


tight_bound_merge2_for_sel0_1clk: cover property(
	tight_bound_for_sel0_1clk(i5$MRG$merge2.arb0$sel));

tight_bound_merge2_for_sel0_2clk: cover property(
	tight_bound_for_sel0_2clk(i5$MRG$merge2.arb0$sel));

tight_bound_merge2_for_sel0_3clkOrMore_fail: cover property(
	tight_bound_for_sel0_3clkOrMore_fail(i5$MRG$merge2.arb0$sel));
