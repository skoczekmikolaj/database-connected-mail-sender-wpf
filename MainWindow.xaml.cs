using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Net.Mime;

namespace ProjektInformatyczny
{
    public partial class MainWindow : Window
	{
		Uri fileUri;
		public MainWindow()
		{
		InitializeComponent();
		}
		private void ListaKlientow_Click(object sender, RoutedEventArgs e)
		{
		try
		{  
			List<string> AdresyKlientow = new List<string>();
			FirmaKursyEntities en = new FirmaKursyEntities();
				foreach (Klient k in en.Klient.ToList())
				{
					if (k.ZgodaNaNewsletter == true)
					{
						AdresyKlientow.Add(k.AdresMail);
					}
				}
			string txt = "";
			txt = "Klienci, którzy wyrazili zgodę na wysłanie newslettera:\n";
			foreach (string a in AdresyKlientow)
			{
				txt += a + '\n';
			}
			
			MessageBox.Show(txt);
		}
			catch (System.Data.Entity.Core.EntityException)
			{
				MessageBox.Show("nie masz polaczenia z bazą. Dostosuj właściwość Data Source w connection stringu" +
					"w pliku App.config tego projektu");
			}
		}
		private void Plik_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			openFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
			openFileDialog1.Title = "Wybierz obraz";
			openFileDialog1.ShowDialog();
			try
			{
				fileUri = new Uri(openFileDialog1.FileName);
				BitmapImage image1 = new BitmapImage(fileUri);
				NazwaPliku.Content += fileUri.ToString();
				Plik.IsEnabled = false;
				MessageBox.Show("Pomyślnie wybrano plik");
			}
			catch (UriFormatException)
			{MessageBox.Show("nieprawidlowy format pliku");}
			catch (ArgumentException)
			{MessageBox.Show("nieprawidlowy format pliku");}
		}
		private void Wyslij_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				List<string> AdresyKlientow = new List<string>();
				FirmaKursyEntities en = new FirmaKursyEntities();
				foreach (Klient k in en.Klient.ToList())
				{
					if (k.ZgodaNaNewsletter == true)
					{
						AdresyKlientow.Add(k.AdresMail);
					}
				}
				bool jeden = true;
				foreach (string k in AdresyKlientow)
				{
					var message = new MailMessage();
					message.From = new MailAddress("empatyzacja@gmail.com", "EMTRI COURSE ADACEMY");
					message.To.Add(new MailAddress(k));
					message.Subject = "Poznaj nasze nowości";
					message.Body = trescMaila.Text;
					string body = trescMaila.Text;
					byte[] reader = File.ReadAllBytes(fileUri.LocalPath);
					MemoryStream obraz = new MemoryStream(reader);
					AlternateView av = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
					LinkedResource headerImage = new LinkedResource(obraz, MediaTypeNames.Image.Jpeg);
					headerImage.ContentId = "newsletter";
					headerImage.ContentType = new ContentType("image/jpg");
					av.LinkedResources.Add(headerImage);
					message.AlternateViews.Add(av);
					ContentType mimeType = new ContentType("text/html");
					AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
					message.AlternateViews.Add(alternate);
					SmtpClient smtp = new SmtpClient();
					smtp.Host = "smtp.gmail.com";
					smtp.Port = 587;
					smtp.EnableSsl = true;
					smtp.UseDefaultCredentials = false;
					smtp.Credentials = new NetworkCredential("empatyzacja@gmail.com", "Empatyzacja1!");
					try
					{
						smtp.SendAsync(message, k);
						if (jeden == true)
						{ MessageBox.Show("Wysłano pomyślnie do wszystkich"); }
						jeden = false;
					}
					catch (SmtpException ex)
					{
						throw new ApplicationException("Klient SMTP wywołał wyjątek. Sprawdź połączenie z internetem." + ex.Message);
					}
				}
			}
			catch (System.Data.Entity.Core.EntityException)
			{
				MessageBox.Show("nie masz polaczenia z bazą. Dostosuj właściwość Data Source w connection stringu" +
					"w pliku App.config tego projektu");
			}
		}
	}
}
