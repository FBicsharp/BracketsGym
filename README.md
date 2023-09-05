[![Main test](https://github.com/FBicsharp/BracketsGym/actions/workflows/main.yml/badge.svg?branch=master)](https://github.com/FBicsharp/BracketsGym/actions/workflows/main.yml) 
# CLEANING 

## Brackets 
User can input multiple strings, one per line,
and click on a button to obtain the same strings, with all the external matching round brackets
removed. 
Few examples of input and output are:
> - Input -> Output
> - (abc) ->abc
> - ((abc)) -> abc
> - (abc -> (abc
> - () -> Empty string
> - (ab) (cd) -> (ab) (cd)
> - ((ab) (cd)) -> (ab) (cd)
> - ab(cd) -> ab(cd)

### Task Analitics
The input is a string, and the output is a string too so 
the input function must accept multiple string at time for optimize the http request (time for handshaking and serializing data) , 
in this case i covered all needs.
For this task  I assume that the brackets is only ( and ), in case there was other brackets requiremensts i will prepare the code base for handle 
it but i do not expose this feature via api becouse is not requested.

I assume that the string lenght is short (less then 50 char) and the number of string is a single or multiple per request.
Condisering the test case i suppose this following rules:
	- if the brackets are not balanced the output is the same as given input 
	- if the brackets are balanced the output is the cleaned string without any extra brackets, the extra brackets are removed untill the string is balanced.
	- the input string must be not empty


## Pairs-en

User can input multiple strings, one per line,
and click on a button to obtain the same strings, with all the external matching letter pairs of the english alphabeth
removed. 
Few examples of input and output are:
if the Pair letter is the following:
> - az
> - by
> - cx
> - dw
> - ev
> - fu
> - gt
> - hs
> - ir
> - jq
> - kp
> - lo
> - mn

The input and output must be:

> Input -> Output
> - man -> a
> - keep -> ee
> - gqwertyuioplkjhgfdsazxcvbnm:?t -> qwertyuioplkjhgfdsazxcvbnm:?
> - abcdefghijklmnopqrstuvwxyz ->  Empty string

### Task Analitics
The input is a string and the output is a string too so 
the input function must accept multiple string at time for optimize the http request (time for handshaking and serializing data) , 
in this case i covered all needs.

For this task  I assume that the character pais should be processend only one time in a given order, so for each pairs of character given I had to check  is at the first and last character
of the string matching the pair, if yes i remove it and i check the next pair.
Only if the first and last character of the string is matching the fist and last character of the pair i remove it and i check the next pair.
The pairs and string is all lower case so I so i need to check the case of the character for matching .
The valid pairs at te moment is only the pais specified in the task, but if we need in the future tio change it i will prepare the code base for handel it but i do not expose this feature via api becouse is not requested.
I assume that the string lenght is short (less then 50 char) and the number of string is a single or multiple per request.

Condisering the test case i suppose this following rules:
	- if the first and last character of the string is matching the fist and last character of the pair i remove it and i check the next pair.
	- the check is case sensitive
	- the input string must be not empty

## PDF generation
Create a button that generates a PDF file containing a spiral with the  processed strings by Pairs-en pages.

Condisering this following rules:
	- the spirla must be quadratic 
	- the spiral direction and start point is free 
	- if the character has a pair of the same lenght the pair must be rounded by a dashed character
	- the process must be asyncronous
	- ths user do not must wait the process for download the pdf

### Task Analitics
The input string should not exceed 45 char, becouse the longest word in the world is 45 char long [longhest world](https://www.dictionary.com/e/longest-words-in-the-world/).
The spiral imposes a limit in cases where there are multiple strings of the same length, they would overlap, so to prevent this multiple PDF's could be generated 
by dividing the strings (but the information would not be in a single spiral). Therefore, in cases when there are multiple strings of the same length, 
they must be processed by changing their length (adding extra space characters at the beginning) to make them appear graphically as a spiral. 
The string cannot be empty."


## Architecture and logic
I had decide to separate the frontend and backend for have a better scalability and flexibility of installation in various environments 
and in the future we can reuse the backend funtionality for other process.
The cons of this structure is that when we need to implemente a new feature we need to change the code base in both side, but in this case is not a problem 
becouse the api is very simple and the frontend is very simple too.

I add a swashbuckle for simply the api  debugging so it will not be available in pruduction environment.
I assuming that the authentication process is not required for this task, 
so i do not implement it becouse is not requested, the site should be installed on premise or under other authetication or authorization stystem.


### FONTEND:
Web page is  builded with blazor web assembly with NET 6.0 and could be installed as PWA (progressive web application), 
the site running as docker container ,in this way we have the same code languages for the frontend and backend, and I chose last LTS support version of NET for 
have the best performance and security.
with his framework we reduced the http traffic as installed the pwa application directly on browser so for the next time we have less data to transfer on client.


### BACKEND:
The api is implemented with NET 6.0 and running as docker container, the api multiple endpoint that accept a post request with a json body for the strings.
I suppose this endpoind,one per function:
- /cleanbrackets
	- accept a list of string and return a list of string
- /cleanpairs-en
	- accept a list of string and return a list of string
- /topdf
	- accept a list of string and return a file content

I assume that for this purpose structure will be available on http protocol, but if the api will be available on https protocol we need to configure it.


# HOW TO RUN 

## REQUIREMENTS 
- docker
- docker compose version 3.8 or higher- 

## SETP UP
Download the repository, on project folder and run the docker compose command
```sh
docker compose up -d
```

the site will be available on http://localhost:33000 and the api on http://localhost:33500
```sh
http://localhost:33000
```
```sh
http://localhost:33500
```


# NOTE
> For the pdf generation i use a not free library that have a watermark on the pdf, if you want to remove it you need to buy a license.
> Unfortunatly this take a few minute to startup response at the first request, becouse i suppose that some license check is performed at the first request.

 
# ARCHITECTURE REVIEW 

In the case of many requests, it could be beneficial to introduce the use of Kubernetes to achieve a simpler and faster orchestration and deployment system. 
This would also provide an easier and quicker scalability system under a load balancer, enhancing both resilience and scalability and ensuring redundancy.

Alternatively, if you wish to maintain the current structure, you could consider adding a caching system to establish a queuing system for handling requests sequentially, 
potentially penalizing requests in case of peak loads. However, it's worth noting that this latter approach would still have limitations
The benefit of use docker is that we can run it everyware and we can have the same environment on every machine, so we can have the same result on every machine.
If we use the cloud providere such as azure or aws we can use the container service for have a better scalability and resilience, in this case we have a payment for a service that we can use only on cloud provider.
THe clould offer a lot of service that semplyfile interaction with various service such as database, storage, queue, etc. so it depends of the application that we need to develop.
if we need to develop a simple application that do not need a lot of interaction with other service we can use a simple docker container on a simple vm,
but if we need to develop a complex application that need to interact with other service we can use a cloud provider .
The other point o consider security, if we use a cloud provider we can use the security service that the cloud provider offer, 
but if we use a simple vm on premise we need to implement the security by ourself.



# TODO 

## BACKEND
- adding test runner on docker build
- add api documentation 

## FRONTEND
- adding unit test to UI
- adding test runner on docker build
- add tost notification or loading spinner 
