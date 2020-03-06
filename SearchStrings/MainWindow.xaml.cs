using System.Windows;


namespace SearchStrings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtQuery.Text = "";
            lstAND.Items.Clear();
            lstNOT.Items.Clear();
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {

            var SearchObject = new ProcessString(txtQuery.Text);
            SearchObject.Validate();
            if (SearchObject.ANDlist.Count > 0) 
            {
                foreach (string s in SearchObject.ANDlist)
                {
                    lstAND.Items.Add(s);
                }
            }

            if (SearchObject.NOTlist.Count > 0)
            {
                foreach (string s in SearchObject.NOTlist)
                {
                    lstNOT.Items.Add(s);
                }
            }

            

            

        }
    }
}
