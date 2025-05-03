# PROJEKTBESKRIVNING
Projektuppgiften går ut på att skapa ett enkelt system för ett fiktivt bibliotek. Applikationen
kommer att användas av bibliotekspersonal för att hantera bibliotekets samling av böcker och
dess kunder.

Applikationen kan utvecklas i version 1 eller version 2. Alternativt kan du borja med version 1
och sedan utoka den till version 2. Version 1 kan ses som en grundversion som kan uppgraderas
till version 2.

## Kravbeskrivning och Specifikationer för version 1
### Grundläggande funktioner
Biblioteket hanterar böker och kunder. Applicationen ska innehålla funktioner för att hantera 
utlåning och återlämning av böcker. I version 1 ska du skapa en klass som representerar en 
bok (**Book**), en klass som representerar en kund (**Customer**) samt en klass för att 
hantera all logik och data (**LibraryLogic**).
### Funktioner för biblioteket
- Registrera nya böcker med titel, författare och unikt ISBN-nummer.
- Ta bort böcker från systemet om de inte längre finns tillgängliga.
- Registrera nya kunder med namn och unikt kund-ID.
- Ta bort kunder från systemet.
- Utlåna en bok till en specifik kund.
- Återlämna en bok och markera den som tillgänglig igen.
### Funktioner för en specifik kund
- Se information om kunden, inklusive lånade böcker.
- Låna nya böcker (om de är tillgängliga).
- Återlämna böcker.

## Klassdesign
### Rekomenderade metoder
Följande är rekomenderade publika metoder som bör inkluderas i klasserna för att säkerställa
funktionalitet:
- **public string GetBookDetails(string ISBN)**: Returnerar detaljerad information om en
  specifik bok baserat på ISBN.
- **public List GetCustomerLoans(string customerID)**: Returnerar en lista över böcker som
  en specifik kund har lånat.
- **public bool ReserveBook(string ISBN, string customerID)**: Låter en kund reservera en
  bok som är utlånad.

## Klass: Book
Hantera följande information:
- Titel
- Författare
- ISBN (unikt)
- Status (tillgänglig/utlånad)
Metoder för att hämta information om boken och ändra dess status (t.ex. tillgänglig eller
utlånad) ska implementeras.

## Klass: Customer
Hantera följande information:
- Kundens namn
- Kund-ID (unikt)
- En lista över lånade böcker
Metoder för att lägga till, ta bort och visa lånade böcker ska implementeras.

## Klass: LibraryLogic
Hantera en lista över alla böcker och kunder. Klassen ska innehålla publika metoder för att
hantera böcker och kunder, som t.ex.:
- **public List GetBooks()**: Returnerar en lista med alla böcker (inlkusive information om
  deras status).
- **public bool AddBook(string title, string author, string ISBN)**: Lägger till en ny bok i
  biblioteket om ISBN är unikt.
- **public bool RemoveBook(string ISBN)**: Tar bort en bok från biblioteket baserat på dess
  ISBN.
- **public bool LoanBook(string ISBN, string customerID)**: Markerar boken som utlånad till
  en specifik kund.
- **public bool ReturnBook(string ISBN)**: Markerar boken som återlämnad och tillgänglig.

## Felhantering
Programmet ska hantera fel på ett robust sätt:
- Om användaren försöker låna en bok som redan är utlånad ska ett meddelande visas.
- Om en kund försöker återlämna en bok som inte är utlånad ska systemet ge en varning.

## Krav och specifikationer för version 2
Version 2 ska inkludera följande utlösningar:
- **Beräkna förseningsavgifter** för sena återlämningar. Avgiften beror på antalet dagar som
  boken är sen (exempelvis 10kr/dag).
- **Möjlighet att söka efter böcker** baserat på titel eller författare.
- **Hantera reserveringar** av böcker som är utlånade.
- **Generera rapporter** som visar alla lånade böcker och vilka kunder som har dem.

## Checklista
På projektarbetet ska följande krav uppfyllas:
Studenterna ska testa applikationen noggrant innan det muntliga testet. Under det muntliga
testet ska studenterna visa att applikationen fungerar som förventat genom att demonstrera
dess funktioner och lösningar.
### Generella krav:
- Applikationen ska kunna köras i Visual Studio utan några fel.
- Applikationens funktionalitet ska kunna användas från konsolfönstret eller det grafiska
  gränssnittet.
### Version 1:
- Registrering av nya böcker med titel, författare och unikt ISBN.
- Radering av böcker som inte längre är tillgängliga.
- Registrering av nya kunder med namn och unikt kund-ID.
- Radering av kunder.
- Utlåning av böcker till en specifik kund.
- Återlämning av böcker.
- Visa information om kunder och deras lånade böcker.
### Version 2:
- Förseningsavgifter ska kunna beräknas och visas vid sena återlämningar.
- Sökning efter böcker baserat på titel eller författare.
- Reservering av utlånade böcker.
- Generering av rapporter som visar lånade böcker och deras kunder.
### Kodkrav:
- Koden ska följa objektorienterade principer.
- En skriftlig rapport ska lämnas in som beskriver implementeringen, inklusive ett UML-
  diagram som visar klassinteraktionen.

## Inlämning
Följande ska lämnas in via Canvas:
1. **Projektkoden.**
2. En **projektrapport** (max 3 sidor) som innehåller:
   - En beskrivning av lösningen.
   - Ett UML-diagram som visar klassernas samverkan.
*Rapportmallen ska följas (Mallen finns på Canvas)*

## Muntligt Test
Alla studenter ska delta i ett **obligatoriskt muntligt test** efter inlämningen. Testet
innefattar:
- En presentation av projektet.
- Frågor om implementation och designval.
- Demonstration av applikationens funktioner.
Testet syftar till att verifiera studentens förståelse och att projektet är egenproducerat.

## Bedömning och Återkoppling
Tabellen nedan visar bedömningen av projektet:
| **Version** | **Console** | **UWP (Grafisk)** |
| ----------- | ----------- | ----------------- |
| **Version 1** | Betyg 3 | Betyg 4 |
| **Version 2** | Betyg 4 | Betyg 5 |

### Andra faktorer som kan påverka projektbetyget:
- Rapporten
  - Kvalitet i rapporten kan påverka betyget, alltså korrekt klassdiagram, bra beskrivning
    av lösningar, m.m.
- Buggar
  - Om applikationen har större och flera buggar.
- Felhantering/Validering av inputvärde
  - Är det lätt för en användare att krascha applikationen genom att mata in fel inputvärde?
- Användarvänlig applikation
  - Är det tydligt för användare hur han/hon ska använda applikationen för olika syften?
    Eller ska användaren gissa hur man tar sig vidare och navigera mellan olika val?
