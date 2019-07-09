using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Remote;
using System.Windows.Input;
using AutoIt;

namespace vse10RUbot
{
    public partial class Main : Form
    {
        ChromeDriver CD;
        
       string Login
        {
            get
            {
                return textBox1.Text;
            }
        }
        string Password
        {
            get
            {
                return textBox2.Text;
            }
        }

        public Main()
        {
            ChromeOptions OP = new ChromeOptions();
            OP.AddArgument("--allow-no-sandbox-job");
            ChromeDriverService SERVICE = ChromeDriverService.CreateDefaultService();
            //SERVICE.HideCommandPromptWindow = true;
            CD = new ChromeDriver(SERVICE, OP);
             InitializeComponent();

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Auth(textBox1.Text, textBox2.Text);
            CommitTest(TestLang.RUS);
        }

        public void Auth(string login, string password)
        {
            CD.Navigate().GoToUrl("https://www.ratatype.ru/exit/");
            CD.Navigate().GoToUrl("https://www.ratatype.ru/login/");
            ///////////////
            CD.FindElementByXPath("//input[@name='login']").Clear();
            CD.FindElementByXPath("//input[@name='login']").SendKeys(Login);
                CD.FindElementByXPath("//input[@name='password']").SendKeys(Password);
                ///////////////
                CD.FindElementByXPath("//button[@type='submit']").Submit();
                ///////////////
                log.Text += "Логин успешен! \n";
                //////////////
        }
        public bool CommitTest(TestLang lang)
        {
            if (lang == TestLang.RUS)
            {
               
                CD.Navigate().GoToUrl($"https://www.ratatype.ru/typing-test/test/"); 
                
                
            }
            else
            {
                CD.Navigate().GoToUrl($"https://www.ratatype.ru/typing-test/test/en");
            }

            CD.FindElementByXPath("//button[@class='submit gr']").Click();
            var Elements = CD.FindElementsByXPath("//div[@class='mainTxt']/span");
            var TextField = CD.FindElementByXPath("//div[@class='divTextarea']/textarea");
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("ru-RU"));
            AutoItX.MouseClick("LEFT", TextField.Location.X, TextField.Location.Y, 1);


            foreach (var item in Elements)
            {
                Thread.Sleep((this.trackBar1.Value));
                string TextToSend = item.Text;
                if (TextToSend == "")
                {
                    AutoItX.Send(" ");
                }
                else
                {
                    AutoItX.Send(TextToSend);
                }

                //TextField.SendKeys(TextToSend);


            }
            return true;
        }
        public enum TestLang
        {
            RUS = 1,
            ENG = 2
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Auth(textBox1.Text, textBox2.Text);
            CommitTest(TestLang.ENG);
        }
    }
}
