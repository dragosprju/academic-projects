class Environment;

	Generator	gen;
	Driver		drv;
	
	Scoreboard	scb;
	
	fifo		fif(interf_fifo.asDUT);
	mailbox gen2drv;

	initial
	begin
		#0 clk=1'b0; 
		forever #5 clk = ~clk;
	end

	function void build();
		gen2drv = new();

		gen = new(gen2drv);

		// The designed FIFO and the imitation FIFO
		fif = new()
		scb = new(fif)

		drv = new(gen2drv, fif, scb);		
	endfunction

	task run();
		fork
			gen.run()
			drv.run()
			//scb.run() -> is stepping along with drv
		join_none
	endtask

	task wrap_up();
		// Nothing
	endtask

endclass