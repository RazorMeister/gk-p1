Autor: Tymoteusz Bartnik 305852

Klawiszologia:
Żadne klawisze nie są potrzebne do poprawnego działania programu, jedynie myszka i naciskania odpowiednich przycisków w aplikacji. Aby przerwać rysowanie lub odznaczyć wszystkie elementy, należy kliknąć prawym przyciskiem myszy na pustą przestrzeń bitmapy.

Instrukcja obsługi:
 - Rysowanie: aby narysować wielokąt lub okrąg trzeba kliknąć w przycisk: Polygon lub Circle i zacząć rysować na bitmapie.
 - Dodawanie relacji: zaznacząjąc obiekt (krawędź, wierzchołek, cały wielokąt, okrąg) podświetlą się przyciski odpowiedzialne za relacje, które mogą być nadane dla obecnie zaznaczonego elementu. Obecnie zaznaczony element podświetlany jest kolorem czerwonym. Jeśli relacja wymaga podania dwóch obiektów (np. styczność okręgu) to należy najpierw wybrać odpowiedni obiekt np. krawędź, następnie naciśnąć przycisk wybranej relacji i na końcu wybrać drugi element np. okrąg. Jeśli drugi obiekt nie będzie spełniał warunków relacji to zostanie wyświetlony odpowiedni komunikat.
 - W prawym górnym rogu bitmapy jest wyświetlany aktualny tryb aplikacji. Jeśli wyświetlany jest status "None" to można wtedy zacząć rysowanie.
 - Pod bitmapą wyświetlane są informacje dotyczące obecnie wybranego obiektu.
 - Przycisk "Reset form" usuwa z bitmapy wszystkie obiekty.

Algorytm relacji:
 - Przy każdym ruchu stan wszystkich obiektów jest zapisywany dzięki czemu poszczególne relacje mogą rzucić wyjątek CannotMoveException, który spowoduje przywrócenie poprzedniego stanu elementów.
 - Przy każdorazowym ruchu, relacje sąsiednich elementów dodawane są do stosu. Relacje ze stosu są pobierane i na bieżąco poprawiane (przy poprawianiu relacji do stosu mogą zostać dodawane kolejne relacje). Jeśli ilość iteracji relacji ze stosu przekroczy 30 to rzucany jest wyjątek CannotMoveException.
 - Do okręgu mogą być przypisane 3 relacje jednocześnie (zakotwiczenie, stały promień oraz styczność).
 - Do krawędzi może być przypisana maksymalnie 1 relacja.

