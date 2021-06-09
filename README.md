## Database-connected mail sender in WPF
Prosta aplikacja WPF połączona z bazą danych służąca do wysyłania wiadomości email

Aplikacja pozwala na wysyłanie wiadomości email i dołączenia do niej wybranego obrazu z naszego komputera, do wskazanych użytkowników z bazy danych.

Do połączenia z bazą danych użyty został Entity Framework, ponieważ pozwala on na traktowanie wierszy z tabel bazy danych jako obiektów klas odpowiadających poszczególnym tabelom. Takie podejście znacznie ułatwia pracę z zawartymi w bazie danymi osobie lubiącej programowanie obiektowe - na przykład mi :)

Aplikacja może posłużyć firmie np do szybkiego wysyłania newsletterów do użytkowników, którzy wyrazili zgodę na wysłanie do nich newslettera (a ta informacja została odnotowana w bazie danych). 

Kod programu znajduje się w pliku projektInformatyczny.sln, przeznaczonego do otwarcia np w Visual Studio.

#Kod programu od strony mechaniki działania aplikacji (backend) znajduje się w pliku o nazwie "kod aplikacji". Można go tam podejrzeć bez potrzeby otwierania plików w zewnętrznych programach.  
