#include <stdio.h>

int main(void) {
	char s[100];
	printf("Introduceti un sir de caractere: ");
	fflush(stdout);
	scanf("%s", s);
	printf("Sirul de caractere introdus este: %s", s);
	return 0;
}
