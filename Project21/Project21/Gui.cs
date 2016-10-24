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
        public Bike bike { get; set; }
        string comPort = string.Empty;

        public List<string> Messages { get; set; } = new List<string>();
        public List<string[]> Results { get; set; } = new List<string[]>();
        public List<string[]> graphdata { get; set; } = new List<string[]>();
        public bool doctor = false;
        private string[] data;
        private string clientname;
        private string selected;
        bool secondClient = false;
        private bool created = false;
        private bool graph1 = false;
        clientGui second;
        private Graph graph;

        private Timer download;

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
            
            //Create client, start function gets called when the user logs in
            client = new Client(Environment.UserName);
            comboBox3.SelectedIndexChanged += combo3SelectedIndexChanged;
            graph = new Graph();
            graph.Hide();
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
                        case "graphdata":
                            if (!words[1].Equals("done"))
                            {
                                string[] separating = { " " };
                                string[] dat = words[1].Split(separating, System.StringSplitOptions.RemoveEmptyEntries);
                                graphdata.Add(dat);
                            }
                            else if (words[1].Equals("done")&&graph1 == false)
                            {
                                graph.graphdata = graphdata;
                                graph.updatechart();
                                graph.Show();
                            }
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
                            if (!created)
                            {
                                second = new clientGui();
                            }
                            timer.Tick += Tick;
                            timer.Interval = 500;
                            timer.Start();
                            Timer getdata = new Timer();
                            getdata.Tick += ClientData;
                            getdata.Interval = 500;
                            getdata.Start();
                            client.UploadQeue.Add("get_connections");                            
                            clientPanel.Show();
                            comboBox3.Show();
                            button3.Show();
                            button6.Show();
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
            button6.Hide();
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
            else
            {
                Timer data = new Timer();
                data.Tick += ClientData;
                data.Interval = 500;
                data.Start();
            }
            timer.Tick += Tick;
            timer.Interval = 500;
            timer.Start();
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

        private void Tick(object sender, EventArgs e)
        {

            if (!doctor)
            {
                if (bike.bikeCom.BikeList.Count > 0)
                {
                    //updating the labels
                    client.UploadQeue.Add("bike_" + clientname + "_" +
                    bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].GetAll());
                    pulsev.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].pulse.ToString();
                    rpmv.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].rpm.ToString();
                    kmhv.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].kmh.ToString();
                    distancev.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].distance.ToString();
                    reqpowerv.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].reqPower.ToString();
                    energyv.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].energy.ToString();
                    minutesv.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].minutes.ToString();
                    secondsv.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].seconds.ToString();
                    actpowerv.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].actPower.ToString();
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
                    pulsev.Text = Results[Results.Count - 1][3].ToString();
                    rpmv.Text = Results[Results.Count - 1][4].ToString();
                    kmhv.Text = Results[Results.Count - 1][5].ToString();
                    distancev.Text = Results[Results.Count - 1][6].ToString();
                    reqpowerv.Text = Results[Results.Count - 1][7].ToString();
                    energyv.Text = Results[Results.Count - 1][8].ToString();
                    minutesv.Text = Results[Results.Count - 1][9].ToString();
                    secondsv.Text = Results[Results.Count - 1][10].ToString();
                    actpowerv.Text = Results[Results.Count - 1][11].ToString();
                    chart1.Series[0].Points.Add(Int32.Parse(Results[Results.Count - 1][3]));
                }

                while (chart1.Series[0].Points.Count >= 30) chart1.Series[0].Points.RemoveAt(0);
            }
        }
        
        private void combo3SelectedIndexChanged(object sender, System.EventArgs e)
        {
            clientname = comboBox3.SelectedItem.ToString();
            chart1.Series[0].Points.Clear();
            client.UploadQeue.Add("get_alldata_" + comboBox3.SelectedItem);
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
        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                client.UploadQeue.Add("get_graphdata_" + comboBox3.SelectedItem.ToString());
            }
        }
    }
}
