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
