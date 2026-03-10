Portfolio Tracker

Acesta este un proiect academic dezvoltat pentru cursul de Programare Interfețe Utilizator. Aplicația reprezintă un sistem simplu de tip "Portfolio Tracker", conceput pentru a monitoriza investițiile financiare (acțiuni, criptomonede, ETF-uri) și pentru a calcula automat profitul sau pierderea în timp real.

Obiectivul Proiectului

Scopul principal este proiectarea și implementarea unei interfețe grafice intuitive (GUI) care să interacționeze cu o logică de backend bine definită. Utilizatorul va putea vizualiza rapid situația financiară a investițiilor sale prin intermediul unui dashboard.

Funcționalități Propuse

- Gestionarea Activelor:Adăugarea, editarea și ștergerea activelor financiare din portofoliu.
- Calcule Automate: Determinarea valorii totale investite, a valorii curente a portofoliului și a profitului/pierderii (atât în valoare absolută, cât și procentual).
- Consolidarea Datelor: Dacă se adaugă tranzacții multiple pentru același activ, aplicația calculează automat noul preț mediu de achiziție.
- Afișare Dinamică: Datele backend-ului vor fi legate direct la elementele vizuale din interfață (Data Binding).

Structura Proiectului (Până în prezent)

În acest moment, proiectul conține definită partea de Backend (Core Models):
- `Asset.cs`: Clasa care definește un instrument financiar individual și logica matematică a acestuia.
- `Portfolio.cs`: Clasa care stochează colecția de active și calculează metricele agregate la nivelul întregului portofoliu.

Urmează implementarea Frontend-ului, care va consuma aceste clase pentru a afișa datele într-un mod vizual atractiv.

 Tehnologii Utilizate
- Limbaj: C#
- Paradigmă: Programare Orientată pe Obiecte (OOP)
- UI Framework(Urmează a fi stabilit)
