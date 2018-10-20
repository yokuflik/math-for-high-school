using System.Windows.Forms;

namespace math
{
    public class FuncsViewAndDrawPanel
    {
        public FuncsViewAndDrawPanel(Panel funcsViewPanel, Panel funcsDrawPanel)
        {
            FuncsViewPanel = funcsViewPanel;
            FuncsDrawPanel = funcsDrawPanel;

            //start to show the funcs view
        }

        public Panel FuncsViewPanel { get; set; }
        public Panel FuncsDrawPanel { get; set; }
    }
}