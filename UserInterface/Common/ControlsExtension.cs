using CoreLibrary.Enums;
using System.Windows.Forms;


namespace UserInterface.Shared
{
    public static class ControlsExtension
    {
        public static void ValidityInfo(this Label control, InputStatus status)
        {
            switch (status)
            {
                case InputStatus.Valid:
                    control.Text = string.Empty;
                    break;
                case InputStatus.Duplicate:
                    control.Text = "• Duplicate";
                    break;
                case InputStatus.Blank:
                    control.Text = "• Blank";
                    break;
                case InputStatus.Invalid:
                    control.Text = "• Invalid";
                    break;
                default:
                    break;
            }
        }
    }


}