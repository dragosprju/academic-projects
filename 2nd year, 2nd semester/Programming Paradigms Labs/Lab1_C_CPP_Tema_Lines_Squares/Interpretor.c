/*
 * Interpretor.c
 *
 *  Created on: 19 feb. 2015
 *      Author: Dragos
 */

#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <conio.h>
#include "Interpretor.h"
#include "Sir.h"
#include "Desenator.h"

void interpreteazaConsola() {
	struct linie l;
	int x1, y1, x2, y2, r;
	const char *eroare_comanda = "Eroare comanda. Tastati 'help' pt. ajutor.\n";
	while (1) {
		char *intrare = citesteSir("> ");
		switch(interpreteazaSirul(intrare)) {
		case 1:
			// Linie
			l.x1 = interpreteazaValoarea(intrare, 1);
			l.y1 = interpreteazaValoarea(intrare, 2);
			l.x2 = interpreteazaValoarea(intrare, 3);
			l.y2 = interpreteazaValoarea(intrare, 4);
			if (l.x1 > -1 && l.y1 > -1 && l.x2 > -1 && l.y2 > -1) {
				system("cls");
				deseneazaLinie3(l);
				getch();
				system("cls");
			}
			else if (l.x1 == -1)
				printf("Sintaxa comanda: linie(x1,y1,x2,y2).\n");
			else
				printf ("%s", eroare_comanda);
			break;
		case 2:
			// Cerc
			x1 = interpreteazaValoarea(intrare, 1);
			y1 = interpreteazaValoarea(intrare, 2);
			r = interpreteazaValoarea(intrare, 3);
			if (x1 > -1 && y1 > -1 && r > -1) {
				system("cls");
				deseneazaCerc(x1,y1,r);
				getch();
				system("cls");
			}
			else if (l.x1 == -1)
				printf("Eroare. Sintaxa comanda: cerc(x,y,r).\n");
			else
				printf ("%s", eroare_comanda);
			break;
		case 3:
			// Patrat
			x1 = interpreteazaValoarea(intrare, 1);
			y1 = interpreteazaValoarea(intrare, 2);
			r = interpreteazaValoarea(intrare, 3);
			if (x1 > -1 && y1 > -1 && r > -1) {
				system("cls");
				deseneazaPatrat(x1, y1, r);
				getch();
				system("cls");
			}
			else if (l.x1 == -1)
				printf("Eroare. Sintaxa comanda: patrat(x,y,l).\n");
			else
				printf ("%s", eroare_comanda);
			break;
		case 4:
			x1 = interpreteazaValoarea(intrare, 1);
			y1 = interpreteazaValoarea(intrare, 2);
			x2 = interpreteazaValoarea(intrare, 3);
			y2 = interpreteazaValoarea(intrare, 4);
			if (x1 > -1 && y1 > -1 && x2 > -1 && y2 > -1) {
				system("cls");
				deseneazaDreptunghi(x1, y1, x2, y2);
				getch();
				system("cls");
			}
			else if (l.x1 == -1)
				printf("Eroare. Sintaxa comanda: dreptunghi(x1,y1,x2,y2).\n");
			else
				printf ("%s", eroare_comanda);
			break;
		case 5:
			x1 = interpreteazaValoarea(intrare, 1);
			y1 = interpreteazaValoarea(intrare, 2);
			x2 = interpreteazaValoarea(intrare, 3);
			y2 = interpreteazaValoarea(intrare, 4);
			if (x1 > -1 && y1 > -1 && x2 > -1 && y2 > -1) {
				system("cls");
				deseneazaTriunghi(x1, y1, x2, y2);
				getch();
				system("cls");
			}
			else if (l.x1 == -1)
				printf("Eroare. Sintaxa comanda: triunghi(x1,y1,x2,y2).\n");
			else
				printf ("%s", eroare_comanda);
			break;
		case 6:
			printf("Hexagon. Neimplementat.\n");
			break;
		case 7:
			printf("Pentagon. Neimplementat.\n");
			break;
		case 8:
			mesajHelp(interpreteazaHelp(intrare));
			break;
		case 9:
			system("cls");
			break;
		case 10:
			exit(0);
			break;
		default:
			printf ("%s", eroare_comanda);
			break;
		}
		free(intrare);
	}
}

