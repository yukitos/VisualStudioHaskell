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
    public partial class HaskellDebugPropertyPageControl : UserControl
    {
        private readonly HaskellDebugPropertyPage _page;

        public HaskellDebugPropertyPageControl(HaskellDebugPropertyPage page)
        {
            InitializeComponent();

            _page = page;
        }
    }
}
