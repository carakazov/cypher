using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EncodingApp.logic;

namespace EncodingApp
{
    public partial class Form1 : Form
    {
        private string alphabet;
        private IEncoder encoder;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = encoder.Encode(richTextBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = encoder.Decode(richTextBox2.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DefineAlphabet();
            encoder = new VigenereEncoder(textBox1.Text, alphabet);
        }
        
        private void DefineAlphabet()
        {
            if (radioButtonEnglish.Checked)
            {
                alphabet = Utils.LatinAlphabet;
            }
            else
            {
                alphabet = Utils.CyrillicAlphabet;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            encoder = new ShuffleEncoder(textBox1.Text);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            encoder = new GammaEncoder();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            encoder = new MatrixEncoder();
        }
    }
}
