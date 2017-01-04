/*
 * String.c
 *
 *  Created on: 19 feb. 2015
 *      Author: Dragos
 */

#include "Sir.h"
#include <stdlib.h>
#include <stdio.h>
#include <string.h>

#define MAXLINE 78

char* copiereSir(char *src) {
	char* dst = 0;
	dst = malloc(sizeof(char)*(strlen(src)+1));
	if (!dst) {
		fprintf(stderr, "[EROARE][COPIERESIR] Alocarea de memorie pt. sirul destinatie a esuat\n");
		return 0;
	}
	strcpy(dst, src);
	return dst;
}

char* citesteSir(char *msg) {
	char *str = 0;
	str = malloc(sizeof(char)*(MAXLINE+1));
	if (!str) {
		fprintf(stderr, "[EROARE][CITESTESIR] Alocarea de memorie pt. sirul de citit a esuat\n");
		return 0;
	}
	printf("%s", msg);
	fgets(str, sizeof(char)*(MAXLINE+1), stdin);
	str[strlen(str)-1] = '\0';
	return str;
}
