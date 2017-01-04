#include <iostream>
using namespace std;

enum Tip {
	evil,
	divine
};

class Mamifer {
protected:
	Tip tip;
	int dataNasterii;
public:
	Mamifer(Tip t, int d) : tip(t), dataNasterii(d) {};
	virtual void mananca() = 0;
	virtual void mergeLaBaie() = 0;
	virtual void hranesteAnimale() = 0;
	virtual ~Mamifer() {};
};

class Pisica : public Mamifer {
public:
	Pisica(Tip t, int d) : Mamifer(t, d) {};
	void hranesteAnimale() {};
	virtual void miauna() = 0;
	virtual ~Pisica() {
		cout << "Stergem pisica!\n";
	};
};

class Bunica : public Mamifer {
public:
	Bunica(Tip t, int d) : Mamifer(t, d) {};
	void mananca() {
		cout << "Mananca bunica\n";
	}

	void mergeLaBaie() {
		cout << "Bunica merge la baie\n";
	}

	void hranesteAnimale() {
		cout << "Bunica isi hraneste pisicile\n";
	}

	~Bunica() {
		cout << "Stergem bunica!\n";
	}
};

class PisicaSiameza : public Pisica {
public:
	PisicaSiameza(Tip t, int d) : Pisica(t, d) {};
	void mananca() {
		cout << "Mananca pisica siameza\n";
	}

	void mergeLaBaie() {
		cout << "Pisica siameza merge la baie\n";
	}

	void miauna() {
		cout << "Miau!\n";
	}
};

class PisicaDeCartier : public Pisica {
public:
	PisicaDeCartier(Tip t, int d) : Pisica(t, d) {};
	void mananca() {
		cout << "Mananca pisica de cartier\n";
	}

	void mergeLaBaie() {
		cout << "Pisica de cartier merge la baie\n";
	}

	void miauna() {
		cout << "Meew!\n";
	}
};

class PisicaEgipteana : public Pisica {
public:
	PisicaEgipteana(Tip t, int d) : Pisica(t, d) {};
	void mananca() {
		cout << "Mananca pisica egipteana\n";
	}

	void mergeLaBaie() {
		cout << "Pisica egipteana merge la baie\n";
	}

	void miauna() {
		cout << "Moo!\n";
	}
};

int main() {
	Bunica bunica(divine, 1);
	Pisica *p[3];
	p[0] = new PisicaDeCartier(evil, 1);
	p[1] = new PisicaEgipteana(divine, 2);
	p[2] = new PisicaSiameza(evil, 3);

	for (int i = 0; i < 3; i++)
		p[i]->miauna();

	Mamifer *x = new PisicaSiameza(divine, 5);
	dynamic_cast<PisicaSiameza*>(x)->miauna();

	system("pause");
	return 0;
}