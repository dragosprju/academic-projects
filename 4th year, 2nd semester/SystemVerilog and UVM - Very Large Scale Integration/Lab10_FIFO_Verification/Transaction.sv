`define WIDTH 8
`define NADDR 8
`define NADDRB 3

class Transaction;
	rand bit rst;

	rand bit push, pop;
	rand bit [`WIDTH-1:0] data;

	constraint push_pop {
		rst  dist {0:=80, 1:=20};
		push dist {0:=40, 1:=60};
		// src = 0, weight = 40/40
		// src = 1, weight = 60/100
		// If added more, would add to 100 (would go over 100)
		pop  dist {0:/60, 1:/40};
		// src = 0, weight = 40/100
		// src = 1, weight = 60/100
		// If added more, would still be scaled to 100
	}

	virtual function void display(input string prefix="");
		if (push == 1) begin
			$display("%dns: Pushing. Data pushed = %h", $time, prefix, data);
		end
		else if (pop == 1) begin
			$display("%dns: Pop'd.", $time, prefix)
		end
		else if (rst == 1) begin
			$display("%dns: Resetted.", $time, prefix)
		end

	endfunction

endclass