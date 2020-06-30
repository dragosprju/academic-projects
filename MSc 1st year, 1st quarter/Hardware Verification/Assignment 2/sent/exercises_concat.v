// Dragos Perju, Mart Pluijmaekers

//************//
// Exercise 1 //
//************//

// newgen?1

property persistency(irdy, trdy);
   @(posedge clk)
   // irdy should happen at least once before this assertion is verified: irdy |->
   // irdy should hold high until it overlaps once with an high trdy: until_with trdy
   irdy |-> irdy until_with trdy;
   //irdy |-> irdy [*1:$] ##[0:1] (irdy & trdy); // I believe they are equivalent
endproperty


// Assertions are used instead of covers because we always want the persistency
// property to happen, and not just have one single example of it happening (as
// covers do check)
persistency_reqrsp_o0: assert property (
        persistency(i0$SRC$reqrsp.o0$irdy, i0$SRC$reqrsp.o0$trdy));

persistency_req_o0: assert property (
        persistency(i1$SRC$req.o0$irdy, i1$SRC$req.o0$trdy));

persistency_q1_o0: assert property(
        persistency(i2$Q$q1.o0$irdy, i2$Q$q1.o0$trdy));

persistency_switch_o0: assert property(
        persistency(i4$SW$switch.o0$irdy, i4$SW$switch.o0$trdy));

persistency_switch_o1: assert property(
        persistency(i4$SW$switch.o1$irdy, i4$SW$switch.o1$trdy));

persistency_merge2_o0: assert property(
        persistency(i5$MRG$merge2.o0$irdy, i5$MRG$merge2.o0$trdy));

persistency_join_o0: assert property(
        persistency(i6$CJN$join.o0$irdy, i6$CJN$join.o0$trdy));

persistency_torsp_o0: assert property(
        persistency(i7$FUN$torsp.o0$irdy, i7$FUN$torsp.o0$trdy));

persistency_q0_o0: assert property(
        persistency(i8$Q$q0.o0$irdy, i8$Q$q0.o0$trdy));

persistency_fork_o0: assert property(
        persistency(i9$FRK$fork.o0$irdy, i9$FRK$fork.o0$trdy));

persistency_fork_o1: assert property(
        persistency(i9$FRK$fork.o1$irdy, i9$FRK$fork.o1$trdy));

persistency_merge1_o0: assert property(
        persistency(i10$MRG$merge1.o0$irdy, i10$MRG$merge1.o0$trdy));

//----------------------------------------------------------------------------
// We don't do iN's because we already do their pairs, the oN's
//----------------------------------------------------------------------------
/*
persistency_q1_i0: assert property(
        persistency(i2$Q$q1.i0$irdy, i2$Q$q1.i0$trdy));

persistency_snk_i0: assert property(
        persistency(i3$SNK$snk.i0$irdy, i3$SNK$snk.i0$trdy));

persistency_switch_i0: assert property(
        persistency(i4$SW$switch.i0$irdy, i4$SW$switch.i0$trdy));

persistency_merge2_i0: assert property(
        persistency(i5$MRG$merge2.i0$irdy, i5$MRG$merge2.i0$trdy));

persistency_merge2_i1: assert property(
        persistency(i5$MRG$merge2.i1$irdy, i5$MRG$merge2.i1$trdy));

persistency_join_i0: assert property(
        persistency(i6$CJN$join.i0$irdy, i6$CJN$join.i0$trdy));

persistency_join_i1: assert property(
        persistency(i6$CJN$join.i1$irdy, i6$CJN$join.i1$trdy));

persistency_torsp_i0: assert property(
        persistency(i7$FUN$torsp.i0$irdy, i7$FUN$torsp.i0$trdy));

persistency_q0_i0: assert property(
        persistency(i8$Q$q0.i0$irdy, i8$Q$q0.i0$trdy));

persistency_fork_i0: assert property(
        persistency(i9$FRK$fork.i0$irdy, i9$FRK$fork.i0$trdy));

persistency_merge1_i0: assert property(
        persistency(i10$MRG$merge1.i0$irdy, i10$MRG$merge1.i0$trdy));

persistency_merge1_i1: assert property(
        persistency(i10$MRG$merge1.i1$irdy, i10$MRG$merge1.i1$trdy));             
*/

//----------------------------------------------------------------------------
// This part of the code was written because I understood
// that we have to fix the design if it's broken on Tuesday 26.09
// and I therefore started to investigate why some automatic covers
// created for triggers for some assertions were always failing.
// I figured a queue wasn't sending data through the code below and
// then finally managed to read the note at exercise 4 
// to have all the automatic covers passing for this exercise.
//
// On Thursday 28.09 it was announced that we don't need to edit the design.
//----------------------------------------------------------------------------

property bug_on_irdy_SW$switch_i0;
	@(posedge clk)
	i4$SW$switch.i0$irdy;
endproperty

property bug_on_datatype_SW$switch_i0;
	@(posedge clk)
	i4$SW$switch.i0$data$type==1;
endproperty

property bug_on_irdy_assign_SW$switch_i0;
	@(posedge clk)
	i4$SW$switch.i0$irdy && (i4$SW$switch.i0$data$type==1);
endproperty

property bug_on_datatype_assign_Q1$q1_o0_out0;
	@(posedge clk)
	(i2$Q$q1.out==0) && (i2$Q$q1.data$type$addr0==1);
endproperty

property bug_on_datatype_assign_Q1$q1_o0_out1;
	@(posedge clk)
	(i2$Q$q1.out==1) && (i2$Q$q1.data$type$addr1==1);
endproperty

