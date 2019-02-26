using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WeGamePlus.Bar_Plug.Models
{
    public class UserPicturebox : PictureBox
    {
        public void SetThisImage(Image image)
        {
            base.Image = image;
        }

        public string ClickUrl { get; set; }

        public string GameType { get; set; }

        public int Statistics { get; set; }
    }
}

