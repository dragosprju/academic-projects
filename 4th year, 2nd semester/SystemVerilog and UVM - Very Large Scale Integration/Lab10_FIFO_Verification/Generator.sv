`define WIDTH 8
`define NADDR 8
`define NADDRB 3

class Generator;
	mailbox gen2drv;
	Transaction blueprint;

	function new(input mailbox gen2drv);
		this.gen2drv = gen2drv;
		blueprint = new();
	endfunction

	task run();
		Transaction tr;
		forever begin
			assert(blueprint.randomize)
			tr = blueprint.copy()
			gen2drv.put(tr);
		end
	endtask
endclass