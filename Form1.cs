using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;

namespace NewPlex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Hide();
            g = 1;

        }
        [DllImport("User32.dll")]
        static extern bool SetForegroundWindow(IntPtr point);
        public void NewPlex()
        {
            g = 0;
            notifyIcon1.ShowBalloonTip(1000, "Adding plex User", " Dont do anything while the process is running!", ToolTipIcon.Info);
            string b = File.ReadAllLines(@"c:\temp\access.txt").Last();
            Process.Start(@"C:\Program Files (x86)\Opera\launcher.exe", "http://sindre-server.net:32400/web/index.html#!/settings/users/friends");
            File.AppendAllText(@"c:\temp\AddedPlexUsers.txt", DateTime.Now + ": Added user = " + b + "\n");
            try
            {
                new SmtpClient
                {
                    Host = "Smtp.Gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Timeout = 10000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("##################", "##############")
                }.Send(new MailMessage { From = new MailAddress("##################", "Juky"), To = { "Sindrema@gmail.com" }, Subject = "Plex Access ", Body = "Hey, \n \n" + b + " now got access to your plex server.\n" });
            }
            catch (Exception ex)
            {
                File.AppendAllText(@"c:\temp\mailerror.txt", (DateTime.Now + ": ERROR = " + ex.Message + "\n"));
            }
            Thread.Sleep(10000);
            Process[] processlist = Process.GetProcesses();
            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    Thread.Sleep(1000);
                    if (process.MainWindowTitle.Contains("pera") == true)
                    {
                        int id = process.Id;

                        Process p = Process.GetProcessById(id);
                        if (p != null)
                        {
                            focus();
                            Thread.Sleep(500);
                            SendKeys.Send("{TAB}");
                            Thread.Sleep(500);
                            SendKeys.Send("{TAB}");
                            Thread.Sleep(500);
                            SendKeys.Send("{TAB}");
                            Thread.Sleep(500);
                            SendKeys.Send("{TAB}");
                            Thread.Sleep(500);
                            SendKeys.Send("{TAB}");
                            Thread.Sleep(500);
                            SendKeys.Send("{TAB}");
                            Thread.Sleep(500);
                            SendKeys.Send("{TAB}");
                            Thread.Sleep(500);
                            SendKeys.Send("{TAB}");
                            Thread.Sleep(500);
                            SendKeys.Send("{TAB}");
                            Thread.Sleep(500);
                            SendKeys.Send("{TAB}");
                            Thread.Sleep(500);
                            //@add user
                            SendKeys.Send("{ENTER}");
                            Thread.Sleep(2500);
                            //@Input user
                            SendKeys.Send(b);
                            Thread.Sleep(1500);
                            SendKeys.Send("{ENTER}");
                            Thread.Sleep(500);
                            SendKeys.Send("{TAB}");
                            Thread.Sleep(500);
                            SendKeys.Send("{TAB}");
                            Thread.Sleep(500);
                            SendKeys.Send("{TAB}");
                            Thread.Sleep(500);
                            SendKeys.Send("{TAB}");
                            Thread.Sleep(1500);
                            SendKeys.Send("{ENTER}");
                            Thread.Sleep(1500);
                            Process.Start("powershell.exe", @"/C Stop-Process -name Opera");
                            File.Delete(@"c:\temp\access.txt");
                            g = 1;




                        }
                    }
                }
            }
        }

        public void focus()
        {

            Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    if (process.MainWindowTitle.Contains("pera") == true)
                    {
                        int id = process.Id;

                        Process p = Process.GetProcessById(id);
                        if (p != null)
                        {
                            AutomationElement element = AutomationElement.FromHandle(process.MainWindowHandle);
                            if (element != null)
                            {
                                element.SetFocus();
                            }

                        }

                    }

                }
            }
        

    }
        public int g;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (File.Exists(@"c:\temp\access.txt") && g == 1)
            {
                NewPlex();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.Visible = false;
            this.ShowInTaskbar = false;
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width,
                                      workingArea.Bottom - Size.Height);
        }


        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
