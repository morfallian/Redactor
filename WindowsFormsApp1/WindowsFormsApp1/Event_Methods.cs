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
    class Event_Methods
    {
        public static void run_code(string c)
        {
            //создаю временный файл
            string tempDirectory = @"C:\Users\Morfa\AppData\Local\Temp";
            System.CodeDom.Compiler.TempFileCollection coll = new System.CodeDom.Compiler.TempFileCollection(tempDirectory, true);
            string fileTMP = coll.AddExtension("py", true);
            File.WriteAllText(Path.Combine(tempDirectory, fileTMP), c);

            using (System.Diagnostics.Process myProcess = new System.Diagnostics.Process())
            {
                //передаем в консоль название файла 
                myProcess.StartInfo.Arguments = @"/k python3 " + fileTMP;
                myProcess.StartInfo.FileName = "cmd.exe";
                //запускаем консоль
                myProcess.Start();
                //ожидаем закрытия консоли
                myProcess.WaitForExit();

                //удаляем временные файлы
                System.IO.File.Delete(fileTMP);
                fileTMP = fileTMP.Replace("py", "tmp");
                System.IO.File.Delete(fileTMP);
            }
        }

        public static bool FindText(string find, RichTextBox box, TextBox tbox)
        {

            bool returnValue = false;
            int linenumber = 0;
            int indexToText = -1;
            if (find.Length > 0)
            {


                indexToText = box.Find(find);
                if (indexToText >= 0)
                {
                    tbox.Text = find;
                    linenumber = box.GetLineFromCharIndex(indexToText);
                    box.SelectionStart = indexToText;
                    box.SelectionLength = find.Length;
                    box.Select(indexToText, find.Length);
                    box.Focus();
                    returnValue = true;

                }

                else
                {

                    MessageBox.Show("Не найдено!");
                    returnValue = false;
                }
            }
            return returnValue;
        }

        public static void Save_Txt(RichTextBox box)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = saveFileDialog1.FileName;

            System.IO.File.WriteAllText(filename, box.Text);

        }

        public static void RunHelp(Form2 form2)
        {
            form2.Show();
        }
    }
}
