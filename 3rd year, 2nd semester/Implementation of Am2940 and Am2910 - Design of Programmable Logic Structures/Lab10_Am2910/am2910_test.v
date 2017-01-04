module am2910_test();	

	reg[11:0] di;
	reg[3:0] instr;
	reg clk, ccn, rldn, ci, plpc;

	wire[11:0] do;
	wire fulln, emptyn, pln, mapn, vectn;

	am2910 am2910_inst(di, do, instr, ccn, rldn, ci, plpc, clk, fulln, emptyn, pln, mapn, vectn);

	initial
	begin
		#0 clk = 0;
		forever #5 clk = ~clk;
	end

	initial
	begin
		/* 0- (0) JUMP ZERO
		 * La ieșire trebuie să fie 0 */
		#0 di = 12'b1001; instr = 4'b0000;
		   ccn = 1; rldn = 1; ci = 1; plpc = 1;
		
		/* 10- (1) CONDITIONAL JUMP-TO-SUBROUTINE (fără condiție)
		 * La ieșire se continuă incrementarea, nefiind condiția satisfăcută
		 * pt. jump */
		#10 instr = 4'b0001; ccn = 1;

		/* 40- (1) CONDITIONAL JUMP-TO-SUBROUTINE (cu condiție)
		 * La ieșire trebuie să fie ce este la intrare, fiind satisfăcută
		 * condiția pt. jump. Se salvează în stivă adresa anterioară. */
		#30 instr = 4'b0001; ccn = 0;

			/* Dacă ținem instrucțiunea prea mult, se încarcă pe stivă
			 * instrucțiunea de la intrare în loc să rămână doar
			 * instrucțiunea de la care am plecat */
			#10 ccn = 1;

		/* 70- (10) CONDITIONAL RETURN-FROM-SUBROUTINE (fără condiție)
		 * La ieșire ar trebui să se incrementeze, nefiind condiția satisfăcută
		 * pt. scos din stivă */
		#20 instr = 4'b1010; ccn = 1; 

		/* 80- (10) CONDITIONAL RETURN-FROM-SUBROUTINE (cu condiție)
		 * La ieșire ar trebui să apară ce am salvat dinainte pe stivă
		 * și apoi să înceapă să incrementeze */
		#10 instr = 4'b1010; ccn = 0; 

		/* 90- (2) JUMP MAP (necondiționat)
		 * La ieșire trebuie să apară ce avem la intrare. De asemenea, observăm
		 * că MAPn devine 0. */
		#10 instr = 4'b0010; di = 12'b0110;

		/* 105- (3) CONDITIONAL JUMP PIPELNE (fără condiție)
		 * La ieșire se continuă incementarea. Observăm PLn devine 0. */
		#15 instr = 4'b0011; ccn = 1; di = 12'b1010;

		/* 125- (3) CONDITIONAL JUMP PIPELINE (cu condiție)
		 * La ieșire apare ce este la intrare. Observăm PLn este 0. */
		#20 instr = 4'b0011; ccn = 0; di = 12'b1010;

		/* 135- (4) PUSH/CONDITIONAL LOAD COUNTER (fără condiție)
		 * La ieșire, după instrucțiune, apoi resetare ieșire, apoi pop
		 * de pe stivă, ar trebui să avem la ieșire ce valoare am încărcat
		 * la început. */
		#10 instr = 4'b0100; ccn = 1; di = 12'b0101;
		#10 instr = 4'b0000; // Zerorizam iesirea
		#15 instr = 4'b1010; ccn = 0; // Golim stiva (vezi instr. 10 la ns 80)

		/* 175- (4) PUSH/CONDITIONAL LOAD COUNTER (cu condiție)
		 * La ieșire se incrementează. Folosind comanda următoare, observăm
		 * de fapt că s-a încărcat registrul prin afișarea lui la ieșire. */
		#15 instr = 4'b0100; ccn = 0; di = 12'b0001;

		/* 190- (5) CONDITIONAL JUMP-TO-SUBROUTINE via REG/PL (fără condiție)
		 * La ieșire afișăm ce s-a salvat în registru comanda trecută (val 1) */
		#15 instr = 4'b0000; // Zerorizăm iesirea
		#10 instr = 4'b0101; ccn = 1; di = 12'b0011;

		/* 210- (5) CONDITIONAL JUMP-TO-SUBROUTINE via REG/PL (cu condiție)
		 * La ieșire se afișează ce avem la intrare. */
		#10 instr = 4'b0101; ccn = 0; di = 12'b0011;

		/* 225- (6) CONDITIONAL JUMP VECTOR (fără condiție)
		 * Observăm că la ieșire se incrementează. VECTn e 0. */
		#15 instr = 4'b0110; ccn = 1; di = 12'b0111;

		/* 245- (6) CONDITIONAL JUMP VECTOR (cu condiție)
		 * Observăm că ieșirea devine intrarea. VECTn e 0. */		
		#20 instr = 4'b0110; ccn = 0; di = 12'b0111;

		/* 255- (7) CONDITIONAL JUMP via REG/PL (fără condiție)
		 * Observăm că la ieșire apare din nou valoarea registrului
		 * stabilit înainte. (era valoarea 1)*/
		#10 instr = 4'b0111; ccn = 1; di = 12'b1010;

		/* 265- (7) CONDITIONAL JUMP via REG/PL (fără condiție)
		 * Observăm că la ieșire apare intrarea. */
		#10 instr = 4'b0111; ccn = 0; di = 12'b1010;

		/* 275- (8) REPEAT LOOP, COUNTER != 0 (nu are condiție)
		 * Încărcăm registrul cu 3 pași de loop și, în același timp, stiva
		 * cu adresa curentă + 1. Apoi, observăm că la ieșire se menține pentru
		 * 3 cicluri de clock, ultima valoare pusă pe stivă (adr_curr + 1) */
		#10 instr = 4'b0100; ccn = 0; di = 12'b0011; // Încărcăm registrul
		#15 instr = 4'b1000;

		/* 340- (9) REPEAT PIPELINE REGISTER, COUNTER != 0 (nu are condiție)
         * Ar trebui să fie ca data trecută, numai că primește adresa de
         * unde începe incrementarea de la PL (di). */

        /* Ar trebui extins micropc să scoată PLPC când pornește
         * instrucțiunea, să fie 0, apoi când e activat ZEROR să 
         * fie 1. */
		#50 instr = 4'b0100; ccn = 0; di = 12'b0011; // Încărcăm registrul
		#10 instr = 4'b1001; di = 12'b1000; plpc = 0;
		#25 plpc = 1;

		/* 395- (11) CONDITIONAL JUMP PIPELINE + POP STACK (fără condiție) */
		/* Se incrementează în continuare, nefiind satisfăcută condiția. */
		#20 instr = 4'b1011; ccn = 1; di = 12'b0011;

		/* 395- (11) CONDITIONAL JUMP PIPELINE + POP STACK (cu condiție) */
		/* Observăm că se afișează ce este la intrare și se golește
		 * stiva */
		#20 instr = 4'b1011; ccn = 0;

		/* 465- (12) LOAD COUNTER AND CONTINUE
		 * Observăm linia R cu valoarea corespunzătoare. */
		#50 instr = 4'b1100; di = 12'b1111;
		#15 instr = 4'b0111; ccn = 1; di = 12'b0001; // Afișăm registrul (7)

		/* 480- (14) CONTINUE */
		#10 instr = 4'b1110;

		/* 520- (15) THREE-WAY BRANCH */
		#30 instr = 4'b0100; ccn = 0; di = 12'b0100; // Încărcăm reg + stivă

		/* Trebuie implementat plpc și aici, începând la zeror != 0 și
		 * ccn == 1 cu plpc = 0, apoi când ccn == 0 și R == 0 atunci
		 * plpc = 0 */

		/* Cu conditia eșuată și registrul care se decrementează încă
		 * pozitiv, la ieșire apare ce e pe stivă (0024) */
		#20 instr = 4'b1111; ccn = 1; plpc = 0;
		#5  di = 12'b0001;

		/* Apoi, apare exclusiv ce este la ieșire, iar când condiția
		 * e valabilă, se continuă incrementarea cu ce este la
		 * ieșire */
		#70 ccn = 0; plpc = 1;

		/* 655- (13) TEST END-OF-LOOP (fără condiție)
		 * Vom observa că la ieșire se incrementează (fiind comanda
		 * continue) și apoi se revine la ceea ce s-a salvat pe stivă,
		 * dacă condiția a eșuat. Fix ca un loop. */
		#40 instr = 4'b0000; di = 12'b1010;
		#20 instr = 4'b0101; ccn = 1; // Încărcăm stiva
		#10 instr = 4'b1110; // Continue
		#50 instr = 4'b1101; ccn = 1;		

		/* 745- (13) TEST END-OF-LOOP (cu condiție)
		 * Continuă să se incrementeze, fiind condiția validă */
		#10 instr = 4'b1110; /* Continue, trebuie făcut imediat după
							  * comanda asta cu condiția eșuată, ca
							  * uPC să nu se blocheze și ieșirea
							  * să fie fixă */
		#50 instr = 4'b1101; ccn = 0;

		#100 $finish;
	end
endmodule