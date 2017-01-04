from graphics import *
import sys
import re

class Hangman:
	def __init__(self, window, leftpoint, downpoint):
		self.leftpoint = leftpoint
		self.downpoint = downpoint
		self.window = window
		self.step = 0

	def drawHinges(self):
		self.hinge1 = Line(Point(self.leftpoint, self.downpoint), Point(self.leftpoint+1, self.downpoint))
		self.hinge2 = Line(Point(self.leftpoint+0.5, self.downpoint), Point(self.leftpoint+0.5, self.downpoint+4))
		self.hinge3 = Line(Point(self.leftpoint+0.5, self.downpoint+4), Point(self.leftpoint+2, self.downpoint+4))
		self.hinge4 = Line(Point(self.leftpoint+2, self.downpoint+4), Point(self.leftpoint+2, self.downpoint+3.5))
		self.hinge1.draw(self.window)
		self.hinge2.draw(self.window)
		self.hinge3.draw(self.window)
		self.hinge4.draw(self.window)

	def drawHead(self):
		self.head = Circle(Point(self.leftpoint+2, self.downpoint+3), 0.5)
		self.head.setOutline("black")
		self.head.draw(self.window)

	def drawTorso(self):
		self.torso = Line(Point(self.leftpoint+2, self.downpoint+2.5), Point(self.leftpoint+2, self.downpoint+0.9))
		self.torso.draw(self.window)

	def drawLeftHand(self):
		self.leftHand = Line(Point(self.leftpoint+2, self.downpoint+2), Point(self.leftpoint+1.25, self.downpoint+1.25))
		self.leftHand.draw(self.window)

	def drawRightHand(self):
		self.rightHand = Line(Point(self.leftpoint+2,self.downpoint+2), Point(self.leftpoint+3,self.downpoint+3))
		self.rightHand.draw(self.window)

	def drawLeftLeg(self):
		self.leftLeg = Line(Point(self.leftpoint+1.5, self.downpoint+0.25), Point(self.leftpoint+2, self.downpoint+1))
		self.leftLeg.draw(self.window)

	def drawRightLeg(self):
		self.rightLeg = Line(Point(self.leftpoint+2.5, self.downpoint+0.25), Point(self.leftpoint+2, self.downpoint+1))
		self.rightLeg.draw(self.window)

	def drawNext(self):
		if self.step == 0:
			self.drawHinges()
		elif self.step == 1:
			self.drawHead()
		elif self.step == 2:
			self.drawTorso()
		elif self.step == 3:
			self.drawLeftHand()
		elif self.step == 4:
			self.drawRightHand()
		elif self.step == 5:
			self.drawLeftLeg()
		elif self.step == 6:
			self.drawRightLeg()
		else:
			pass
			return
		self.step += 1

	def drawnEverything(self):
		if self.step == 7:
			return True
		else:
			return False

def main():
	win = GraphWin("Hangman")
	win.setCoords(0.0, 0.0, 10.0, 10.0)
	hangman = Hangman(win, 1, 5)
	text = Text(Point(5,0.5), "Word goes here")
	text.draw(win)
	won = False
	if len(sys.argv) == 2:
		wordToGuess = sys.argv[1]
		wordToDisplay = re.sub('[a-zA-Z]', '_ ', sys.argv[1])[:-1]
		text.setText(wordToDisplay)
		hangman.drawNext()
		wordToGuessList = list(wordToGuess)
	else:
		for i in range(7):
			hangman.drawNext()
		text.setText("Parametru lipsa")
		won = True		
	while won == False: 
		key = win.getKey()
		indexes = []
		if wordToGuess.find(key) == -1:
			hangman.drawNext()
			if hangman.drawnEverything() == True:
				text.setText("Ai pierdut!")
				won = True
			continue
		for i, ltr in enumerate(wordToGuess):
			if ltr == key:
				indexes.append(i) # sau extend, cu care poti baga alte liste sa extinda lista
								  # initiala; cu append adaugi toata lista ca o sublista
		wordToDisplayList = list(wordToDisplay)
		for i in indexes:
			wordToDisplayList[i*2] = wordToGuessList[i]
		wordToDisplay = "".join(wordToDisplayList)
		text.setText(wordToDisplay)
		if wordToDisplay.replace(" ", "") == wordToGuess:
			text.setText("Ai castigat!")
			won = True
	win.getKey()
	win.close()
main()

