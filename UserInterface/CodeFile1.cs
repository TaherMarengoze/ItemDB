

using System.Windows.Forms;

public class Demo
{
    public string A { get; set; }
    public string B { get; set; }
    public string C { get; set; }
    public string D { get; set; }
}

public class DemoUI
{

    TextBox txtParam1 = new TextBox();
    TextBox txtParam2 = new TextBox();
    TextBox txtParam3 = new TextBox();
    TextBox txtParam4 = new TextBox();

    public DemoUI(Demo demo)
    {
        txtParam1.Text = demo.A;
        txtParam2.Text = demo.B;
        txtParam3.Text = demo.C;
        txtParam4.Text = demo.D;
    }

}
