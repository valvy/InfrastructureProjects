# Het gebruiken van het Web in Python
Tegenwoordig wordt HTTP(s) steeds meer gebruikt om data uit te wisselen tussen applicaties. 

## Terminologie
1. Endpoint, het adres(of URL) van de API. Dit is vergelijkbaar als google.com of duckduckgo.
2. [JSON](https://nl.wikipedia.org/wiki/JSON), Javascript Object Notation. Hiermee kunnen we makkelijk door de data navigeren en uitlezen.

## Wat doet dit programma
1. Dit voorbeeld doet een request naar een API (De API geeft JSON terug)
2. Daarna wordt deze JSON geconverteerd naar iets wat Python kan gebruiken
3. We pakken een willekeurig nummer
4. Op basis van de resultaat  en het willekeurig nummer wordt een een kat feitje geprint

 
```python
#!/usr/bin/env python
# Author Heiko van der Heijden
# h.j.m.van.der.heijden@hva.nl
# A small example printing cat facts to the terminalscreen
# The cat facts are received from https://alexwohlbruck.github.io/cat-facts
from urllib.request import urlopen
import json.tool
from random import randint
from json import loads as jsonLoader

# The endpoint of the API
CAT_FACTS_API_END_POINT = "https://cat-fact.herokuapp.com/facts"

# Some magic numbers regarding json input from the request
JSON_ALL_FACTS = "all"; JSON_ALL_TEXT = "text"

# Do a request to receive the data
jsonContents = jsonLoader(urlopen(CAT_FACTS_API_END_POINT).read())

# Pick a random cat fact
randomCatFactNumber = randint(0,len(jsonContents[JSON_ALL_FACTS]))

# Print it
print(jsonContents
        [JSON_ALL_FACTS] # Get the json elemnt containing the facts
        [randomCatFactNumber] # Pick a random one
        [JSON_ALL_TEXT] # Pick the text
    )

```