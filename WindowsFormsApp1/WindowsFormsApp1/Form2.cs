using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public delegate void ActionHandler();
        public event ActionHandler OnsaveComands;
        public Form2()
        {
            InitializeComponent();
            ShowCommands();
        }

        private void ShowCommands()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string grammarPath = @"C:\Users\Morfa\Diplom\WindowsFormsApp1\WindowsFormsApp1\Comands.xml";

            string filename = grammarPath;

            string fileText = System.IO.File.ReadAllText(filename);
            richTextBox1.Text = fileText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Event_Methods.Save_Txt(richTextBox1);
            OnsaveComands?.Invoke();
        }
    }
}
