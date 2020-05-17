# Amber
Endpointurile se pot vedea la http://localhost:5000/swagger/index.html<br/>
Coordonatele de plecare pentru tank-uri sunt arrays de 2 int<br/>
Harta Kursk e de 10 pe 10, am testat cu coordonatele de plecare din colturi opuse<br/>
Nu am testat mai multe jocuri odata, ar trebui sa mearga :)<br/>
Am pus un screenshot in radacina solutiei cu un sample de completare in swagger pentru declansarea unui joc<br/>
<br/>
<br/>
In mod ideal as fi pus seeding-ul bazei de mongo in composer, dintr-un fisier json si nu in cod.<br/>
Ii lipseste error handling si logging.<br/>
As fi revizuit metodele async.<br/>
Am lasat niste clase nefolosite in solutie, am incercat initial sa mimez referetierea gen sql prin ObjectIds si atribute, cu popularea obiectelor copil prin repository.

Inspiratie:
https://medium.com/@kristaps.strals/docker-mongodb-net-core-a-good-time-e21f1acb4b7b<br/>
https://morioh.com/p/fe249dd19cc1<br/>
https://medium.com/swlh/containerize-asp-net-core-3-1-with-docker-c5e1acabba21<br/>
Repo: https://medium.com/@marekzyla95/mongo-repository-pattern-700986454a0e<br/>
Algorhitm https://gist.github.com/Amitkapadi/b13ee4a68afba3a5ace7497b25e11fca<br/>
