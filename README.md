# DevOpsCSharp2324EP3

voor het  opstarten van de applicatie: 
De locatie loopt nog lokaal op een mysql databank dus afhankelijk van je systeem zal je de juiste connectionstring moeten gebruiken. 
Dit pas je aan in de appsettings van het Server project bij DefaultConnection. 

DefaultConnection:
 // Windows (when installed Visual Studio 2022+:
    "SqlServer": "Server=(localdb)\\mssqllocaldb;Database=svk;Trusted_Connection=True;",
    // Docker
    //"SqlServer": "Data Source=localhost,1433;Database=svk;User ID=SA;Password=p@ssw0rd;TrustServerCertificate=true"

Dan moet je met visual studio het server project opstarten voor de web app klaar te krijgen. 

Afhankelijk van met welk account je bent aangemeld kan je verschillende pagina's bekijken / handelingen maken. 

Account met read rechten: 
 - email: test@test.com
 - passwoord: Test.123

Account met read/write rechten: 
 - email: lader@lader.com
 - passwoord: Lader.123
