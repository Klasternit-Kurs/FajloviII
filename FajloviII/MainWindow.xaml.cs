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
using System.IO;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;

namespace FajloviII
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ObservableCollection<Osoba> listaO = new ObservableCollection<Osoba>();

		public MainWindow()
		{
			InitializeComponent();
			DataContext = new Osoba();
			/*try
			{
				File.ReadAllText("t.txt");
			} catch
			{
				MessageBox.Show("Nema fajla :(");
			}

			if (File.Exists("t.txt"))
			{
				File.ReadAllText("t.txt");
			} else
			{
				MessageBox.Show("Nema fajla :(");
			}*/

			/*if (File.Exists("osobe.txt"))
			{
				foreach (string red in File.ReadLines("osobe.txt"))
				{
					//0 - Ime   1 - Prezime
					string[] polja = red.Split(';');
					listaO.Add(new Osoba(polja[0], polja[1]));
				}
			}*/

			if (File.Exists("osobe.txt"))
			{
				BinaryFormatter bf = new BinaryFormatter();
				using (FileStream fs = new FileStream("osobe.txt", FileMode.Open, FileAccess.Read))
				{
					listaO = bf.Deserialize(fs) as ObservableCollection<Osoba>;
				}
			}
			dg.ItemsSource = listaO;
		}

		private void Unos(object sender, RoutedEventArgs e)
		{
			listaO.Add(DataContext as Osoba);
			DataContext = new Osoba();
		}

		private void Snimanje(object sender, RoutedEventArgs e)
		{
			Snimi();
		}

		private void ZatvramSe(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Snimi();

		}

		private void Snimi()
		{
			BinaryFormatter Binarno = new BinaryFormatter();
			using (Stream tokPodataka = new FileStream("osobe.txt", FileMode.Create, FileAccess.Write))
			{
				Binarno.Serialize(tokPodataka, listaO);
			}
			//tokPodataka.Close();

			/*if (File.Exists("osobe.txt"))
			{
				File.Delete("osobe.txt");
			}
			foreach(Osoba o in listaO)
			{
				File.AppendAllText("osobe.txt", $"{o.Ime};{o.Prezime}" + Environment.NewLine);
			}*/
		}
	}
}
