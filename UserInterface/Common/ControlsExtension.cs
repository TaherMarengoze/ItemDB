using CoreLibrary.Enums;
using System.Windows.Forms;


namespace UserInterface.Shared
{
    public static class ControlsExtension
    {
        public static void ValidityInfo(this Label control, ValidityStatus status)
        {
            switch (status)
            {
                case ValidityStatus.Valid:
                    control.Text = string.Empty;
                    break;
                case ValidityStatus.Duplicate:
                    control.Text = "• Duplicate";
                    break;
                case ValidityStatus.Blank:
                    control.Text = "• Blank";
                    break;
                case ValidityStatus.Invalid:
                    control.Text = "• Invalid";
                    break;
                default:
                    break;
            }
        }
    }


}