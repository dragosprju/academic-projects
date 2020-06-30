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


