using Abeslamidze_Kursovaya7.ViewModels;
using System.Windows;

namespace Abeslamidze_Kursovaya7
{
    public partial class DiagramWindow : Window
    {
        public DiagramWindow(BaseDiagramWindowViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
