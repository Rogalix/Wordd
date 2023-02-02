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
using Words = Microsoft.Office.Interop.Word;

namespace Word
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Word_Click(object sender, RoutedEventArgs e)
        {
            Core db = new Core();
            var allUsers = db.context.users.ToList();
            var allCategories = db.context.Categoty.ToList();

            var application = new Words.Application();

            Words.Document document = application.Documents.Add();

            foreach (var user in allUsers)
            {
                Words.Paragraph userParagrapth = document.Paragraphs.Add();
                Words.Range userRange = userParagrapth.Range;
                userRange.Text = user.last_name;
                userParagrapth.set_Style("Заголовок 1");
                userRange.InsertParagraphAfter();

                Words.Paragraph tableParagraph = document.Paragraphs.Add();
                Words.Range tableRange = tableParagraph.Range;
                Words.Table paymentsTable = document.Tables.Add(tableRange, allCategories.Count() + 1, 3);
                paymentsTable.Borders.InsideLineStyle = paymentsTable.Borders.OutsideLineStyle = Words.WdLineStyle.wdLineStyleSingle;
                paymentsTable.Range.Cells.VerticalAlignment = Words.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

               

            }
            application.Visible = true;
        }
    }
}
