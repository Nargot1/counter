using CounterJakubWitkowski.Models;

namespace CounterJakubWitkowski
{
    public partial class MainPage : ContentPage
    {
        CountersService countersService;

        public MainPage()
        {
            countersService = new CountersService();
            BindingContext = countersService;
            InitializeComponent();
        }

        private void AddCounter_Clicked(object sender, EventArgs e)
        {
            int defaultValue;
            if (!Int32.TryParse(counterDefault.Text, out defaultValue)) defaultValue = 0;
            Counter counter = new Counter(counterName.Text,  defaultValue, Color.FromRgb((int)sliderR.Value, (int)sliderG.Value, (int)sliderB.Value));
            countersService.AddCounter(counter);

            counterName.Text = "";
            counterDefault.Text = "";
            sliderR.Value = 0;
            sliderG.Value = 0;
            sliderB.Value = 0;
        }

        private void ColorSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            colorPreview.BackgroundColor = Color.FromRgb(
                (int)sliderR.Value,
                (int)sliderG.Value,
                (int)sliderB.Value
            );
        }
    }

}