int interpreteazaSirul(char *str) {
	if (!str) return 0;
	char* str2 = copiereSir(str);
	char *token = strtok(str2, " (\n");
	if (!token) return 0;
	int ret = 0;
	if (stricmp(token, "linie") == 0)
		ret =  1;
	else if (stricmp(token, "cerc") == 0)
		ret =  2;
	else if (stricmp(token, "patrat") == 0)
		ret =  3;
	else if (stricmp(token, "dreptunghi") == 0)
		ret =  4;
	else if (stricmp(token, "triunghi") == 0)
		ret =  5;
	else if (stricmp(token, "hexagon") == 0)
		ret =  6;
	else if (stricmp(token, "pentagon") == 0)
		ret =  7;
	else if (stricmp(token, "help") == 0)
		ret =  8;
	else if (stricmp(token, "clear") == 0)
		ret =  9;
	else if (stricmp(token, "exit") == 0)
		ret =  10;
	free(token);
	free(str2);
	return ret;
}

int interpreteazaHelp(char* str) {
	char* str2 = copiereSir(str);
	char *token = strtok(str2, " \n");
	token = strtok('\0', " \n");
	int ret = interpreteazaSirul(token);
	free(str2);
	free(token);
	return ret;
}

int interpreteazaValoarea(char *str, int poz) {
	char* str2 = copiereSir(str);
	char* token = strtok(str2, "(,");
	while(poz--)
		token = strtok('\0', ",");
	if (!token) return -1;
	int ret = atoi(token);
	free(str2);
	free(token);
	return ret;
}

void mesajHelp(int opt) {
	switch (opt) {
	case 1:
		printf("Sintaxa: linie(x1,y1,x2,y2)\n"
				"Descriere: Deseneaza o linie, in conditia in care x1<=x2 si y1<=y2\n");
		break;
	case 2:
		printf("Sintaxa: cerc(x,y,r)\n"
				"Descriere: Deseneaza un cerc cu coltul stanga sus (x,y), centru (x+r,y+r)\n si raza r\n");
		break;
	case 3:
		printf("Sintaxa: patrat(x,y,l)\n"
				"Descriere: Deseneaza un patrat de latura l, incepand\n cu coltul stanga sus (x,y)\n");
		break;
	case 4:
		printf("Sintaxa: dreptunghi(x1,y1,x2,y2)\n"
				"Descriere: Deseneaza un dreptunghi de la coltul stanga sus (x1,y1)\n pana la coltul dreapta jos (x2,y2)\n");
		break;
	case 5:
		printf("Sintaxa: triunghi(x1,y1,x2,y2)\n"
				"Descriere: Deseneaza un triunghi dreptunghic de la coltul stanga sus (x1,y1)\n pana la coltul dreapta jos (x2,y2)\n");
		break;
	case 6:
		printf("Comanda pentru hexagon. Neimplementat\n");
		break;
	case 7:
		printf("Comanda pentru pentagon. Neimplementat\n");
		break;
	case 8:
		printf("Sintaxa: help [argument]\n"
				"Descriere: Ofera sintaxa si descrieri pentru comenzile din program.\n Scrieti simplu 'help' pentru lista de comenzi\n");
		break;
	case 9:
		printf("Sintaxa: clear\n"
				"Descriere: Comanda ce elimina toate caracterele de pe ecran\n");
		break;
	case 10:
		printf("Sintaxa: exit\n"
				"Descriere: Comanda ce iese din program\n");
		break;
	default:
		printf("Lista de comenzi:\n"
				" linie\n"
				" cerc\n"
				" patrat\n"
				" dreptunghi\n"
				" triunghi\n"
				" hexagon\n"
				" pentagon\n"
				" help\n"
				" clear\n"
				" exit\n");
		break;
	}
}
