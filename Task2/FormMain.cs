using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System.Windows.Forms;
using TSPGeneticAlgorithm;
using TSPGeneticAlgorithm.Crossovers;
using TSPGeneticAlgorithm.Evaluators;
using TSPGeneticAlgorithm.Models;
using TSPGeneticAlgorithm.Mutations;
using TSPGeneticAlgorithm.Selections;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Collections.Concurrent;
using LiveChartsCore.SkiaSharpView.WinForms;
using System.Data;
using Task2.SaverUtils;
using OpenTK.Graphics.ES20;
using System.Data.Common;

namespace Task2
{
    public partial class MainForm : Form
    {
        private List<City> cities;
        private GeneticAlgorithm geneticAlgorithm;
        private CancellationTokenSource cts;
        private Task gaTask;
        private LineSeries<double> fitnessSeries;
        private ConcurrentQueue<double> fitnessHistory = new ConcurrentQueue<double>();
        private object lockObj = new object();
        private System.Windows.Forms.Timer uiUpdateTimer;
        private ExperimentStorageService experimentStorageService = new ExperimentStorageService();
        private bool isExperimentInitialized = false; 
        int maxGenerations = 0;

        public MainForm()
        {
            InitializeComponent();
            InitializeChart();
            InitializeUIUpdateTimer();
            dataGridView1.Font = new Font("Segoe UI", 10);
            isBlockControls(false);
        }

        private void InitializeUIUpdateTimer()
        {
            uiUpdateTimer = new System.Windows.Forms.Timer();
            uiUpdateTimer.Interval = 10; // Обновление каждые 100 мс
            uiUpdateTimer.Tick += (sender, e) => UpdateUI();
        }

        private void UpdateUI()
        {
            UpdateChart();
            UpdateLabels();

            if (geneticAlgorithm?.BestChromosome != null)
            {
                DrawRoute(geneticAlgorithm.BestChromosome);
            }
        }

        private void ClearUI()
        {
            dataGridView1.DataSource = null;

            foreach (var series in cartesianChartFitness.Series)
                if (series.Values is ObservableCollection<double> observableValues)
                    observableValues.Clear();
            cartesianChartFitness.Update();

            using (var bitmap = new Bitmap(pictureBoxRoute.Width, pictureBoxRoute.Height))
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White); 
                pictureBoxRoute.Image?.Dispose();
                pictureBoxRoute.Image = (Bitmap)bitmap.Clone();
            }

