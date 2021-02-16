using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Speech;
using System.Media;
using System.Globalization; //totitlecase
using System.Threading;

namespace SystemTrayApp
{
    public partial class AppWindow : Form
    {
        SpeechRecognitionEngine rec = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
        SpeechSynthesizer speech = new SpeechSynthesizer();
        SoundPlayer player = new SoundPlayer();
        readonly string on = Application.StartupPath + "\\SiriOn.wav";
        readonly string understood = Application.StartupPath + "\\SiriUnderstood.wav";

        private void StartingVoice()
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                Choices choices = new Choices();
                string[] words = { "hello", "open paint", "open google", "open youtube", "what time is it","how are you"
                ,"hey assistant","exit the application","stop listen","open other form","show todays exchange rate"};
                choices.Add(words);
                Grammar grammar = new Grammar(new GrammarBuilder(choices));
                rec.LoadGrammar(grammar);
                rec.SetInputToDefaultAudioDevice();
                rec.RecognizeAsync(RecognizeMode.Multiple);
                rec.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognized);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Lutfen bilgisayariniza 'en-US' paketi yukleyip tekrar deneyin", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        public AppWindow()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            this.CenterToScreen();
            this.Icon = Properties.Resources.Default;
            this.SystemTrayIcon.Icon = Properties.Resources.Default;
            this.SystemTrayIcon.Text = "System Tray App";
            this.SystemTrayIcon.Visible = true;
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add("Exit", ContextMenuExit);
            this.SystemTrayIcon.ContextMenu = menu;

            this.Resize += WindowResize;
            this.FormClosing += WindowClosing;
        }

        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            string result = e.Result.Text;
            string newy = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result);
            bool control = false;
            Random rd = new Random();
            Console.WriteLine(result);
            if (this.WindowState == FormWindowState.Minimized)
                Console.WriteLine("hello");
            {
                if (result == "hello")
                    {
                    this.Activate();
                    this.BringToFront();

                    this.TopMost = true;
                        this.CenterToScreen();

                        this.Resize += WindowResize;
                        this.FormClosing += WindowClosing;
                        //this.Show();
                        this.Visible = true;
                        this.Opacity = 100;
                        this.FormBorderStyle = FormBorderStyle.FixedSingle; //or whatever it was previously set to
                        this.ShowInTaskbar = true;
                        this.StartPosition = FormStartPosition.CenterScreen;
                        this.WindowState = FormWindowState.Normal;





                    Console.WriteLine("lol");

                    }

            }
            if (result.Contains("are you"))
            {
                result = "I'm better now that I'm talking to you";
            }
            else if (result == "how are you")
            {
                result = "I'm better now that I'm talking to you";
            }
            else if (result == "hello")
            {

                int i = rd.Next(1, 3);
                if (i == 1)
                {
                    result = "Hello,How are you";
                }
                else
                {
                    result = "What can I do for you?";
                }

            }

            else if (result == "open youtube")
            {
                System.Diagnostics.Process.Start("https://www.youtube.com/?gl=TR");
                result = "Opening youtube";
            }

            else if (result == "open google")
            {
                result = "Opening google";
                System.Diagnostics.Process.Start("https://www.google.com.tr");
            }
            else if (result == "open paint")
            {
                result = "Opening paint";
                System.Diagnostics.Process.Start("MSpaint.Exe");

            }
            else if (result == "exit the application")
            {
                result = "Okey closing it";
                Application.Exit();

            }
            else if (result == "what time is it")
            {
                result = "It is " + DateTime.Now.ToLongTimeString();
            }
          
            else
            {
                result = "Please repeat";

            }
         
        }

        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        string[] data_personal =  { "ahmed","what is your name", "GoSmart"
                                    , "hallo", "what are you doing now", "where are you from"
                                    ,"how old are you","thank you","do you know INDONESIA" };

        void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    if (e.Result.Text == "Ahmed")
                    {
                        synthesizer.SpeakAsync("no one know who am i, i come from another galaxy");
                        this.Show();
                        Console.WriteLine("lol");

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }
        //public AppWindow()
        //{
        //    InitializeComponent();

        //    Choices commands = new Choices();

        //    commands.Add(new string[] {
        //            "Ahmed"
        //        });



        //    Grammar gmr = new Grammar(new GrammarBuilder(commands));
        //    gmr.Name = "Ahmed";
        //    // My Dic

        //    recEngine.LoadGrammar(gmr);
        //    recEngine.SpeechRecognized +=
        //    new EventHandler<SpeechRecognizedEventArgs>(recEngine_SpeechRecognized);
        //    recEngine.SetInputToDefaultAudioDevice();
        //    recEngine.RecognizeAsync(RecognizeMode.Multiple);


        //    this.CenterToScreen();
        //    this.Icon = Properties.Resources.Default;
        //    this.SystemTrayIcon.Icon = Properties.Resources.Default;
        //    this.SystemTrayIcon.Text = "System Tray App";
        //    this.SystemTrayIcon.Visible = true;
        //    ContextMenu menu = new ContextMenu();
        //    menu.MenuItems.Add("Exit", ContextMenuExit);
        //    this.SystemTrayIcon.ContextMenu = menu;

        //    this.Resize += WindowResize;
        //    this.FormClosing += WindowClosing;
        //}
        
        private void SystemTrayIconDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void ContextMenuExit(object sender, EventArgs e)
        {
            this.SystemTrayIcon.Visible = false;
            Application.Exit();
            Environment.Exit(0);
        }

        private void WindowResize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {

                this.Hide();
            }
        }

        private void WindowClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void AppWindow_Load(object sender, EventArgs e)
        {
            player.SoundLocation = on;
            player.Play();
            StartingVoice();
        }
    }
}
