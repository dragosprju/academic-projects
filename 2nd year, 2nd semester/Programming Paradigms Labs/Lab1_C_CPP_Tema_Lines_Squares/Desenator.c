/*
 * Desenator.c
 *
 *  Created on: 19 feb. 2015
 *      Author: Dragos
 */

#include "Desenator.h"
#include <stdio.h>
#include <math.h>
#include <stdlib.h>

#define DELTA_CERC 2

void deseneazaCerc(int x, int y, int r) {
	int i, j;
	for (i=0; i<y; i++)
		printf("\n");
	// (i-x)^2+(j-y)^2 = r^2
	int membru_drept = r*r;
	for (i = x-r; i <= x+r; i++) {
		for (j=0; j<x; j++)
				printf(" ");
		for (j = y-r; j <= y+r; j++) {
			int membru_stang = (i-x)*(i-x)+(j-y)*(j-y);
			if ((membru_stang >= (membru_drept - DELTA_CERC)) && (membru_stang <= (membru_drept + DELTA_CERC)))
				printf("*");
			else
				printf(" ");
		}
		printf("\n");
	}
}

int apartineLiniei2(int i, int j, linie l, int d) {
	float delta_linie, membru_drept, membru_stang;
	if (l.x2 == l.x1)
		membru_stang = 0;
	else
		membru_stang = (i-(float)l.x1)/(l.x2-l.x1);
	if (l.y2 == l.y1)
		membru_drept = 0;
	else
		membru_drept = (j-(float)l.y1)/(l.y2-l.y1);
	delta_linie = abs(membru_stang - membru_drept) / d;
	if (membru_stang >= (membru_drept - delta_linie) && membru_stang <= (membru_drept + delta_linie))
		return 1;
	else
		return 0;
}

linie artificiuLinie(linie l) {
	int aux;
	if (l.x1 >= l.x2) {
		aux = l.x1;
		l.x1 = l.x2;
		l.x2 = aux;
	}
	if (l.y1 >= l.y2) {
		aux = l.y1;
		l.y1 = l.y2;
		l.y2 = aux;
	}
	return l;
}

void deseneazaLinie3(linie l) {
	int i, j;
	struct linie l2 = artificiuLinie(l);
	for (i=0; i<l2.y1; i++)
		printf("\n");
	for (i=l2.y1; i<=l2.y2; i++) {
		for (j=0; j<l2.x1; j++)
			printf(" ");
		for (j=l2.x1; j<=l2.x2; j++) {
			if (apartineLiniei2(i,j,l,5))
				printf("*");
			else
				printf(" ");
		}
		printf("\n");
	}
}

void deseneazaDreptunghi(int x1, int y1, int x2, int y2) {
	int i,j;
	for (i=0; i<y1; i++)
		printf("\n");
	for (i=y1; i<=y2; i++) {
		for (j=0; j<x1; j++)
			printf(" ");
		for (j=x1; j<=x2; j++)
			if (i == y1 || i == y2 || j == x1 || j == x2)
				printf("*");
			else
				printf(" ");
		printf("\n");
	}
}

void deseneazaPatrat(int x, int y, int l) {
	deseneazaDreptunghi(x, y, x+l, y+l);
}

void deseneazaTriunghi(int x1, int y1, int x2, int y2) {
	struct linie l = {.x1 = x1, .y1 = y1, .x2 = x2, .y2 = y2};
	int i, j;
	for (i=0; i<y1; i++)
		printf("\n");
	for (i=l.y1; i<=l.y2; i++) {
		for (j=0; j<x1; j++)
			printf(" ");
		for (j=l.x1; j<=l.x2; j++) {
			if (apartineLiniei2(i,j,l,5) || i == x2 || j == y1)
				printf("*");
			else
				printf(" ");
		}
		printf("\n");
	}
}