            ExpName.Text = "Безымянный*";
            lblGeneration.Text = "Поколение:";
            lblBestFitness.Text = $"Лучшая приспособленность:";
            lblSumLength.Text = $"Лучшее расстояние:";
        }

        private void UpdateLabels()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateLabels));
                return;
            }

            if (geneticAlgorithm != null)
            {
                lblGeneration.Text = $"Поколение: {geneticAlgorithm.Generation}";
                lblBestFitness.Text = $"Лучшая приспособленность: {geneticAlgorithm.BestChromosome?.Fitness:F2}";
                lblSumLength.Text = $"Лучшее расстояние: {geneticAlgorithm.BestChromosome?.GetTotalDistance():F2}";
            }
        }

        private void InitializeChart()
        {
            fitnessSeries = new LineSeries<double>
            {
                Values = new ObservableCollection<double>(),
                Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 2 },
                GeometrySize = 0,
                Fill = null
            };

            cartesianChartFitness.Series = new ISeries[] { fitnessSeries };

            cartesianChartFitness.XAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Поколение",
                    LabelsRotation = 0,
                    UnitWidth = 1,
                    Labeler = value => value.ToString("N0"),
                    MinStep = 1,
                    NameTextSize = 15,
                }
            };

            cartesianChartFitness.YAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Приспособленность",
                    LabelsRotation = 0,
                    Labeler = value => value.ToString("N2"),
                    NameTextSize = 15,
                }
            };
        }


        private void UpdateChart()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateChart));
                return;
            }

            if (fitnessSeries == null)
                return;

            double[] dataArray;
            lock (lockObj)
            {
                dataArray = fitnessHistory.ToArray();
            }

            var data = dataArray.ToList();

            if (data.Count == 0)
                return;

            var observableValues = fitnessSeries.Values as ObservableCollection<double>;

            if (observableValues == null)
                return;

            Application.DoEvents();

            observableValues.Clear();
            foreach (var value in data)
            {
                observableValues.Add(value);
            }

            cartesianChartFitness.Invalidate();
        }


        private void btnGenerateCities_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtCityCount.Text, out int cityCount))
            {
                cities = GenerateRandomCities(cityCount);
                lblCitiesCnt.Text = $"Кол-во городов: {cityCount}";
                isExperimentInitialized = false;
                isBlockControls(false);
                resetControls();
                ClearUI();
                DrawRoute(_cities: cities);
            }
            else
            {
                MessageBox.Show("Введите корректное количество городов.");
            }
        }

        private List<City> GenerateRandomCities(int count)
        {
            var random = new Random();
            var cities = new List<City>();

            for (int i = 0; i < count; i++)
            {
                double x = random.NextDouble() * 100;
                double y = random.NextDouble() * 100;
                cities.Add(new City(i, x, y));
            }

            Verify(cities);

            return cities;
        }

        private void Verify(List<City> cities)
        {
            int cityCount = cities.Count;
            bool triViolated = false;

            Parallel.For(0, cityCount, (i, state) =>
            {
                if (triViolated)
                    state.Stop();

                for (int j = i + 1; j < cityCount && !triViolated; j++)
                {
                    for (int k = j + 1; k < cityCount && !triViolated; k++)
                    {
                        double ij = cities[i].DistanceTo(cities[j]);
                        double ik = cities[i].DistanceTo(cities[k]);
                        double kj = cities[k].DistanceTo(cities[j]);

                        if (ij > ik + kj || ik > ij + kj || kj > ij + ik)
                        {
                            triViolated = true;
                            state.Stop();
                        }
                    }
                }
            });

            if (triViolated)
                MessageBox.Show("Неравенство треугольников нарушено!");
            else
                MessageBox.Show("Города удовлетворяют\nнеравенству треугольников.");
        }

        private void btnLoadCities_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "CSV files (*.csv)|*.csv|All files (.*.)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    cities = LoadSitiesFromCSV(ofd.FileName);
                    lblCitiesCnt.Text = $"Кол-во городов: {cities.Count}";
                    isBlockControls(false);
                    isExperimentInitialized = false;
                    resetControls();
                    ClearUI();
                    DrawRoute(_cities: cities);
                }
            }
        }

        private List<City> LoadSitiesFromCSV(string filePath)
        {
            var cities = new List<City>();
            var lines = File.ReadAllLines(filePath);
            int id = 0;

            foreach (var line in lines)
            {
                var parts = line.Split(',');

                if (parts.Length >= 3)
                {
                    if (double.TryParse(parts[1], out double x) &&
                        double.TryParse(parts[2], out double y))
                    {
                        string name = parts[0].Trim();
                        cities.Add(new City(id++, name, x, y));
                    }
                }
            }
            return cities;
        }

        private void runExperiment()
        {
            if (!ExpName.Text.EndsWith("*"))
                ExpName.Text += "*";

            isBlockControls(true);
            uiUpdateTimer.Start();

            cts = new CancellationTokenSource();

            gaTask = Task.Run(() => RunAlgorithm(geneticAlgorithm, cts.Token, maxGenerations))
                .ContinueWith(t =>
                {
                    uiUpdateTimer.Stop();

                    if (t.IsFaulted)
                    {
                        MessageBox.Show($"Error stop timer\n{t.Exception.Message}");
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void resetControls()
        {
            txtPopulationSize.Text = "20";
            txtMutationRate.Text = "0,02";
            txtMaxGenerations.Text = "0";
            txtPrecision.Text = "0,0001";
            txtMaxStagnationCnt.Text = "100";
        }

        private void isBlockControls(bool flag)
        {
            btnStart.Text = isExperimentInitialized ? 
                            "Продолжить работу" : 
                            "Запустить алгоритм";

            flag = !flag;
            txtPopulationSize.Enabled = flag;
            txtMutationRate.Enabled = flag;
            txtMaxGenerations.Enabled = flag;
            txtPrecision.Enabled = flag;
            txtMaxStagnationCnt.Enabled = flag;
            btnStop.Enabled = !flag;
            btnStart.Enabled = flag;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (isExperimentInitialized)
            {
                runExperiment();
                return;
            }

            if (cities == null || cities.Count == 0)
            {
                MessageBox.Show("Сначала необходимо задать города!");
                return;
            }

            if (!int.TryParse(txtPopulationSize.Text, out int populationSize) || populationSize <= 0)
            {
                MessageBox.Show("Введите корректный размер популяции!");
                return;
            }

            if (!double.TryParse(txtMutationRate.Text, out var mutationRate) || mutationRate <= 0)
            {
                MessageBox.Show("Введите корректную вероятность мутации!");
                return;
            }

            int.TryParse(txtMaxGenerations.Text, out maxGenerations);
            txtMaxGenerations.Text = maxGenerations.ToString();

            var maxStagnationCount = 0;
            int.TryParse(txtMaxStagnationCnt.Text, out maxStagnationCount);
            txtMaxStagnationCnt.Text = maxStagnationCount.ToString();

            var improvementThreshold = 1e-4;
            double.TryParse(txtPrecision.Text, out improvementThreshold);
            txtPrecision.Text = improvementThreshold.ToString();

            var initialPopulation = GenerateInitialPopulation(cities, populationSize);

            geneticAlgorithm = new GeneticAlgorithm(
                initialPopulation,
                new DistanceFitnessEvaluator(),
                new RouletteWheelSelections(),
                new OrderCrossover(),
                new SwapMutation(mutationRate),
                maxStagnationCount: maxStagnationCount,
                improvementThreshold: 1e-4
            );

            fitnessHistory.Clear();
            InitializeChart();
            isExperimentInitialized = true;
            runExperiment();
        }

        private void RunAlgorithm(GeneticAlgorithm ga, CancellationToken token, int maxGenerations)
        {
            try
            {
                while (!token.IsCancellationRequested && 
                       (maxGenerations == 0 || ga.Generation < maxGenerations))
                {
                    bool continueEvolving = ga.Evolve();

                    var bestFitness = ga.BestChromosome?.Fitness ?? 0;
                    lock (lockObj)
                    {
                        fitnessHistory.Enqueue(bestFitness);
                    }

                    if (!continueEvolving)
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    $"Ошибка в RunAlgorithm: {e.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally 
            {
                Invoke((MethodInvoker)delegate
                {
                    uiUpdateTimer.Stop();
                    isBlockControls(false);
                    MessageBox.Show("Алгоритм завершил работу.");
                    displayTableData();
                });
            }            
        }

        private void displayTableData()
        {
            DataTable originalTable = new DataTable();
            originalTable.Columns.Add("Name");
            originalTable.Columns.Add("Pos");
            foreach (var city in geneticAlgorithm.BestChromosome?.Cities)
            {
                originalTable.Rows.Add(city.Name, $"({city.X:F1}; {city.Y:F1})");
            }

            DataTable transposedTable = new DataTable();

            for (int i = 0; i < originalTable.Rows.Count; i++)
            {
                transposedTable.Columns.Add($"#_{i + 1}");
            }

            for (int i = 0; i < originalTable.Columns.Count; i++)
            {
                DataRow newRow = transposedTable.NewRow();

                for (int j = 0; j < originalTable.Rows.Count; j++)
                {
                    newRow[j] = originalTable.Rows[j][i];
                }
                transposedTable.Rows.Add(newRow);
            }

            dataGridView1.DataSource = transposedTable;
        }

        private void DrawRoute(Chromosome? bestChromosome = null, List<City>? _cities = null)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => DrawRoute(bestChromosome)));
                return;
            }

            using (var bitmap = new Bitmap(pictureBoxRoute.Width, pictureBoxRoute.Height))
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                var cities = _cities ?? bestChromosome.Cities;
                double minX = cities.Min(c => c.X);
                double maxX = cities.Max(c => c.X);
                double minY = cities.Min(c => c.Y);
                double maxY = cities.Max(c => c.Y);

                Func<double, float> scaleX = x => (float)((x - minX) / (maxX - minX) * (pictureBoxRoute.Width - 40) + 20);
                Func<double, float> scaleY = y => (float)((y - minY) / (maxY - minY) * (pictureBoxRoute.Height - 40) + 20);

                // Рисуем линии маршрута
                using (var pen = new Pen(Color.Blue, 2))
                {
                    for (int i = 0; i < cities.Count; i++)
                    {
                        var city1 = cities[i];
                        var city2 = cities[(i + 1) % cities.Count];
                        g.DrawLine(pen, scaleX(city1.X), scaleY(city1.Y), scaleX(city2.X), scaleY(city2.Y));
                    }
                }

                // Рисуем города
                foreach (var city in cities)
                {
                    float x = scaleX(city.X);
                    float y = scaleY(city.Y);
                    g.FillEllipse(Brushes.Red, x - 5, y - 5, 10, 10);
                    g.DrawString((city.ID + 1).ToString(), Font, Brushes.Black, x + 5, y + 5);
                }

                pictureBoxRoute.Image?.Dispose();
                pictureBoxRoute.Image = (Bitmap)bitmap.Clone();
            }
        }

        private List<Chromosome> GenerateInitialPopulation(
            List<City> cities, int populationSize)
        {
            var population = new List<Chromosome>();
            var random = new Random();

            for (int i = 0; i < populationSize; i++)
            {
                var shuffledCities = cities.OrderBy(c => random.Next()).ToList();
                population.Add(new Chromosome(shuffledCities));
            }

            return population;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel(); 
                isBlockControls(false);
            }
        }

        private void txtMutationRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (geneticAlgorithm == null)
            {
                MessageBox.Show(
                    "Нет активного эксперимента!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            using (var saveForm = new SaveForm())
            {
                if (saveForm.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    if (cts != null)
                    {
                        cts.Cancel();
                        if (gaTask != null)
                            await gaTask;
                    }

                    var parameters = new ExperimentParameters
                    {
                        PopulationSize = int.Parse(txtPopulationSize.Text),
                        MutationRate = double.Parse(txtMutationRate.Text),
                        MaxGenerations = int.Parse(txtMaxGenerations.Text),
                        MaxStagnationCount = int.Parse(txtMaxStagnationCnt.Text),
                        ImprovementThreshold = double.Parse(txtPrecision.Text),
                        Cities = cities
                    };

                    await experimentStorageService.SaveExperiment(
                        saveForm.ExperimentName,
                        geneticAlgorithm,
                        parameters,
                        fitnessHistory
                    );

                    ExpName.Text = saveForm.ExperimentName;

                    MessageBox.Show(
                        $"Эксперимент '{saveForm.ExperimentName}' успешно сохранен!",
                        "Успешность",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Ошибка при сохранении эксперимента: {ex.Message}",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            var experimentsNames = experimentStorageService.GetExperimentNames();
            if (!experimentsNames.Any())
            {
                MessageBox.Show(
                    "Нет сохраненных экспериментов!",
                    "Информация",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            using (var loadForm = new LoadForm(experimentsNames))
            {
                if (loadForm.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    if (cts != null)
                    {
                        cts.Cancel();
                        if (gaTask != null)
                            await gaTask;
                    }

                    var (loadedGA, parameters, loadedFH) =
                        await experimentStorageService.LoadExperiment(loadForm.ExperimentName);

                    cities = parameters.Cities;
                    txtPopulationSize.Text = parameters.PopulationSize.ToString();
                    txtMutationRate.Text = parameters.MutationRate.ToString();
                    txtMaxGenerations.Text = parameters.MaxGenerations.ToString();
                       maxGenerations      = parameters.MaxGenerations;
                    txtMaxStagnationCnt.Text = parameters.MaxStagnationCount.ToString();
                    txtPrecision.Text = parameters.ImprovementThreshold.ToString();
                    txtCityCount.Text = cities.Count.ToString();
                    lblCitiesCnt.Text = $"Кол-во городов: {cities.Count}";

                    geneticAlgorithm = loadedGA;

                    fitnessHistory.Clear();
                    foreach (var fitness in loadedFH)
                        fitnessHistory.Enqueue(fitness);
                    displayTableData();
                    DrawRoute(geneticAlgorithm.BestChromosome);
                    UpdateUI();

                    isExperimentInitialized = true;
                    isBlockControls(true); 
                    btnStop.Enabled = false;
                    btnStart.Enabled = true;

                    ExpName.Text = loadForm.ExperimentName;

                    MessageBox.Show(
                        $"Эксперимент '{loadForm.ExperimentName}' успешно загружен!",
                        "Тотальный успех!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Ошибка при загрузке эксперимента: {ex.Message}",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }
    }
}