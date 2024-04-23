﻿using LiveCharts.Wpf.Charts.Base;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;
using LiveCharts.Defaults;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Windows.Ink;
using System.Collections.Generic;

namespace lab7
{
    public partial class Form1 : Form
    {
        const int SIZE = 500;
        private Dictionary<string, Series> seriesDictionary = new Dictionary<string, Series>();
        
        public Form1()
        {
            InitializeComponent();            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            linearSearchChart.Dock = DockStyle.Fill;
            barrierLinearSearchChart.Dock = DockStyle.Fill;
            bubbleSortChart.Dock = DockStyle.Fill;
            selectionSortChart.Dock = DockStyle.Fill;

            // Настраиваем графики
            linearSearchChart.AxisX = new AxesCollection { new Axis { Title = "Размер входа\n(Линейный поиск)" } };
            linearSearchChart.AxisY = new AxesCollection { new Axis { Title = "Время (нс)", MinValue = 0, MaxValue = 2000 } };
            AddSeriesForAlgorithm("Линейный поиск", System.Windows.Media.Colors.Green, linearSearchChart);

            barrierLinearSearchChart.AxisX = new AxesCollection { new Axis { Title = "Размер входа\n(Линейный поиск с барьером)" } };
            barrierLinearSearchChart.AxisY = new AxesCollection { new Axis { Title = "Время (нс)", MinValue = 0, MaxValue = 2000 } };
            AddSeriesForAlgorithm("Линейный поиск с барьером", System.Windows.Media.Colors.Blue, barrierLinearSearchChart);

            bubbleSortChart.AxisX = new AxesCollection { new Axis { Title = "Размер входа\n(Сортировка простым обменом)" } };
            bubbleSortChart.AxisY = new AxesCollection { new Axis { Title = "Время (нс)", MinValue = 0, MaxValue = 10000 } };
            AddSeriesForAlgorithm("Сортировка обменом", System.Windows.Media.Colors.Red, bubbleSortChart);

            selectionSortChart.AxisX = new AxesCollection { new Axis { Title = "Размер входа\n(Сортировка слиянием)" } };
            selectionSortChart.AxisY = new AxesCollection { new Axis { Title = "Время (нс)", MinValue = 0, MaxValue = 10000 } };
            AddSeriesForAlgorithm("Сортировка выбором", System.Windows.Media.Colors.Purple, selectionSortChart);

            TestAlgorithms();
        }

        private void AddSeriesForAlgorithm(string algorithmName, System.Windows.Media.Color color, LiveCharts.WinForms.CartesianChart chart)
        {
            var series1 = new LiveCharts.Wpf.LineSeries
            {
                Title = $"{algorithmName} - Лучший",
                Values = new ChartValues<ObservablePoint>(),
                Stroke = System.Windows.Media.Brushes.Green
            };

            var series2 = new LiveCharts.Wpf.LineSeries
            {
                Title = $"{algorithmName} - Средний",
                Values = new ChartValues<ObservablePoint>(),
                Stroke = System.Windows.Media.Brushes.Blue
            };

            var series3 = new LiveCharts.Wpf.LineSeries
            {
                Title = $"{algorithmName} - Худший",
                Values = new ChartValues<ObservablePoint>(),
                Stroke = System.Windows.Media.Brushes.Red
            };

            chart.Series = new SeriesCollection { series1, series2, series3 };
        }



        private void TestLinearSearch(int maxSize, int[][] bestCaseData, int[][] averageCaseData, int[][] worstCaseData)
        {
            int numIterations = 5; // Количество итераций для усреднения

            for (int size = 1; size <= maxSize; size++)
            {
                double totalTimeBest = 0;
                double totalTimeAverage = 0;
                double totalTimeWorst = 0;

                for (int i = 0; i < numIterations; i++)
                {
                    double time = MeasureTime(() => LinearSearch(bestCaseData[size - 1], bestCaseData[size - 1][0]));
                    totalTimeBest += time;

                    time = MeasureTime(() => LinearSearch(averageCaseData[size - 1], averageCaseData[size - 1][size / 2]));
                    totalTimeAverage += time;

                    time = MeasureTime(() => LinearSearch(worstCaseData[size - 1], worstCaseData[size - 1][size - 1]));
                    totalTimeWorst += time;
                }

                AddChartDataForAlgorithm("Линейный поиск", "Лучший", size, totalTimeBest / numIterations);
                AddChartDataForAlgorithm("Линейный поиск", "Средний", size, totalTimeAverage / numIterations);
                AddChartDataForAlgorithm("Линейный поиск", "Худший", size, totalTimeWorst / numIterations);
            }
        }

