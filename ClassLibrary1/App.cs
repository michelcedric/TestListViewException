using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ClassLibrary1
{
    public class App : Application
    {
        public App()
        {
            MainPage = new MyPage();
        }
    }

    public class MyPage : ContentPage
    {
        ListView listView = new ListView();
        ObservableCollection<string> firstList = new ObservableCollection<string>(new[] { "Fred", "Barney", "Wilma", "Betty" });
        ObservableCollection<string> secondList = new ObservableCollection<string>(new[] { "Cedric" });
        ObservableCollection<string> emptyList = new ObservableCollection<string>();
        ObservableCollection<string> workaroundList = new ObservableCollection<string>();
        public MyPage()
        {

            Button clearButton = new Button { Text = "Clear (exception)" };
            Button nullButton = new Button { Text = "Null (exception)" };
            Button changeButton = new Button { Text = "Change source (no exception)" };
            Button changeToEmptyListButton = new Button { Text = "Change to empty list (exception)" };
            Button workaroundButton = new Button { Text = "workaround list (no exception)" };

            listView.ItemsSource = firstList;
            clearButton.Clicked += ClearButton_Clicked;
            nullButton.Clicked += NullButton_Clicked;
            changeButton.Clicked += ChangeButton_Clicked;
            changeToEmptyListButton.Clicked += ChangeToEmptyListButton_Clicked;
            workaroundButton.Clicked += WorkaroundButton_Clicked;

            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    clearButton,
                    nullButton,
                    changeButton,
                    changeToEmptyListButton,
                    workaroundButton,
                    listView
                }
            };
        }

        private void NullButton_Clicked(object sender, EventArgs e)
        {
            //exception
            listView.ItemsSource = null;
        }

        private void WorkaroundButton_Clicked(object sender, EventArgs e)
        {
            //workaround if the list is empty add a fake item, assign this list, remove this item
            if (workaroundList.Count <= 0)
            {
                workaroundList.Add("fake item");
            }
            listView.ItemsSource = workaroundList;
            workaroundList.RemoveAt(0);
        }

        private void ChangeToEmptyListButton_Clicked(object sender, EventArgs e)
        {
            //exception
            listView.ItemsSource = emptyList;
        }

        private void ChangeButton_Clicked(object sender, EventArgs e)
        {
            //no exception
            listView.ItemsSource = secondList;
        }

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            //Bug exception
            firstList.Clear();
        }
    }
}
