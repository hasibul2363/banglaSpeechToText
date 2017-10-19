using PhoneticConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhoneticConversionTest
{
    public partial class Form1 : Form
    {
        BDConverter _bdConverter;
        public Form1()
        {
            InitializeComponent();
            _bdConverter = new BDConverter();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show (_bdConverter.GetBanglaPhonaticSentence(textBox1.Text ));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