        private void TestBarrierLinearSearch(int maxSize, int[][] bestCaseData, int[][] averageCaseData, int[][] worstCaseData)
        {
            int numIterations = 5; // Количество итераций для усреднения

            for (int size = 1; size <= maxSize; size++)
            {
                double totalTimeBest = 0;
                double totalTimeAverage = 0;
                double totalTimeWorst = 0;

                for (int i = 0; i < numIterations; i++)
                {
                    double time = MeasureTime(() => BarrierLinearSearch(bestCaseData[size - 1], bestCaseData[size - 1][0]));
                    totalTimeBest += time;

                    time = MeasureTime(() => BarrierLinearSearch(averageCaseData[size - 1], averageCaseData[size - 1][size / 2]));
                    totalTimeAverage += time;

                    time = MeasureTime(() => BarrierLinearSearch(worstCaseData[size - 1], worstCaseData[size - 1][size - 1]));
                    totalTimeWorst += time;
                }

                AddChartDataForAlgorithm("Линейный поиск с барьером", "Лучший", size, totalTimeBest / numIterations);
                AddChartDataForAlgorithm("Линейный поиск с барьером", "Средний", size, totalTimeAverage / numIterations);
                AddChartDataForAlgorithm("Линейный поиск с барьером", "Худший", size, totalTimeWorst / numIterations);
            }
        }

        private void TestBubbleSort(int maxSize, int[][] bestCaseData, int[][] averageCaseData, int[][] worstCaseData)
        {
            int numIterations = 5; // Количество итераций для усреднения

            for (int size = 1; size <= maxSize; size++)
            {
                double totalTimeBest = 0;
                double totalTimeAverage = 0;
                double totalTimeWorst = 0;

                for (int i = 0; i < numIterations; i++)
                {
                    int[] dataCopy;

                    // Лучший случай
                    dataCopy = new int[size];
                    Array.Copy(bestCaseData[size - 1], dataCopy, size);
                    double time = MeasureTime(() => BubbleSort(dataCopy));
                    totalTimeBest += time;

                    // Средний случай
                    dataCopy = new int[size];
                    Array.Copy(averageCaseData[size - 1], dataCopy, size);
                    time = MeasureTime(() => BubbleSort(dataCopy));
                    totalTimeAverage += time;

                    // Худший случай
                    dataCopy = new int[size];
                    Array.Copy(worstCaseData[size - 1], dataCopy, size);
                    time = MeasureTime(() => BubbleSort(dataCopy));
                    totalTimeWorst += time;
                }

                AddChartDataForAlgorithm("Сортировка обменом", "Лучший", size, totalTimeBest / numIterations);
                AddChartDataForAlgorithm("Сортировка обменом", "Средний", size, totalTimeAverage / numIterations);
                AddChartDataForAlgorithm("Сортировка обменом", "Худший", size, totalTimeWorst / numIterations);
            }
        }
        private void TestSelectionSort(int maxSize, int[][] bestCaseData, int[][] averageCaseData, int[][] worstCaseData)
        {
            int numIterations = 5; // Количество итераций для усреднения

            for (int size = 1; size <= maxSize; size++)
            {
                double totalTimeBest = 0;
                double totalTimeAverage = 0;
                double totalTimeWorst = 0;

                for (int i = 0; i < numIterations; i++)
                {
                    int[] dataCopy;

                    // Лучший случай
                    dataCopy = new int[size];
                    Array.Copy(bestCaseData[size - 1], dataCopy, size);
                    double time = MeasureTime(() => SelectionSort(dataCopy));
                    totalTimeBest += time;

                    // Средний случай
                    dataCopy = new int[size];
                    Array.Copy(averageCaseData[size - 1], dataCopy, size);
                    time = MeasureTime(() => SelectionSort(dataCopy));
                    totalTimeAverage += time;

                    // Худший случай
                    dataCopy = new int[size];
                    Array.Copy(worstCaseData[size - 1], dataCopy, size);
                    time = MeasureTime(() => SelectionSort(dataCopy));
                    totalTimeWorst += time;
                }

                AddChartDataForAlgorithm("Сортировка выбором", "Лучший", size, totalTimeBest / numIterations);
                AddChartDataForAlgorithm("Сортировка выбором", "Средний", size, totalTimeAverage / numIterations);
                AddChartDataForAlgorithm("Сортировка выбором", "Худший", size, totalTimeWorst / numIterations);
            }
        }

        private int[] GenerateAverageCaseData(int size)
        {
            Random random = new Random();
            int[] data = new int[size];
            for (int i = 0; i < size; i++)
            {
                data[i] = random.Next(size); // Генерируем случайные числа от 0 до size-1
            }
            return data;
        }

        private int[] GenerateWorstCaseData(int size)
        {
            int[] data = Enumerable.Range(1, size).ToArray();
            Array.Reverse(data);
            return data;
        }

        private int[] GenerateBestCaseData(int size)
        {
            return Enumerable.Range(1, size).ToArray(); // Создает массив чисел от 1 до size
        }

