# WorkManager
## Projekt zaliczeniowy "Programowanie Obiektowe"

### --FUNKCJONALNOŚCI WORKMANAGER--

1. Tworzenie i autoryzacja użytkownika
   1. Rejestracja
   1. Logowanie

1. Zarządzanie Taskami
   1. Dodawanie zadań
   1. Wyświetlanie zadań
   1. Edycja statsów

  Aplikacja pozwala zalogowanym użytkownikom na kontrolę własnych zadań służbowych poprzez magazynowanie danych o nich oraz możliwość interakcji z nimi.

//opis rejestracji/logowania/autoryzacji



  Użytkownik po pierwszym pomyślnym zalogowaniu dostrzeże okno z polami tekstowymi oraz pustą listą, na której docelowo znajdą się zadania.

2.1 Dodawanie zadań
Poprzez wypełnienie na formularzu TaskCreation pól tekstowych Title i Description oraz wybranie terminu, przed którym zrealizowany powinien zostać dany cel, użytkownik może dodać zadanie. Wszystkie z wymienionych wartości są wartościami wymaganymi, a aplikacja nie dopuści do przypadku, w którym dodane zostaje zagadanienie o niepełnych danych. 
Po wypełnieniu wyżej wymienionych pól wymaganych, zadanie można dodać poprzez kliknięcie przycisku ADD. W przypadku powodzenia przeprowadzonej operacji, wyświetlone zostaje okno świadczące o sukcesie, rekord zadania zapisany jest w bazie danych z unikatowym identyfikatorem (zarówno własnym jak i użytkownika, który je stworzył), a dodana powinność wyświetla się na liście ze statusem New.

2.2 Lista zadań
Jeżeli dany użytkownik pomyślnie przeszedł proces dodawania zadań, swoje postępy będzie mógł zaobserwować na liście zadań. Wyświetlane są na niej dane kluczowe dla użytkownika. Widzi on zarówno skrótowe tytuły, jak i bardziej konkretne opisy swoich zadań, a także datę, przed którą powinien je ukończyć. Do listy dodać można nieskończoną ilość wierszy. Jednak aby uniknąć sytuacji nadwyraz długiej listy, kiedy w ostatniej kolumnie danego zadania pojawi się status Done - przy następnym logowaniu nie będzie ono już widoczne - zostanie usunięte.

2.3 Edycja statusów.
Najważniejszym elementem panowania nad własnymi celami jest ich status. Aplikacja umożliwia jego edycję, aby użytkownik mógł odpowiednio oznaczać swoje zadania zgodnie z faktycznym stanem ich realizacji. Po wybraniu wiersza na rejestrze, w lewym dolnym rogu okna aplikacji wybrać można z rozwijanej listy jeden z 4 statusów: {New, InProgress, Postponed, Done}. Domyślną wartością jest Done, aby przyspieszyć proces pomyślnego wykonywania zadań przez pracowników. Kiedy zaznaczony jest odpowiedni wiersz i pożądany status, wybranie przycisku Change status spowoduje nadpisanie stanu zadania oraz wyświetlenie okna potwierdzającego powodzenie.

_________________
UŻYTE TECHNOLOGIE:

