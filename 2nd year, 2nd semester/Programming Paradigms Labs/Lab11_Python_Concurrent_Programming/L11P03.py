def byFreq(pair):
	return pair[1]

def test():
	main()

def main():
	print("This program analyzes word frequency in a file")
	print("and prints a report on the n most frequent words.\n")

	fname = raw_input("File to analyze: ")
	"print(fname)"
	text = open(fname, 'r').read()	
	print text
	text = text.lower()

	for ch in '!"#$%&()*+,-./:;<=>?@[\\]^_`{|}~':
		text = text.replace(ch, ' ')
		words = text.split()

	# construct a dictionary of word counts
	counts = {}

	for w in words:
		counts[w] = counts.get(w,0) + 1

	# output analysis of n most frequent words
	print ""
	n = input("Output analysis of how many words? ")
	items = list(counts.items())
	items.sort()
	items.sort(key = byFreq, reverse = True)

	for i in range(n):
		word, count = items[i]
		print("{0:<15}{1:>5}". format(word, count))
		
if __name__ == '__main__': test()