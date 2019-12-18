using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab25ASPFirstWebsite
{
    public partial class HelloWorld : System.Web.UI.Page
    {
        static int i = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_Click(object sender, EventArgs e)
        {
            i++;
            Label1.Text = "Button clicked " + i + " times";
        }
    }
}