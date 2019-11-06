using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        async void AcquireFromCamera(object sender, RoutedEventArgs e)
        {

            try
            {
                var s3f18 = device.Send(s3f17); //direct get device's reply secondary message
                                                //access item value with strong type
                byte returnCode = (byte)s3f18.SecsItem.Items[0]; // access item value. Equal to s3f18.SecsItem.Items[0].Value<byte>()
            }
            catch (SecsException)
            {
                // exception  when
                // T3 timeout
                // device reply SxF0
                // device reply S9Fx
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
