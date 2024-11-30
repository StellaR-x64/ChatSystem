# ChatSystem
SVARĪGI: uz viena porta drīkst būt tikai viens serveris atvērts(client aplikācijas var slēgties cik grib), piemēram ja serveris ir uz port 5000, tad vēlvienu serveri tur vairs nedrīkstēs veidot, jāizvēlas cits ports. 
ClientSolution un ServerSolution ir paši pirmie (var teikt ka alfa testēšana) protoipi kad iesāku šo projektu, tālu no izstrādāta projekta, bet to pašu funkciju vien pilda (sarakstīties var, bet tikai ja vairāki ClientSolution ir pieslēgti pie servera, jo no paša ServerSolution nevar sūtīt isziņas).

Kā testēt/izmantot šo programmu: No sākuma nepieciešams ielādēt šo "ChatSystem" mapi kopā ar "ChatSystem.sln" un jāatver "ChatSystem.sln" (ieteicams vairākus lai pilnība izstestētu kā šī programma strādā), un tas arī viss. Tālāk var droši sarakstīties cik vien daudz gribās.

Lietas ko gribēti pielabot vai uzkodēt: kā jau redzams aplikācijā tur ir pogas "Paint, Change to UDP, Use encryption" kuras pašlaik nestrāda jo nevarēju paspēt uzstaisīt vai arī nezināju kā(nebiju domājis ka šis projekts aizņems tik daudz laika). Kā arī sānā var redzēt paneli ar dažādiem "User 1, 2, 3, 4", bij doma uzstaisīt tāda ka tur rādīsies visas pievienotās Client aplikācijas, bet diemžēl nevarēju saprast kā to abūt gatavu.

Alpha/Beta testēšana: Sākumā biju testējis visu viens, jo nebij vajadzības citiem dot testēt un arī vieglāk pašam atrast visas problēmas un atrisināt, bet uz beigām mēģināju ar draugiem izstestēt vai strādās online sarakstīšanās, bet tas tikai strādāja izmantojot port forwarding.

Uzsturēšana: Nekādu plānu pilnveidot vai uzsturēt šito projektu nav, varbūt kad kļūšu labāks, atgriezīšos un pabeigšu visu ko gribēju uzstaisīt vai vienkāršī lai izstestētu kautkādu jaunu konceptu kas man likās interesants. 

patēretais laiks, kā arī personīgie ieguvumi: Šim projektam esmu patērējis apmēram 100 - 140 stundām (20 dienas aizņēma) un esmu daudz ko  ieguvis strādājot uz ši projekta. Līdz šim nekad baigi neesmu strādājis ar c#, kā arī nekad nebiju eksperimentējis ar "networking", kautkas ko es ļoti ilgi gribēju pamēģināt, ieguvu jaunus konceptus/pamatus, piemēram kā strāda "encryption", kas ir interneta protokols (TCP/UDP).Kā dators sūtot citam datoram sadala šos datus mazos gabalos ko saudz par paketēm. Kā paketes tiek nosūtītas un piegādatas (piemēram ja kurjers kļudās tad tas pats TCP protokols pieprasa vēlreiz sūtīt šīs zaudētās/bojātās paketes) un arī kā paketes ceļo cauri maršrutētājiem lai nonāktu līdz savam gala mērķim jau iepriekš aprēķinot ātrāko  ceļu līdz tam.
Bija ļoti interesanti lasīt par to visu un mēģināt to visu pielietot.

Grūtības: protams bij arī savas grūtības, pirmo reizi uzsākot šo projektu, vispār nezināju ko daru, kur vispār sākt, bet nu grūtākais šķērslis priekš manis bija tāds kad es mēģināju uzstaisīt to server un client aplikāciju, jo nu vispār nevarēju saprast kā to dabūt gatavu, kā tas ir iespējams, gan jau arī aizņemā viss ilgāk, teiktu ka puse no patērētā laika aizgāja tikai uz šī.

Izstrādes līdzekļi: ļoti bieži izmantoju "https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/sockets/tcp-classes" šo mājaslapu, kā arī Youtube lai vispār saprastu šo "networking" procesu.
Mājaslapas/video kuri vislabāk palīdzēja saprast: "https://www.geeksforgeeks.org/tcp-server-client-implementation-in-c/"
 "https://www.geeksforgeeks.org/basics-computer-networking/"
"https://learn.microsoft.com/en-us/visualstudio/get-started/csharp/tutorial-wpf?view=vs-2022"
"https://learn.microsoft.com/en-us/training/modules/network-fundamentals/" 
"https://youtu.be/keeqnciDVOo?si=Sipw3UUsrgc0NJse"
"https://www.youtube.com/playlist?list=PLIFyRwBY_4bRLmKfP1KnZA6rZbRHtxmXi"
"https://youtu.be/tSodBEAJz9Y?si=c1155JAZoIfMtDiN"

Mērķauditorija: šī programm (ja būtu tiešām sakarīga) būtu domāta kā vienkārša čatošanas vietne, līdzigi kā whatsapp vai discord.
Projekta Ideja: No sākuma vispār bij doma veidot Unity spēli, bet tomēr gribējās labāk iemācīties par "netowrking" un likās ka būs vieglāk (nebij necik vieglāk, teiktu ka tieši grūtak bija).
pati ideja radās tikai tādēļ ka tajā dienā mums ar draugiem discord galīgi negribējas strādāt normāli un kā joku teicu ka pats izviedošu labāku versiju no tā  :).
