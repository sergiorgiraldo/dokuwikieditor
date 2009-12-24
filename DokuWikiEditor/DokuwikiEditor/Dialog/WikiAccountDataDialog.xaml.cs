using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DokuwikiClient.Domain.Entities;

namespace DokuWikiEditor.Dialog
{
	/// <summary>
	/// Interaction logic for WikiAccountDataDialog.xaml
	/// </summary>
	public partial class WikiAccountDataDialog : Window
	{
		private WikiAccount accountToEdit;

		public WikiAccountDataDialog(WikiAccount account)
		{
			this.accountToEdit = account;
			this.DataContext = account;
			InitializeComponent();
		}

		private void buttonOk_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
