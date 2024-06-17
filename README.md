# Анализ алгоритмов поиска и сортировки

Этот репозиторий содержит учебный проект, реализующий алгоритмы поиска и сортировки, а также визуализацию их временной сложности.

## Алгоритмы

Проект реализует следующие алгоритмы:

* **Линейный поиск:** 
    * Линейный поиск - простой алгоритм, который последовательно просматривает все элементы массива, пока не найдет искомый элемент или не будет достигнут конец массива.
* **Линейный поиск с барьером:**
    * Линейный поиск с барьером - модификация линейного поиска, где в конец массива добавляется искомый элемент, что позволяет избежать проверки последнего элемента массива.
* **Сортировка методом простого обмена (пузырьковая сортировка):**
    * Сортировка методом простого обмена - алгоритм, который последовательно сравнивает соседние элементы массива и меняет их местами, если они находятся в неправильном порядке. Процесс повторяется до тех пор, пока массив не будет отсортирован.
* **Сортировка методом простого выбора:**
    * Сортировка методом простого выбора - алгоритм, который находит минимальный элемент массива, меняет его местами с первым элементом, затем находит минимальный элемент в оставшейся части массива и меняет его местами со вторым элементом, и так далее.

## Задача

Проект реализует построение графика зависимости роста времени работы алгоритмов от роста размера входа в лучшем, среднем и худшем случаях.

## Реализация

Проект реализован на языке С# c использование Windows Forms 

##  Результаты

![Графики временной сложности в 3-х случаях для вышеопсианных алгоритмов](https://images2.imgbox.com/e1/f8/PlkqzyEU_o.png?download=true)

