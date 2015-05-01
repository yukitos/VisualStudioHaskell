using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Company.VisualStudioHaskell.Project
{
    public partial class WpfPropertyPageControl<T> : UserControl
        where T : System.Windows.Controls.Control
    {
        public WpfPropertyPageControl(T control)
        {
            InitializeComponent();
            elementHost1.Child = control;
        }
    }
}
