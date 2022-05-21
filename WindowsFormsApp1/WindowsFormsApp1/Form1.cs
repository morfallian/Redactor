using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Recognition.SrgsGrammar;



namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string str = "";
        Form2 form2;
        SpeechRecognition SpeechRecog;

        public Form1()
        {
            InitializeComponent();
            //textBox1.ScrollBars = ScrollBars.Both;
            form2 = new Form2();
            form2.OnsaveComands += OnsaveComands;
            SpeechRecog = new SpeechRecognition(richTextBox1, textBox1, form2);
            richTextBox1.ScrollBars = (RichTextBoxScrollBars)ScrollBars.Both;
        }

        private void OpenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = openFileDialog1.FileName;

            string fileText = System.IO.File.ReadAllText(filename);
            richTextBox1.Text = fileText;
            Text = filename;
            str = fileText;
        }
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Event_Methods.Save_Txt(richTextBox1);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (richTextBox1.Text == str)
                e.Cancel = false;
            else
                switch (MessageBox.Show(this, "Произошли изменения в файле, закрыть без сохранения?", "Closing", MessageBoxButtons.YesNo))
                {
                    case DialogResult.No:
                        e.Cancel = true;
                        Event_Methods.Save_Txt(richTextBox1);
                        str = richTextBox1.Text;
                        break;
                    default:
                        break;
                }
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            SpeechRecog.SpechRecog();
        }
        //Жмем кнопку "Найти"
        private void button2_Click(object sender, EventArgs e)
        {
            Event_Methods.FindText(textBox1.Text, richTextBox1, textBox1);
        }
        //запускаем код
        private void button3_Click(object sender, EventArgs e)
        {
            Event_Methods.run_code(richTextBox1.Text);
        }
        //Scrool
        private void button4_Click(object sender, EventArgs e)
        {
            Event_Methods.RunHelp(form2);
        }

        private void OnsaveComands()
        {
            SpeechRecog.StopRecognition();
            SpeechRecog.SpechRecog();
        }
    }
}

