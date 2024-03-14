# Stock Portfolio API

## Opis i cel

Programista próbował zbudować API do pobierania portfolio papierów wartościowych, liczenia ich wartości oraz opcji usuwania. Większość elementów systemu działa, ale zawiera błędy. Jednym z podejrzeń jest, że konwersja walut nie działa prawidłowo. Niestety, programista który pisał API wyjechał w Bieszczady pasać owce (podobno dobrze mu idzie), więc nie rozwiąże już tego problemu. Liczymy na Ciebie :)

Kod jest słaby, wypełniony nietrafionymi decyzjami i niedokończoną logiką. Twoim zadaniem jest dokończenie projektu API, tak aby działał on prawidłowo.

Wymagania to:
- endpoint, który zwraca konkretne portfolio
- endpoint, który zwraca wartość wszystkich akcji w podanej walucie
- endpoint, który usuwa portfolio przez soft-delete
- trochę unit testów, tak na dobry początek (nie ma potrzeby pisania ich dla całego projektu)
- kursy walut powinny być pobierane dynamicznie z zewnętrznego API - integrtacja CurrencyLayer z kluczem jest dodana do projektu (może być inna); kursy walut nie muszą być pobierane za każdym razem, tylko co 24 godziny
- UWAGA: podprojekt StocksService jest finalny i nie podlega modyfikacjom

Dodatkowo chcemy wykonać refactor kodu najlepiej jak się da, z użyciem prawidłowych wzorców projektowych i zasad pisania wysokiej jakości kodu. Możesz wykorzystać dodatkowe biblioteki, o ile są otwartoźródłowe.

## Baza danych

W projekcie jest użyta przenośna wersja MongoDB do przechowywania danych. Możesz zmienić bazę na inną o ile jest ku temu dobre uzasadnienie.

## Podsumowanie

Zmieniony projekt udostepnij jako repozytorium git, oczywiścnie z opisem zmian. Możesz też opisać dodatkowe pomysły, jakie można w wdrożyć do projektu, ale nie są zaimplementowane.