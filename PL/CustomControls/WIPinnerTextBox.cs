using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    using System.Windows.Forms;

    public class WIPinnerTextBox : Control
    {
        private string text = "";
        private Timer caretTimer;
        private bool showCaret = true;
    }
}
