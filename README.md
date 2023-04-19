# ASP.NET MVC-Library-System

<img src="https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fart.pixilart.com%2F5f87eb158540489.png&f=1&nofb=1&ipt=e5e5556bc56ad00d429faa77e69a5241adb0a0572e965cc10b98312e049ce221&ipo=images" width="200"/>

## Opis aplikacji
 Głównym zadaniem aplikacji jest udostępnienie użytkownikowi możliwości elektronicznego wypożyczania książek.
 Aplikacja implementuje wzorzec projektowy Unit of work i Data Access Layer.

## Właściwości aplikacji
 <ul>
  <li>Możliwość elektronicznego wypożyczenia ksiażki po wybraniu jej z listy</li>
  <li>Filtrowanie książek po autorze lub wydawnictwie</li>
  <li>Administracja biblioteki korzystając z systemu CRUD</li>
 </ul>
 
 ## Funkcjonalności dla użytkownika
 <ul>
  <li>Przeglądanie książek i jej opisów</li>
  <li>Filtrowanie książek po wydawnictwie i autorach</li>
  <li>Wypożyczanie i zwracanie książek</li>
 </ul>
 
 ## Przypadki użycia
 <h3>Operacje GET na obiektach w bazie:</h3>
 <ul>
  <li>Pobranie listy wszystkich książkek dostępnych w bibliotece</li>
  <li>Pobranie listy wszystkich autorów książek dostępnych w bibliotece</li>
  <li>Pobranie listy wszystkich czytelników (klientów) biblioteki</li>
  <li>Pobranie listy wszystkich wydawnictw książek dostępnych w bibliotece</li>
  <li>Pobranie informacji o książce za pomocą id</li>
  <li>Pobranie informacji o autorze i jego książkach znajdujących się w bibliotece</li>
  <li>Pobranie informacji o wypożyczeniach</li>
  <li>Pobranie informacji o książkach o danym tytule, dostajemy w tym przypadku wszystkie książki o danym tytule</li>
  <li>Pobranie informacji o ilości dostępnych książek o danym tytule</li>
 </ul>
 <h3>Operacje POST na obiektach w bazie:</h3>
  <ul>
  <li>Dodanie nowej książki</li>
  <li></li>
 </ul>
 
 ## Funkcjonalności dla administratora
 <ul>
  <li>Tworzenie, czytanie, aktualizowanie i usuwanie (CRUD), każdej encji w bazie danych za pomocą interfejsu graficznego</li>
 </ul>

## Diagram klas dla modelu danych:
![Library-Data-Model](https://user-images.githubusercontent.com/72617970/228890663-bb0614f0-ceb9-4799-8397-2eaab9064670.png)

## Autorzy
- Bartłomiej Spleśniały [@bartspl860](https://www.github.com/bartspl860)
- Konrad Kobryń [@Kon1Kobryn](https://www.github.com/Kon1Kobryn)
