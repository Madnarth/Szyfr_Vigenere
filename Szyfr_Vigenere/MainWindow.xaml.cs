using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Szyfr_Vigenere
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            byte[] ASCIIbytes = new byte[91 - 65];
            for (byte i = 65, n = 0; i < 91; ++i, ++n)
            {
                ASCIIbytes[n] = i;
            }
            char[] znaki = Encoding.ASCII.GetChars(ASCIIbytes);
            for (sbyte i = 0; i < 26; ++i)
            {
                kolejnoscAlfabetu.Add(i, znaki[i]);
            }
        }
        private readonly Dictionary<sbyte, char> kolejnoscAlfabetu = new Dictionary<sbyte, char>();

        private bool SprawdzCzyPustyTekst(string klucz, string tekst)
        {
            if (string.IsNullOrEmpty(klucz) || string.IsNullOrWhiteSpace(klucz))
            {
                return true;
            }
            if (string.IsNullOrEmpty(tekst) || string.IsNullOrWhiteSpace(tekst))
            {
                return true;
            }
            return false;
        }

        private void szyfruj(object sender, RoutedEventArgs e)
        {
            try
            {
                TxtBoxKlucz.Text = TxtBoxKlucz.Text.ToUpper();
                TxtBoxNieszyf.Text = TxtBoxNieszyf.Text.ToUpper();

                if (SprawdzCzyPustyTekst(TxtBoxKlucz.Text, TxtBoxNieszyf.Text)) { MessageBox.Show("Proszę wprowadź klucz"); }

                TxtBoxSzyf.Text = "";

                int i = 0;

                foreach (char element in TxtBoxNieszyf.Text)
                {
                    if (!Char.IsLetter(element)) { TxtBoxSzyf.Text += element; }
                    else
                    {
                        sbyte TOrder = kolejnoscAlfabetu.FirstOrDefault(x => x.Value == element).Key;
                        sbyte KOrder = kolejnoscAlfabetu.FirstOrDefault(x => x.Value == TxtBoxKlucz.Text[i]).Key;
                        sbyte Final = (sbyte)(TOrder + KOrder);
                        if (Final > 25) { Final -= 26; }
                        TxtBoxSzyf.Text += kolejnoscAlfabetu[Final];
                        ++i;
                    }
                    if (i == TxtBoxKlucz.Text.Length) { i = 0; }
                }

            }
            catch (Exception E)
            {
                
            }
        }

        private void deszyfruj(object sender, RoutedEventArgs e)
        {
            try
            {
                TxtBoxKlucz.Text = TxtBoxKlucz.Text.ToUpper();
                TxtBoxZaszyf.Text = TxtBoxZaszyf.Text.ToUpper();

                if (SprawdzCzyPustyTekst(TxtBoxKlucz.Text, TxtBoxZaszyf.Text)) { MessageBox.Show("Proszę wprowadź klucz"); }

                TxtBoxRoszyf.Text = "";

                int i = 0;

                foreach (char element in TxtBoxZaszyf.Text)
                {
                    if (!Char.IsLetter(element)) { TxtBoxRoszyf.Text += element; }
                    else
                    {
                        sbyte TOrder = kolejnoscAlfabetu.FirstOrDefault(x => x.Value == element).Key;
                        sbyte KOrder = kolejnoscAlfabetu.FirstOrDefault(x => x.Value == TxtBoxKlucz.Text[i]).Key;
                        sbyte Final = (sbyte)(TOrder - KOrder);
                        if (Final < 0) { Final += 26; }
                        TxtBoxRoszyf.Text += kolejnoscAlfabetu[Final];
                        ++i;
                    }
                    if (i == TxtBoxKlucz.Text.Length) { i = 0; }
                }
            }
            catch (Exception E)
            {
                
            }
        }
        private void sprawdzaj_klucz(object sender, RoutedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(TxtBoxKlucz.Text, "[^A-Z]"))
            {
                TxtBoxKlucz.Text = TxtBoxKlucz.Text.Remove(TxtBoxKlucz.Text.Length - 1);
            }
        }
    }
}
            
