using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Project21
{
    public partial class clientGui : Form
    {
        public enum ConnectionState { Disconnected, Connected }
        private ConnectionState currentState = ConnectionState.Connected;

        Timer timer = new Timer();
        Timer sessieUpdater = new Timer();
        public Bike bike { get; set; }
        string comPort = string.Empty;

        public List<string> Messages { get; set; } = new List<string>();
        public List<string[]> Results { get; set; } = new List<string[]>();
        public bool doctor = false;
        private string[] data;
        private string clientname;
        private string selected;
        bool secondClient = false;
        private bool created = false;

        private string sessieState = "warming-up";
        private int counter = 0;
        private int power = 25;
        private int beginDistance = 0;
        private int beginTijd = 0;
        private double VO2max = 0;
        private int leeftijd = 40;
        private List<int> HR;

        private Timer download;
        private Timer getdata;

        private Client client;

        public static void Main(string[] args)
        {
            clientGui form = new clientGui();
            form.ShowDialog();
        }

        public clientGui()
        {
            InitializeComponent();

            download = new Timer();
            download.Tick += IncomingData;
            download.Interval = 1;

            getdata = new Timer();
            getdata.Tick += ClientData;
            getdata.Interval = 500;

            sessieUpdater = new Timer();
            sessieUpdater.Tick += SessieUpdater;
            sessieUpdater.Interval = 1000;

            HR = new List<int>();

            //Create client, start function gets called when the user logs in
            client = new Client(Environment.UserName);
            comboBox3.SelectedIndexChanged += combo3SelectedIndexChanged;
        }

        public Bike getBike()
        {
            return bike;
        }

        private void ClientData(Object sender, EventArgs e)
        {
            client.UploadQeue.Add("get_data_" + clientname);
        }

        private void IncomingData(Object sender, EventArgs e)
        {
            switch (currentState)
            {
                case ConnectionState.Connected:
                    handleIncomingData();
                    break;
                case ConnectionState.Disconnected:
                    //Stop timer that handles incoming data
                    download.Stop();
                    Utilities.showPopup("Connection lost", this, true);
                    break;
            }
        }

        private void SessieUpdater(Object sender, EventArgs e)
        {
            counter++;
            switch (sessieState)
            {
                case "warming-up":
                    bike.bikeCom.SendCommand("PW 25");
                    if (counter > 120)
                        sessieState = "set-begin";
                    break;

                case "set-begin":
                    beginDistance = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].distance;
                    beginTijd = counter;
                    sessieState = "steady-state";
                    break;

                case "voorbereiding":
                    if (counter % 60 == 0)
                        UpdateData();
                    if (bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].rpm < 50)
                    {
                        power += 5;
                        bike.bikeCom.SendCommand("PW " + power);
                    }
                    else if (bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].rpm > 60)
                    {
                        power -= 5;
                        bike.bikeCom.SendCommand("PW " + power);
                    }
                    else if (bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].pulse < (208 - int.Parse(comboBox1.SelectedText) * 0.7))
                    {
                        bike.bikeCom.SendCommand("PW 25");
                        sessieState = "stop-sessie";
                    }
                    if (counter > 360)
                        sessieState = "steady-state";
                    break;

                case "steady-state":
                    if (counter % 15 == 0)
                    {
                        UpdateData();
                        HR.Add(bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].pulse);
                    }   
                    if (bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].rpm < 50)
                    {
                        power += 5;
                        bike.bikeCom.SendCommand("PW " + power);
                    }
                    else if (bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].rpm > 60)
                    {
                        power -= 5;
                        bike.bikeCom.SendCommand("PW " + power);
                    }
                    else if (bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].pulse < (208 - int.Parse(comboBox1.SelectedText) * 0.7))
                    {
                        bike.bikeCom.SendCommand("PW 25");
                        sessieState = "stop-sessie";
                    }
                    if (counter > 480)
                    {
                        if (((HR[0] - HR[5] < -5) && (HR[0] - HR[5] > 5)) || ((HR[5] - HR[9] < -5) && (HR[5] - HR[9] > 5)))
                        {
                            int total = 0;
                            int aantal = 0;
                            foreach (int hr in HR)
                            {
                                total += hr;
                                total++;
                            }
                            Vo2Max(total / aantal);
                        }
                        sessieState = "cooldown";
                    }    
                    break;

                case "cooldown":
                    if (power > 25)
                        power -= 10;
                    bike.bikeCom.SendCommand("PW " + power);
                    if (counter > 600)
                        sessieState = "stop-sessie";
                    break;

                case "stop-sessie":
                    beginTijd = 0;
                    beginDistance = 0;
                    counter = 0;
                    timer.Stop();
                    client.UploadQeue.Add("stop");
                    break;
            }
        }

        private void handleIncomingData()
        {
            List<string> Downloaded = client.Downloaded;
            if (Downloaded.Count >= 1)
            {
                string command = Downloaded[0];
                char[] seperatingchar = new char[1];
                string cha = "_";
                seperatingchar[0] = System.Convert.ToChar(cha);
                string[] words = command.Split(seperatingchar);

                //handle your incoming shit
                if (words[0].Equals("account"))
                {
                    switch (words[1])
                    {
                        case "Login Succesfull":
                            loginPanel.Hide();
                            comPanel.Show();
                            comTextBox.Focus();
                            AcceptButton = comOKButton;
                            break;
                        case "Created":
                            loginPanel.Hide();
                            comPanel.Show();
                            comTextBox.Focus();
                            AcceptButton = comOKButton;
                            break;
                        case "Incorrect Credentials":
                            download.Stop();
                            client.Stop();
                            Utilities.showPopup("Incorrect Password", this, false);
                            infoLabel.Text = "Incorrect Password";
                            return;
                    }
                }
                else
                {
                    //Console.WriteLine("Received word: " + words[0]);
                    switch (words[0])
                    {
                        
                        case "command":
                            bike.bikeCom.SendCommand(words[1]);
                            break;
                        case "meting":
                            string[] separatingChars = { " " };
                            data = words[1].Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                            Console.WriteLine(words[1]);
                            if (!Results.Contains(data))
                            {
                                //TODO: Remove
                                Console.WriteLine("Results added");
                                Results.Add(data);
                                UpdateGraph();
                            }
                            break;
                        case "doctor":
                            doctor = true;
                            client.UploadQeue.Add("get_connections");                            
                            clientPanel.Show();
                            comboBox3.Show();
                            button3.Show();
                            comPanel.Hide();
                            loginPanel.Hide();
                            this.AutoSize = false;
                            this.Size = new Size(Size.Width, 576);
                            break;
                        case "client":
                            if (!comboBox3.Items.Contains(words[1]))
                                comboBox3.Items.Add(words[1]);
                            break;
                        case "No Connection":
                            currentState = ConnectionState.Disconnected;
                            return;
                    }
                }
                if(client.Downloaded.Count>0)
                client.Downloaded.Remove(Downloaded[0]);
            }
        }

        private void Gui_Load(object sender, EventArgs e)
        {
            comPanel.Parent = this;
            clientPanel.Parent = this;
            comPanel.Hide();
            comboBox3.Hide();
            button3.Hide();
        }

        private void comOKButton_Click(object sender, EventArgs e)
        {
            comPort = comTextBox.Text;
            comPanel.Hide();
            clientPanel.Show();
            if (!doctor)
            {
                bike = new Bike(comPort);
            }
            if (!doctor)
            {
                client.UploadQeue.Add("get_messages_" + clientname);
            }
            if (doctor)
            {
                client.UploadQeue.Add("get_connections");
            }
        }

        private void logInButton_Click_1(object sender, EventArgs e)
        {
            if (accNameBox.Text == string.Empty || passBox.Text == string.Empty)
            {
                infoLabel.Text = "No name or password...";
                Utilities.showPopup("No name or password...", this, false);
            }
            else
            {
                //Start client and check if it was succesfull in connecting to the server
                if (!client.Start())
                {
                    //download.Stop();
                    Utilities.showPopup("Can't connect to server", this, false);
                    return;
                }
                //Start tcp incoming data and outgoing data
                download.Start();

                string name = accNameBox.Text;
                string pass = passBox.Text;
                if (name.Contains("dokter_"))
                {
                    client.UploadQeue.Add("account_" + name + "_password_" + pass + "_true");
                }
                else
                {
                    client.UploadQeue.Add("account_" + name + "_password_" + pass + "_false");
                }
                clientname = name;
                passBox.Clear();
            }
        }

        private void UpdateData()
        {
            bike.bikeCom.RequestData();
            client.UploadQeue.Add("bike_" + clientname + "_" + bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].GetAll());
            chart1.Series[0].Points.Add(bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].pulse);
            if (chart1.Series[0].Points.Count == 30) chart1.Series[0].Points.RemoveAt(0);
        }

        private void Tick(object sender, EventArgs e)
        {

            if (!doctor)
            {
                if (bike.bikeCom.BikeList.Count > 0)
                {
                    //updating the labels
                    client.UploadQeue.Add("bike_" + clientname + "_" +
                    bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].GetAll());
                    chart1.Series[0].Points.Add(bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].pulse);
                    if (chart1.Series[0].Points.Count == 30) chart1.Series[0].Points.RemoveAt(0);
                }
            }

        }

        private void UpdateGraph()
        {
            lock (Results)
            {
                if (Results.Count > 0)
                {
                    //updating the labels
                    chart1.Series[0].Points.Add(Int32.Parse(Results[Results.Count - 1][3]));
                }
                
            }
        }
        
        private void combo3SelectedIndexChanged(object sender, System.EventArgs e)
        {
            clientname = comboBox3.SelectedItem.ToString();
            chart1.Series[0].Points.Clear();
            client.UploadQeue.Add("set_" + comboBox3.SelectedItem);
            client.UploadQeue.Add("get_alldata");
        }
        private void Gui_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            client.UploadQeue.Add("get_connections");
        }
        
        private void loginPanel_Enter(object sender, EventArgs e)
        {
            AcceptButton = logInButton;
            accNameBox.Focus();
        }

        private double Vo2Max(int HRsss)
        {
            double VO2max;
            double workload = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].actPower;
            int HRss = HRsss; //dit moet gemiddelde HR zijn van de afgelopen 2 minuten na een HRss
            workload = workload * 6.12;
            bool male = checkBox1.Checked;
            if (male)
            {
                VO2max = (0.00212*workload + 0.299)/(0.769*HRss - 48.5)*1000;
            }
            else
            {
                VO2max = (0.00193*workload + 0.326)/(0.769*HRss - 56.1)*1000;
            }
            int leeftijd = Int32.Parse(comboBox1.SelectedText);
            switch (leeftijd)
            {
                case 30:
                    break;
                case 35:
                    VO2max = VO2max * 0.87;
                    break;
                case 40:
                    VO2max = VO2max * 0.83;
                    break;
                case 45:
                    VO2max = VO2max * 0.78;
                    break;
                case 50:
                    VO2max = VO2max * 0.75;
                    break;
                case 55:
                    VO2max = VO2max * 0.71;
                    break;
                case 60:
                    VO2max = VO2max * 0.68;
                    break;
                case 65:
                    VO2max = VO2max * 0.65;
                    break;
            }
            label6.Text = VO2max.ToString();
            return VO2max;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.UploadQeue.Add("session_"+client.userName+" "+DateTime.Now);
            timer.Start();
            chart1.Series.Clear();
            sessieUpdater.Start();
        }
    }
    
    
}
