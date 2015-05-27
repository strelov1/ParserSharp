using System;
using System.IO;
using System.Data.OleDb;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xNet.Net;
using xNet.Text;

namespace Data
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void сохранитьToolStripButton_Click(object sender, EventArgs e)
        {
            Stream str;
                saveFileDialog1.FileName = "Отчет по сайту";
                saveFileDialog1.Filter = "Текстовыефайлы (*.txt) |*.txt";
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    str = saveFileDialog1.OpenFile();
                    StreamWriter sym = new StreamWriter(str);
                    try
                    {
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            {
                                sym.Write(dataGridView1.Rows[i].Cells[j].Value.ToString() + '\t' + '\t');
                            }
                            sym.WriteLine();
                        }
                    }
                    catch {}
                    sym.Close();
                    str.Close();
                }

        }

        private string GetPages(string PageUrl)
        {
                string[] raw;
                using (var response = new HttpRequest())
                {
                    string SourcePage;
                    string kode;
                    string urlPage;
                    string inUrl;

                    SourcePage = response.Get(PageUrl).ToString();

                    raw = SourcePage.Substrings("<a href=\"", "\"", 0);

                    for (int i = 0; i < raw.Length; i++)
                    {
                        inUrl = raw[i].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).First();

                        if (raw[i] != "#" && raw[i] != "/" && raw[i] != "javascript://" && inUrl != "mailto" && inUrl != "tel" && inUrl != "http")
                        {
                            urlPage = PageUrl + raw[i];
                            kode = response.Get(urlPage).StatusCode.ToString();
                            dataGridView1.Rows.Add(i, urlPage, kode);
                        }
                    }
                    return "";
                }

       }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetPages(textBox1.Text);
        }

    }
}
