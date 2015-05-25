using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MojeCisnienie.ViewModels;
using System.Diagnostics;

namespace MojeCisnienie
{
    public partial class CisnienieForm : PhoneApplicationPage
    {
        public CisnienieForm()
        {
            InitializeComponent();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.isDataLoaded)
            {
                Debug.WriteLine("No data, loading it now");
                App.ViewModel.loadData();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RozkurczoweText.Text = "";
            SkurczoweText.Text = "";
            TetnoText.Text = "";
            NotatkaText.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool isValid = true;
            int rozkurczowe;
            int skurczowe;
            int tetno;
            string notatka = NotatkaText.Text;

            int.TryParse(RozkurczoweText.Text, out rozkurczowe);
            int.TryParse(SkurczoweText.Text, out skurczowe);
            int.TryParse(TetnoText.Text, out tetno);

            if (rozkurczowe <= 0 || skurczowe <= 0 || tetno <= 0)
            {
                MessageBox.Show("Zadna z wartosci pomiaru nie moze byc zerem.");
                isValid = false;
            }
            
            if (isValid && (rozkurczowe < 70 || rozkurczowe > 190))
            {
                MessageBox.Show("Podaj poprawną wartosc pomiaru cisnienia rozkurczowego.");
                isValid = false;
            }

            if (isValid && (skurczowe < 40 || skurczowe > 100))
            {
                MessageBox.Show("Podaj poprawną wartosc pomiaru cisnienia skurczowego.");
                isValid = false;
            }

            if (isValid)
            {
                Debug.WriteLine("Dodano nowy pomiar");

                App.ViewModel.dodajPomiar(rozkurczowe,skurczowe,tetno,notatka);
                App.ViewModel.isDataLoaded = false;

                NavigationService.GoBack();
            }     
        }
    }
}