from graphics import *
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

def main():
	win = GraphWin("A Button");
	win.setCoords(0.0, 0.0, 10.0, 10.0)
	button = Button(win,Point(5,5), 5, 3, "Hello")
	button.activate()	
	while(True):
		ret = button.clicked(win.getMouse())
		if (ret == True):
			button.setLabel("Clicked!")
		else:
			button.setLabel("Missed!")
	win.close()
main()
