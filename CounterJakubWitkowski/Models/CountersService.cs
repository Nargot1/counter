using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace CounterJakubWitkowski.Models
{
    internal class CountersService
    {
        public ObservableCollection<Counter> Counters { get; private set; } = new();

        public ICommand AddToCounterCommand { get; private set; }
        public ICommand SubtractFromCounterCommand { get; private set; }
        public ICommand ResetCounterCommand { get; private set; }
        public ICommand RemoveCounterCommand { get; private set; }

        public CountersService()
        {
            LoadCounters();
            AddToCounterCommand = new Command<Guid>(AddToCounter);
            SubtractFromCounterCommand = new Command<Guid>(SubtractFromCounter);
            ResetCounterCommand = new Command<Guid>(ResetCounter);
            RemoveCounterCommand = new Command<Guid>(RemoveCounter);
        }

        public void AddToCounter(Guid id)
        {
            var counter = Counters.FirstOrDefault(c => c.id == id);
            if (counter == null) return;

            counter.value++;
            SaveCounters();
        }

        public void SubtractFromCounter(Guid id)
        {
            var counter = Counters.FirstOrDefault(c => c.id == id);
            if (counter == null) return;

            counter.value--;
            SaveCounters();
        }

        public void ResetCounter(Guid id)
        {
            var counter = Counters.FirstOrDefault(c => c.id == id);
            if (counter == null) return;

            counter.value = counter.defaultValue;
            SaveCounters();
        }

        public void AddCounter(Counter counter)
        {
            Counters.Add(counter);
            SaveCounters();
        }

        public void RemoveCounter(Guid id)
        {
            var counter = Counters.FirstOrDefault(c => c.id == id);
            if (counter == null) return;

            Counters.Remove(counter);
            SaveCounters();
        }

        public void LoadCounters()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "counters.json");
            if (!File.Exists(filePath))
                return;
            string jsonLines = File.ReadAllText(filePath);
            JsonDocument doc = JsonDocument.Parse(jsonLines);
            foreach (JsonElement item in doc.RootElement.EnumerateArray())
            {
                Counter counter = Counter.FromJson(item);
                if (counter != null)
                    Counters.Add(counter);
            }
        }

        public void SaveCounters()
        {
            string jsonSaveData = "[";
            bool first = true;
            foreach (Counter counter in Counters)
            {
                if (first)
                    first = false;
                else
                    jsonSaveData += ",";
                string json = counter.ToJson();
                jsonSaveData += json;
            }
            jsonSaveData += "]";

            File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, "counters.json"), jsonSaveData);
        }
    }
}
