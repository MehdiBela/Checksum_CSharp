﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Checksum
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Benchmark : IDisposable
        {
            private readonly Stopwatch timer = new Stopwatch();
  

            public Benchmark()
            {
                timer.Start();
            }

            public void Dispose()
            {
                timer.Stop();
                Console.WriteLine($"{timer.Elapsed}");
                Console.ReadLine();
            }
        }

        private static string GetSHA2(string file)
        {
            using (var stream = new BufferedStream(File.OpenRead(file), 1200000))
            {
                var sha2 = new SHA256Managed();
                byte[] checksum_sha2 = sha2.ComputeHash(stream);
                return BitConverter.ToString(checksum_sha2).Replace("-", String.Empty);

            }
        }
        private static string GetSHA1(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                var sha1 = new SHA1Managed();
                byte[] checksum_sha1 = sha1.ComputeHash(stream);
                return BitConverter.ToString(checksum_sha1).Replace("-", String.Empty);
            }
        }

        private static string GetSHA3(string file)
        {
            using (var stream = new BufferedStream(File.OpenRead(file), 1200000))
            {
                var sha3 = new SHA384Managed();
                byte[] checksum_sha3 = sha3.ComputeHash(stream);
                return BitConverter.ToString(checksum_sha3).Replace("-", String.Empty);
            }
        }

        private static string GetSHA5(string file)
        {
            using (var stream = new BufferedStream(File.OpenRead(file), 1200000))
            {
                var sha5 = new SHA512Managed();
                byte[] checksum_sha5 = sha5.ComputeHash(stream);
                return BitConverter.ToString(checksum_sha5).Replace("-", String.Empty);
            }
        }



        private static string GetMD5(string file)
        {
            using (var stream = new BufferedStream(File.OpenRead(file), 1200000))
            {
                var md5 = new MD5CryptoServiceProvider();
                byte[] checksum_md5 = md5.ComputeHash(stream);
                return BitConverter.ToString(checksum_md5).Replace("-", String.Empty);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label6.Visible = false;
                textBox4.Text = openFileDialog1.FileName;
            }
        }
        private void textBox4_DragDrop(object sender,
        System.Windows.Forms.DragEventArgs e)
        {
            textBox4.Text = e.Data.GetData(DataFormats.Text).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(textBox1.Text);
            }
            catch (ArgumentNullException) { }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(textBox2.Text);
            }
            catch (ArgumentNullException) { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(textBox3.Text);
            }
            catch (ArgumentNullException) { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string input = textBox5.Text;
            if (input != "")
            {
                if (input == textBox1.Text)
                {
                    label6.Text = "SHA256 OK";
                    label6.Visible = true;
                    label6.ForeColor = System.Drawing.Color.Green;
                }
                else if (input == textBox2.Text)
                {
                    label6.Text = "SHA1 OK";
                    label6.Visible = true;
                    label6.ForeColor = System.Drawing.Color.Green;
                }
                else if (input == textBox3.Text)
                {
                    label6.Text = "MD5 OK";
                    label6.Visible = true;
                    label6.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    label6.Text = "Aucun hash ne correspond";
                    label6.Visible = true;
                    label6.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                label6.Text = "Veuillez saisir un hash !";
                label6.Visible = true;
                label6.ForeColor = System.Drawing.Color.Red;
            }

        }


        private void hash_sha2_Click(object sender, EventArgs e)
        {
            using (var bench = new Benchmark())
            {
                textBox1.Text = GetSHA2(openFileDialog1.FileName);
            }
        }

        private void hash_sha1_Click(object sender, EventArgs e)
        {
            using (var bench = new Benchmark())
            {
                textBox2.Text = GetSHA1(openFileDialog1.FileName);
            }
            
        }

        private void hash_md5_Click(object sender, EventArgs e)
        {
            using (var bench = new Benchmark())
            {
                textBox3.Text = GetMD5(openFileDialog1.FileName);
            }
            
        }
    }
}
