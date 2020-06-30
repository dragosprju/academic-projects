//************//
// Exercise 2 //
//************//

// For a fork, write assertions stating that there is a transfer on one of the output channels
// if and only if there is a transfer on the other output channel. Write more assertions stating that
// there is a transfer on one of the outputs if and only if there is a transfer on the input channel.

// We assume the persistency on channels from exercise 1 is already there:
//  transfers happen when trdy is high

property transfer(irdya, irdyb);
   @(posedge clk)
   // When either channel is ready to send, the other should be ready to send as well
   (irdya && irdyb) || (!irdya && !irdyb); // We never find a pattern where irdya is true and irdyb is false
					   //                            or irdya is false and irdyb is true
endproperty

// My alternative 2:
property transfer2(i_trdy, o_irdy);
   @(posedge clk)
   // We never find a trdy from an input without an irdy from the output
   !(i_trdy && !o_irdy);
endproperty

transfer_fork_o: assert property (
        transfer(i9$FRK$fork.o0$irdy, i9$FRK$fork.o1$irdy));

transfer_fork_i: assert property (
        transfer2(i9$FRK$fork.i0$trdy, i9$FRK$fork.o1$irdy));

transfer_fork_ii: assert property (
        transfer2(i9$FRK$fork.i0$trdy, i9$FRK$fork.o0$irdy));




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
        transfer2(i6$CJN$join.i0$trdy, i6$CJN$join.o0$irdy));

transfer_join_oo: assert property (
        transfer2(i6$CJN$join.i1$trdy, i6$CJN$join.o0$irdy));


