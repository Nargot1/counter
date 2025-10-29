using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;

namespace CounterJakubWitkowski.Models
{
    internal class Counter : ObservableObject
    {
        public Guid id { get; }
        public string name { get; }

        public Color color { get; }
        public string invertedColor { 
            get
            {
                return Color.FromRgb(255 - color.Red, 255 - color.Green, 255 - color.Blue).ToHex();
            } 
        }
        public int defaultValue { get; }

        private int _value;
        public int value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        


        public Counter(string name, int defaultValue,Color color)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.color = color;
            this.defaultValue = defaultValue;
            this.value = defaultValue;
        }
        public string ToJson()
        {
            var colorHex = color.ToArgbHex();
            int def = this.defaultValue;
            var obj = new
            {
                name = this.name,
                value = this.value,
                color = colorHex,
                defaultValue = def
            };
            
            return JsonSerializer.Serialize(obj);
        }

        public static Counter FromJson(JsonElement root)
        {

            var name = root.GetProperty("name").GetString();
            var value = root.GetProperty("value").GetInt32();
            var defaultValue = root.GetProperty("defaultValue").GetInt32();
            var colorHex = root.GetProperty("color").GetString();


            var counter = new Counter(name, defaultValue, Color.FromArgb(colorHex));
            counter.value = value;

            return counter;
        }
    }
}
