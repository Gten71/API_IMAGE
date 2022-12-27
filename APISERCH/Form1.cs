using AngleSharp.Html.Parser;
using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Windows.Forms;

namespace APISERCH
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string templateUrl = "https://www.bing.com/images/search?q=" + textBox.Text;
            
            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show("Please supply a search term"); return;
            }
            else
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0)");
                    string result = wc.DownloadString(templateUrl);
                   
                        //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        //doc.LoadHtml(result);
                        
                        //var Img = from a in doc.DocumentNode.Descendants("img")
                        //          from img in a.Descendants("img")

                        //              // where a.Attributes["class"] != null && a.Attributes["class"].Value == "img"
                        //              //&& img.Attributes["src"] != null && img.Attributes["src"].Value.Contains("img")
                        //          select img;
                        
                        var parser = new HtmlParser();
                        var document = parser.ParseDocument(result);
                        var elementsOl = document.GetElementsByTagName("img");
                        var el=elementsOl[1];
                    
                        string txt = el.OuterHtml;
                        var doc = parser.ParseDocument(txt);
                        var href = doc.QuerySelector("img");
                        string url = href.GetAttribute("src");

                        byte[] downloadedData = wc.DownloadData(url);


                        if (downloadedData != null)
                        {
                            System.IO.MemoryStream ms = new System.IO.MemoryStream(downloadedData, 0, downloadedData.Length);
                            ms.Write(downloadedData, 0, downloadedData.Length);
                            pictureBox1.Image = Image.FromStream(ms);
                        }

                    }

                
            }
        }
    }
}