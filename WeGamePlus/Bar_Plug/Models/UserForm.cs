using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WeGamePlus.Bar_Plug.Models
{
    public class UserForm : Form
    {
        public string text { get; set; }

        public string type { get; set; }

        public Point location1 { get; set; }

        public Size size1 { get; set; }

        public Point location2 { get; set; }

        public Size size2 { get; set; }

        public Point location3 { get; set; }

        public Size size3 { get; set; }

        public Image image { get; set; }

        public string imageType { get; set; }

        public Image hoverimage { get; set; }

        public Point hideLocation1 { get; set; }

        public Point hideLocation2 { get; set; }

        public Point hideLocation3 { get; set; }

        public Image closeimage { get; set; }

        public Point closeLocation { get; set; }

        public Point closeSize { get; set; }

        public string hideColor1 { get; set; }

        public string hideColor2 { get; set; }

        public string hideColor3 { get; set; }

        public string isShow { get; set; }

        public bool isOpen { get; set; }

        public string WeGameLeftStart { get; set; }

        public string WeGameLeftEnd { get; set; }

        public string WeGameTop { get; set; }

        public string WeGameLoLColor { get; set; }

        public string WeGameDnfColor { get; set; }

        public string WeGameCFColor { get; set; }

        public string WeGameNZColor { get; set; }

        public Image WeGameLoLImage { get; set; }

        public Image WeGameDnfImage { get; set; }

        public Image WeGameCFImage { get; set; }

        public Image WeGameNZImage { get; set; }

        public string ClassName { get; set; }

        public string WindowName { get; set; }

        public string ClickUrl { get; set; }

        public int statistics { get; set; }
    }
}

