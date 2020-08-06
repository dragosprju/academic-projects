class Driver;
	Scoreboard scoreboard;
	fifo design_under_test;
	mailbox gen2drv;

	function new(input mailbox gen2drv, input fifo design_under_test, input Scoreboard scoreboard);
		this.gen2drv = gen2drv;
		this.design_under_test = design_under_test;
	endfunction

	task main;
		Transaction tr;

		forever begin
			gen2drv.get(tr);
			@(posedge design_under_test.clk)
			design_under_test.di   <= tr.data
			design_under_test.rst  <= tr.rst
			design_under_test.push <= tr.push
			design_under_test.pop  <= tr.pop

			scoreboard.step(tr)
		end
	endtask
endclass
