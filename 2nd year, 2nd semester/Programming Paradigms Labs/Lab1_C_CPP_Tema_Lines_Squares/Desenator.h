/*
 * Desenator.h
 *
 *  Created on: 19 feb. 2015
 *      Author: Dragos
 */

#ifndef DESENATOR_H_
#define DESENATOR_H_

typedef struct linie {
	int x1;
	int y1;
	int x2;
	int y2;
} linie;

void deseneazaCerc(int x, int y, int r);
void deseneazaLinie2(linie l);
void deseneazaLinie3(linie l);
void deseneazaDreptunghi(int x1, int y1, int x2, int y2);
void deseneazaPatrat(int x, int y, int l);
void deseneazaTriunghi(int x1, int y1, int x2, int y2);
int apartineLiniei(int i, int j, linie l, int d);
int apartineLiniei2(int i, int j, linie l, int d);;
int verificaCoordLinie2(linie l);
linie reparaLinie(linie l);

#endif /* DESENATOR_H_ */
