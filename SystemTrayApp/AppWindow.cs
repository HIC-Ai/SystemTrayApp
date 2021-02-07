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

namespace SystemTrayApp
{
    public partial class AppWindow : Form
    {
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
        public AppWindow()
        {
            InitializeComponent();

            Choices commands = new Choices();

            commands.Add(new string[] {
                    "Ahmed"
                });



            Grammar gmr = new Grammar(new GrammarBuilder(commands));
            gmr.Name = "Ahmed";
            // My Dic

            recEngine.LoadGrammar(gmr);
            recEngine.SpeechRecognized +=
            new EventHandler<SpeechRecognizedEventArgs>(recEngine_SpeechRecognized);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.RecognizeAsync(RecognizeMode.Multiple);


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
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
        }
    }
}
