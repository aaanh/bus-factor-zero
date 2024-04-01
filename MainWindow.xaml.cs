using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace bus_factor_zero
{
	class Account
	{
		public string service;
		public string username;
		public string password;

		public Account()
		{

		}

		public Account(string service, string username, string password)
		{
			this.service = service;
			this.username = username;
			this.password = password;
		}
	}

	class Contact
	{
		public string name;
		public string email;
		public string phone;
		public string comments;

		public Contact()
		{

		}

		public Contact(string name, string email, string phone)
		{
			this.name = name;
			this.email = email;
			this.phone = phone;
		}
	}

	/// <summary>
	/// An empty window that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainWindow : Window
	{
		List<Account> accounts = new List<Account> ();
		List<Contact> contacts = new List<Contact> ();

		void updateCredentialDataDisplay()
		{
			dataCredentials.Children.Clear();
			Int32 idx = 1;
			foreach (Account acc in accounts)
			{
				TextBlock txtBlk = new TextBlock();
				string obfuscatedPwd = "";
				foreach (char c in acc.password)
				{
					obfuscatedPwd += "*";
				}
				txtBlk.Text = "#" + idx + " Service: " + acc.service + " - Username: " + acc.username + " - Password: " + obfuscatedPwd;
				dataCredentials.Children.Add(txtBlk);
				idx++;
			}
		}

		void updateContactDataDisplay()
		{
			dataContacts.Children.Clear();
			Int32 idx = 1;
			foreach (Contact contact in contacts)
			{
				TextBlock txtBlk = new TextBlock();
				txtBlk.Text = "#" + idx + " Name: " + contact.name + " - E-mail: " + contact.email + " - Phone: " + contact.phone + " - Comments: " + contact.comments;
				dataContacts.Children.Add(txtBlk);
				idx++;
			}
		}

		bool TrySetDesktopAcrylicBackdrop()
		{
			if (Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController.IsSupported())
			{
				Microsoft.UI.Xaml.Media.DesktopAcrylicBackdrop DesktopAcrylicBackdrop = new Microsoft.UI.Xaml.Media.DesktopAcrylicBackdrop();
				this.SystemBackdrop = DesktopAcrylicBackdrop;

				return true; // Succeeded.
			}

			return false; // DesktopAcrylic is not supported on this system.
		}

		public MainWindow()
		{
			TrySetDesktopAcrylicBackdrop();
			this.InitializeComponent();
		}

		private void Add_Credential_Handler(object sender, RoutedEventArgs e)
		{
			infoPopupStackPanel.Children.Clear();
			InfoBar infoBar = new InfoBar();
			infoBar.IsOpen = true;
			infoBar.Severity = InfoBarSeverity.Success;
			infoBar.Title = "Perform action: Add New Credential";

			Account acc = new Account();
			acc.username = GrpAddAccountUsername.Text;
			acc.service = GrpAddAccountService.Text;
			acc.password = GrpAddAccountPassword.Password;


			String msg = "Service: " + GrpAddAccountService.Text + ", Username: " + GrpAddAccountUsername.Text;
			infoBar.Message = msg;
			infoPopupStackPanel.Children.Add(infoBar);
			accounts.Add(acc);
			updateCredentialDataDisplay();
		}

		private void Add_Contact_Handler(object sender, RoutedEventArgs e)
		{
			infoPopupStackPanel.Children.Clear();
			InfoBar infoBar = new InfoBar();
			infoBar.IsOpen = true;
			infoBar.Severity = InfoBarSeverity.Success;
			infoBar.Title = "Perform action: Add New Contact";

			Contact contact = new Contact();
			contact.name = GrpAddContactName.Text;
			contact.email = GrpAddContactEmail.Text;
			contact.phone = GrpAddContactPhone.Text;
			contact.comments = GrpAddContactComments.Text;

			String msg = "Name: " + GrpAddContactName.Text + ", E-mail: " + GrpAddContactEmail.Text + ", Phone: " + GrpAddContactPhone.Text;
			infoBar.Message = msg;
			infoPopupStackPanel.Children.Add(infoBar);



			contacts.Add(contact);
			updateContactDataDisplay();
		}
	}
}
