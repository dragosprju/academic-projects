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