        private int LinearSearch(int[] data, int value)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == value)
                {
                    return i; // Возвращаем индекс найденного элемента
                }
            }
            return -1; // Элемент не найден
        }

        private int BarrierLinearSearch(int[] data, int value)
        {
            int[] dataWithBarrier = new int[data.Length + 1];
            Array.Copy(data, dataWithBarrier, data.Length);
            dataWithBarrier[data.Length] = value; // Добавляем барьер в конец массива

            int i = 0;
            while (dataWithBarrier[i] != value)
            {
                i++;
            }
            if (i < data.Length)
            {
                return i; // Элемент найден
            }
            else
            {
                return -1; // Элемент не найден
            }
        }

        private void BubbleSort(int[] data)
        {
            for (int i = 0; i < data.Length - 1; i++)
            {
                for (int j = 0; j < data.Length - i - 1; j++)
                {
                    if (data[j] > data[j + 1])
                    {
                        // Меняем элементы местами
                        int temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
                }
            }
        }

        private void SelectionSort(int[] data)
        {
            for (int i = 0; i < data.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < data.Length; j++)
                {
                    if (data[j] < data[minIndex])
                    {
                        minIndex = j;
                    }
                }
                // Меняем местами минимальный элемент с текущим
                int temp = data[minIndex];
                data[minIndex] = data[i];
                data[i] = temp;
            }
        }

        private double MeasureTime(Action action)
        {
            if (!Stopwatch.IsHighResolution)
            {
                throw new Exception("Your hardware doesn't support high resolution counter");
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            return stopwatch.ElapsedTicks * 1000000.0 * 1000 / Stopwatch.Frequency;
        }

        private void AddChartDataForAlgorithm(string algorithmName, string caseType, int size, double time)
        {
            var values = new ObservablePoint(size, time);

            if (algorithmName == "Линейный поиск")
            {
                if (caseType == "Лучший")
                {
                    ((ChartValues<ObservablePoint>)linearSearchChart.Series[0].Values).Add(new ObservablePoint(size, time));
                }
                else if (caseType == "Средний")
                {
                    ((ChartValues<ObservablePoint>)linearSearchChart.Series[1].Values).Add(new ObservablePoint(size, time));
                }
                else if (caseType == "Худший")
                {
                    ((ChartValues<ObservablePoint>)linearSearchChart.Series[2].Values).Add(new ObservablePoint(size, time));
                }
            }

            else if (algorithmName == "Линейный поиск с барьером")
            {
                if (caseType == "Лучший")
                {
                    ((ChartValues<ObservablePoint>)barrierLinearSearchChart.Series[0].Values).Add(values);
                }
                else if (caseType == "Средний")
                {
                    ((ChartValues<ObservablePoint>)barrierLinearSearchChart.Series[1].Values).Add(values);
                }
                else if (caseType == "Худший")
                {
                    ((ChartValues<ObservablePoint>)barrierLinearSearchChart.Series[2].Values).Add(values);
                }
            }
            else if (algorithmName == "Сортировка обменом")
            {
                if (caseType == "Лучший")
                {
                    ((ChartValues<ObservablePoint>)bubbleSortChart.Series[0].Values).Add(values);
                }
                else if (caseType == "Средний")
                {
                    ((ChartValues<ObservablePoint>)bubbleSortChart.Series[1].Values).Add(values);
                }
                else if (caseType == "Худший")
                {
                    ((ChartValues<ObservablePoint>)bubbleSortChart.Series[2].Values).Add(values);
                }
            }
            else if (algorithmName == "Сортировка выбором")
            {
                if (caseType == "Лучший")
                {
                    ((ChartValues<ObservablePoint>)selectionSortChart.Series[0].Values).Add(values);
                }
                else if (caseType == "Средний")
                {
                    ((ChartValues<ObservablePoint>)selectionSortChart.Series[1].Values).Add(values);
                }
                else if (caseType == "Худший")
                {
                    ((ChartValues<ObservablePoint>)selectionSortChart.Series[2].Values).Add(values);
                }
            }
        }        


        private void TestAlgorithms()
        {
            int[][] bestCaseData = new int[SIZE][];
            int[][] averageCaseData = new int[SIZE][];
            int[][] worstCaseData = new int[SIZE][];

            for (int size = 1; size <= SIZE; size++)
            {
                bestCaseData[size - 1] = GenerateBestCaseData(size);
                averageCaseData[size - 1] = GenerateAverageCaseData(size);
                worstCaseData[size - 1] = GenerateWorstCaseData(size);
            }

            TestLinearSearch(SIZE, bestCaseData, averageCaseData, worstCaseData);
            TestBarrierLinearSearch(SIZE, bestCaseData, averageCaseData, worstCaseData);
            TestBubbleSort(SIZE, bestCaseData, averageCaseData, worstCaseData);
            TestSelectionSort(SIZE, bestCaseData, averageCaseData, worstCaseData);

            ShowCharts();
        }


        private void ShowCharts()
        {
            linearSearchChart.Refresh();
            barrierLinearSearchChart.Refresh();
            bubbleSortChart.Refresh();
            selectionSortChart.Refresh();
        }
    }
}