from graphics import *
import random
import tkMessageBox

class Button:
	"""	A button is a labeled rectangle in a window
		It is activated or deactivated with the active()
		and deactive() methods. The clicked(p) method
		returns true if the button is active and the p is inside i. """

	def __init__(self, win, center, width, height, label):
		""" Creates a rectangular button, eg:
		qb = Button(myWin, centerPoint, width, height, 'Quit') """
		w,h = width/2.0, height/2.0
		x,y = center.getX(), center.getY()
		self.xmax, self.xmin = x+w, x-w
		self.ymax, self.ymin = y+h, y-h
		p1 = Point(self.xmin, self.ymin)
		p2 = Point(self.xmax, self.ymax)
		self.rect = Rectangle(p1, p2)
		self.rect.setFill('lightgray')
		self.rect.draw(win)
		self.label = Text(center, label)
		self.label.draw(win)
		self.deactivate()

	def clicked(self, p):
		"Returns true if button active and p is inside"
		return (self.active and
				self.xmin <= p.getX() <= self.xmax and
				self.ymin <= p.getY() <= self.ymax)

	def getLabel(self):
		"Returns the label string of this button"
		return self.label.getText()

	def setLabel(self, text):
		self.label.setText(text)

	def activate(self):
		"Sets this button to 'active'."
		self.label.setFill('black')
		self.rect.setWidth(2)
		self.active = True

	def deactivate(self):
		"Sets this button to 'inactive'."
		self.label.setFill('darkgrey')
		self.rect.setWidth(1)
		self.active = False

class Puzzle:
	buttons = []
	numbers = [[0 for i in range (4)] for i in range (4)]

	def __init__(self, window):
		self.window = window

	def generate(self):
		numbersTaken = [False for i in range(15)]
		for i in range(4):
			for j in range(4):
				if i == 3 and j == 3:
					continue
				ok = False
				while ok == False:
					random.seed()		
					chosenNumber = random.randint(0,14)
					#print str(chosenNumber) + " <- " + str(i) + ", " + str(j)
					if numbersTaken[chosenNumber] == False:
						numbersTaken[chosenNumber] = True
						self.numbers[i][j] = chosenNumber + 1
						ok = True

	def draw(self):
		for i in range(4):
			for j in range(4):
				if i == 3 and j == 3:
					text = " "
				else:
					text = self.numbers[i][j]
				toAdd = Button(self.window, Point(j*2.5+1.4, i*2.5+1.175), 2.5, 2.5, text)
				self.buttons.append(toAdd)
				if i != 3 or j != 3:
					toAdd.activate()

	def loop(self):
		while (self.detectWin() == False):
			clickLocation = self.window.getMouse()
			for button in self.buttons:
				if button.clicked(clickLocation) == True:
					x,y = self.detectEmpty(button)
					#print "Empty detected: " + str(x) + ", " + str(y)
					if x == -1 and y == -1:
						break

					buttonToSwitch = self.getButtonFromIndex(x,y)

					a,b = self.getIndexFromButton(button)
					aux = self.numbers[x][y]
					self.numbers[x][y] = self.numbers[a][b]
					self.numbers[a][b] = aux

					aux = buttonToSwitch.getLabel()
					buttonToSwitch.setLabel(button.getLabel())
					button.setLabel(aux)
					button.deactivate()
					buttonToSwitch.activate()
		tkMessageBox.showinfo("Hurray", "You won!")

					
	def detectWin(self):
		x = 1
		for i in range(4):
			for j in range(4):
				if i == 3 and j == 3 and self.numbers[i][j] == 0:
					return True
				if self.numbers[i][j] == x:
					x += 1
				else:
					return False

	def getIndexFromButton(self, button):
		for i in range(4):
			for j in range(4):
				if button.getLabel() == " " and self.numbers[i][j] == 0:
					return i,j
				if button.getLabel() == self.numbers[i][j]:
					return i,j

	def getButtonFromIndex(self, i, j):
		for button in self.buttons:
			if self.numbers[i][j] == 0 and button.getLabel() == " ":
				return button
			if button.getLabel() == self.numbers[i][j]:
				return button

	def detectEmpty(self, button):
		indexH, indexV = self.getIndexFromButton(button)
		#print "Button clicked: " + str(indexH) + ", " + str(indexV)
		#print "Value: '" + str(button.getLabel()) + "'; Number: " + str(self.numbers[indexH][indexV])
		if indexH < 3 and self.numbers[indexH+1][indexV] == 0:
			return (indexH + 1, indexV)
		if indexV < 3 and self.numbers[indexH][indexV+1] == 0:
			return (indexH, indexV + 1)
		if indexH > 0 and self.numbers[indexH-1][indexV] == 0:
			return (indexH-1, indexV)
		if indexV > 0 and self.numbers[indexH][indexV-1] == 0:
			return (indexH, indexV-1)
		return (-1,-1)

def main():
	win = GraphWin("Number Puzzle")
	win.setCoords(0.0, 0.0, 10.1, 10.1)
	puzzle = Puzzle(win)
	puzzle.generate()
	puzzle.draw()
	puzzle.loop()	

	win.getKey()
	win.close()
main()
