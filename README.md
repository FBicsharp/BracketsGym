

# Cleaning Brackets Gym

## Task given
User can input multiple strings, one per line,
and click on a button to obtain the same strings, with all the external matching round brackets
removed. Few examples of input and output are:
Input -> Output
(abc) ->abc
((abc)) -> abc
(abc -> (abc
() -> Empty string
(ab) (cd) -> (ab) (cd)
((ab) (cd)) -> (ab) (cd)
ab(cd) -> ab(cd)


## Task Analitics
The input is a string, and the output is a string too so 
the input function must accept multiple string at time for optimize the http request (time for handshaking and serializing data) , 
in this case i covered all needs.
for this task  I assume that the brackets is only ( and ), in case there was other brackets requiremensts i prepare the code base for handel it but i do not expose this feature via api becouse is not requested.
I assume that the string lenght is short (less then 10 char) and the number of string is a few hundred per request.
Condisering the test case i suppose this following rules:
	- if the brackets are not balanced the output is the same as given input 
	- if the brackets are balanced the output is the cleaned string without any extra brackets, the extra brackets are removed untill the string is balanced.
	
I assuming that the authentication process is not required for this task, 
so i do not implement it becouse is not requested, the site should be installed on premise or under other authetication or authorization stystem.

## Architecture and logic
I had decide to separate the frontend and backend for have a better scalability and flexibility of installation in various environments.
The cons of this structure is that when we need to implemente a new feature we need to change the code base in both side, but in this case is not a problem becouse the api is very simple and the frontend is very simple too.
I add a swashbuckle for semply the api  debugging so it will not be available in pruduction environment.

### FONTEND:
Web page is  builded with blazor web assembly with NET 6.0 and installed as PWA (progressive web application), 
the site running as docker container ,in this way we have the same code languages for the frontend and backend, and I chose last LTS support version of NET for have the best performance and security.
whit his framework we reduced the http traffic as installed the pwa application directly on browser so for the next time we have less data to transfer on client.


### BACKEND:
The api is implemented with NET 6.0 and running as docker container, the api have only 1 endpoint that accept a post request with a json body.
I assume that the site and api will be available only on the same domain, so i do not implement any cors policy, but if the site and api will be on different domain we need to implement a cors policy for allow the site to call the api.
I assume that the api will be available only on https protocol, so i do implement a http redirection to https only for the api, but if the api will be available on http protocol we need to remove this redirection.









// nel caso di molte richieste si potrebbe aggiungendo l'utilizzo di kubernetes per avere un sistema di orchestrazione e deploy piu semplice e veloce e avere un sistema di scalabilita piu semplice e veloce sotto un load balancer per avere un sistema di failover e load balancing piu semplice e veloce 
in modo da aumnetarne sia la resilienza che la scalabilita e avere anche un sistema ridondante.
se invece si vuole mantenere la struttura attuale  si potrebbe aggiungere un sistema di cache per avere un sistema di queuing per gestire le richieste in modo sequenzaile penalizzando in caso di pacchi massimi la richiesta,
in ogni caso con quest'ultimo si avrebbe un sistema limitato



# PDF
trattandosi di parople assumo che le stringhe non supereranno mai i 45 caratteri (la parola più lunga a mondo è di 45 https://www.dictionary.com/e/longest-words-in-the-world/)  devono essere con una lunghezza massima proporzionale alla spiralke in cui si trova,
se cosi non fosse , devrebbero essere generati più pdf , inoltre la struttura impone un limita sul numero di parola stampabi pari alla lunghezza della paroila ossia 1< x <=45 considerando che la spirale ha 4 lati  il numero di spirali sarà quini massimo a 45/4  

le parole della stessa lunghewzza devono essere raggruppate per lunghezza?

uso un servizio zpl di terze parti che gira in locale [link zpl](https://www.neodynamic.com/products/zpl-printer-web-api-docker/)



# how to install and run
# docker compose up -d