property bug_on_datatype_assign_Q1$q1_o0_out0_irdy;
	@(posedge clk)
	(i2$Q$q1.out==0) && (i2$Q$q1.data$type$addr0==1) && (i2$Q$q1.o0$irdy);
endproperty

// bug_on_datatype_switch_i0: cover property(bug_on_datatype_SW$switch_i0);
// bug_on_irdy_switch_i0: cover property(bug_on_irdy_SW$switch_i0);
// bug_on_irdy_assign_switch_i0: cover property(bug_on_irdy_assign_SW$switch_i0);
// bug_on_datatype_assign_q1_o0_val0: cover property(bug_on_datatype_assign_Q1$q1_o0_out0);
// bug_on_datatype_assign_q1_o0_val1: cover property(bug_on_datatype_assign_Q1$q1_o0_out1);
// bug_on_datatype_assign_q1_o0_val0_irdy: cover property(bug_on_datatype_assign_Q1$q1_o0_out0_irdy);

//property persistency_for_SW$switch_o0(irdy, trdy);
//	@(posedge clk)
//	(i4$SW$switch.i0$data$type==1 && i4$SW$switch.i0$irdy) |-> irdy |-> ##[0:$] trdy; // not a correct variant, discussed and revised for the actual exercise
//endproperty

//persistency_SW$switch_o0: assert property(
//        persistency_for_SW$switch_o0(i4$SW$switch.o0$irdy, i4$SW$switch.o0$trdy));





//************//
// Exercise 2 //
//************//

// For a fork, write assertions stating that there is a transfer on one of the output channels
// if and only if there is a transfer on the other output channel. Write more assertions stating that
// there is a transfer on one of the outputs if and only if there is a transfer on the input channel.

property transfer(irdya, irdyb);
   @(posedge clk)
   // When either channel is ready to send, the other should be ready to send as well
   irdya |-> irdyb && irdyb |-> irdya;
endproperty

transfer_fork_o: assert property (
        transfer(i9$FRK$fork.o0$irdy, i9$FRK$fork.o1$irdy));

transfer_fork_i: assert property (
        transfer(i9$FRK$fork.i0$trdy, i9$FRK$fork.o1$irdy));

transfer_fork_ii: assert property (
        transfer(i9$FRK$fork.i0$trdy, i9$FRK$fork.o0$irdy));




//************//
// Exercise 3 //
//************//

// For a join, write assertions stating that if there is a transfer on one of the input
// channels, then there is transfer on the other input channel at the same cycle and there is a transfer
// at the output channel. Write similar assertions for the other input. Add assertions checking that
// transfers occur at the input if and only if there is a transfer at the output.

transfer_join_i: assert property (
        transfer(i6$CJN$join.i0$trdy, i6$CJN$join.i1$trdy));

transfer_join_o: assert property (
        transfer(i6$CJN$join.i0$trdy, i6$CJN$join.o0$irdy));

transfer_join_oo: assert property (
        transfer(i6$CJN$join.i1$trdy, i6$CJN$join.o0$irdy));



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




//************//
// Exercise 5 //
//************//

// Write assertions expressing the fact that each source is "alive". This means that every
// time a source is ready to send a message, it will eventually be able to do so. Write similar assertions
// for the two queues.

property eventualSend(trdy, irdy);
   @(posedge clk)
   always (irdy |-> s_eventually trdy);
endproperty


eventualSend_reqrsp_fork: assert property(
        eventualSend(i0$SRC$reqrsp.o0$irdy, i9$FRK$fork.i0$trdy));

eventualSend_req_merge1_1: assert property(
        eventualSend(i1$SRC$req.o0$irdy, i10$MRG$merge1.i1$trdy));

// Queues
eventualSend_q0_io: assert property(
        eventualSend(i8$Q$q0.i0$trdy, i8$Q$q0.o1$irdy));


eventualSend_q1_io: assert property(
        eventualSend(i2$Q$q1.i0$trdy, i2$Q$q1.o1$irdy));

// Initially I assumed we needed to do this for every channel (source being channel initiator)
// eventualSend_fork_0_q0: assert property(
//         eventualSend(i9$FRK$fork.o0$irdy, i8$Q$q0.i0$trdy));
// 
// 
// eventualSend_q0_join_0: assert property(
//         eventualSend(i8$Q$q0.o0$irdy, i6$CJN$join.i0$trdy));
// 
// 
// eventualSend_join_merge2_0: assert property(
//         eventualSend(i6$CJN$join.o0$irdy, i5$MRG$merge2.i0$trdy));
// 
// 
// eventualSend_merge2_snk: assert property(
//         eventualSend(i5$MRG$merge2.o0$irdy, i3$SNK$snk.i0$trdy));
// 
// 
// eventualSend_fork_1_merge1_0: assert property(
//         eventualSend(i9$FRK$fork.o1$irdy, i10$MRG$merge1.i0$trdy));
// 
// 
// eventualSend_merge1_q1: assert property(
//         eventualSend(i10$MRG$merge1.o0$irdy, i2$Q$q1.i0$trdy));
// 
// 
// eventualSend_q1_switch: assert property(
//         eventualSend(i2$Q$q1.o0$irdy, i4$SW$switch.i0$trdy));
// 
// 
// eventualSend_switch_0_torsp: assert property(
//         eventualSend(i4$SW$switch.o0$irdy, i7$FUN$torsp.i0$trdy));
// 
// 
// eventualSend_torsp_join_1: assert property(
//         eventualSend(i7$FUN$torsp.o0$irdy, i6$CJN$join.i1$trdy));
// 
// 
// eventualSend_switch_1_merge2_1: assert property(
//         eventualSend(i4$SW$switch.o1$irdy, i5$MRG$merge2.i1$trdy));





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


