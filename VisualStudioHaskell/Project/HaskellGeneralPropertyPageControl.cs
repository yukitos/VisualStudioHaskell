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
    public partial class HaskellGeneralPropertyPageControl : UserControl
    {
        private readonly HaskellGeneralPropertyPage _propPage;

        public HaskellGeneralPropertyPageControl(HaskellGeneralPropertyPage newHaskellGeneralPropertyPage)
        {
            InitializeComponent();

            _propPage = newHaskellGeneralPropertyPage;
        }
    }
}
