using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication13.Controls
{
    public partial class PaymentDestinationControl : UserControl
    {
        public PaymentDestinationControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
