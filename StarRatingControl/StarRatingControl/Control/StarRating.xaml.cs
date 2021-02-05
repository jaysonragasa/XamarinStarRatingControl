using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarRatingControl.Control
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StarRating : ContentView
    {
        public static readonly BindableProperty MaxProperty = BindableProperty.Create(nameof(Max), typeof(int), typeof(StarRating), 5);
        public int Max
        {
            get { return (int)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(StarRating), 0.0d);
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public StarRating()
        {
            InitializeComponent();
            RedrawStar();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == MaxProperty.PropertyName || propertyName == ValueProperty.PropertyName)
            {
                RedrawStar();
            }
        }

        void RedrawStar()
        {
            stack.Children.Clear();

            int starIndex = 0;

            // draw yello stars first
            for (int i = 0; i < Math.Floor(Value); i++)
            {
                starIndex++;

                Image star = new Image()
                {
                    HeightRequest = this.HeightRequest,
                    WidthRequest = this.HeightRequest,
                    Source = "starYellow.png",
                    Margin = new Thickness(0, 0, 5, 0),
                    TabIndex = starIndex
                };

                TapGestureRecognizer tap = new TapGestureRecognizer()
                {
                    CommandParameter = starIndex,
                    Command = new Command<int>((index) =>
                    {
                        this.Value = index;
                        RedrawStar();
                    })
                };

                star.GestureRecognizers.Add(tap);
                
                stack.Children.Add(star);
            }

            // then the remaining stars
            for (int i = 0; i < Max - Value; i++)
            {
                starIndex++;

                Image star = new Image()
                {
                    HeightRequest = this.HeightRequest,
                    WidthRequest = this.HeightRequest,
                    Source = "starGrey.png",
                    Margin = new Thickness(0, 0, 5, 0),
                    TabIndex = starIndex
                };

                TapGestureRecognizer tap = new TapGestureRecognizer()
                {
                    CommandParameter = starIndex,
                    Command = new Command<int>((index) =>
                    {
                        this.Value = index;
                        RedrawStar();
                    })
                };

                star.GestureRecognizers.Add(tap);

                stack.Children.Add(star);
            }
        }
    }
}